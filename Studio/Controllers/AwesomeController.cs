using System.Collections.Generic;
using System.Web.Http;

namespace MightyFX.Studio.Controllers
{
    public class AwesomeController : ApiController
    {
        // GET: api/Awesome
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Awesome/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Awesome
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Awesome/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Awesome/5
        public void Delete(int id)
        {
        }
    }
}
