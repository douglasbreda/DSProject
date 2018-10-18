using System;
using System.Collections.Generic;
using System.Linq;
using DSProject.Model;
using DSProject.Util;
using Microsoft.AspNetCore.Mvc;
using static DSProject.Util.Enums;

namespace DSProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        #region [Attributes]

        private DSBaseContext _context;

        #endregion

        #region [Constructor]

        /// <summary>
        /// Construtor que recebe o contexto do banco via injeção de dependência
        /// </summary>
        public ConfigController(DSBaseContext context)
        {
            _context = context;
        }

        #endregion

        #region [HTTP Methods]

        // GET: api/Function
        [HttpPost]
        public ActionResult Config()
        {
            return Ok(ApplyMask());
        }

        #endregion

        #region [Methods]

        /// <summary>
        /// Ajusta as máscaras de telefones e celulares de todos os integrantes
        /// </summary>
        private string ApplyMask()
        {
            string _message = "Ajuste das máscaras finalizado.";
            try
            {
                List<Integrant> _integrants = _context.Integrants.ToList();

                foreach (Integrant integrant in _integrants)
                {
                    if (Utils.RemoveMask(integrant.Phone).Trim().Length >= 10)
                        integrant.Phone = Utils.PutPhoneMask(Utils.RemoveMask(integrant.Phone), eMaskType.phoneWithDDD, true);
                    else if (integrant.Phone.Trim().Length == 8)
                        integrant.Phone = Utils.PutPhoneMask(Utils.RemoveMask(integrant.Phone), eMaskType.phoneWithoutDDD);

                    if (Utils.RemoveMask(integrant.CellPhone).Length == 10)
                        integrant.CellPhone = Utils.PutPhoneMask(Utils.RemoveMask(integrant.CellPhone), eMaskType.cellPhoneWithDDD, false);
                    else if (Utils.RemoveMask(integrant.CellPhone).Length == 11)
                        integrant.CellPhone = Utils.PutPhoneMask(Utils.RemoveMask(integrant.CellPhone), eMaskType.cellPhoneWithDDD, true);
                    else if (Utils.RemoveMask(integrant.CellPhone).Length == 8)
                        integrant.CellPhone = Utils.PutPhoneMask(Utils.RemoveMask(integrant.CellPhone), eMaskType.cellPhoneWithDDD);
                    else if (Utils.RemoveMask(integrant.CellPhone).Length == 9)
                        integrant.CellPhone = Utils.PutPhoneMask(Utils.RemoveMask(integrant.CellPhone), eMaskType.cellPhoneWithDDD, true);

                }

                _context.UpdateRange(_integrants);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    _message = ex.InnerException.ToString();
                else
                    _message = ex.Message;
            }

            return _message;
        }

        #endregion

    }
}