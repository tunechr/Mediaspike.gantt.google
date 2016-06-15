using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Authentication.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mediaspike.Gantt.Google.Controllers.Gantt
{
    public class GoogleSheetsController : Controller
    {
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

                SpreadsheetsResource.ValuesResource vres= service.Spreadsheets.Values;

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
        public ActionResult Details(int id)
        {
            return View();
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
