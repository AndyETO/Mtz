using MaritzaData;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maritza.Controllers.Base
{
    public class BaseController: Controller
    {
        
        //public string SaveImage(HttpPostedFileBase file)
        //{
        //    string fileRoute = "";
        //    if (file != null && file.ContentLength > 0)
        //    {
        //        string NameImage = Path.GetFileName(file.FileName);
        //        string ext = Path.GetExtension(file.FileName);
        //        string NewName = DateTime.Now.ToString("dd-MM-yyyyy") + "_" + DateTime.Now.ToString("HH_mm_ss_FFF") + ext;
        //        string Route = Path.Combine(Server.MapPath("~/ProyectImages"), NewName);
        //        file.SaveAs(Route);
        //        fileRoute = "ProyectImages/" + NewName;
        //    }
        //    return fileRoute;
        //}

        public int CurrentUserID => Int32.Parse(HttpContext.User.Identity.Name);

    }
}