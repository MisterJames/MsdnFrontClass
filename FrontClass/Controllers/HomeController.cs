using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontClass.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "A Brief Description on FrontClass.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contacts Related to This Project.";

            return View();
        }
    }
}