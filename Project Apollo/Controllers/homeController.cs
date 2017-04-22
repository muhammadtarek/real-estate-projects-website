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

        public object deleteProject(int ?id)
        {
            Project p = db.ProjectTable.Find(id);
            if (p != null) { 
                db.ProjectTable.Remove(p);
                db.SaveChanges();
                return new { opertaion = true };
            }
            return new { opertaion = false };
        }
        public object updateProject(string projectName, string projectDescription, int projectId)
        {
            Project p = db.ProjectTable.Find(projectId);
            p.Name = projectName;
            p.Description = projectDescription;
            db.SaveChanges();
            return new { postingTime = p.createDate,
                projectName = p.Name ,
                projectDescription =  p.Description,
                projectId =  p.ID
            };
        }
    }
}
