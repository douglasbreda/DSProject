using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSProject.Model
{
    ///<summary>
    /// Dados dos integrantes da equipe
    ///</summary>
    [Table("Integrant")]
    public class Integrant
    {
        #region [Properties]

        /// <summary>
        /// Chave primária da tabela
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Data de cadastro do integrante
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Nome do integrante
        /// </summary>
        public string Name { get; set; }       

        /// <summary>
        /// RG do integrante
        /// </summary>
        public string RG { get; set; }

        /// <summary>
        /// CPF do integrante
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        /// Data de nascimento
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Telefone Fixo
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Celular
        /// </summary>
        public string CellPhone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Hora do nascimento
        /// </summary>
        public string HourOfBirth { get; set; }

        /// <summary>
        /// Local do nascimento
        /// </summary>
        public string BirthPlace { get; set; }

        /// <summary>
        /// Signo
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// Se deseja ser voluntário
        /// </summary>
        public bool IsVoluntary { get; set; }

        /// <summary>
        /// Logradouro do endereço
        /// </summary>
        public string Adress { get; set; }

        /// <summary>
        /// Número do endereço
        /// </summary>
        public string AdressNumber { get; set; }

        /// <summary>
        ///  Bairro do endereço
        /// </summary>
        public string AdressDistrict { get; set; }

        /// <summary>
        /// Cidade 
        /// </summary>
        public string AdressCity { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        public string AdressState { get; set; }

        /// <summary>
        /// CEP
        /// </summary>
        public string AdressCep { get; set; }

        /// <summary>
        /// Vezes que participu do challenge
        /// </summary>
        public int NumberOfParticipations { get; set; }

        /// <summary>
        ///  Se irá inscrever o carro para plotagem
        /// </summary>
        public bool IsUseCarPlotting { get; set; }

        /// <summary>
        ///  Se irá inscrever o carro para as provas
        /// </summary>
        public bool IsUseCarTests { get; set; }

        /// <summary>
        /// Marca do carro
        /// </summary>
        public string CarBrand { get; set; }

        /// <summary>
        /// Modelo do carro
        /// </summary>
        public string CarModel { get; set; }

        /// <summary>
        /// Ano do carro
        /// </summary>
        public int CarYear { get; set; }

        /// <summary>
        /// Cor do carro
        /// </summary>
        public string CarColor { get; set; }

        /// <summary>
        /// Placa do carro
        /// </summary>
        public string CarPlate { get; set; }

        /// <summary>
        /// Renavam do carro
        /// </summary>
        public string CarRenavam { get; set; }

        /// <summary>
        /// Proprietário do carro
        /// </summary>
        public string CarOwner { get; set; }

        /// <summary>
        /// Motorista autorizado nº 1
        /// </summary>
        public string DriverAuthorizedOne { get; set; }

        /// <summary>
        /// Motorista autorizado nº 2
        /// </summary>
        public string DriverAuthorizedTwo { get; set; }

        /// <summary>
        /// Escolaridade
        /// </summary>
        public string Scholarity { get; set; }

        /// <summary>
        /// Instituição 
        /// </summary>
        public string Institution { get; set; }

        /// <summary>
        ///  Curso
        /// </summary>
        public string Course { get; set; }
        
        /// <summary>
        /// Ocupação
        /// </summary>
        public string Occupation { get; set; }

        /// <summary>
        ///  Empresa em que trabalha
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        ///  Conhece as ruas da cidade
        /// </summary>
        public bool KnowStreets { get; set; }

        /// <summary>
        ///  Lista de dispositivos 
        /// </summary>
        public List<Device> Devices { get; set; }
        
        /// <summary>
        ///  Lista de habilidades do integrante 
        /// </summary>
        public List<Ability> Abilities { get; set; }

        /// <summary>
        ///  Lista de esportes do integrante 
        /// </summary>
        public List<Sport> Sports { get; set; }

        /// <summary>
        /// Instrumentos que o integrante tem
        /// </summary>
        public List<Instrument> Instruments { get; set; }

        /// <summary>
        /// Instrumentos que o integrante toca
        /// </summary>
        public List<InstrumentPlayed> InstrumentPlayed { get; set; }

        /// <summary>
        ///  Lista de idiomas do integrante 
        /// </summary>
        public List<Language> Languages { get; set; }

        /// <summary>
        ///  Lista de áreas de conhecimento
        /// </summary>
        public List<Knowledge> Knowledges { get; set; }

        /// <summary>
        ///  Área de conhecimento avançada
        /// </summary>
        public string AdvancedKnowledge { get; set; }

        /// <summary>
        /// Se é colecionado e o que coleciona
        /// </summary>
        public string IsCollectorOfSomething { get; set; }

        /// <summary>
        ///  Se o integrante conhece algum colecionador
        /// </summary>
        public string AboutCollectors { get; set; }

        /// <summary>
        ///  Deixamos algo passar?
        /// </summary>
        public string SomethingMore { get; set; }

        #endregion
    }
}