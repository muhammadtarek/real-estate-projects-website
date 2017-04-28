using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Apollo.Controllers
{
    public class profileController : Controller
    {
        // GET: profile
        public ActionResult Index()
        {
            return View();
        }


        public void declineProject(int projectID)
        {
            var Result = new HomeController().deleteProject(projectID);
        }

       
    }

}
