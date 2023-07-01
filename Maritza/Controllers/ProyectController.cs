﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaritzaBusness;
using MaritzaData;
using MaritzaData.ConfigClasses;
using MaritzaData.Filters;

namespace Maritza.Controllers
{
    public class ProyectController : Controller
    {
        //Objets seccion
        ProyectB ProyectB = new ProyectB();



        // GET: Proyect
        public ActionResult Index(fltProyects filter = null)
        {
            ViewBag.Filter = filter;

            var lstProyects = ProyectB.GetAll(filter);

            return View(lstProyects);
        }

        // GET: Proyect/Details/5
        public ActionResult Details(int id)
        {
            var proyect = ProyectB.getById(id);
            if (proyect == null)
            {
                return HttpNotFound();
            }
            return View(proyect);
        }

        // GET: Proyect/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proyect/Create
        [HttpPost]
        public ActionResult Create(tblProyects model)
        {
            if (model == null)
            {
                ViewBag.ErrorMessage = $"Ocurrio un error con el controlador";
                return View(model);
            }
            Response response = new Response();

            model.Active = true;
            model.UpdatedDate = model.CreatedDate = DateTime.Now;
            model.UpdatedBy = model.CreatedBy = 1;


            if (model.ImageBase != null && model.ImageBase.ContentLength > 0)
            {
                string NameImage = Path.GetFileName(model.ImageBase.FileName);
                string ext = Path.GetExtension(model.ImageBase.FileName);
                string NewName = DateTime.Now.ToString("dd-MM-yyyyy")+"_"+DateTime.Now.ToString("HH_mm_ss_FFF")+ ext;
                string Route = Path.Combine(Server.MapPath("~/ProyectImages"), NewName);
                model.ImageBase.SaveAs(Route);

                model.Image = "ProyectImages/"+NewName;

            }


            response = ProyectB.Create(model);

            if (response.Result != Result.Ok)
            {
                ViewBag.ErrorMessage = $"Ocurrio un error al guardar la información";
                return View(model);
            }
            ViewBag.ErrorMessage = $"Se guardo la información exitosamente";
            return RedirectToAction("Index");
        }

        // GET: Proyect/Edit/5
        public ActionResult Edit(int id)
        {

            var proyect = ProyectB.getById(id);

            return View(proyect);
        }

        // POST: Proyect/Edit/5
        [HttpPost]
        public ActionResult Edit(tblProyects model)
        {
            if (model == null)
            {
                return HttpNotFound();
            }

            model.UpdatedDate=DateTime.Now;
            model.UpdatedBy = 2;

            Response response = ProyectB.Update(model);

            if (response.Result != Result.Ok)
            {
                ViewBag.ErrorMessage = $"Ocurrio un error al guardar la información";
                return View(model);
            }
            ViewBag.ErrorMessage = $"Se guardo la información exitosamente";

            return RedirectToAction("Index");
        }



        // POST: Proyect/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var model = ProyectB.getById(id);
            if (model == null)
            {
                ViewBag.ErrorMessage = $"Ocurrio un error al guardar la información";
                RedirectToAction("Index");
            }
            model.Active = false;
            model.DeletedDate = DateTime.Now;
            model.DeletedBy = 3;
            Response response = ProyectB.Delete(model);

            if (response.Result != Result.Ok)
            {
                ViewBag.ErrorMessage = $"Ocurrio un error al guardar la información";
                return View(model);
            }
            ViewBag.ErrorMessage = $"Se guardo la información exitosamente";
            return RedirectToAction("Index");
        }
    }
}