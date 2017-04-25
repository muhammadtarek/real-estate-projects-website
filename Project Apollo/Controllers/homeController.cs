using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Apollo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.showNav = true;
            ViewBag.tabs = new string[4] { "Home", "Profile", "FAQ", "Contact us" };
            ViewBag.userPhoto = "/Public/assets/images/picture.jpg";
            ViewBag.userName = "Muhammad Tarek";
            return View();
        }
    }
}
