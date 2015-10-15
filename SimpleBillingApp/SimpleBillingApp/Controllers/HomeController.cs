using SimpleBillingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBillingApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult index() 
        {
            ViewBag.Message = "This is home page";
            return View();
        }

        public ActionResult Settings()
        {
            ViewBag.Message = "Settings";

            return RedirectToAction("Index","Settings");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
