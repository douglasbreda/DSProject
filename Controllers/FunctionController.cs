using System.Collections.Generic;
using System.Linq;
using DSProject.Interface;
using DSProject.Model;
using DSProject.Util;
using Microsoft.AspNetCore.Mvc;

namespace DSProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FunctionController : ControllerBase
    {
        #region [Attributes]

        private List<Function> _lstFunctions = new List<Function>();

        private string _value = string.Empty;

        private DSBaseContext _context;

        #endregion

        #region [Constructor]

        /// <summary>
        /// Construtor que recebe o contexto do banco via injeção de dependência
        /// </summary>
        public FunctionController(DSBaseContext context)
        {
            _context = context;
        }

        #endregion

        #region [HTTP Methods]

        // GET: api/Function
        [HttpGet]
        public string Get()
        {
            return "Chegou aqui";
        }

        // GET: api/Function/5
        [HttpGet("{value}", Name = "GetFunction")]
        // public IEnumerable<IFunction> Get(string value)
        public IActionResult Get(string value)
        {
            _value = value;
            return new ObjectResult(GetResults());
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

        #endregion

        #region [Functions]

        /// <summary>
        /// Monta todos os resultados e retorna uma lista 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public List<Function> GetResults()
        {
            OnlyNumber();
            OnlyCharacter();
            Cpf();

            return _lstFunctions;
        }

        /// <summary>
        /// Adiciona um objeto na lista conforme os parâmetros
        /// </summary>
        /// <param name="description"></param>
        /// <param name="result"></param>
        /// <param name="isMatch"></param>
        /// <param name="pattern"></param>
        /// <param name="size"></param>
        private void AddList(string description, object result, bool isMatch, string pattern = "", int size = 0)
        {
            _lstFunctions.Add(new Function
            {
                Description = description,
                Result = result,
                IsMatch = true,
                Pattern = pattern,
                Size = size
            });

        }

        /// <summary>
        /// Verifica se a string possui somente números
        /// </summary>
        /// <param name="value"></param>
        private void OnlyNumber()
        {
            AddList("Somente Números", _value.Count(x => char.IsDigit(x)), true);
        }

        /// <summary>
        /// Verifica somente os não numéricos
        /// </summary>
        /// <param name="value"></param>
        private void OnlyCharacter()
        {
            AddList("Somente letras", _value.Count(x => !char.IsDigit(x)), true);
        }

        private void Cpf()
        {
            _value = Utils.GetOnlyNumbers(_value);
            
            if (_value.Length < 11 || _value.Length > 11)
                AddList("CPF", $"O valor {_value} possui {_value.Length} números. CPFs contém 11 números. ", false, "000.000.000-00", 0);
            else
            {
                Integrant integrant = _context.Integrants.Where(x => x.CPF.Equals(Utils.PutCpfMask(_value))).FirstOrDefault();

                if (integrant != null)
                    AddList("CPF", $"O integrante da Dark Side {integrant.Name} possui este CPF.", true, "000.000.000-00", 11);
                else
                    AddList("CPF", $"Pode ser que o valor seja um CPF: {Utils.PutCpfMask(_value)}", true, "000.000.000-00", 11);
            }
        }

        #endregion
    }
}
