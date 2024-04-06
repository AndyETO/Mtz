using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Maritza.Controllers.Base;
using MaritzaBusness;
using MaritzaData;
using MaritzaData.ConfigClasses;
using MaritzaData.Filters;

namespace Maritza.Controllers
{
    [Authorize(Roles = RolesApp.Rol_Administrador)]
    public class ProyectsController : BaseController
    {
        ProyectCharacteristicsB ProyectCharacteristicsB = new ProyectCharacteristicsB();
        CharacteristicsB CharacteristicsB = new CharacteristicsB();
        ProyectImagesB ProyectImagesB = new ProyectImagesB();
        ProyectTypesB ProyectTypesB = new ProyectTypesB();
        PublishesB PublishB = new PublishesB();
        ProyectB ProyectB = new ProyectB();
        GenderB GenderB = new GenderB();
        TopicsB TopicsB = new TopicsB();
        public ActionResult Index(fltProyects filters = null)
        {
            ViewBag.Filter = filters;
            var lstProyects = ProyectB.GetAll(filters);
            if (Request.IsAjaxRequest())
                return PartialView("_Index", lstProyects);
            return View(lstProyects);
        }

        public ActionResult Details(int id)
        {
            var model = ProyectB.getById(id);
            if (model == null)
                return HttpNotFound();


            var MainPagePublish = PublishB.GetMainPageByProyectID(model.ProyectID);
            var ProyectPublish = PublishB.GetProyectByProyectID(model.ProyectID);
            model.MainPagePublish = MainPagePublish;
            model.ProyectPublish = ProyectPublish;

            var lstModelProyectCharacteristics = ProyectCharacteristicsB.GetListByProyectID(model.ProyectID);
            model.lstModelProyectCharacteristics = lstModelProyectCharacteristics;

            var lstModelProyectImages = ProyectImagesB.GetListByProyectID(model.ProyectID);
            model.lstModelProyectImages = lstModelProyectImages;

            var MainImage = ProyectImagesB.GetMainImageByProyectID(model.ProyectID);
            model.MainImage = MainImage;

            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.lstProyectTypes = ProyectTypesB.GetList();
            ViewBag.lstTopics = TopicsB.GetList();
            ViewBag.lstGenders = GenderB.GetList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(tblProyects model)
        {
            ViewBag.lstProyectTypes = ProyectTypesB.GetList();
            ViewBag.lstTopics = TopicsB.GetList();
            ViewBag.lstGenders = GenderB.GetList();
            if (model == null)
            {
                ViewBag.ErrorMessage = $"Ocurrio un error con el controlador";
                return View(model);
            }

            model.Active = true;
            model.UpdatedDate = model.CreatedDate = DateTime.Now;
            model.UpdatedBy = model.CreatedBy = CurrentUserID;
            Response response = ProyectB.Create(model);
            if (response.Result != Result.Ok)
            {
                ViewBag.ErrorMessage = $"Ocurrio un error al guardar la información";
                return View(model);
            }
            if (model.lstCharacteristics != null)
            {
                if (model.lstCharacteristics.Count > 0)
                {
                    foreach (var characteristic in model.lstCharacteristics)
                    {
                        try
                        {
                            var exists = CharacteristicsB.CheckIfExistsCharacteristicByID(int.Parse(characteristic));
                            if (exists)
                            {
                                tblProyectCharacteristics tblProyectCharacteristics = new tblProyectCharacteristics();
                                tblProyectCharacteristics.ProyectID = model.ProyectID;
                                tblProyectCharacteristics.CharacteristicID = int.Parse(characteristic);
                                tblProyectCharacteristics.Active = true;
                                tblProyectCharacteristics.CreatedBy = CurrentUserID;
                                tblProyectCharacteristics.CreatedDate = DateTime.Now;
                                Response reponse = ProyectCharacteristicsB.Create(tblProyectCharacteristics);
                                if (reponse.Result != Result.Ok)
                                {
                                    //log error
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            //log error
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(model.MainImageBase))
            {
                string url = SaveImage(model.MainImageBase, model.ProyectID);
                if (!string.IsNullOrEmpty(url))
                {
                    tblProyectImages tblProyectImages = new tblProyectImages();
                    tblProyectImages.Url = url;
                    tblProyectImages.IsMainImage = true;
                    tblProyectImages.ProyectID = model.ProyectID;
                    tblProyectImages.Active = true;
                    tblProyectImages.CreatedBy = CurrentUserID;
                    tblProyectImages.CreatedDate = DateTime.Now;
                    ProyectImagesB.Create(tblProyectImages);
                }
            }
            if (model.ImageBase != null)
            {
                if (model.ImageBase.Count > 0)
                {

                    foreach (var strImage in model.ImageBase)
                    {
                        string urlImage = SaveImage(strImage, model.ProyectID);
                        if (!string.IsNullOrEmpty(urlImage))
                        {
                            tblProyectImages tblProyectImages = new tblProyectImages();
                            tblProyectImages.Url = urlImage;
                            tblProyectImages.IsMainImage = false;
                            tblProyectImages.ProyectID = model.ProyectID;
                            tblProyectImages.Active = true;
                            tblProyectImages.CreatedBy = CurrentUserID;
                            tblProyectImages.CreatedDate = DateTime.Now;
                            ProyectImagesB.Create(tblProyectImages);
                        }
                    }

                }
            }
            if (!string.IsNullOrEmpty(model.StartDateMainPage) && !string.IsNullOrEmpty(model.EndDateMainPage))
            {
                tblPublishes tblPublish = new tblPublishes();
                tblPublish.ProyectID = model.ProyectID;
                tblPublish.PublishTypeID = 1;//Main page
                tblPublish.PriorityLevel = model.PrirityMainPage;//Baja 3 - Mediana 2 - Alta 3
                tblPublish.StarDate = DateTime.ParseExact(model.StartDateMainPage, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                tblPublish.EndDate = DateTime.ParseExact(model.EndDateMainPage, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                tblPublish.Active = true;
                tblPublish.CreatedBy = tblPublish.UpdatedBy = CurrentUserID;
                tblPublish.CreatedDate = tblPublish.UpdatedDate = DateTime.Now;

                Response response_ = PublishB.Create(tblPublish);
                if (response_.Result != Result.Ok)
                {
                    //error log
                }
            }
            if (!string.IsNullOrEmpty(model.StartDateProyects) && !string.IsNullOrEmpty(model.EndDateProyects))
            {
                tblPublishes tblPublish = new tblPublishes();
                tblPublish.ProyectID = model.ProyectID;
                tblPublish.PublishTypeID = 2;//Proyects
                tblPublish.PriorityLevel = model.PrirityProyects;//Baja 3 - Mediana 2 - Alta 3
                tblPublish.StarDate = DateTime.ParseExact(model.StartDateProyects, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                tblPublish.EndDate = DateTime.ParseExact(model.EndDateProyects, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                tblPublish.Active = true;
                tblPublish.CreatedBy = tblPublish.UpdatedBy = CurrentUserID;
                tblPublish.CreatedDate = tblPublish.UpdatedDate = DateTime.Now;

                Response response_ = PublishB.Create(tblPublish);
                if (response_.Result != Result.Ok)
                {
                    //error log
                }
            }
            ViewBag.ErrorMessage = $"Se guardo la información exitosamente";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var model = ProyectB.getById(id);
            if (model == null)
                return HttpNotFound();

            ViewBag.lstProyectTypes = ProyectTypesB.GetList();
            ViewBag.lstTopics = TopicsB.GetList();
            ViewBag.lstGenders = GenderB.GetList();

            var MainPagePublish = PublishB.GetMainPageByProyectID(model.ProyectID);
            var ProyectPublish = PublishB.GetProyectByProyectID(model.ProyectID);
            model.MainPagePublish = MainPagePublish;
            model.ProyectPublish = ProyectPublish;

            var lstModelProyectCharacteristics = ProyectCharacteristicsB.GetListByProyectID(model.ProyectID);
            model.lstModelProyectCharacteristics = lstModelProyectCharacteristics;

            var lstModelProyectImages = ProyectImagesB.GetListByProyectID(model.ProyectID);
            model.lstModelProyectImages = lstModelProyectImages;

            var MainImage = ProyectImagesB.GetMainImageByProyectID(model.ProyectID);
            model.MainImage = MainImage;
            if (MainImage != null)
            {
                string path = Server.MapPath(model.MainImage.Url);
                byte[] imageArray = System.IO.File.ReadAllBytes(path);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                model.MainImage.base64Image =$"data:image/png;base64,{base64ImageRepresentation}";
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(tblProyects model)
        {
            if (model == null)
                return HttpNotFound();
            //string Route = SaveImage(model.ImageBase);
            //if (Route.Length > 0 && Route != "")
            //{
            //    model.Image = Route;
            //}
            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = CurrentUserID;

            Response response = ProyectB.Update(model);

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
            var model = ProyectB.getById(id);
            if (model == null)
            {
                ViewBag.ErrorMessage = $"Ocurrio un error al guardar la información";
                RedirectToAction("Index");
            }
            model.Active = false;
            model.DeletedDate = DateTime.Now;
            model.DeletedBy = CurrentUserID;
            Response response = ProyectB.Delete(model);

            if (response.Result != Result.Ok)
            {
                ViewBag.ErrorMessage = $"Ocurrio un error al guardar la información";
                return View(model);
            }
            ViewBag.ErrorMessage = $"Se guardo la información exitosamente";
            return RedirectToAction("Index");
        }

        //public JsonResult SearchCharacteristicByName(string name)
        //{
        //    int id = 0;
        //    string displayName = "";
        //    var Characteristic = CharacteristicsB.GetByName(name);
        //    if (Characteristic != null)
        //    {
        //        id = Characteristic.CharacteristicID;
        //        displayName = Characteristic.Name;

        //    }
        //    var json = new { id = id, displayName = displayName };
        //    return Json(json, JsonRequestBehavior.AllowGet);
        //}

        private string SaveImage(string Base64, int id)
        {
            try
            {
                string cadena = Base64.Substring(Base64.LastIndexOf(",") + 1);
                byte[] bytes = System.Convert.FromBase64String(cadena);
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                    string PathSave = $"{Server.MapPath("~/ProyectImages")}\\{id}";
                    Directory.CreateDirectory(PathSave);
                    string NewName = $"{id}_{DateTime.Now.ToString("dd-MM-yyyyy")}_{DateTime.Now.ToString("HH_mm_ss_FFF")}.jpg";
                    string filePath = Path.Combine(PathSave, NewName);
                    image.Save(filePath);
                    string pathDB = $"~/ProyectImages/{id}/{NewName}";
                    return pathDB;
                }
            }
            catch (Exception ex)
            {
                //log error
                return string.Empty;
            }

        }
    }


}
