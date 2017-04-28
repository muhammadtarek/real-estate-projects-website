using Newtonsoft.Json;
using Project_Apollo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Apollo.Controllers
{
    public class HomeController : Controller
    {
        DBase db = new DBase();
        // GET: Home
        public ActionResult Index(int id)
        {
            User user = db.userTable.Find(id);
            ViewBag.showNav = true;
            ViewBag.tabs = new string[4] { "Home", "Profile", "FAQ", "Contact us" };
            if (user.Photo == null)
            {
                ViewBag.userPhoto = "/Public/assets/images/picture.jpg";
            }
            else
            {
                var img = "";
                if (user.Photo != null)
                {
                    var base64 = Convert.ToBase64String(user.Photo);
                    img = String.Format("data:image/gif;base64,{0}", base64);
                }
                ViewBag.userPhoto = img;
            }
            ViewBag.userName = user.name;
            return View();
        }
        public object deleteProject(int id)
        {
            Project p = db.ProjectTable.Find(id);

            if (p != null)
            {
                db.ProjectTable.Remove(p);
                db.SaveChanges();

                return JsonConvert.SerializeObject(new { opertaion = true });
                //new { opertaion = false }
            }
            return JsonConvert.SerializeObject(new { opertaion = false });
        }
        [HttpPost]
        public object updateProject(string projectName, string projectDescription, int projectId)
        {
            Project p = db.ProjectTable.Find(projectId);
            p.Name = projectName;
            p.Description = projectDescription;
            db.SaveChanges();
            return JsonConvert.SerializeObject(new
            {
                postingTime = p.createDate,
                projectName = p.Name,
                projectDescription = p.Description,
                projectId = p.ID
            });
        }

        public object applyToProject(int userId, int projectId, String applyingLetter, double price, DateTime startDate, DateTime endDate)
        {
            var data = (from proj in db.ProjectTable   // query to get the project status before apply
                       where proj.ID == projectId
                       select new { proj.status }).ToArray();
            if ((int)data[0].status == 0) // if project isn't assigned to anyone yet (Waiting)
            {
                ApplyProject apply = new ApplyProject();
                apply.applyingLetter = applyingLetter;
                apply.endDate = endDate;
                apply.price = price;
                apply.project.ID = projectId;
                apply.projectManager.ID = userId;
                apply.startDate = startDate;
                db.ApplyProjectTable.Add(apply);
                db.SaveChanges();
                return JsonConvert.SerializeObject(new
                {
                    operation = true
                });
            }else
            {
                return JsonConvert.SerializeObject(new
                {
                    operation = false
                });
            }    
        }

		[HttpPost]
		public string createProject(string projectName, string projectDescription, int userId)
		{
			Project project = db.ProjectTable.Add(new Project()
			{
				Name = projectName,
				Description = projectDescription,
				customer = db.userTable.Find(userId),
			});
			db.SaveChanges();
			return JsonConvert.SerializeObject(new
			{
				postingTime = project.createDate,
				projectName = project.Name,
				projectDescription = project.Description,
				projectId = project.ID
			}, Formatting.Indented);
		}

	    public object getProject(int projectId)
        {
            Project p = db.ProjectTable.Find(projectId);
            return JsonConvert.SerializeObject(new
            {
                projectName = p.Name,
                projectDescription = p.Description,
                projectId = p.ID
            });
        }

        public object writeComment(int userId, int projectId, String comment)
        {
            var data = (from apply in db.ApplyProjectTable   // query to get the project status before apply
                        where apply.project.ID == projectId
                        select apply).ToArray();
            if (data.Length > 0)
            {
                Comments comm = new Comments();
                comm.comment = comment;
                comm.project.ID = projectId;
                comm.projectManager.ID = userId;
                db.CommentsTable.Add(comm);
                db.SaveChanges();
                return JsonConvert.SerializeObject(new
                {
                    operation = true
                });
            }
            else
            {
                return JsonConvert.SerializeObject(new
                {
                    operation = false
                });
            }
        }
        public object loadUnassignedProjects()
        {
            var arr = db.ProjectTable.Where(x => ((int)x.status) == 1).ToList();
            return View(arr);
        }

    }
}
