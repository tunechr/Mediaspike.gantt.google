using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Authentication.Controllers;
using Mediaspike.Gantt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mediaspike.Gantt.Google.Controllers.Gantt
{
    public class Gantt1Controller : Controller
    {
        static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static string ApplicationName = "Google Sheets API .NET Quickstart";

        // GET: Gantt1
        //   public async Task<IEnumerable<GanttTaskModel>> All()
        [HttpGet]
        public async Task<JsonResult> All() 
             
        {
            UserCredential credential;
            CancellationToken cancellationToken;
            //get all the task for the current user, 
            var l = new List<GanttTaskModel>();

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
                String spreadsheetId = "1ea3HupjO8snbOxzHIW2mDVT-pX4GsRE5lnJZXHSv4sA";
                String range = "Sheet1!A2:F5";

                SpreadsheetsResource.ValuesResource.GetRequest request =
                        service.Spreadsheets.Values.Get(spreadsheetId, range);

                // Prints the names and majors of students in a sample spreadsheet:
                // https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit
                ValueRange response = request.Execute();
                IList<IList<Object>> values = response.Values;
                if (values != null && values.Count > 0)
                {
                                    
                    foreach (var row in values)
                    {
                        // Print columns A and E, which correspond to indices 0 and 4.
                        Console.WriteLine("{0}, {1}", row[0], row[1]);
                        var t = new GanttTaskModel()
                        {
                            ID =Convert.ToInt32(row[0]) ,
                            End = DateTime.Parse(row[3].ToString()),
                            Expanded = true,
                            OrderID = 1,
                            ParentID = null,
                            PercentComplete = Convert.ToDecimal(row[4]) ,
                            Start = DateTime.Parse(row[2].ToString()),
                            Summary = false,
                            Title = row[1].ToString()

                        };
                        l.Add(t);
                    }
                }
                else
                {
                 //   Console.WriteLine("No data found.");
                }
               // Console.Read();
            }

            
           
 

            return Json(l, JsonRequestBehavior.AllowGet);
             
        }
    }
}