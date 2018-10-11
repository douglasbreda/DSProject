using System.Net.Http;
using System.Threading.Tasks;
using DSProject.Model;

namespace DSProject.Util
{
    public class WebServiceCEP
    {
        #region [Attributes]

        private static readonly WebServiceCEP _instance = null;

        public static WebServiceCEP Instance = _instance ?? new WebServiceCEP();

        #endregion

        #region     

        /// <summary>
        /// Realiza a consulta de CEP utilizando a API da VIACEP
        /// </summary>
        public Adress SearchCep(string cep)
        {
            try
            {
                Adress _adress = null;
                using (var client = new HttpClient())
                {
                    var resp =  client.GetAsync(GetUrl(cep)).Result;

                    if (resp.IsSuccessStatusCode)
                    {
                        _adress = resp.Content.ReadAsAsync<Adress>().Result;
                    }
                }

                return _adress;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Monta a URL de consulta
        /// </summary>
        private string GetUrl(string cep)
        {
            string _cep = Utils.RemoveMask(cep);
            return $"https://viacep.com.br/ws/{_cep}/json/";
        }

        #endregion
    }
}