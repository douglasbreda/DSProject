using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
        private string _cifraCesar = string.Empty;

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
        [HttpGet("{value}/{cifraCesar}", Name = "GetFunction")]
        // public IEnumerable<IFunction> Get(string value)
        public IActionResult Get(string value, string cifraCesar)
        {
            _value = value;
            _cifraCesar = cifraCesar;
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
            //OnlyNumber();
            //OnlyCharacter();
            Cpf();
            Cnpj();
            DataCheck();
            DataCheckInverted();
            Phone();
            CellPhone();
            CheckColor();
            LetterToNumber();
            NumberToLetter();
            Cep();
            CaesarCipher();
            BinaryToString();
            MorseToText();
            NfeKey();

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
            try
            {
                string _cpf = Utils.GetOnlyNumbers(_value);

                if (_cpf.Length < 11 || _cpf.Length > 11)
                    AddList("CPF", $"O valor {_cpf} possui {_cpf.Length} números. CPFs contém 11 números. ", false, Utils.GetMask(eMaskType.cpf), 0);
                else
                {
                    Integrant integrant = _context.Integrants.Where(x => x.CPF.Equals(Utils.PutCpfMask(_cpf))).FirstOrDefault();

                    if (integrant != null)
                        AddList("CPF", $"O(A) integrante da Dark Side {integrant.Name} possui este CPF.", true, Utils.GetMask(eMaskType.cpf), 11);
                    else
                        AddList("CPF", $"Pode ser que o valor seja um CPF: {Utils.PutCpfMask(_cpf)}", true, Utils.GetMask(eMaskType.cpf), 11);
                }
            }
            catch { }
        }

        /// <summary>
        /// Verificação de CNPJ
        /// </summary>
        private void Cnpj()
        {
            try
            {
                string _cnpj = Utils.GetOnlyNumbers(_value);

                if (_cnpj.Length < 14 || _cnpj.Length > 14)
                    AddList("CNPJ", $"O valor {_cnpj} possui {_cnpj.Length} números. CNPJs contém 14 números.", false, Utils.GetMask(eMaskType.cnpj));
                else
                {
                    AddList("CNPJ", $"Pode ser o valor seja um CNPJ: {Utils.PutCnpjMask(_cnpj)}.", true, Utils.GetMask(eMaskType.cnpj), 14, "http://www.receita.fazenda.gov.br/pessoajuridica/cnpj/cnpjreva/cnpjreva_solicitacao.asp");
                }
            }
            catch { }
        }

        /// <summary>
        /// Verifica se a sequência pode ser uma chave de NFe
        /// </summary>
        private void NfeKey(){

            string _nfe = Utils.GetOnlyNumbers(_value);

            if(_nfe.Length == 44)
                AddList("Nota Fiscal (NFe)", "Pode ser que o valor seja uma nota fiscal eletrônica. Acesse o link ao lado ou tente os disponíveis na aba de links úteis ", true, "", 0, "http://www.nfe.fazenda.gov.br/portal/consultaRecaptcha.aspx?tipoConsulta=completa&tipoConteudo=XbSeqxE8pl8=");
        }


        /// <summary>
        ///  Verificação de data no padrão dd/MM/yyyy
        /// </summary>
        private void DataCheck()
        {
            try
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
            catch { }
        }

        /// <summary>
        ///  Verificação de data no formato yyyy/MM/dd
        /// </summary>
        private void DataCheckInverted()
        {
            try
            {
                string _date = Utils.GetOnlyNumbers(_value);



                if (_date.Length == 8)
                {
                    int year = _date.Substring(0, 4).ToInt32();
                    int month = _date.Substring(4, 2).ToInt32();
                    int day = _date.Substring(6, 2).ToInt32();

                    DateTime dt = new DateTime(year, month, day);

                    if (dt != new DateTime())
                    {
                        CultureInfo cult = new CultureInfo("pt-BR");
                        string dtFormated = dt.ToString("yyyy/MM/dd", cult);
                        AddList("Data", $"Pode ser que o valor seja uma data no padrão: {dtFormated}", true, Utils.GetMask(eMaskType.data), 8);
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Validação para telefones
        /// </summary>
        private void Phone()
        {
            try
            {
                string phone = Utils.GetOnlyNumbers(_value);

                if (phone.Length == 8)
                {
                    string _integrantPhone = IntegrantPhone(phone);
                    if (string.IsNullOrEmpty(_integrantPhone))
                    {
                        string formatedPhone = phone.Insert(4, "-");
                        AddList("Telefone", $"Pode ser que o valor seja um número de telefone {formatedPhone}", true, Utils.GetMask(eMaskType.phoneWithoutDDD), 8);
                    }
                    else
                        AddList("Telefone", _integrantPhone, true, Utils.GetMask(eMaskType.phoneWithoutDDD));
                }
                else if (phone.Length == 10)
                {
                    string _integrantPhone = IntegrantPhone(phone);
                    if (string.IsNullOrEmpty(_integrantPhone))
                    {
                        string formatedPhone = Utils.PutPhoneMask(phone, eMaskType.phoneWithDDD);
                        AddList("Telefone", $"Pode ser que o valor seja um número de telefone {formatedPhone}", true, Utils.GetMask(eMaskType.phoneWithoutDDD), 10);
                    }
                    else
                        AddList("Telefone", _integrantPhone, true, Utils.GetMask(eMaskType.phoneWithDDD));
                }
                else if (phone.Length < 8)
                {
                    AddList("Telefone", $"Não é uma número de telefone válido pois contém menos de 8 números", false, "", 0);
                }
                else if (phone.Length > 10)
                    AddList("Telefone", $"Não é uma número de telefone válido pois contém mais de 10 números", false, "", 0);
            }
            catch { }

        }

        /// <summary>
        /// Verifica se no banco de dados há algum(a) integrante com este telefone
        /// </summary>
        private string IntegrantPhone(string phone)
        {
            try
            {
                string _message = string.Empty;

                try
                {
                    Integrant _integrant = _context.Integrants.Where(x => Utils.RemoveMask(x.Phone).Equals(Utils.RemoveMask(phone)))
                                                               .FirstOrDefault();

                    if (_integrant != null)
                        _message = $"O(A) integrante da Dark Side {_integrant.Name} possui este número de telefone";
                    else
                    {
                        //Faz o teste desconsiderando possível DDD na máscara
                        Integrant _integrantWithoudDDD = _context.Integrants.Where(x => !string.IsNullOrWhiteSpace(x.Phone.Trim()) && x.Phone.Length > 2 &&
                                                                                         Utils.RemoveMask(x.Phone).Contains(Utils.RemoveMask(phone)))
                                                                            .FirstOrDefault();

                        if (_integrantWithoudDDD != null)
                            _message = $"O(A) integrante da Dark Side {_integrantWithoudDDD.Name} possui este número de celular";
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.InnerException.ToString());
                }

                return _message;
            }
            catch { return ""; }
        }


        /// <summary>
        /// Validação para números de telefone
        /// </summary>
        private void CellPhone()
        {
            try
            {
                string cellPhone = Utils.GetOnlyNumbers(_value);
                if (cellPhone.Length == 9 || cellPhone.Length == 8)
                {
                    string _integrantCellPhone = IntegrantCellPhone(cellPhone);

                    if (string.IsNullOrEmpty(_integrantCellPhone))
                    {
                        string formatedPhone = cellPhone.Insert(5, "-");
                        AddList("Celular", $"Pode ser que o valor seja um número de celular {formatedPhone}", true, Utils.GetMask(eMaskType.cellPhoneWithoutDDD), 9);
                    }
                    else
                        AddList("Celular", _integrantCellPhone, true, Utils.GetMask(eMaskType.cellPhoneWithoutDDD), 9);
                }
                else if (cellPhone.Length == 11 || cellPhone.Length == 10)
                {
                    string _integrantCellPhone = IntegrantCellPhone(cellPhone);

                    if (string.IsNullOrEmpty(_integrantCellPhone))
                    {
                        string formatedPhone = Utils.PutPhoneMask(cellPhone, eMaskType.cellPhoneWithDDD);
                        AddList("Celular", $"Pode ser que o valor seja um número de celular {formatedPhone}", true, Utils.GetMask(eMaskType.cellPhoneWithoutDDD), 11);
                    }
                    else
                        AddList("Celular", _integrantCellPhone, true, Utils.GetMask(eMaskType.cellPhoneWithDDD), 9);

                }
                else if (cellPhone.Length < 9)
                {
                    AddList("Celular", $"Não é uma número de celular válido pois contém menos de 9 números", false, "", 0);
                }
                else if (cellPhone.Length > 11)
                    AddList("Celular", $"Não é uma número de celular válido pois contém mais de 11 números", false, "", 0);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException.ToString());
            }
        }

        /// <summary>
        /// Verifica se no banco de dados há algum(a) integrante com este telefone
        /// </summary>
        private string IntegrantCellPhone(string cellPhone)
        {
            try
            {
                string _message = string.Empty;
                Integrant _integrant = _context.Integrants.Where(x => !string.IsNullOrWhiteSpace(x.CellPhone) &&
                                                                       Utils.RemoveMask(x.CellPhone).Equals(Utils.RemoveMask(cellPhone)))
                                                          .FirstOrDefault();

                if (_integrant != null)
                    _message = $"O(A) integrante da Dark Side {_integrant.Name} possui este número de celular";
                else
                {
                    //Faz o teste desconsiderando possível DDD na máscara
                    Integrant _integrantWithoudDDD = _context.Integrants.Where(x => !string.IsNullOrEmpty(x.CellPhone) &&
                                                                                    Utils.RemoveMask(x.CellPhone).Contains(Utils.RemoveMask(cellPhone)))
                                                                        .FirstOrDefault();

                    if (_integrantWithoudDDD != null)
                        _message = $"O(A) integrante da Dark Side {_integrantWithoudDDD.Name} possui este número de celular";
                }

                return _message;
            }
            catch { return ""; }
        }

        /// <summary>
        /// Verifica se um sequencia pode ser um hexadecimal de cores
        /// </summary>
        private void CheckColor()
        {
            try
            {
                string _color = _value;
                if (!_color.StartsWith("#"))
                    _color = $"#{_value}";

                Regex regex = new Regex("^#(?:[0-9a-fA-F]{3}){1,2}$");

                if (regex.Match(_color).Success)
                {
                    // <div class=\"boxColor\" style=\"background: #13b4ff\"></div>
                    // <div style='width:10px;height:100px;border:1px solid #000;background-color:#13b4ff'>
                    AddList("Cor", $"Pode ser que a sequência seja uma cor: {_color}", true, "", 0, "", $"colorBox;{_color}");
                }
            }
            catch { }
        }

        /// <summary>
        /// Conversão de letras para números
        /// </summary>
        private void LetterToNumber()
        {
            try
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
            catch { }
        }

        /// <summary>
        /// 
        /// </summary>
        private void NumberToLetter()
        {
            try
            {
                string _onlyNumbers = Utils.GetOnlyNumbers(_value);
                string[] _withSpace = null;

                if (_value.Contains(" "))
                    _withSpace = _value.Split(" ");

                if (!string.IsNullOrEmpty(_onlyNumbers))
                {
                    if (_withSpace == null || _withSpace.Length == 0)//Se não for informado com espaço considera número por número
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
            catch { }
        }

        /// <summary>
        /// Função para verificar se o CEP é válido e existe na base de dados da equipe
        /// </summary>
        private void Cep()
        {
            try
            {
                string _cep = Utils.RemoveMask(_value);

                if (_cep.Length == 8)
                {
                    List<Integrant> _integrants = _context.Integrants.Where(integrant => !string.IsNullOrWhiteSpace(integrant.AdressCep) &&
                                                                                         Utils.RemoveMask(integrant.AdressCep).Equals(_cep))
                                                                     .ToList();

                    if (_integrants != null && _integrants.Count > 0)
                    {
                        if (_integrants.Count > 1)
                        {
                            string _integrantsNames = string.Join(", ", _integrants.Select(x => x.Name));

                            AddList("CEP", $"Os integrantes da Dark Side {_integrantsNames} possuem este CEP", true, Utils.GetMask(eMaskType.cep), 8, "", "");
                        }
                        else
                        {
                            AddList("CEP", $"O(A) integrante da Dark Side {_integrants.FirstOrDefault().Name} possui este CEP", true, Utils.GetMask(eMaskType.cep), 8, "", "");
                        }

                    }
                    else
                    {
                        Adress _adress = WebServiceCEP.Instance.SearchCep(_cep);

                        if (_adress != null && !_adress.erro)
                        {
                            StringBuilder _sbAdressInfo = new StringBuilder();

                            _sbAdressInfo.AppendLine($"<br><strong>Endereço: {_adress.logradouro} </strong>");
                            _sbAdressInfo.AppendLine($"<br><strong>Bairro: {_adress.bairro} </strong>");
                            _sbAdressInfo.AppendLine($"<br><strong>Complemento: {_adress.complemento} </strong>");
                            _sbAdressInfo.AppendLine($"<br><strong>Cidade: {_adress.localidade} </strong>");
                            _sbAdressInfo.AppendLine($"<br><strong>IBGE: {_adress.ibge} </strong>");
                            _sbAdressInfo.AppendLine($"<br><strong>Estado: {_adress.uf} </strong>");

                            AddList("CEP", $"Foi encontrado um CEP no site dos correios. Verifique ao lado ", true, Utils.GetMask(eMaskType.cep), 0, "", _sbAdressInfo.ToString());
                        }
                        else
                            AddList("CEP", $"Pode ser que o valor seja um CEP: {Utils.PutCepMask(_cep)}", true, Utils.GetMask(eMaskType.cep), 0, "", "");
                    }
                }
                else
                {
                    AddList("CEP", "Não é um CEP válido. Um CEP contém 8 caracteres", false, Utils.GetMask(eMaskType.cep), _cep.Length, "", "");
                }
            }
            catch { }
        }

        /// <summary>
        /// Retorna a palavra alterada utilizando a Cifra de Cesar
        /// </summary>
        private void CaesarCipher()
        {
            try
            {
                int _movement = _cifraCesar.ToInt32();
                if (_movement > 0)
                {
                    string _lowerValue = _value.ToLower();
                    string _encrypt = string.Empty;

                    for (int i = 0; i < _lowerValue.Length; i++)
                    {
                        int ASCII = (int)_lowerValue[i];

                        //Coloca a chave fixa adicionando X posições no numero da tabela ASCII
                        int ASCIIC = ASCII + _movement;

                        _encrypt += Char.ConvertFromUtf32(ASCIIC);
                    }

                    if (!string.IsNullOrEmpty(_encrypt))
                        AddList("Cifra de César", _encrypt, true, "", _encrypt.Length, "", "");
                }
            }
            catch { }
        }

        /// <summary>
        /// Converte um sequência de binários em texto
        /// </summary>
        private void BinaryToString()
        {
            try
            {
                Encoding enc = System.Text.Encoding.UTF8;

                string binaryString = _value.Replace(" ", "");

                var bytes = new byte[binaryString.Length / 8];

                var ilen = (int)(binaryString.Length / 8);

                for (var i = 0; i < ilen; i++)
                {
                    bytes[i] = Convert.ToByte(binaryString.Substring(i * 8, 8), 2);
                }

                string str = enc.GetString(bytes);

                AddList("Binário para letra", str, false);
            }
            catch { }
        }

        /// <summary>
        /// Coverte código morse para texto
        /// </summary>
        private void MorseToText()
        {
            try
            {
                Dictionary<char, string> morseToChar = new Dictionary<char, string>()
                {
                    {'a', ".-"},
                    {'b', "-..."},
                    {'c', "-.-."},
                    {'d', "-.."},
                    {'e', "."},
                    {'f', "..-."},
                    {'g', "--."},
                    {'h', "...."},
                    {'i', ".."},
                    {'j', ".---"},
                    {'k', "-.-"},
                    {'l', ".-.."},
                    {'m', "--"},
                    {'n', "-."},
                    {'o', "---"},
                    {'p', ".--."},
                    {'q', "--.-"},
                    {'r', ".-."},
                    {'s', "..."},
                    {'t', "-"},
                    {'u', "..-"},
                    {'v', "...-"},
                    {'w', ".--"},
                    {'x', "-..-"},
                    {'y', "-.--"},
                    {'z', "--.."},

                    {'0', "-----"},
                    {'1', ".----"},
                    {'2', "..---"},
                    {'3', "...--"},
                    {'4', "....-"},
                    {'5', "....."},
                    {'6', "-...."},
                    {'7', "--..."},
                    {'8', "---.."},
                    {'9', "----."},

                    {' ', "__"},//O correto seria a "/", porém ao enviar por parâmetro a API considera como um caminho. Para tal foi substituída por __.
                    {'.', ".-.-.-"},
                    {',', "--..--"},
                    {':', "---..."},
                    {'?', "..--.."},
                    {'!', "..--."},
                    {'\'', ".----."},
                    {'-', "-....-"},
                    {'/', "-..-."},
                    {'"', ".-..-."},
                    {'@', ".--.-."},
                    {'=', "-...-"}
                };

                string[] input = _value.ToLower().Trim().Split(' ');
                StringBuilder output = new StringBuilder();
                foreach (string s in input)
                {
                    if (morseToChar.ContainsValue(s))
                    {
                        output.Append(morseToChar.FirstOrDefault(x => x.Value == s).Key);

                        if (s.Equals("__"))
                            output.Append(" ");
                    }
                }

                if (!string.IsNullOrEmpty(output.ToString()))
                    AddList("Morse para texto", output.ToString(), true, "", output.Length, "", "");
            }
            catch { }
        }


        #endregion
    }
}
