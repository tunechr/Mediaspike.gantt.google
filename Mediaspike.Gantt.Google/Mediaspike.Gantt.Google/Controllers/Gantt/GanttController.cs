using Google.Apis.Auth.OAuth2.Mvc;
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

namespace Mediaspike.Gantt.Google.Controllers.Gantt
{
    public class GanttController : ApiController
    {
        // GET: api/Gantt
        public async Task<IEnumerable<GanttTaskModel>> Get()
        {
            //CancellationToken cancellationToken;
            //var result = await new AuthorizationCodeMvcApp(this, new AppFlowMetaData()).
            //  AuthorizeAsync(cancellationToken);

            //if (result.Credential != null)
            //{
            //}

            //get all the task for the current user, 
            var l = new List<GanttTaskModel>();

            for (int i = 0; i < 10; i++)
            {
                var t = new GanttTaskModel()
                {
                    ID = i,
                    End = DateTime.Now.AddDays(5),
                    Expanded = true,
                    OrderID = 1,
                    ParentID = null,
                    PercentComplete = 0,
                    Start = DateTime.Now,
                    Summary = false,
                    Title = "Test Task" + i.ToString()

                };
                l.Add(t);
            }


            return l;
        }

        // GET: api/Gantt/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Gantt
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Gantt/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Gantt/5
        public void Delete(int id)
        {
        }
    }
}
