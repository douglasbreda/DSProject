using DSProject.Model;
using System.Collections.Generic;
using System.Linq;

namespace DSProject.Functions
{
    public class GeneralFunction
    {
        #region [Attributes]

        private List<Function> _lstFunctions = new List<Function>();

        #endregion

        #region [Methods]

        /// <summary>
        /// Monta todos os resultados e retorna uma lista 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public List<Function> GetResults( string value )
        {
            OnlyNumber( value );
            OnlyCharacter( value );

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
        private void AddList( string description, object result, bool isMatch, string pattern = "", int size = 0 )
        {
            _lstFunctions.Add( new Function
            {
                Description = description,
                Result = result,
                IsMatch = true,
                Pattern = pattern, 
                Size = size
            } );

        }

        /// <summary>
        /// Verifica se a string possui somente números
        /// </summary>
        /// <param name="value"></param>
        private void OnlyNumber( string value )
        {
            AddList( "Somente Números", value.Count( x => char.IsDigit( x ) ), true );
        }

        /// <summary>
        /// Verifica somente os não numéricos
        /// </summary>
        /// <param name="value"></param>
        private void OnlyCharacter( string value )
        {
            AddList( "Somente letras", value.Count( x => !char.IsDigit( x ) ), true );
        }

        #endregion
    }
}
