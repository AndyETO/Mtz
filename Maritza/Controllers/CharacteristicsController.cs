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
using ClosedXML.Excel;
using DocumentFormat.OpenXml.AdditionalCharacteristics;
using MaritzaData.Base;

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
                Ids = Ids.Substring(0, Ids.Length - 1);
            var characteristics = CharacteristicsB.GetTop10ByName(Sample, Ids);
            return Json(characteristics, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(fltCharacteristics filters = null)
        {
            ViewBag.Filter = filters;
            var lstCharacteristicss = CharacteristicsB.GetAll(filters);
            if (Request.IsAjaxRequest())
            {
                //return PartialView("_Index", lstCharacteristicss);
                return PartialView("Empty", lstCharacteristicss);

            }
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




        public ActionResult CreateByExcel()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateByExcel(HttpPostedFileBase ExcelFile)
        {

            if (ExcelFile != null && ExcelFile.ContentLength > 0)
            {
                if (ExcelFile.FileName.EndsWith(".xlsx") || ExcelFile.FileName.EndsWith(".xls"))
                {
                    XLWorkbook Workbook;
                    try
                    {
                        Workbook = new XLWorkbook(ExcelFile.InputStream);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, "Archivo invalido:" + ex.Message);
                        return View();
                    }
                    IXLWorksheet WorkSheet = null;
                    try
                    {
                        WorkSheet = Workbook.Worksheet("Sheet1");
                    }
                    catch
                    {
                        ModelState.AddModelError(String.Empty, "Hoja no encontrada (Sheet1)");
                        return View();
                    }
                    var xs = WorkSheet.FirstRow();
                    if (xs.Cell(1).Value.ToString() != "Characteristic")
                    {
                        ModelState.AddModelError(String.Empty, "Favor de introducir el archivo correcto");
                        return View();
                    }
                    try
                    {
                        List<tblCharacteristics> lstCreateCharacteristics = new List<tblCharacteristics>();
                        List<ExcelErrors> lstExcelErrors = new List<ExcelErrors>();
                        string Characteristic = string.Empty;


                        foreach (var row in WorkSheet.RowsUsed())
                        {
                            if (row.Cell(1).Value.ToString().Length != 0)
                            {
                                if (row.RowNumber() >= 2)
                                {
                                    Characteristic = row.Cell(1).Value.ToString();
                                    bool Exists = CharacteristicsB.CheckIfExistsCharacteristicByTerm(Characteristic);
                                    if (!Exists)
                                    {
                                        tblCharacteristics newCharacteristic = new tblCharacteristics();
                                        newCharacteristic.Name = Characteristic;
                                        newCharacteristic.Active = true;
                                        newCharacteristic.CreatedBy = newCharacteristic.UpdatedBy = CurrentUserID;
                                        newCharacteristic.CreatedDate = newCharacteristic.UpdatedDate = DateTime.Now;
                                        lstCreateCharacteristics.Add(newCharacteristic);
                                    }
                                    else
                                    {
                                        lstExcelErrors.Add(new ExcelErrors() { row = row.RowNumber(), message = "Ya existe en la base de datos." });
                                    }
                                    Characteristic = string.Empty;
                                }
                            }
                            else
                            {
                                lstExcelErrors.Add(new ExcelErrors() { row = row.RowNumber(), message = "Esta vacía la fila." });
                            }
                        }
                        if (lstCreateCharacteristics != null)
                        {
                            if (lstCreateCharacteristics.Count > 0)
                            {
                                Response response = CharacteristicsB.CreateList(lstCreateCharacteristics);
                                if (response.Result != Result.Ok)
                                {
                                    //error
                                }
                            }
                        }
                        if (lstExcelErrors != null)
                        {

                            if (lstExcelErrors.Count > 0)
                            {
                                ViewBag.ExcelErrors = lstExcelErrors;
                                return View();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(String.Empty, "Ocurrió un error al procesar los datos, verifique que las columnas tengan valores válidos ");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Solo se permiten archivos .xlsx y .xls.");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Archivo invalido.");
                return View();
            }
            TempData["Message"] = "Carga exitosa";
            return RedirectToAction("Index");
        }

        public FileResult Download()
        {
            return File("~/Content/Samples/SampleCharacteristics.xlsx", System.Net.Mime.MediaTypeNames.Application.Octet, "SampleCharacteristics.xlsx");
        }

    }
}
