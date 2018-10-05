using System.Collections.Generic;
using DSProject.Model;
using Microsoft.AspNetCore.Mvc;

namespace DSProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntegrantController : ControllerBase
    {
        #region [Attributes]

        private DSBaseContext _context;
         
        #endregion

        #region [Constructor]

        /// <summary>
        /// Construtor que receber o context por DI
        /// </summary>
        public IntegrantController(DSBaseContext context)
        {
            _context = context;
        }

        #endregion

        #region [Methods]

        // GET: api/Integrant
        [HttpGet]
        public IEnumerable<Integrant> Get()
        {
            return _context.Integrants;
        }

        #endregion
    }
}