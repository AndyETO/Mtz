using MaritzaBusness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maritza.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        ProyectB ProyectB = new ProyectB();
        [AllowAnonymous]
        public ActionResult Index()
        {
            var model = ProyectB.getRandomItems();
            //ViewBag. = true;
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}