using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCAuthenticationCheck.Controllers
{
    public class HomeController : BASEController
    {
        public ActionResult Index()
        {
            return View(); 
        }
         
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() //
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Anonymous() //
        {
            ViewBag.Message = "Your Anonymous page.";

            return View();
        }

    }
}