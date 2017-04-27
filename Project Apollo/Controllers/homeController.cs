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
        public ActionResult Index()
        {
            ViewBag.showNav = true;
            ViewBag.tabs = new string[4] { "Home", "Profile", "FAQ", "Contact us" };
            ViewBag.userPhoto = "/Public/assets/images/picture.jpg";
            ViewBag.userName = "Muhammad Tarek";
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
        public object loadProjects(int projectStatus)
        {
            var projectData = (from proj in db.ProjectTable   // query to get all of the projects with the wanted status
                               join usr in db.userTable on proj.customer.ID equals usr.ID
                               where (int)proj.status == projectStatus
                               select new { proj.ID, proj.Description, proj.Name, proj.createDate, usr.Photo, usr.name }).ToList();

            ICollection<object> allProjects = new List<object>(); // this array should be returned

            int i = 0;
            if (projectStatus == 0 || projectStatus == 1 || projectStatus == 2) //Waiting or inProgress or finished
            {
                
                foreach (var x in projectData)
                {
                    List<int> managersID = new List<int>();
                    if (projectStatus == 0) //GET DATA FROM ApplyProjectTable (waiting)
                    {                       
                       var ManagerData = (from appProj in db.ApplyProjectTable   // query to get all of the applied Managers for specific project
                                       where appProj.project.ID == x.ID
                                       select new { appProj.projectManager.ID }).ToArray();
                        foreach (var y in ManagerData)
                        {
                            managersID.Add(y.ID);  // for waiting it will have many records
                        }
                    }
                    else //GET DATA FROM projectTable (inProgress & finished)
                    {
                        var ManagerData = (from appProj in db.ProjectTable  // query to get the applied Managers for specific project
                                       where appProj.ID == x.ID
                                       select new { appProj.projectManager.ID }).ToArray();
                        foreach (var y in ManagerData)
                        {
                            managersID.Add(y.ID);  // for inProgress & finished it will have only one record
                        }
                    }

                    ICollection<object> commentsArr = new List<object>();
                    
                    List<String> finalResultofIDs = new List<String>();
                    
                    var result = String.Join(",", managersID);
                    
                    finalResultofIDs = result.Split(',').ToList();
                    //finalResultofIDs = result.Split(',').Select(int.Parse).ToList(); // split the string by ( , ) to INT LIST 

                    if (projectStatus == 0) // if it was waiting so it will have comments
                    {
                        var commentData = (from comm in db.CommentsTable   // query to get all of the comments
                                           where comm.project.ID == x.ID
                                           select new { comm.projectManager.name, comm.projectManager.Photo, comm.comment }).ToArray();
                        foreach (var y in commentData)
                        {
                            commentsArr.Add(new
                            {
                                userPhoto = y.Photo,
                                userName = y.name,
                                userComment = y.comment
                            });
                        }
                    }
                    else // if it was inProgress or finished it willn't have comments anymore
                    {
                        commentsArr.Add("");
                    }

                    allProjects.Add(
                        new
                        {
                            userPhoto = projectData[i].Photo,
                            userName = projectData[i].name,
                            postingTime = projectData[i].createDate,
                            projectName = projectData[i].Name,
                            projectDescription = projectData[i].Description,
                            projectId = projectData[i].ID,
                            appliedProjectManagers = finalResultofIDs,
                            userComment = commentsArr
                        });
                    i++;
                }
                return JsonConvert.SerializeObject(allProjects);
            }
            return null;
        }
    }
}
