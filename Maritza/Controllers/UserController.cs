﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maritza.Controllers
{
    public class UserController : Controller
    {
        // GET: tblUser
        public ActionResult Index()
        {
            return View();
        }

        // GET: tblUser/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: tblUser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblUser/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: tblUser/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: tblUser/Edit/5
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

        // GET: tblUser/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: tblUser/Delete/5
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
