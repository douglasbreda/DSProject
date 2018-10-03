using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DSProject.Interface;
using DSProject.Model;
using DSProject.Util;
using Microsoft.AspNetCore.Mvc;
using static DSProject.Util.Enums;

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
            Cnpj();
            DataCheck();

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
        private void AddList(string description, object result, bool isMatch, string pattern = "", int size = 0, string link = "")
        {
            _lstFunctions.Add(new Function
            {
                Description = description,
                Result = result,
                IsMatch = true,
                Pattern = pattern,
                Size = size,
                Link = link
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

        /// <summary>
        /// Validação e verificação de integrantes com possível CPF
        /// </summary>
        private void Cpf()
        {
            _value = Utils.GetOnlyNumbers(_value);

            if (_value.Length < 11 || _value.Length > 11)
                AddList("CPF", $"O valor {_value} possui {_value.Length} números. CPFs contém 11 números. ", false, Utils.GetMask(eMaskType.cpf), 0);
            else
            {
                Integrant integrant = _context.Integrants.Where(x => x.CPF.Equals(Utils.PutCpfMask(_value))).FirstOrDefault();

                if (integrant != null)
                    AddList("CPF", $"O integrante da Dark Side {integrant.Name} possui este CPF.", true, Utils.GetMask(eMaskType.cpf), 11);
                else
                    AddList("CPF", $"Pode ser que o valor seja um CPF: {Utils.PutCpfMask(_value)}", true, Utils.GetMask(eMaskType.cpf), 11);
            }
        }

        /// <summary>
        /// Verificação de CNPJ
        /// </summary>
        private void Cnpj()
        {

            _value = Utils.GetOnlyNumbers(_value);

            if (_value.Length < 14 || _value.Length > 14)
                AddList("CNPJ", $"O valor {_value} possui {_value.Length} números. CNPJs contém 14 números.", false, Utils.GetMask(eMaskType.cnpj));
            else
            {
                AddList("CNPJ", $"Pode ser o valor seja um CNPJ: {Utils.PutCnpjMask(_value)}.", true, Utils.GetMask(eMaskType.cnpj), 14, "http://www.receita.fazenda.gov.br/pessoajuridica/cnpj/cnpjreva/cnpjreva_solicitacao.asp");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void DataCheck()
        {
            _value = Utils.GetOnlyNumbers(_value);

            if (_value.Length < 8 || _value.Length > 8)
                AddList("Data", $"Número com {_value.Length} dígitos. Uma data contém 8 dígitos.", false, Utils.GetMask(eMaskType.data));
            else
            {
                int year = _value.Substring(4, 4).ToInt32();
                int month = _value.Substring(2, 2).ToInt32();
                int day = _value.Substring(0, 2).ToInt32();

                try
                {
                    DateTime dt = new DateTime(year, month, day);

                    if (dt != new DateTime())
                    {
                        CultureInfo cult = new CultureInfo("pt-BR");
                        string dtFormated = dt.ToString("dd/MM/yyyy", cult);
                        AddList("Data", $"Pode ser que o valor seja uma data: {dtFormated}", true, Utils.GetMask(eMaskType.data), 8);
                    }
                }
                catch
                {
                    AddList("Data", $"Não é uma data válida", false, Utils.GetMask(eMaskType.data), 8);
                }
            }
        }

        #endregion
    }
}
