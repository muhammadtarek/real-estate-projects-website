using Newtonsoft.Json;
using Project_Apollo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Apollo.Controllers {
	public class HomeController : Controller {
		DBase db = new DBase();
		// GET: Home
		public ActionResult Index() {
			User user = db.userTable.Find((int)Session["id"]);
			ViewBag.showNav = true;
			ViewBag.tabs = new string[4] { "Home", "Profile", "FAQ", "Contact us" };

            //TESTING ONLY
            Session["userRole"] = (int)user.UserRole;

			if (user.Photo == null) {
                Session["userPhoto"] = "/Public/assets/images/default-user.jpg";
			} else {
                var img = ImageConverter.convertPhotoToString(user.Photo);
                Session["userPhoto"] = img;
			}

            Session["userName"] = user.name;
			return View(this.loadUnassignedProjects());
        }

		public object deleteProject(int id) {
			Project p = db.ProjectTable.Find(id);

			if (p != null) {
				db.ProjectTable.Remove(p);
				db.SaveChanges();

				return JsonConvert.SerializeObject(new { opertaion = true });
				//new { opertaion = false }
			}
			return JsonConvert.SerializeObject(new { opertaion = false });
		}
		[HttpPost]
		public void updateProject(string projectName, string projectDescription, int projectId) {
			Project p = db.ProjectTable.Find(projectId);
			p.Name = projectName;
			p.Description = projectDescription;
			db.SaveChanges();
		}

		public object applyToProject(int userId, int projectId, String applyingLetter, double price, DateTime startDate, DateTime endDate) {
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
				return JsonConvert.SerializeObject(new {
					operation = true
				});
			} else {
				return JsonConvert.SerializeObject(new {
					operation = false
				});
			}
		}

		[HttpPost]
		public void createProject(string name, string description) {
            Project project = db.ProjectTable.Add(new Project() {
                Name = name,
                Description = description,
                customer = db.userTable.Find((int)Session["id"]),
                status = (int)Session["userRole"] == 0 ? status.waiting :status.pending
            });
			db.SaveChanges();
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
            var arr = db.ProjectTable.Where(x => ((int)x.status) == 0).ToList();
            return arr;
        }
        public object loadAssignedProjects(int userId)
        {
            var arr = db.ProjectTable.Where(x => ((int)x.status) == 1 && x.projectManager.ID == userId).ToList();
            return arr;
        }
        public object loadFinishedProjects()
        {
            var arr = db.ProjectTable.Where(x => ((int)x.status) == 2).ToList();
            return arr;
        }

        public object getUsers()
        {
            var data = (from usr in db.userTable
                        select usr).ToList();
           return data;
        }

        public void setStatus(int projectId, int status)
        {
            var proj = db.ProjectTable.Find(projectId);
            proj.status = (status)status;
            if (TryUpdateModel(proj))
            {
                db.SaveChanges();
            }
        }

        public void leaveProject(int userId = 1, int projectId = 12)
        {            
            Project proj = db.ProjectTable.Find(projectId);
            proj.status = (status)0;
            proj.startDate = null;
            proj.endDate = null;
            db.Entry(proj).Reference("projectManager").CurrentValue = null;
            db.Entry(proj).Reference("teamLeader").CurrentValue = null;
            proj.price = null;
            proj.workers.Clear();
            if (TryUpdateModel(proj))
            {
                db.SaveChanges();
            }
        }
    }
}
