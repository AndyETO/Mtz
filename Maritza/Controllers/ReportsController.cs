using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MaritzaBusness;
using MaritzaData;
using MaritzaData.ConfigClasses;
using MaritzaData.DTO;
using MaritzaData.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paragraph = iTextSharp.text.Paragraph;

namespace Maritza.Controllers
{
    public class ReportsController : Controller
    {
        ProyectB ProyectB = new ProyectB();
        ReportsB ReportsB = new ReportsB();
        UsersB UsersB = new UsersB();
        RolesB RolesB =  new RolesB();

        public ActionResult ReportProyect()
        {
            return View("Proyects/ReportProyect");
        }
        [HttpPost]
        public ActionResult ReportProyect(fltProyects filters)
        {
            //EXCEL
            //ID
            //Name
            //Email
            //CreatedDate
            //Users created
            //Validated comments - number
            //Publish - number
            //Proyects added


            List<dtoUsers> model = UsersB.getList();

            XLWorkbook XLWorkBook = new XLWorkbook();
            string Name = "Reporte de usuarios";
            IXLWorksheet IXLWorksheet = XLWorkBook.AddWorksheet("Reporte de usuarios");

            int iRow = 1;
            int iColumn = 1;

            IXLWorksheet.SheetView.FreezeRows(iRow);

            IXLWorksheet.Cell(iRow, iColumn).SetValue("").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left).Font.Bold = true;
            IXLWorksheet.Cell(iRow, iColumn).Style.Fill.BackgroundColor = XLColor.FromHtml("#0F0F0F");
            IXLWorksheet.Cell(iRow, iColumn).Style.Font.FontColor = XLColor.White;
            iColumn++;
            IXLWorksheet.Cell(iRow, iColumn).SetValue("").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left).Font.Bold = true;
            IXLWorksheet.Cell(iRow, iColumn).Style.Fill.BackgroundColor = XLColor.FromHtml("#0F0F0F");
            IXLWorksheet.Cell(iRow, iColumn).Style.Font.FontColor = XLColor.White;
            iColumn++;
            IXLWorksheet.Cell(iRow, iColumn).SetValue("").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left).Font.Bold = true;
            IXLWorksheet.Cell(iRow, iColumn).Style.Fill.BackgroundColor = XLColor.FromHtml("#0F0F0F");
            IXLWorksheet.Cell(iRow, iColumn).Style.Font.FontColor = XLColor.White;
            iColumn++;
            IXLWorksheet.Cell(iRow, iColumn).SetValue("").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left).Font.Bold = true;
            IXLWorksheet.Cell(iRow, iColumn).Style.Fill.BackgroundColor = XLColor.FromHtml("#0F0F0F");
            IXLWorksheet.Cell(iRow, iColumn).Style.Font.FontColor = XLColor.White;
            iColumn++;
            IXLWorksheet.Cell(iRow, iColumn).SetValue("x").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left).Font.Bold = true;
            IXLWorksheet.Cell(iRow, iColumn).Style.Fill.BackgroundColor = XLColor.FromHtml("#0F0F0F");
            IXLWorksheet.Cell(iRow, iColumn).Style.Font.FontColor = XLColor.White;
            iColumn++;
            IXLWorksheet.Cell(iRow, iColumn).SetValue("x").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left).Font.Bold = true;
            IXLWorksheet.Cell(iRow, iColumn).Style.Fill.BackgroundColor = XLColor.FromHtml("#0F0F0F");
            IXLWorksheet.Cell(iRow, iColumn).Style.Font.FontColor = XLColor.White;
            iColumn++;
            IXLWorksheet.Cell(iRow, iColumn).SetValue("x").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left).Font.Bold = true;
            IXLWorksheet.Cell(iRow, iColumn).Style.Fill.BackgroundColor = XLColor.FromHtml("#0F0F0F");
            IXLWorksheet.Cell(iRow, iColumn).Style.Font.FontColor = XLColor.White;
            iColumn++;

            iRow++;
            if (model?.Any() == true)
            {
                foreach (dtoUsers item in model)
                {
                    iColumn = 1;

                    IXLWorksheet.Cell(iRow, iColumn).SetValue(item.UserID).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    iColumn++;
                    IXLWorksheet.Cell(iRow, iColumn).SetValue(item.UserName).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    iColumn++;
                    IXLWorksheet.Cell(iRow, iColumn).SetValue(item.Email).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    iColumn++;
                    IXLWorksheet.Cell(iRow, iColumn).SetValue(item.RolName).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    iColumn++;
                    IXLWorksheet.Cell(iRow, iColumn).SetValue(item.RolName).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    iColumn++;
                    IXLWorksheet.Cell(iRow, iColumn).SetValue(item.RolName).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    iColumn++;
                    IXLWorksheet.Cell(iRow, iColumn).SetValue(item.RolName).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    iColumn++;

                    iRow++;
                }
            }
            IXLWorksheet.Columns().AdjustToContents();

            MemoryStream memoryStream = new MemoryStream();
            XLWorkBook.SaveAs(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Name + ".xlsx");
        }
        [HttpPost]
        public ActionResult ReportProyectPDF(int id)
        {
            //PDF
            //Photo
            //title
            //Count publish
            //Number of likes and Number of dislikes
            ///Statics - a long the time
            //Number of people watcherd the proyect
            ///Statics - a long the time
            List<tblProyects> model = ProyectB.getNRandom(10);
            string strPath = Server.MapPath("~");
            strPath = strPath + $"\\Files\\Reports\\ReportProyect\\ProyectID_{id}";
            Directory.CreateDirectory(strPath);
            strPath = strPath + "\\";
            string strName = $"File_ProyectID_{id}_{Guid.NewGuid()}.pdf";
            iTextSharp.text.Document document = new iTextSharp.text.Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(strPath + strName, FileMode.Create));
            document.Open();
            #region BASE
            PdfContentByte content = writer.DirectContent;
            string paragraphText = String.Empty;
            Paragraph paragraph = new Paragraph();
            BaseFont bfNormal = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            BaseFont bfBold = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            BaseFont bfItalic = BaseFont.CreateFont(BaseFont.TIMES_BOLDITALIC, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            #endregion
            #region CONTENT PAGE
            content.BeginText();

            content.SetFontAndSize(bfBold, 30);
            content.SetColorFill(BaseColor.BLACK);
            content.ShowTextAligned(Element.ALIGN_LEFT, "Title ".ToUpper(), 60, document.PageSize.Height - 80, 0);

            content.SetFontAndSize(BaseFont.CreateFont(), 10);
            content.SetColorFill(BaseColor.BLACK);

            paragraphText = "paragraphText";
            ColumnText columnText = new ColumnText(content);
            columnText.SetSimpleColumn(new Rectangle(60, document.PageSize.Height - 105, document.PageSize.Width - 80, 20));
            paragraph = new Paragraph(new Phrase(paragraphText, FontFactory.GetFont(FontFactory.TIMES, 12f)));
            paragraph.Alignment = Element.ALIGN_JUSTIFIED;
            paragraph.SetLeading(0, 1f);
            columnText.AddElement(new Paragraph(paragraph));
            columnText.Go();

            #region table Sample
            PdfPTable table = new PdfPTable(5);

            paragraph = new Paragraph(new Phrase("Field", FontFactory.GetFont(FontFactory.TIMES_BOLD, 12f)));
            PdfPCell cell = new PdfPCell(new Phrase(paragraph));
            cell.Padding = 5;
            cell.BackgroundColor = BaseColor.BLUE;
            table.AddCell(cell);
            paragraph = new Paragraph(new Phrase("Field", FontFactory.GetFont(FontFactory.TIMES_BOLD, 12f)));
            cell = new PdfPCell(new Phrase(paragraph));
            cell.Padding = 5;
            cell.BackgroundColor = BaseColor.BLUE;
            table.AddCell(cell);
            paragraph = new Paragraph(new Phrase("Field", FontFactory.GetFont(FontFactory.TIMES_BOLD, 12f)));
            cell = new PdfPCell(new Phrase(paragraph));
            cell.Padding = 5;
            cell.BackgroundColor = BaseColor.BLUE;
            table.AddCell(cell);
            paragraph = new Paragraph(new Phrase("Field", FontFactory.GetFont(FontFactory.TIMES_BOLD, 12f)));
            cell = new PdfPCell(new Phrase(paragraph));
            cell.Padding = 5;
            cell.BackgroundColor = BaseColor.BLUE;
            table.AddCell(cell);
            paragraph = new Paragraph(new Phrase("Field", FontFactory.GetFont(FontFactory.TIMES_BOLD, 12f)));
            cell = new PdfPCell(new Phrase(paragraph));
            cell.Padding = 5;
            cell.BackgroundColor = BaseColor.BLUE;
            table.AddCell(cell);

            paragraph = new Paragraph(new Phrase("Field", FontFactory.GetFont(FontFactory.TIMES_BOLD, 12f)));
            cell = new PdfPCell(new Phrase(paragraph));
            cell.Padding = 5;
            table.AddCell(cell);
            paragraph = new Paragraph(new Phrase("Result", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12f)));
            cell = new PdfPCell(new Phrase(paragraph));
            cell.Padding = 5;
            table.AddCell(cell);
            paragraph = new Paragraph(new Phrase("Result", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12f)));
            cell = new PdfPCell(new Phrase(paragraph));
            cell.Padding = 5;
            table.AddCell(cell);
            paragraph = new Paragraph(new Phrase("Result", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12f)));
            cell = new PdfPCell(new Phrase(paragraph));
            cell.Padding = 5;
            table.AddCell(cell);
            paragraph = new Paragraph(new Phrase("Result", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12f)));
            cell = new PdfPCell(new Phrase(paragraph));
            cell.Padding = 5;
            table.AddCell(cell);

            table.TotalWidth = document.PageSize.Width - 120;
            table.LockedWidth = true;
            table.SetWidths(new float[] { 1, 1, 1, 1, 1 });
            table.WriteSelectedRows(0, -1, 60, document.PageSize.Height - 160, writer.DirectContent);

            #endregion


            paragraphText = "paragraphText";
            columnText = new ColumnText(content);
            columnText.SetSimpleColumn(new Rectangle(100, document.PageSize.Height - 205, document.PageSize.Width - 80, 20));
            paragraph = new Paragraph(new Phrase(paragraphText, FontFactory.GetFont(FontFactory.TIMES_BOLD, 12f)));
            columnText.AddElement(new Paragraph(paragraph));
            columnText.Go();


            //Image img = Image.GetInstance("");

            content.EndText();
            #endregion
            document.Close();
            #region Result
            string fullPath = Path.Combine(strPath, strName);

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + strName);
            Response.TransmitFile(fullPath);
            Response.End();
            #endregion
            return View();
        }
        public ActionResult ReportUsers(fltUsers filters=null)
        {
            ViewBag.Filter = filters;
            ViewBag.lstRoles = RolesB.GetList();
            var lstTopicss = ReportsB.GetAllUsersRpt(filters);
            if (Request.IsAjaxRequest())
                return PartialView("Users/_ReportUsers", lstTopicss);
            return View("Users/ReportUsers", lstTopicss);
        }
        [HttpPost]
        public ActionResult ReportUsers(fltUsers filters, int x=2)
        {
            //EXCEL
            //ID
            //Name
            //Email
            //CreatedDate
            //Users created
            //Validated comments - number
            //Publish - number
            //Proyects added


            List<dtoUsers> model = UsersB.getList();

            XLWorkbook XLWorkBook = new XLWorkbook();
            string Name = "Reporte de usuarios";
            IXLWorksheet IXLWorksheet = XLWorkBook.AddWorksheet("Reporte de usuarios");

            int iRow = 1;
            int iColumn = 1;

            IXLWorksheet.SheetView.FreezeRows(iRow);

            IXLWorksheet.Cell(iRow, iColumn).SetValue("UserID").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left).Font.Bold = true;
            IXLWorksheet.Cell(iRow, iColumn).Style.Fill.BackgroundColor = XLColor.FromHtml("#0F0F0F");
            IXLWorksheet.Cell(iRow, iColumn).Style.Font.FontColor = XLColor.White;
            iColumn++;
            IXLWorksheet.Cell(iRow, iColumn).SetValue("UserName").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left).Font.Bold = true;
            IXLWorksheet.Cell(iRow, iColumn).Style.Fill.BackgroundColor = XLColor.FromHtml("#0F0F0F");
            IXLWorksheet.Cell(iRow, iColumn).Style.Font.FontColor = XLColor.White;
            iColumn++;
            IXLWorksheet.Cell(iRow, iColumn).SetValue("Email").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left).Font.Bold = true;
            IXLWorksheet.Cell(iRow, iColumn).Style.Fill.BackgroundColor = XLColor.FromHtml("#0F0F0F");
            IXLWorksheet.Cell(iRow, iColumn).Style.Font.FontColor = XLColor.White;
            iColumn++;
            IXLWorksheet.Cell(iRow, iColumn).SetValue("RolName").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left).Font.Bold = true;
            IXLWorksheet.Cell(iRow, iColumn).Style.Fill.BackgroundColor = XLColor.FromHtml("#0F0F0F");
            IXLWorksheet.Cell(iRow, iColumn).Style.Font.FontColor = XLColor.White;
            iColumn++;
            IXLWorksheet.Cell(iRow, iColumn).SetValue("x").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left).Font.Bold = true;
            IXLWorksheet.Cell(iRow, iColumn).Style.Fill.BackgroundColor = XLColor.FromHtml("#0F0F0F");
            IXLWorksheet.Cell(iRow, iColumn).Style.Font.FontColor = XLColor.White;
            iColumn++;
            IXLWorksheet.Cell(iRow, iColumn).SetValue("x").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left).Font.Bold = true;
            IXLWorksheet.Cell(iRow, iColumn).Style.Fill.BackgroundColor = XLColor.FromHtml("#0F0F0F");
            IXLWorksheet.Cell(iRow, iColumn).Style.Font.FontColor = XLColor.White;
            iColumn++;
            IXLWorksheet.Cell(iRow, iColumn).SetValue("x").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left).Font.Bold = true;
            IXLWorksheet.Cell(iRow, iColumn).Style.Fill.BackgroundColor = XLColor.FromHtml("#0F0F0F");
            IXLWorksheet.Cell(iRow, iColumn).Style.Font.FontColor = XLColor.White;
            iColumn++;

            iRow++;
            if (model?.Any() == true)
            {
                foreach (dtoUsers item in model)
                {
                    iColumn = 1;

                    IXLWorksheet.Cell(iRow, iColumn).SetValue(item.UserID).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    iColumn++;
                    IXLWorksheet.Cell(iRow, iColumn).SetValue(item.UserName).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    iColumn++;
                    IXLWorksheet.Cell(iRow, iColumn).SetValue(item.Email).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    iColumn++;
                    IXLWorksheet.Cell(iRow, iColumn).SetValue(item.RolName).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    iColumn++;
                    IXLWorksheet.Cell(iRow, iColumn).SetValue(item.RolName).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    iColumn++;
                    IXLWorksheet.Cell(iRow, iColumn).SetValue(item.RolName).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    iColumn++;
                    IXLWorksheet.Cell(iRow, iColumn).SetValue(item.RolName).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    iColumn++;

                    iRow++;
                }
            }
            IXLWorksheet.Columns().AdjustToContents();

            MemoryStream memoryStream = new MemoryStream();
            XLWorkBook.SaveAs(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Name + ".xlsx");
        }
        public ActionResult ProyectBadget()
        {
            //EXCEL
            //title proyect
            //badget
            //Gender
            //--show total
            return View();
        }

   

      
    }
}
