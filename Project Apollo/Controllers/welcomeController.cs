using Newtonsoft.Json;
using Project_Apollo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Project_Apollo.Controllers
{

    public class welcomeController : Controller
    {
        public DBase db = new DBase();
        // GET: welcome
        public ActionResult Index()
        {
            ViewBag.currentPage = 0;
            return View();
        }

        public Object login(string email, string password)
        {
            var data = (from d in db.userTable
                        where d.Email == email
                        select new { d.ID, d.name, d.UserRole, d.Photo, d.Password }).ToArray();

            if (data.Length != 0 && password.Equals(data[0].Password)) // if email & password TRUE
            {
                ViewBag.id = data[0].ID;
                ViewBag.name = data[0].name;
                ViewBag.userRole = data[0].UserRole;
                ViewBag.userPhoto = data[0].Photo;
                return JsonConvert.SerializeObject(new
                {
                    Result = new
                    {
                        Email = true,
                        password = true
                    },
                    user = new
                    {
                        id = data[0].ID,
                        name = data[0].name,
                        userRole = data[0].UserRole,
                        userPhoto = data[0].Photo
                    }
                });
            }
            else if (data.Length != 0 && !password.Equals(data[0].Password)) // Email is true password is wrong
            {
                return JsonConvert.SerializeObject(new
                {
                    Result = new
                    {
                        Email = true,
                        password = "Password doesn't match with the email"
                    },
                    user = new
                    {
                        id = "",
                        name = "",
                        userRole = "",
                        userPhoto = ""
                    }
                });
            }
            else if (data.Length == 0)  // email is wrong
            {
                return JsonConvert.SerializeObject(new
                {
                    Result = new
                    {
                        Email = "Email is not registered",
                        password = ""
                    },
                    user = new
                    {
                        id = "",
                        name = "",
                        userRole = "",
                        userPhoto = ""
                    }
                });
            }
            return null;
        }

        public object signUp(HttpPostedFileBase userPicture, string name, string email, string password, string phoneNumber, string Desciption, int userType = 1)
        {

            var v = (from a in db.userTable
                     where a.Email == email
                     select a.Email);

            if (v.Count() > 0)
            {
                return JsonConvert.SerializeObject(new
                {
                    result = new
                    {
                        email = "Email is alrady registered"
                    },
                    user = new
                    {
                        id = "",
                        name = "",
                        userRole = "",
                        userPhoto = ""
                    }
                });
            }
            else
            {
                User user = new User();
                user.name = name;
                user.Email = email;
                user.Password = password;
                user.Mobile = phoneNumber;
                user.UserRole = (userRole)userType;
                user.Description = Desciption;

                if (userPicture != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        userPicture.InputStream.CopyTo(ms);
                        byte[] arr = ms.GetBuffer();
                        user.Photo = arr;
                    }
                }
                db.userTable.Add(user);
                db.SaveChanges();

                var img = "";
                if (user.Photo != null)
                {
                    var base64 = Convert.ToBase64String(user.Photo);
                    img = String.Format("data:image/gif;base64,{0}", base64);
                }

                ViewBag.userPhoto = img;
                ViewBag.userName = user.name;
                ViewBag.userID = user.ID;
                ViewBag.userRole = user.UserRole;


                return JsonConvert.SerializeObject(new
                {
                    result = new
                    {
                        email = true
                    },
                    user = new
                    {
                        id = user.ID,
                        name = user.name,
                        userRole = userType,
                        userPhoto = img
                    }
                });
            }
        }

		public String getUser(int userId)
		{
			User u = db.userTable.Find(userId);
			return JsonConvert.SerializeObject(new
			{
				userPhoto = u.Photo,
				userName = u.name,
				userBio = u.Description,
				userRole = Enum.GetName(typeof(userRole), u.UserRole)
			}, Formatting.Indented);
		}
	}
}
