using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using DSProject.Model;
using DSProject.Util;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace DSProject.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private IHostingEnvironment _hostingEnvironment;
        private DSBaseContext _context;

        public UploadController(IHostingEnvironment hostingEnvironment, DSBaseContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        [HttpPost, DisableRequestSizeLimit]
        public ActionResult UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                string folderName = "Upload";

                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                string fullPath = string.Empty;

                if (!Directory.Exists(newPath))
                    Directory.CreateDirectory(newPath);

                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    fullPath = Path.Combine(newPath, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                SendToDataBase(fullPath);

                return Ok("Upload realizado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest("Upload Failed: " + ex.Message);
            }
        }

        ///Envia as informações do arquivo enviado para o banco
        private void SendToDataBase(string filePath)
        {
            EntityFactory _entityFactory = new EntityFactory();
            List<Integrant> _lstNewIntegrants = new List<Integrant>();
            List<Integrant> _lstOldIntegrants = new List<Integrant>();

            if (!string.IsNullOrEmpty(filePath))
            {
                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });

                        foreach (DataRow line in result.Tables[0].Rows)
                        {
                            Integrant _integrant = CheckIntegrantAlreadyExists(Utils.PutCpfMask(line["CPF"].ToString()));

                            if (_integrant == null)
                            {
                                _integrant = new Integrant();

                                _lstNewIntegrants.Add(SetIntegrantData(_integrant, line));
                            }
                            else
                                _lstOldIntegrants.Add(SetIntegrantData(_integrant, line));
                        }
                    }
                }

                if (_lstNewIntegrants.Count > 0)
                {
                    _context.Integrants.AddRange(_lstNewIntegrants);
                    _context.SaveChanges();
                }

                if (_lstOldIntegrants.Count > 0)
                {
                    _context.Integrants.UpdateRange(_lstOldIntegrants);
                    _context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Atribui os dados de cada integrante
        /// </summary>
        private Integrant SetIntegrantData(Integrant integrantInstance, DataRow dataLine)
        {
            EntityFactory _entityFactory = new EntityFactory();

            integrantInstance.RegistrationDate = dataLine["Carimbo de data/hora"].ToDateTime();
            integrantInstance.Name = dataLine["Nome Completo"].ToString();
            integrantInstance.RG = dataLine["RG"].ToString();
            integrantInstance.CPF = Utils.PutCpfMask(dataLine["CPF"].ToString());
            integrantInstance.Phone = dataLine["Telefone"].ToString();
            integrantInstance.CellPhone = dataLine["Celular"].ToString();
            integrantInstance.Email = dataLine["E-mail"].ToString();
            integrantInstance.DateOfBirth = dataLine["Data de Nascimento"].ToDateTime();
            integrantInstance.HourOfBirth = dataLine["Horário de Nascimento"].ToString();
            integrantInstance.BirthPlace = dataLine["Local de Nascimento"].ToString();
            integrantInstance.Sign = dataLine["Signo "].ToString();
            integrantInstance.IsVoluntary = Utils.ToBoolean(dataLine["Deseja ser voluntário para ações sociais"]);
            integrantInstance.Adress = dataLine["Logradouro"].ToString();
            integrantInstance.AdressNumber = dataLine["Nº"].ToString();
            integrantInstance.AdressDistrict = dataLine["Bairro"].ToString();
            integrantInstance.AdressCity = dataLine["Cidade"].ToString();
            integrantInstance.AdressState = dataLine["Estado"].ToString();
            integrantInstance.AdressCep = dataLine["CEP"].ToString();
            integrantInstance.NumberOfParticipations = dataLine["Quantas vezes já participou da gincana ?"].ToInt32();
            integrantInstance.IsUseCarPlotting = Utils.ToBoolean(dataLine["Irá inscrever o veículo para plotagem"].ToString(), "Sim");
            integrantInstance.IsUseCarTests = Utils.ToBoolean(dataLine["Irá inscrever o veículo para provas"].ToString(), "Sim");
            integrantInstance.CarBrand = dataLine["Marca"].ToString();
            integrantInstance.CarModel = dataLine["Modelo"].ToString();
            integrantInstance.CarYear = dataLine["Ano"].ToInt32();
            integrantInstance.CarColor = dataLine["Cor"].ToString();
            integrantInstance.CarPlate = dataLine["Placa"].ToString();
            integrantInstance.CarRenavam = dataLine["Renavan"].ToString();
            integrantInstance.CarOwner = dataLine["Proprietário"].ToString();
            integrantInstance.DriverAuthorizedOne = dataLine["Motorista autorizado nº 1"].ToString();
            integrantInstance.DriverAuthorizedTwo = dataLine["Motorista autorizado nº 2"].ToString();
            integrantInstance.Scholarity = dataLine["Qual seu nível de escolaridade ?"].ToString();
            integrantInstance.Institution = dataLine["Instituição"].ToString();
            integrantInstance.Course = dataLine["Curso"].ToString();
            integrantInstance.Occupation = dataLine["Ocupação"].ToString();
            integrantInstance.Company = dataLine["Empresa onde trabalha"].ToString();
            integrantInstance.KnowStreets = Utils.ToBoolean(dataLine["Conhece com familiaridade as ruas de Itajaí?"].ToString(), "Sim");
            integrantInstance.Devices = _entityFactory.CreateDevices(dataLine["Possui ( disponível para ser utilizado na gincana ) "].ToString());
            integrantInstance.Abilities = _entityFactory.CreateAbilities(dataLine["Habilidades "].ToString());
            integrantInstance.Sports = _entityFactory.CreateSports(dataLine["Esportes que pratica"].ToString());
            integrantInstance.Instruments = _entityFactory.CreateInstruments(dataLine["Instrumentos musicais"].ToString());
            integrantInstance.InstrumentPlayed = _entityFactory.CreateInstrumentsPlayed(dataLine["Quais instrumentos musicais você toca??"].ToString());
            integrantInstance.Languages = _entityFactory.CreateLanguages(dataLine["Idiomas"].ToString());
            integrantInstance.Knowledges = _entityFactory.CreateKnowledges(dataLine["Área de conhecimento que se sente confortável"].ToString());
            integrantInstance.AdvancedKnowledge = dataLine["Tem conhecimento avançado em algum assunto? Qual?"].ToString();
            integrantInstance.IsCollectorOfSomething = dataLine["É colecionador de algum objeto ?"].ToString();
            integrantInstance.AboutCollectors = dataLine["Conhece algum colecionador de objetos? Tem contato? Qual a coleção?"].ToString();
            integrantInstance.SomethingMore = dataLine["Deixamos algo passar ?"].ToString();

            return integrantInstance;
        }

        /// <summary>
        /// Verifica pelo CPF se o integrant já está cadastrado para somente atualizar os dados
        /// </summary>
        private Integrant CheckIntegrantAlreadyExists(string cpf)
        {
            Integrant _integrant = _context.Integrants.Where(integrant => integrant.CPF.Equals(cpf)).FirstOrDefault();

            return _integrant;
        }

    }
}