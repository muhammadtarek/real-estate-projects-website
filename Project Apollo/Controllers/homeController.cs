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
    }
}
