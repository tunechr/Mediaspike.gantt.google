using Mediaspike.Gantt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mediaspike.Gantt.Google.Controllers.Gantt
{
    public class GanttDependenciesController : ApiController
    {
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
