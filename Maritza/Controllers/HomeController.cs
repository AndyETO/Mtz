using MaritzaBusness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maritza.Controllers
{
    public class HomeController : Controller
    {
        ProyectB ProyectB =new ProyectB();
        public ActionResult Index()
        {
            var model =ProyectB.getRandomItems();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}