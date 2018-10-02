using System.Collections.Generic;
using System.Linq;
using DSProject.Functions;
using DSProject.Interface;
using DSProject.Model;
using Microsoft.AspNetCore.Mvc;

namespace DSProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FunctionController : ControllerBase
    {
        // GET: api/Function
        [HttpGet]
        public IEnumerable<IFunction> Get()
        {
            return null;
        }

        // GET: api/Function/5
        [HttpGet("{value}", Name = "Get")]
        public IEnumerable<IFunction> Get(string value)
        {
            return new GeneralFunction().GetResults( value );
        }

        // POST: api/Function
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Function/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
