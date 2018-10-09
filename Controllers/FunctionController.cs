using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            Phone();
            CellPhone();
            CheckColor();
            LetterToNumber();
            NumberToLetter();

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
        /// <param name="link"></param>
        /// <param name="html"></param>
        private void AddList(string description, object result, bool isMatch, string pattern = "", int size = 0, string link = "", string html = "")
        {
            _lstFunctions.Add(new Function
            {
                Description = description,
                Result = result,
                IsMatch = isMatch,
                Pattern = pattern,
                Size = size,
                Link = link,
                Html = html
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
            string _cpf = Utils.GetOnlyNumbers(_value);

            if (_cpf.Length < 11 || _cpf.Length > 11)
                AddList("CPF", $"O valor {_cpf} possui {_cpf.Length} números. CPFs contém 11 números. ", false, Utils.GetMask(eMaskType.cpf), 0);
            else
            {
                Integrant integrant = _context.Integrants.Where(x => x.CPF.Equals(Utils.PutCpfMask(_cpf))).FirstOrDefault();

                if (integrant != null)
                    AddList("CPF", $"O integrante da Dark Side {integrant.Name} possui este CPF.", true, Utils.GetMask(eMaskType.cpf), 11);
                else
                    AddList("CPF", $"Pode ser que o valor seja um CPF: {Utils.PutCpfMask(_cpf)}", true, Utils.GetMask(eMaskType.cpf), 11);
            }
        }

        /// <summary>
        /// Verificação de CNPJ
        /// </summary>
        private void Cnpj()
        {
            string _cnpj = Utils.GetOnlyNumbers(_value);

            if (_cnpj.Length < 14 || _cnpj.Length > 14)
                AddList("CNPJ", $"O valor {_cnpj} possui {_cnpj.Length} números. CNPJs contém 14 números.", false, Utils.GetMask(eMaskType.cnpj));
            else
            {
                AddList("CNPJ", $"Pode ser o valor seja um CNPJ: {Utils.PutCnpjMask(_cnpj)}.", true, Utils.GetMask(eMaskType.cnpj), 14, "http://www.receita.fazenda.gov.br/pessoajuridica/cnpj/cnpjreva/cnpjreva_solicitacao.asp");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void DataCheck()
        {
            string _date = Utils.GetOnlyNumbers(_value);

            if (_date.Length < 8 || _date.Length > 8)
                AddList("Data", $"Número com {_date.Length} dígitos. Uma data contém 8 dígitos.", false, Utils.GetMask(eMaskType.data));
            else
            {
                int year = _date.Substring(4, 4).ToInt32();
                int month = _date.Substring(2, 2).ToInt32();
                int day = _date.Substring(0, 2).ToInt32();

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

        /// <summary>
        /// Validação para telefones
        /// </summary>
        private void Phone()
        {
            string phone = Utils.GetOnlyNumbers(_value);

            if (phone.Length == 8)
            {
                string formatedPhone = phone.Insert(4, "-");
                AddList("Telefone", $"Pode ser que o valor seja um número de telefone {formatedPhone}", true, Utils.GetMask(eMaskType.phoneWithoutDDD), 8);
            }
            else if (phone.Length == 10)
            {
                string formatedPhone = Utils.PutPhoneMask(phone, eMaskType.phoneWithDDD);
                AddList("Telefone", $"Pode ser que o valor seja um número de telefone {formatedPhone}", true, Utils.GetMask(eMaskType.phoneWithoutDDD), 10);
            }
            else if (phone.Length < 8)
            {
                AddList("Telefone", $"Não é uma número de telefone válido pois contém menos de 8 números", false, "", 0);
            }
            else if (phone.Length > 10)
                AddList("Telefone", $"Não é uma número de telefone válido pois contém mais de 10 números", false, "", 0);

        }

        /// <summary>
        /// Validação para números de telefone
        /// </summary>
        private void CellPhone()
        {
            string cellPhone = Utils.GetOnlyNumbers(_value);

            if (cellPhone.Length == 9)
            {
                string formatedPhone = cellPhone.Insert(5, "-");
                AddList("Celular", $"Pode ser que o valor seja um número de celular {formatedPhone}", true, Utils.GetMask(eMaskType.cellPhoneWithoutDDD), 9);
            }
            else if (cellPhone.Length == 11)
            {
                string formatedPhone = Utils.PutPhoneMask(cellPhone, eMaskType.cellPhoneWithDDD);
                AddList("Celular", $"Pode ser que o valor seja um número de celular {formatedPhone}", true, Utils.GetMask(eMaskType.cellPhoneWithoutDDD), 11);
            }
            else if (cellPhone.Length < 9)
            {
                AddList("Celular", $"Não é uma número de celular válido pois contém menos de 9 números", false, "", 0);
            }
            else if (cellPhone.Length > 11)
                AddList("Celular", $"Não é uma número de celular válido pois contém mais de 11 números", false, "", 0);
        }

        /// <summary>
        /// Verifica se um sequencia pode ser um hexadecimal de cores
        /// </summary>
        private void CheckColor()
        {
            string _color = _value;
            if (!_color.StartsWith("#"))
                _color = $"#{_value}";

            Regex regex = new Regex("^#(?:[0-9a-fA-F]{3}){1,2}$");

            if (regex.Match(_color).Success)
            {
                AddList("Cor", $"Pode ser que a sequência seja uma cor: {_color}", true, "", 0, "", "<div class=\"boxColor\" style=\"background: #13b4ff\"></div>");
            }
        }

        /// <summary>
        /// Conversão de letras para números
        /// </summary>
        private void LetterToNumber()
        {
            string _onlyLetters = Utils.GetOnlyCharacteres(_value).Trim();

            if (!string.IsNullOrEmpty(_onlyLetters))
            {
                string _valueWithouSpaces = _value.Trim();

                string _AZOnlyLetter = Utils.GetNumbersFromLetterAZ(_onlyLetters, false);
                string _AZNumbersLetter = Utils.GetNumbersFromLetterAZ(_valueWithouSpaces.Trim(), false);
                string _ZAOnlyLetter = Utils.GetNumbersFromLetterZA(_onlyLetters, false);
                string _ZANumbersLetter = Utils.GetNumbersFromLetterZA(_valueWithouSpaces.Trim(), false);


                if (string.IsNullOrEmpty(_AZOnlyLetter))
                    AddList("Letras para números (A -> Z) (Não considera os números na palavra)", " - ", false, "", 0, "", "");
                else
                    AddList("Letras para números (A -> Z) (Não considera os números na palavra)", _AZOnlyLetter, true, "", _onlyLetters.Length, "", "");

                if (string.IsNullOrEmpty(_AZNumbersLetter))
                    AddList("Letras para números (A -> Z) (Considerando números na palavra)", " - ", false, "", 0, "", "");
                else
                    AddList("Letras para números (A -> Z) (Considerando números na palavra)", _AZNumbersLetter, true, "", _valueWithouSpaces.Length, "", "");

                if (string.IsNullOrEmpty(_ZAOnlyLetter))
                    AddList("Letras para números (Z -> A) (Não considera os números na palavra)", " - ", false, "", 0, "", "");
                else
                    AddList("Letras para números (Z -> A) (Não considera os números na palavra)", _ZAOnlyLetter, true, "", _onlyLetters.Length, "", "");

                if (string.IsNullOrEmpty(_AZNumbersLetter))
                    AddList("Letras para números (Z -> A) (Considerando números na palavra)", " - ", false, "", 0, "", "");
                else
                    AddList("Letras para números (Z -> A) (Considerando números na palavra)", _ZANumbersLetter, true, "", _valueWithouSpaces.Length, "", "");
            }
            else
                AddList("Letras para números ", " - ", false, "", 0, "", "");

        }

        /// <summary>
        /// 
        /// </summary>
        private void NumberToLetter()
        {
            string _onlyNumbers = Utils.GetOnlyNumbers(_value);
            string[] _withSpace = _value.Split(" ");

            if (!string.IsNullOrEmpty(_onlyNumbers))
            {
                if (_withSpace.Length == 0)//Se não for informado com espaço considera número por número
                {
                    AddList("Números para letras (A -> Z) (Não considera letras na palavra)", Utils.GetLettersFromNumberAZ(_onlyNumbers, false), true, "", _onlyNumbers.Length, "", "");
                    AddList("Números para letras (A -> Z) (Considerando letras na palavra)", Utils.GetLettersFromNumberAZ(_value, false), true, "", _value.Length, "", "");
                    AddList("Números para letras (Z -> A) (Não considera letras na palavra)", Utils.GetLettersFromNumberZA(_onlyNumbers, false), true, "", _onlyNumbers.Length, "", "");
                    AddList("Números para letras (Z -> A) (Considerando letras na palavra)", Utils.GetLettersFromNumberZA(_value, false), true, "", _value.Length, "", "");
                }
                else
                {
                    StringBuilder _AZWithoutLetters = new StringBuilder();
                    StringBuilder _AZWithLetters = new StringBuilder();
                    StringBuilder _ZAWithoutLetters = new StringBuilder();
                    StringBuilder _ZAWithLetters = new StringBuilder();

                    foreach (string item in _withSpace)
                    {
                        _AZWithoutLetters.Append(Utils.GetLettersFromNumberAZ(item, true));
                        _AZWithLetters.Append(Utils.GetLettersFromNumberAZ(item, true));
                        _ZAWithoutLetters.Append(Utils.GetLettersFromNumberZA(item, true));
                        _ZAWithLetters.Append(Utils.GetLettersFromNumberZA(item, true));
                    }

                    AddList("Números para letras (A -> Z) (Não considera letras na palavra)", _AZWithoutLetters.ToString(), true, "", _AZWithoutLetters.Length, "", "");
                    AddList("Números para letras (A -> Z) (Considerando letras na palavra)", _AZWithLetters.ToString(), true, "", _AZWithLetters.Length, "", "");
                    AddList("Números para letras (Z -> A) (Não considera letras na palavra)", _ZAWithoutLetters.ToString(), true, "", _ZAWithoutLetters.Length, "", "");
                    AddList("Números para letras (Z -> A) (Considerando letras na palavra)", _ZAWithoutLetters.ToString(), true, "", _ZAWithoutLetters.Length, "", "");
                }
            }
        }


        #endregion
    }
}
