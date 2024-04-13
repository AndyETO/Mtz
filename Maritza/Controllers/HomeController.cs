using Maritza.Controllers.Base;
using MaritzaBusness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maritza.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        ProyectImagesB ProyectImagesB = new ProyectImagesB();
        ProyectB ProyectB = new ProyectB();
        [AllowAnonymous]
        public ActionResult Index()
        {
            var lstSearchProyects = ProyectB.getNRandom(10);
            lstSearchProyects.ForEach(m =>
            {
                var MainImage = ProyectImagesB.GetMainImageByProyectID(m.ProyectID);
                m.MainImage = MainImage;
            });
            var lstMainProyects = ProyectB.getNRandom(6);
            lstMainProyects.ForEach(m =>
            {
                var MainImage = ProyectImagesB.GetMainImageByProyectID(m.ProyectID);
                m.MainImage = MainImage;
            });
            ViewBag.lstSearchProyects = lstSearchProyects;
            ViewBag.lstMainProyects = lstMainProyects;
            return View();
        }
        [AllowAnonymous]
        public ActionResult SearchProyect()
        {
            
            return View();
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