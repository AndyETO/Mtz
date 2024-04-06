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
    public class TopicsController : BaseController
    {
        TopicsB TopicsB = new TopicsB();

        public ActionResult Index(fltTopics filters = null)
        {
            ViewBag.Filter = filters;
            var lstTopicss = TopicsB.GetAll(filters);
            if (Request.IsAjaxRequest())
                return PartialView("_Index", lstTopicss);
            return View(lstTopicss);
        }

        public ActionResult Details(int id)
        {
            var Topics = TopicsB.getById(id);
            if (Topics == null)
            {
                return HttpNotFound();
            }
            return View(Topics);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(tblTopics model)
        {
            if (model == null)
            {
                ViewBag.ErrorMessage = $"Ocurrio un error con el controlador";
                return View(model);
            }
            

            model.Active = true;
            model.UpdatedDate = model.CreatedDate = DateTime.Now;
            model.UpdatedBy = model.CreatedBy = CurrentUserID;

            Response response = TopicsB.Create(model);

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

            var Topics = TopicsB.getById(id);

            return View(Topics);
        }
        [HttpPost]
        public ActionResult Edit(tblTopics model)
        {
            if (model == null)
            {
                return HttpNotFound();
            }
           
            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = CurrentUserID;

            Response response = TopicsB.Update(model);

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
            var model = TopicsB.getById(id);
            if (model == null)
            {
                ViewBag.ErrorMessage = $"Ocurrio un error al guardar la información";
                RedirectToAction("Index");
            }
            model.Active = false;
            model.DeletedDate = DateTime.Now;
            model.DeletedBy = CurrentUserID;
            Response response = TopicsB.Delete(model);

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
