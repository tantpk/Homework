using ClosedXML.Excel;
using Homework.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Homework.Controllers
{
    public class ExportController : Controller
    {
        客戶資料Repository repo;
        客戶聯絡人Repository repoContact;
        客戶銀行資訊Repository repoBankData;
        public ExportController()
        {
            repo = RepositoryHelper.Get客戶資料Repository();
            repoContact = RepositoryHelper.Get客戶聯絡人Repository(repo.UnitOfWork);
            repoBankData = RepositoryHelper.Get客戶銀行資訊Repository(repo.UnitOfWork);
        }
        public ActionResult ExportClientDataToExcel(string clientType)
        {
            using (var workbook = new XLWorkbook())
            {
                var clientData = repo.All().ToList();
                // Check filter parms
                if (clientType != string.Empty && clientType != null)
                {
                    clientData = repo.FilterByClientType(clientType).ToList();
                }
                var worksheet = workbook.Worksheets.Add("ClientData");
                // Write DataTable headers to the worksheet
                DataTable clientDataTable = ToDataTable(clientData);
                for (int i = 0; i < clientDataTable.Columns.Count; i++)
                {
                    worksheet.Cell(1, i + 1).Value = clientDataTable.Columns[i].ColumnName.Trim();
                }
                // Write DataTable data to the worksheet
                for (int i = 0; i < clientDataTable.Rows.Count; i++)
                {
                    for (int j = 0; j < clientDataTable.Columns.Count; j++)
                    {
                        worksheet.Cell(i + 2, j + 1).Value = clientDataTable.Rows[i][j].ToString().Trim();
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
                var fileName = "ClientData " + DateTime.UtcNow.ToShortDateString() + ".xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                return File(stream, contentType, fileName);
            }
        }
        public ActionResult ExportClientContactsToExcel(string clientTitle)
        {
            using (var workbook = new XLWorkbook())
            {
                var clientContact = repoContact.All().ToList();
                // Check filter parms
                if (clientTitle != string.Empty && clientTitle != null)
                {
                    clientContact = repoContact.FilterByClientTitle(clientTitle).ToList();
                }
                var worksheet = workbook.Worksheets.Add("ClientData");
                // Write DataTable headers to the worksheet
                DataTable clientContactsTable = ToDataTable(clientContact);
                for (int i = 0; i < clientContactsTable.Columns.Count; i++)
                {
                    worksheet.Cell(1, i + 1).Value = clientContactsTable.Columns[i].ColumnName.Trim();
                }
                // Write DataTable data to the worksheet
                for (int i = 0; i < clientContactsTable.Rows.Count; i++)
                {
                    for (int j = 0; j < clientContactsTable.Columns.Count; j++)
                    {
                        worksheet.Cell(i + 2, j + 1).Value = clientContactsTable.Rows[i][j].ToString().Trim();
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
                var fileName = "ClientContacts " + DateTime.UtcNow.ToShortDateString() + ".xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                return File(stream, contentType, fileName);
            }
        }
        public ActionResult ExportBankDataToExcel()
        {
            using (var workbook = new XLWorkbook())
            {
                var clientData = repo.All().ToList();

                var worksheet = workbook.Worksheets.Add("ClientData");
                // Write DataTable headers to the worksheet
                DataTable clientBankDataTable = ToDataTable(clientData);
                for (int i = 0; i < clientBankDataTable.Columns.Count; i++)
                {
                    worksheet.Cell(1, i + 1).Value = clientBankDataTable.Columns[i].ColumnName.Trim();
                }
                // Write DataTable data to the worksheet
                for (int i = 0; i < clientBankDataTable.Rows.Count; i++)
                {
                    for (int j = 0; j < clientBankDataTable.Columns.Count; j++)
                    {
                        worksheet.Cell(i + 2, j + 1).Value = clientBankDataTable.Rows[i][j].ToString().Trim();
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
                var fileName = "ClientBankData " + DateTime.UtcNow.ToShortDateString() + ".xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                return File(stream, contentType, fileName);
            }
        }
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            // Excluding System.Collections.Generic Namespace
            Props = (PropertyInfo[]) Props.Where(p => p.PropertyType.Namespace != "System.Collections.Generic" && p.PropertyType.Namespace != "Homework.Models").ToArray();

            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);                
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);             
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }
}