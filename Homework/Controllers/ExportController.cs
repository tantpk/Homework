using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Homework.Controllers
{
    public class ExportController : Controller
    {        
        public ActionResult ExportToExcel(List<List<string>> data)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("ClientData");
                // Write DataTable headers to the worksheet
                for (int i = 0; i < data[0].Count; i++)
                {
                    worksheet.Cell(1, i + 1).Value = data[0][i].Trim();
                }
                // Write array data to the worksheet
                for (int i = 0; i < data.Count; i++)
                {
                    for (int j = 0; j < data[i].Count; j++)
                    {
                        worksheet.Cell(i + 1, j + 1).Value = data[i][j].Trim();
                    }
                }
                var firstRow = worksheet.Row(1);
                firstRow.Style.Font.Bold = true;
                firstRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Columns().AdjustToContents();
                worksheet.Rows().AdjustToContents();
                // Prepare the response
                var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Position = 0;

                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                return File(stream, contentType);
            }
        }        
    }
}