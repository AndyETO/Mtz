using MaritzaBusness;
using MaritzaData.ConfigClasses;
using MaritzaData.Filters;
using MaritzaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maritza.Controllers.Base;

namespace Maritza.Controllers
{
    public class ProyectTypesController : BaseController
    {
        ProyectTypesB ProyectTypesB = new ProyectTypesB();

        public ActionResult Index(fltProyectTypes filters = null)
        {
            ViewBag.Filter = filters;
            var lstProyectTypes = ProyectTypesB.GetAll(filters);
            if (Request.IsAjaxRequest())
                return PartialView("_Index", lstProyectTypes);
            return View(lstProyectTypes);
        }

        public ActionResult Details(int id)
        {
            var ProyectType = ProyectTypesB.getById(id);
            if (ProyectType == null)
            {
                return HttpNotFound();
            }
            return View(ProyectType);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(tblProyectTypes model)
        {
            if (model == null)
            {
                ViewBag.ErrorMessage = $"Ocurrio un error con el controlador";
                return View(model);
            }
            Response response = new Response();

            model.Active = true;
            model.UpdatedDate = model.CreatedDate = DateTime.Now;
            model.UpdatedBy = model.CreatedBy = CurrentUserID;
            response = ProyectTypesB.Create(model);

            if (response.Result != Result.Ok)
            {
                ViewBag.ErrorMessage = $"Ocurrio un error al guardar la información";
                return View(model);
            }
            ViewBag.ErrorMessage = $"Se guardo la información exitosamente";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var model = ProyectTypesB.getById(id);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(tblProyectTypes model)
        {
            if (model == null)
                return HttpNotFound();
      
            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = CurrentUserID;

            Response response = ProyectTypesB.Update(model);

            if (response.Result != Result.Ok)
            {
                ViewBag.ErrorMessage = $"Ocurrio un error al guardar la información";
                return View(model);
            }
            ViewBag.ErrorMessage = $"Se guardo la información exitosamente";

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var model = ProyectTypesB.getById(id);
            if (model == null)
            {
                ViewBag.ErrorMessage = $"Ocurrio un error al guardar la información";
                RedirectToAction("Index");
            }
            model.Active = false;
            model.DeletedDate = DateTime.Now;
            model.DeletedBy = CurrentUserID;
            Response response = ProyectTypesB.Delete(model);

            if (response.Result != Result.Ok)
            {
                ViewBag.ErrorMessage = $"Ocurrio un error al guardar la información";
                return View(model);
            }
            ViewBag.ErrorMessage = $"Se guardo la información exitosamente";
            return RedirectToAction("Index");
        }

        //UploadExcel method
    }
}
