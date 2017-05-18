using Project_Apollo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Project_Apollo.Controllers {
	public class profileController : Controller {
		DBase db = new DBase();

		// GET: profile
		public ActionResult Index() {
			if (Session["id"] == null)
				return RedirectToAction("Index", "Welcome");
			//Choosing layout depends on user role
			if ((int)Session["userRole"] == 0 || (int)Session["userRole"] == 2 || (int)Session["userRole"] == 3) {
				//If the user is admin, customer or project manager
				ViewBag.showNav = false;
			} else {
				//If the user is team leader or jenior engineer
				ViewBag.showNav = true;
			}

			//Loading tabs depends on user role
			switch ((int)Session["userRole"]) {
				case 0:
					ViewBag.tabs = new string[] { "Projects", "User Managment", "Pending Posts" };
					ViewBag.tabAttr = new string[] { "projects", "user-management", "pending-posts" };
					ViewBag.users = this.getUsers();
					ViewBag.pending = this.loadPendingProjects();
					break;
				case 1:
					ViewBag.tabs = new string[] { "Projects", "Project Managers Requests" };
					ViewBag.tabAttr = new string[] { "projects", "project-manager-requests" };
					ViewBag.pmRequest = this.loadApplyer();
					break;
				case 2:
					ViewBag.tabs = new string[] { "Projects", "Send Invitations" };
					ViewBag.tabAttr = new string[] { "projects", "send-invitations" };
					ViewBag.teamLeaders = this.getTeamLeaders();
					ViewBag.juniorEng = this.getJuniorEngineers();
					break;
				case 3:
				case 4:
					ViewBag.tabs = new string[] { "Projects", "Project Invitations" };
					ViewBag.tabAttr = new string[] { "projects", "project-invitations" };
					ViewBag.requests = this.loadRequest();
					break;
			}

			ViewBag.projects = this.loadAssignedProjects((int)Session["id"]);
			ViewBag.deliverdProject = this.loadDeliverdProjects((int)Session["id"]);
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

		public void removeMember(int userId = 1, int projectId = 6) {//JE
			Project p = db.ProjectTable.Find(projectId);
			User u = db.userTable.Find(userId);
			p.workers.Remove(u);
			db.SaveChanges();
		}

		public List<Project> loadPendingProjects() {
			int status = 3;
			List<Project> projects = db.ProjectTable.Where(x => ((int)x.status) == status).ToList();
			return projects;
		}

		public void deleteUser(int id) {
			User u = db.userTable.Find(id);

			if (u.UserRole == (userRole)1) // customer
			{
				foreach (Project p in u.ownProject.ToList()) {
					db.ProjectTable.Remove(p);
				}
				List<Reports> data = (from x in db.ReportsTable
									  where x.customer.ID == id
									  select x).ToList();
				foreach (Reports r in data) {
					db.ReportsTable.Remove(r);
				}
			} else if (u.UserRole == (userRole)2)//projectManager
			  {
				List<Project> projects = (from x in db.ProjectTable
										  where x.projectManager.ID == id
										  select x).ToList();
				foreach (Project p in projects) {
					Project proj = db.ProjectTable.Find(p.ID);
					proj.status = (status)0;
					proj.startDate = null;
					proj.endDate = null;
					db.Entry(proj).Reference("projectManager").CurrentValue = null;
					db.Entry(proj).Reference("teamLeader").CurrentValue = null;
					proj.price = null;
					proj.workers.Clear();
				}
				List<Feedback> feedbacks = (from x in db.FeedbackTable
											where x.projectManager.ID == id
											select x).ToList();
				foreach (Feedback f in feedbacks) {
					db.FeedbackTable.Remove(f);
				}
				List<Qualifications> qualifications = (from x in db.qualificationsTable
													   where x.user.ID == id
													   select x).ToList();
				foreach (Qualifications q in qualifications) {
					db.qualificationsTable.Remove(q);
				}
				List<Reports> reports = (from x in db.ReportsTable
										 where x.projectManager.ID == id
										 select x).ToList();
				foreach (Reports r in reports) {
					db.ReportsTable.Remove(r);
				}
			} else if (u.UserRole == (userRole)3)//teamLeader
			  {
				foreach (Project p in u.leadProject.ToList()) {
					Project proj = db.ProjectTable.Find(p.ID);
					db.Entry(proj).Reference("teamLeader").CurrentValue = null;
				}
				List<Feedback> feedbacks = (from x in db.FeedbackTable
											where x.teamLeader.ID == id
											select x).ToList();
				foreach (Feedback f in feedbacks) {
					db.FeedbackTable.Remove(f);
				}
				List<Qualifications> qulaifications = (from x in db.qualificationsTable
													   where x.user.ID == id
													   select x).ToList();
				foreach (Qualifications q in qulaifications) {
					db.qualificationsTable.Remove(q);
				}
			} else if (u.UserRole == (userRole)4)//juniorEngineer
			  {
				List<Feedback> feedbacks = (from x in db.FeedbackTable
							where x.juniorEngineering.ID == id
							select x).ToList();
				foreach (Feedback f in feedbacks) {
					db.FeedbackTable.Remove(f);
				}
				var qulaifications = (from x in db.qualificationsTable
									  where x.user.ID == id
									  select x).ToList();
				foreach (Qualifications q in qulaifications) {
					db.qualificationsTable.Remove(q);
				}
			}
			db.userTable.Remove(u);
			db.SaveChanges();

		}

		public void requestTeamLeader(int projectid = 2, int teamLeaderId = 1) {
			db.RequestsTable.Add(new Requests {
				project = db.ProjectTable.Find(projectid),
				sender = db.userTable.Find((int)Session["id"]),
				reciever = db.userTable.Find(teamLeaderId),
				requestType = (request)0
			});
			db.SaveChanges();
		}

		public void requestJuniorEngineer(int projectid = 2, int JuniorId = 1) {
			db.RequestsTable.Add(new Requests {
				project = db.ProjectTable.Find(projectid),
				sender = db.userTable.Find((int)Session["id"]),
				reciever = db.userTable.Find(JuniorId),
				requestType = (request)1
			});
			db.SaveChanges();
		}

		public List<Project> loadAssignedProjects(int userId) {
			List<Project> arr = db.ProjectTable.Where(x => ((int)x.status) == 1 && (x.projectManager.ID == userId || x.teamLeader.ID == userId || x.workers.Any(ss => ss.ID == userId) || x.customer.ID == userId)).ToList();
			return arr;
		}

		public List<Project> loadDeliverdProjects(int userId) {
			List<Project> arr = db.ProjectTable.Where(x => x.status == status.deliverd && (x.projectManager.ID == userId || x.teamLeader.ID == userId || x.workers.Any(ss => ss.ID == userId) || x.customer.ID == userId)).ToList();
			return arr;
		}
		public void removeEngineerFromProject(int engineerId = 2, int projectId = 2) {
			Project proj = db.ProjectTable.Find(projectId);
			var engineer = proj.workers.First(x => x.ID == engineerId);
			proj.workers.Remove(engineer);
			db.SaveChanges();
		}

		public void Te_LeaveProject(int projectId)//TL
		{
			Project proj = db.ProjectTable.Find(projectId);
			db.Entry(proj).Reference("teamLeader").CurrentValue = null;
			db.SaveChanges();
		}

		public void Je_LeaveProject(int JE_ID, int projectId)//JE
		{
			removeEngineerFromProject(JE_ID, projectId);
		}

		public void Te_evaluateJouniorEnginner(int engineerID, int projectID, String feedBack) {
			Project proj = db.ProjectTable.Find(projectID);
			Feedback feedbackMessage = new Feedback();
			feedbackMessage.feedBack = feedBack;
			feedbackMessage.juniorEngineering = proj.workers.First(x => x.ID == engineerID);
			feedbackMessage.projectManager = proj.projectManager;
			feedbackMessage.teamLeader = proj.teamLeader;
			db.FeedbackTable.Add(feedbackMessage);
			db.SaveChanges();
		}


		public void Customer_assignProjectToPM(int PM_ID, int projectID) {
			User pm = db.userTable.Find(PM_ID);
			Project proj = db.ProjectTable.Find(projectID);
			ApplyProject applier = proj.applied.First(x => x.projectManager == pm);
			proj.projectManager = pm;
			proj.price = applier.price;
			proj.startDate = applier.startDate;
			proj.endDate = applier.endDate;
			proj.status = (status)1;

			var appliers = proj.applied.Where(x => x.project.ID == proj.ID);
			db.ApplyProjectTable.RemoveRange(appliers);

			//proj.applied.Clear();

			db.SaveChanges();
		}

		public List<User> getUsers() {

			return db.userTable.Where(p=>p.UserRole != userRole.admin).ToList();
		}

		public void acceptRequest(int requestID) {
			int userID = (int)Session["id"];
			Requests req = db.RequestsTable.Find(requestID);
			Project proj = db.ProjectTable.Where(p => p.ID == req.project.ID).FirstOrDefault();

			if ((int)Session["userRole"] == 3) //TeamLeader
			{
				proj.teamLeader = db.userTable.Find(userID);
				List<Requests> data = db.RequestsTable.Where(x => x.project.ID == proj.ID).ToList();
				foreach (Requests request in data) {
					db.RequestsTable.Remove(request);
				}
			} else if ((int)Session["userRole"] == 4) //Engineer
			  {
				proj.workers.Add(db.userTable.Find(userID));
				db.RequestsTable.Remove(req);
			}
			db.SaveChanges();
		}

		public void deleteRequest(int requestID) {
			Requests req = db.RequestsTable.Find(requestID);
			db.RequestsTable.Remove(req);
			db.SaveChanges();
		}

		public List<Requests> loadRequest() {
			int id = (int)Session["id"];

			return db.RequestsTable.Where(r => r.reciever.ID == id).ToList();
		}

		public List<ApplyProject> loadApplyer() {
			int id = (int)Session["id"];
			List<Project> projects = db.ProjectTable.Where(p => p.customer.ID == id).ToList();
			List<ApplyProject> requests = new List<ApplyProject>();
			foreach (Project p in projects) {
				requests.AddRange(db.ApplyProjectTable.Where(a => a.project.ID == p.ID).ToList());
			}
			return requests;
		}

		public void declineApplyer(int PM_ID, int projectID) {
			User pm = db.userTable.Find(PM_ID);
			Project proj = db.ProjectTable.Find(projectID);
			ApplyProject applier = proj.applied.First(x => x.projectManager == pm);
			db.ApplyProjectTable.Remove(applier);
			db.SaveChanges();
		}

		public void leaveProjectPm(int projectId) {
			Project proj = db.ProjectTable.Find(projectId);
			proj.status = (status)0;
			proj.startDate = null;
			proj.endDate = null;
			db.Entry(proj).Reference("projectManager").CurrentValue = null;
			db.Entry(proj).Reference("teamLeader").CurrentValue = null;
			proj.price = null;
			proj.workers.Clear();
			if (TryUpdateModel(proj)) {
				db.SaveChanges();
			}
		}

		public void leaveProject(int projectId) {
			int id = (int)Session["id"];
			userRole role = (userRole)Session["userRole"];
			if (role == userRole.projectManager)
				this.leaveProjectPm(projectId);
			else if (role == userRole.teamLeader)
				this.Te_LeaveProject(projectId);
			else if (role == userRole.juniorEngineer)
				this.Je_LeaveProject(id, projectId);
		}
		public Boolean checkInvitation(int projectID, int recieverID) {
			var data = (from x in db.RequestsTable
						where x.reciever.ID == recieverID && x.project.ID == projectID
						select x).ToList();
			if (data.Count > 0) {
				return false;
			} else {
				return true;
			}
		}
		public Boolean request(int projectId, int userId) {
			if (!this.checkInvitation(projectId, userId))
				return false;
			User user = db.userTable.Find(userId);
			if (user.UserRole == userRole.teamLeader)
				this.requestTeamLeader(projectId, userId);
			else if (user.UserRole == userRole.juniorEngineer)
				this.requestJuniorEngineer(projectId, userId);
			return true;
		}

		public ActionResult getStatisticalDiagrams() {
			int userId = (int)Session["id"];
			List<String> listx = db.qualificationsTable.Where(u => (u.user.ID) == userId).Select(x => x.qialificationName).ToList();
			List<String> listy = db.qualificationsTable.Where(u => (u.user.ID) == userId).Select(x => (x.percentage).ToString()).ToList();

			int No_projects = 0;

			string[] userrole = db.userTable.Where(u => (u.ID) == userId).Select(x => x.UserRole.ToString()).ToArray();

			if (userrole[0].Equals("projectManager")) {
				No_projects = (db.ProjectTable.Where(p => (p.projectManager.ID) == userId && (int)p.status == 2)).Count();
			} else if (userrole[0].Equals("teamLeader")) {
				No_projects = (db.ProjectTable.Where(p => (p.teamLeader.ID) == userId && (int)p.status == 2)).Count();
			} else //jonuirEngineering
			  {
				User junior = db.userTable.Find(userId);
				foreach (var x in junior.Projects) {
					if ((int)x.status == 2)
						No_projects++;
				}
			}


			listx.Add("No.Projects");
			listy.Add(No_projects + "");



			Chart mychart = new Chart(width: 600, height: 400)
				.AddTitle("Qualification").AddSeries(
				name: userrole[0],
				xValue: listx,
				yValues: listy).Write();

			return null;
		}
	}

}
