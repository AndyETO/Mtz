using System;
using System.Collections.Generic;
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
        public ActionResult Index(fltProyects filter=null)
        {
            ViewBag.Filter = filter;

            var lstProyects = ProyectB.GetAll(filter);

            return View(lstProyects);
        }

        // GET: Proyect/Details/5
        public ActionResult Details(int id)
        {
            var proyect = ProyectB.getById(id);
            if (proyect==null)
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
            model.UpdatedBy = model.CreatedBy =1;

            response = ProyectB.Create(model);

            if (response.Result != Result.Ok)
            {
                ViewBag.ErrorMessage = $"Ocurrio un error al guardar la información";
                return View(model);
            }

            return RedirectToAction("Index");
        }

        // GET: Proyect/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Proyect/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Proyect/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Proyect/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
