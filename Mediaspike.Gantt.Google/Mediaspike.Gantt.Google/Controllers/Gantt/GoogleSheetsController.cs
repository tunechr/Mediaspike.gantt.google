using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Authentication.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mediaspike.Gantt.Google.Controllers.Gantt
{
    public class GoogleSheetsController : Controller
    {
        public string ApplicationName { get; private set; }

        // GET: GoogleSheets
        public async Task<JsonResult> Sheets()

        {
            UserCredential credential;
            CancellationToken cancellationToken;
            //get all the task for the current user, 

            //list of sheets
            var l = new List<string>();

            var result = await new AuthorizationCodeMvcApp(this, new AppFlowMetaData()).
              AuthorizeAsync(cancellationToken);

            if (result.Credential != null)
            {

                credential = result.Credential;

                // Create Google Sheets API service.
                var service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "",
                });

                SpreadsheetsResource.ValuesResource vres = service.Spreadsheets.Values;

                //

            }
            else
            {
                //   Console.WriteLine("No data found.");
            }
            // Console.Read();

            return Json("", JsonRequestBehavior.AllowGet);

        }

        // GET: GoogleSheets/Details/5
        public async Task<JsonResult> Sheet(string id, string range)
        {
            UserCredential credential;
            CancellationToken cancellationToken;
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            var result = await new AuthorizationCodeMvcApp(this, new AppFlowMetaData()).
              AuthorizeAsync(cancellationToken);

            if (result.Credential != null)
            {

                credential = result.Credential;

                // Create Google Sheets API service.
                var service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                // Define request parameters.
                String spreadsheetId = id;// "1ea3HupjO8snbOxzHIW2mDVT-pX4GsRE5lnJZXHSv4sA";
                                          // String range = "Sheet1!A:I";

                SpreadsheetsResource.ValuesResource.GetRequest request =
                        service.Spreadsheets.Values.Get(spreadsheetId, range);

                // Prints the names and majors of students in a sample spreadsheet:
                // https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit
                ValueRange response = request.Execute();
                IList<IList<Object>> values = response.Values;
                var headerRow = response.Values[0];
                //First Row is Headers

              

                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    writer.Formatting = Formatting.Indented;
                    writer.WriteStartObject();

                    writer.WritePropertyName("Drives");
                    writer.WriteStartArray();

                    for (int i = 1; i < response.Values.Count; i++)
                    {
                        writer.WriteStartObject();
                        for (int n = 0; n < headerRow.Count; n++)
                        {
                            writer.WritePropertyName(headerRow[n].ToString());
                            string v = response.Values[i][n].ToString();
                            writer.WriteValue(v);
                        }
                        writer.WriteEndObject();
                    }
                    

                    writer.WriteEnd();

                    // writer.WriteEnd();
                    writer.WriteEndObject();
                }

                //Console.WriteLine(sb.ToString());


                if (values != null && values.Count > 0)
                {

                    //// foreach (var row in values)
                    //for (int i = 1; i < values.Count - 1; i++)
                    //{
                    //    var row = values[i];


                    //    // Print columns A and E, which correspond to indices 0 and 4.
                    //    Console.WriteLine("{0}, {1}", row[0], row[1]);
                    //    var t = new GanttTaskModel()
                    //    {
                    //        ID = Convert.ToInt32(row[0]),
                    //        End = DateTime.Parse(row[3].ToString()),
                    //        Expanded = true,// Convert.ToBoolean(row[6]),
                    //        OrderID = 1,
                    //        ParentID = Convert.ToInt32(row[7]),
                    //        PercentComplete = Convert.ToDecimal(row[4]),
                    //        Start = DateTime.Parse(row[2].ToString()),
                    //        Summary = Convert.ToBoolean(row[6]),
                    //        Title = row[1].ToString()

                    //    };
                    //    l.Add(t);
                    //}
                }
                else
                {
                    //   Console.WriteLine("No data found.");
                }
                // Console.Read();
            }





            return Json(sb.ToString(), JsonRequestBehavior.AllowGet);
        }

        // GET: GoogleSheets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GoogleSheets/Create
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

        // GET: GoogleSheets/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GoogleSheets/Edit/5
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

        // GET: GoogleSheets/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GoogleSheets/Delete/5
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
