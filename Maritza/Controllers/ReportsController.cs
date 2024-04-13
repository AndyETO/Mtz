using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maritza.Controllers
{
    public class ReportsController : Controller
    {
        public ActionResult ReportProyect()
        {
            //PDF
            //Photo
            //title
            //Count publish
            //Number of likes and Number of dislikes
            ///Statics - a long the time
            //Number of people watcherd the proyect
            ///Statics - a long the time

            return View();
        }
        public ActionResult ReportUsers()
        {
            //EXCEL
            //ID
            //Name
            //Email
            //CreatedDate
            //Users created
            //Validated comments - number
            //Publish - number
            //Proyects added
            return View();
        }
        public ActionResult ProyectBadget()
        {
            //EXCEL
            //title proyect
            //badget
            //Gender
            //--show total
            return View();
        }
    }
}
