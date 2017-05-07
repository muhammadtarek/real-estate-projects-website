using Project_Apollo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Apollo.Controllers {
	public class profileController : Controller {
		DBase db = new DBase();

		// GET: profile
		public ActionResult Index() {
			Session["id"] = 1;
			User user = db.userTable.Find((int)Session["id"]);

			//TESTING ONLY
			Session["userRole"] = (int)user.UserRole;
			Session["userPhoneNumber"] = user.Mobile;
			Session["userEmail"] = user.Email;
			Session["userDescription"] = user.Description;

			if ((int)Session["userRole"] < 2) {
				//If the user is admin, customer or project manager
				ViewBag.showNav = false;
			} else {
				//If the user is team leader or jenior engineer
				ViewBag.showNav = true;
			}
			

			if (user.Photo == null) {
				Session["userPhoto"] = "/Public/assets/images/default-user.jpg";
			} else {
				var img = ImageConverter.convertPhotoToString(user.Photo);
				Session["userPhoto"] = img;
			}

			Session["userName"] = user.name;
			return View();
		}


		public void declineProject(int projectID) {
			var Result = new HomeController().deleteProject(projectID);
		}
		public List<User> getTeamLeaders() {
			return db.userTable.Where(u => (int)u.UserRole == 3).ToList();
		}
		public List<User> getJuniorEngineers() {
			return db.userTable.Where(u => (int)u.UserRole == 4).ToList();
		}
		public void approveProject(int projectId) {
			Project p = db.ProjectTable.Find(projectId);
			p.status = (status)0;
			db.SaveChanges();
		}
		public void removeMember(int userId = 1, int projectId = 6) {
			Project p = db.ProjectTable.Find(projectId);
			User u = db.userTable.Find(userId);
			p.workers.Remove(u);
			db.SaveChanges();
		}

		public object loadPendingProjects() {
			int status = 3;
			List<Project> projects = db.ProjectTable.Where(x => ((int)x.status) == status).ToList();
			return View(projects);
		}

	}

}
