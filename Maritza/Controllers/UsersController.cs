using Maritza.Controllers.Base;
using MaritzaBusness;
using MaritzaData;
using MaritzaData.ConfigClasses;
using MaritzaData.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maritza.Controllers
{
    public class UsersController : BaseController
    {
        UsersB UsersB = new UsersB();
        RolesB RolesB = new RolesB();
        // GET: tblUser
        public ActionResult Index(fltUsers filters)
        {
            ViewBag.Filter = filters;
            ViewBag.lstRoles = RolesB.GetList();
            var lstTopicss = UsersB.GetAll(filters);
            if (Request.IsAjaxRequest())
                return PartialView("_Index", lstTopicss);
            return View(lstTopicss);
        }

        // GET: tblUser/Details/5
        public ActionResult Details(int id)
        {
            var model = UsersB.getById(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: tblUser/Create
        public ActionResult Create()
        {
            ViewBag.lstRoles = RolesB.GetList();
            return View();
        }

        // POST: tblUser/Create
        [HttpPost]
        public ActionResult Create(tblUsers model)
        {
            ViewBag.lstRoles = RolesB.GetList();
            if (model == null)
            {
                ViewBag.ErrorMessage = $"Ocurrio un error con el controlador";
                return View(model);
            }


            model.Active = true;
            model.UpdatedDate = model.CreatedDate = DateTime.Now;
            model.UpdatedBy = model.CreatedBy = CurrentUserID;

            Response response = UsersB.Create(model);

            if (response.Result != Result.Ok)
            {
                ViewBag.ErrorMessage = $"Ocurrio un error al guardar la información";
                return View(model);
            }
            ViewBag.ErrorMessage = $"Se guardo la información exitosamente";
            return RedirectToAction("Index");
        }

        // GET: tblUser/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.lstRoles = RolesB.GetList();
            var model = UsersB.getById(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: tblUser/Edit/5
        [HttpPost]
        public ActionResult Edit(tblUsers model)
        {

            if (!UsersB.CheckIfExistsByID(model.UserID))
                return HttpNotFound();
            ViewBag.lstRoles = RolesB.GetList();

            if (model == null)
            {
                return HttpNotFound();
            }

            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = CurrentUserID;

            string sPassword = UsersB.getPasswordById(model.UserID);
            if (string.IsNullOrEmpty(model.Password) && string.IsNullOrEmpty(model.ConfirmPassword))
                model.Password = model.ConfirmPassword = sPassword;

            Response response = UsersB.Update(model);

            if (response.Result != Result.Ok)
            {
                ViewBag.ErrorMessage = $"Ocurrio un error al guardar la información";
                return View(model);
            }
            ViewBag.ErrorMessage = $"Se guardo la información exitosamente";

            return RedirectToAction("Index");
        }

        // GET: tblUser/Delete/5
        public ActionResult Delete(int id)
        {
            var model = UsersB.getById(id);
            if (model == null)
            {
                ViewBag.ErrorMessage = $"Ocurrio un error al guardar la información";
                RedirectToAction("Index");
            }
            model.Active = false;
            model.DeletedDate = DateTime.Now;
            model.DeletedBy = CurrentUserID;
            //if (string.IsNullOrEmpty(model.Password))


            Response response = UsersB.Delete(model);

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
