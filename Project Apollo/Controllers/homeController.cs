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
    }
}
