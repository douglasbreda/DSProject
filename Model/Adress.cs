namespace DSProject.Model
{
    public class Adress
    {
        #region [Proprerties]

        /// <summary>
        /// Cep formatado
        /// </summary>
        public string cep { get; set; }

        /// <summary>
        /// Logradouro
        /// </summary>
        public string logradouro { get; set; }

        /// <summary>
        /// Complemento do endereço
        /// </summary>
        public string complemento { get; set; }

        /// <summary>
        /// Bairro
        /// </summary>
        public string bairro { get; set; }

        /// <summary>
        /// Cidade
        /// </summary>
        public string localidade { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        public string uf { get; set; }

        public string unidade { get; set; }

        /// <summary>
        /// Código do IBGE
        /// </summary>
        public string ibge { get; set; }

        /// <summary>
        /// Guia de Informação e Apuração do ICMS
        /// </summary>
        public string gia { get; set; }

        /// <summary>
        /// Define se houve erro na consulta
        /// </summary>
        public bool erro { get; set; }

        #endregion
    }
}