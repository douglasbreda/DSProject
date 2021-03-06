namespace DSProject.Util
{
    /// <summary>
    /// Classe para enumerados utilizados no sistema
    /// </summary>
    public static class Enums
    {
        /// <summary>
        /// Enumerado que define as máscaras do sistema
        /// </summary>
        public enum eMaskType
        {
            cpf = 0,
            cnpj = 1,
            cep = 2, 
            data = 3, 
            phoneWithoutDDD = 4, 
            phoneWithDDD = 6,
            cellPhoneWithoutDDD = 7,
            cellPhoneWithDDD = 8
        }
    }
}