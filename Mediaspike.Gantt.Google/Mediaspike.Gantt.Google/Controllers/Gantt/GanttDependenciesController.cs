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
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace Mediaspike.Gantt.Google.Controllers.Gantt
{
    public class GanttDependenciesController : Controller
    {

        public async Task<JsonResult> All()

        {
            UserCredential credential;
            CancellationToken cancellationToken;
            //get all the task for the current user, 
            var l = new List<GanttDependencyModel>();

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

                // Define request parameters.
                String spreadsheetId = "1ea3HupjO8snbOxzHIW2mDVT-pX4GsRE5lnJZXHSv4sA";
                String range = "Sheet1!A:F";

                SpreadsheetsResource.ValuesResource.GetRequest request =
                        service.Spreadsheets.Values.Get(spreadsheetId, range);
                                
                ValueRange response = request.Execute();
                IList<IList<Object>> values = response.Values;
                if (values != null && values.Count > 0)
                {
                    for (int i = 1; i < values.Count - 1; i++)
                    {
                        var row = values[i];

                        // Print columns A and E, which correspond to indices 0 and 4.                        
                        var t = new GanttDependencyModel()
                        {
                            ID = Convert.ToInt32(row[0]),
                            SuccessorID= Convert.ToInt32(row[0]),
                            PredecessorID = Convert.ToInt32(row[5]),
                            Type = 1
                        };
                        l.Add(t);
                    }
                }
                else
                {
                    //   Console.WriteLine("No data found.");
                }
                
            }

            return Json(l, JsonRequestBehavior.AllowGet);

        }

        // GET: api/GanttDependencies
        public IEnumerable<GanttDependencyModel> Get()
        {
           // return null; //get all the task for the current user, 
            var l = new List<GanttDependencyModel>();

            for (int i = 0; i < 10; i++)
            {
                var t = new GanttDependencyModel()
                {
                    ID = i,
                    PredecessorID = -1,
                    SuccessorID = -1,
                     Type=1
                    


                };
                l.Add(t);
            }


            return l;
        }

        // GET: api/GanttDependencies/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GanttDependencies
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/GanttDependencies/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GanttDependencies/5
        public void Delete(int id)
        {
        }
    }
}
