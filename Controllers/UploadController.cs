using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
            List<Integrant> _lstIntegrants = new List<Integrant>();

            if (!string.IsNullOrEmpty(filePath))
            {
                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet(new ExcelDataSetConfiguration() {
                            ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration() {
                                UseHeaderRow = true
                            }
                        });

                        foreach (DataRow line in result.Tables[0].Rows)
                        {
                            
                            _lstIntegrants.Add(new Integrant
                            {
                                RegistrationDate = line["Carimbo de data/hora"].ToDateTime(),
                                Name = line["Nome Completo"].ToString(),
                                RG = line["RG"].ToString(),
                                CPF = Utils.PutCpfMask(line["CPF"].ToString()),
                                Phone = line["Telefone"].ToString(),
                                CellPhone = line["Celular"].ToString(),
                                Email = line["E-mail"].ToString(),
                                DateOfBirth = line["Data de Nascimento"].ToDateTime(),
                                HourOfBirth = line["Horário de Nascimento"].ToString(),
                                BirthPlace = line["Local de Nascimento"].ToString(),
                                Sign = line["Signo "].ToString(),
                                IsVoluntary = Utils.ToBoolean(line["Deseja ser voluntário para ações sociais"]),
                                Adress = line["Logradouro"].ToString(),
                                AdressNumber = line["Nº"].ToString(),
                                AdressDistrict = line["Bairro"].ToString(),
                                AdressCity = line["Cidade"].ToString(),
                                AdressState = line["Estado"].ToString(),
                                AdressCep = line["CEP"].ToString(),
                                NumberOfParticipations = line["Quantas vezes já participou da gincana ?"].ToInt32(),
                                IsUseCarPlotting = Utils.ToBoolean(line["Irá inscrever o veículo para plotagem"].ToString(), "Sim"),
                                IsUseCarTests = Utils.ToBoolean(line["Irá inscrever o veículo para provas"].ToString(), "Sim"),
                                CarBrand = line["Marca"].ToString(),
                                CarModel = line["Modelo"].ToString(),
                                CarYear = line["Ano"].ToInt32(),
                                CarColor = line["Cor"].ToString(),
                                CarPlate = line["Placa"].ToString(),
                                CarRenavam = line["Renavan"].ToString(),
                                CarOwner = line["Proprietário"].ToString(),
                                DriverAuthorizedOne = line["Motorista autorizado nº 1"].ToString(),
                                DriverAuthorizedTwo = line["Motorista autorizado nº 2"].ToString(),
                                Scholarity = line["Qual seu nível de escolaridade ?"].ToString(),
                                Institution = line["Instituição"].ToString(),
                                Course = line["Curso"].ToString(),
                                Occupation = line["Ocupação"].ToString(),
                                Company = line["Empresa onde trabalha"].ToString(),
                                KnowStreets = Utils.ToBoolean(line["Conhece com familiaridade as ruas de Itajaí?"].ToString(), "Sim"),
                                Devices = _entityFactory.CreateDevices(line["Possui ( disponível para ser utilizado na gincana ) "].ToString()),
                                Abilities = _entityFactory.CreateAbilities(line["Habilidades "].ToString()),
                                Sports = _entityFactory.CreateSports(line["Esportes que pratica"].ToString()),
                                Instruments = _entityFactory.CreateInstruments(line["Instrumentos musicais"].ToString()),
                                InstrumentPlayed = _entityFactory.CreateInstrumentsPlayed(line["Quais instrumentos musicais você toca??"].ToString()),
                                Languages = _entityFactory.CreateLanguages(line["Idiomas"].ToString()),
                                Knowledges = _entityFactory.CreateKnowledges(line["Área de conhecimento que se sente confortável"].ToString()),
                                AdvancedKnowledge = line["Tem conhecimento avançado em algum assunto? Qual?"].ToString(),
                                IsCollectorOfSomething = line["É colecionador de algum objeto ?"].ToString(),
                                AboutCollectors = line["Conhece algum colecionador de objetos? Tem contato? Qual a coleção?"].ToString(),
                                SomethingMore = line["Deixamos algo passar ?"].ToString(),
                            });
                        }
                    }
                }

                if (_lstIntegrants.Count > 0)
                {
                    foreach (Integrant newIntegrant in _lstIntegrants)
                    {
                        _context.Integrants.Add(newIntegrant);
                        _context.SaveChanges();
                    }
                }
            }
        }
    }
}