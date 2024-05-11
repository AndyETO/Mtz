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
        ProyectDetailsB ProyectDetailsB = new ProyectDetailsB();
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

            tblProyectDetails tblProyectDetails = new tblProyectDetails();
            tblProyectDetails.ProyectID = model.ProyectID;
            tblProyectDetails.Active = true;
            tblProyectDetails.CreatedBy = tblProyectDetails.UpdatedBy = CurrentUserID;
            tblProyectDetails.CreatedDate = tblProyectDetails.UpdatedDate = DateTime.Now;
            Response response_ = ProyectDetailsB.Create(tblProyectDetails);
            if (response_.Result != Result.Ok)
            {
                //something wrong 
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

                Response response__ = PublishB.Create(tblPublish);
                if (response__.Result != Result.Ok)
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

                Response response___ = PublishB.Create(tblPublish);
                if (response___.Result != Result.Ok)
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
                model.MainImage.base64Image = $"data:image/png;base64,{base64ImageRepresentation}";
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(tblProyects model)
        {
            if (model == null)
                return HttpNotFound();

            if (!ProyectDetailsB.CheckIfExistsByProyectID(model.ProyectID))
            {
                tblProyectDetails tblProyectDetails = new tblProyectDetails();
                tblProyectDetails.ProyectID = model.ProyectID;
                tblProyectDetails.Active = true;
                tblProyectDetails.CreatedBy = tblProyectDetails.UpdatedBy = CurrentUserID;
                tblProyectDetails.CreatedDate = tblProyectDetails.UpdatedDate = DateTime.Now;
                Response response_ = ProyectDetailsB.Create(tblProyectDetails);
                if (response_.Result != Result.Ok)
                {
                    //something wrong 
                }
            }

            #region checking
            if (model.lstDeletedCharacteristics != null)
            {
                if (model.lstDeletedCharacteristics.Count > 0)
                {
                    foreach (var characteristic in model.lstDeletedCharacteristics)
                    {
                        try
                        {
                            var tblProyectCharacteristics = ProyectCharacteristicsB.GetByCharacteristicIDAndProyectID(int.Parse(characteristic), model.ProyectID);
                            if (tblProyectCharacteristics != null)
                            {
                                tblProyectCharacteristics.CharacteristicID = int.Parse(characteristic);
                                tblProyectCharacteristics.Active = false;
                                tblProyectCharacteristics.DeletedBy = CurrentUserID;
                                tblProyectCharacteristics.DeletedDate = DateTime.Now;
                                Response reponse = ProyectCharacteristicsB.Update(tblProyectCharacteristics);
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
            if (model.lstCharacteristics != null)
            {
                if (model.lstCharacteristics.Count > 0)
                {
                    foreach (var characteristic in model.lstCharacteristics)
                    {
                        try
                        {
                            var existsInProyect = ProyectCharacteristicsB.CheckIfExistsCharacteristicByCharacteristicIDAndProyectID(int.Parse(characteristic), model.ProyectID);
                            var exists = CharacteristicsB.CheckIfExistsCharacteristicByID(int.Parse(characteristic));
                            if (exists && !existsInProyect)
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
            var publishMain = PublishB.GetMainPageByProyectID(model.ProyectID);
            if (publishMain == null && !string.IsNullOrEmpty(model.StartDateMainPage) && !string.IsNullOrEmpty(model.EndDateMainPage))
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

                Response response__ = PublishB.Create(tblPublish);
                if (response__.Result != Result.Ok)
                {
                    //error log
                }
            }
            else if (publishMain != null && !string.IsNullOrEmpty(model.StartDateMainPage) && !string.IsNullOrEmpty(model.EndDateMainPage) && (!model.StartDateProyects.Equals(publishMain.StarDate.ToString("yyyy-MM-dd")) || !model.EndDateProyects.Equals(publishMain.EndDate.ToString("yyyy-MM-dd"))))
            {
                publishMain.PriorityLevel = model.PrirityMainPage;//Baja 3 - Mediana 2 - Alta 3
                publishMain.StarDate = DateTime.ParseExact(model.StartDateMainPage, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                publishMain.EndDate = DateTime.ParseExact(model.EndDateMainPage, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                publishMain.UpdatedBy = CurrentUserID;
                publishMain.UpdatedDate = DateTime.Now;
                Response response__ = PublishB.Create(publishMain);
                if (response__.Result != Result.Ok)
                {
                    //error log
                }
            }
            var publishSeach = PublishB.GetProyectByProyectID(model.ProyectID);
            if (publishSeach == null && !string.IsNullOrEmpty(model.StartDateProyects) && !string.IsNullOrEmpty(model.EndDateProyects))
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
                Response response__ = PublishB.Create(tblPublish);
                if (response__.Result != Result.Ok)
                {
                    //error log
                }
            }
            else if (publishSeach != null && !string.IsNullOrEmpty(model.StartDateProyects) && !string.IsNullOrEmpty(model.EndDateProyects) && (!model.StartDateProyects.Equals(publishSeach.StarDate.ToString("yyyy-MM-dd")) || !model.EndDateProyects.Equals(publishSeach.EndDate.ToString("yyyy-MM-dd"))))
            {
                publishSeach.PriorityLevel = model.PrirityProyects;//Baja 3 - Mediana 2 - Alta 3
                publishSeach.StarDate = DateTime.ParseExact(model.StartDateProyects, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                publishSeach.EndDate = DateTime.ParseExact(model.EndDateProyects, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                publishSeach.UpdatedBy = CurrentUserID;
                publishSeach.UpdatedDate = DateTime.Now;
                Response response__ = PublishB.Update(publishSeach);
                if (response__.Result != Result.Ok)
                {
                    //error log
                }
            }
            #endregion
            Response response = ProyectB.Update(model);
            if (response.Result != Result.Ok)
            {
                #region if there are an error
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
                model.UpdatedDate = DateTime.Now;
                model.UpdatedBy = CurrentUserID;
                #endregion
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
