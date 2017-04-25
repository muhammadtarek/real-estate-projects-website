using Newtonsoft.Json;
using Project_Apollo.Models;
using System;
using System.Collections.Generic;
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
            return View();
        }

        public Object login(string email, string password)
        {         
            var data = (from d in db.userTable
                         where d.Email == email
                         select new { d.ID, d.name, d.UserRole, d.Photo , d.Password}).ToArray();

            if (data.Length != 0 && password.Equals(data[0].Password)) // if email & password TRUE
            {
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
            }else if (data.Length != 0 && !password.Equals(data[0].Password)) // Email is true password is wrong
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
            }else if(data.Length == 0)  // email is wrong
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
    }
}
