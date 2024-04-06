using Maritza.Controllers.Base;
using MaritzaBusness;
using MaritzaData.ConfigClasses;
using MaritzaData.Filters;
using MaritzaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Maritza.Controllers
{
    public class CharacteristicsController : BaseController
    {
        CharacteristicsB CharacteristicsB = new CharacteristicsB();

        public ActionResult SearchCharacteristicByName(string Sample, string added)
        {
            List<string> addedArray = JsonConvert.DeserializeObject<List<string>>(added);
            string Ids = string.Empty;
            addedArray.ForEach(x => Ids += $"{x},");
            if (!string.IsNullOrEmpty(Ids))
                Ids=Ids.Substring(0, Ids.Length - 1);
            var characteristics = CharacteristicsB.GetTop10ByName(Sample, Ids);
            return Json(characteristics, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(fltCharacteristics filters = null)
        {
            ViewBag.Filter = filters;
            var lstCharacteristicss = CharacteristicsB.GetAll(filters);
            if (Request.IsAjaxRequest())
                return PartialView("_Index", lstCharacteristicss);
            return View(lstCharacteristicss);
        }

        public ActionResult Details(int id)
        {
            var Characteristics = CharacteristicsB.getById(id);
            if (Characteristics == null)
            {
                return HttpNotFound();
            }
            return View(Characteristics);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(tblCharacteristics model)
        {
            if (model == null)
            {
                ViewBag.ErrorMessage = $"Ocurrio un error con el controlador";
                return View(model);
            }

            model.Active = true;
            model.UpdatedDate = model.CreatedDate = DateTime.Now;
            model.UpdatedBy = model.CreatedBy = CurrentUserID;

            Response response = CharacteristicsB.Create(model);

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

            var Characteristics = CharacteristicsB.getById(id);

            return View(Characteristics);
        }
        [HttpPost]
        public ActionResult Edit(tblCharacteristics model)
        {
            if (model == null)
            {
                return HttpNotFound();
            }

            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = CurrentUserID;

            Response response = CharacteristicsB.Update(model);

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
            var model = CharacteristicsB.getById(id);
            if (model == null)
            {
                ViewBag.ErrorMessage = $"Ocurrio un error al guardar la información";
                RedirectToAction("Index");
            }
            model.Active = false;
            model.DeletedDate = DateTime.Now;
            model.DeletedBy = CurrentUserID;
            Response response = CharacteristicsB.Delete(model);

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
