using Project_Apollo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Apollo.Controllers
{
    public class profileController : Controller
    {
        DBase db = new DBase();
        // GET: profile
        public ActionResult Index()
        {
            return View();
        }


        public void declineProject(int projectID)
        {
            var Result = new HomeController().deleteProject(projectID);
        }


        public object loadPendingProjects()
        {
            int status = 3;
            List<Project> projects = db.ProjectTable.Where(x => ((int)x.status) == status).ToList();
            return View(projects);
        }


    }

}
