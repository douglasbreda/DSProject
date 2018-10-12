using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DSProject.Util.Enums;

namespace DSProject.Util
{
    ///Classe com funções úteis que podem ser usadas em todo projeto
    public static class Utils
    {

        #region [Methods]

        ///Extensão para facilitar a conversão de datas
        public static DateTime ToDateTime(this object obj)
        {
            try
            {
                return Convert.ToDateTime(obj);
            }
            catch
            {
                return new DateTime();
            }
        }

        //Aplica a máscara no CPF
        public static string PutCpfMask(string cpf)
        {
            try
            {
                cpf = RemoveMask(cpf);

                cpf = Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");

                return cpf;
            }
            catch
            {
                return cpf;
            }
        }

        /// <summary>
        /// Coloca uma máscara de CNPJ
        /// </summary>
        public static string PutCnpjMask(string cnpj)
        {
            try
            {
                cnpj = RemoveMask(cnpj);

                cnpj = Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00");

                return cnpj;
            }
            catch
            {
                return cnpj;
            }
        }

        /// <summary>
        /// Função para aplicar a máscara de CEP
        /// </summary>
        public static string PutCepMask(string cep)
        {
            try
            {
                cep = RemoveMask(cep);

                cep = Convert.ToUInt64(cep).ToString(@"00000\-000");

                return cep;
            }
            catch
            {
                return cep;
            }
        }

        /// <summary>
        /// Coloca a máscara nos campos de telefone e celular
        /// </summary>
        public static string PutPhoneMask(string phone, eMaskType phoneMask)
        {
            string _phoneWithMask = string.Empty;
            try
            {
                switch (phoneMask)
                {
                    case eMaskType.phoneWithDDD:
                        _phoneWithMask = Convert.ToUInt64(phone).ToString(@"\(00\) 0000\-0000");
                        break;
                    case eMaskType.phoneWithoutDDD:
                        _phoneWithMask = Convert.ToUInt64(phone).ToString(@"0000\-0000");
                        break;
                    case eMaskType.cellPhoneWithDDD:
                        _phoneWithMask = Convert.ToUInt64(phone).ToString(@"\(00\) 00000\-0000");
                        break;
                    case eMaskType.cellPhoneWithoutDDD:
                        _phoneWithMask = Convert.ToUInt64(phone).ToString(@"00000\-0000");
                        break;
                }

                return _phoneWithMask;
            }
            catch
            {
                _phoneWithMask = "";
            }

            return _phoneWithMask;
        }

        //Remove a máscara 
        public static string RemoveMask(string value)
        {
            return value.Replace("-", "")
                        .Replace(".", "")
                        .Replace("/", "");
        }

        //Extensão para facilitar a conversão de bool
        public static bool ToBoolean(this object obj)
        {
            try
            {
                return Convert.ToBoolean(obj);
            }
            catch
            {
                return false;
            }
        }

        ///Retorna verdadeiro ou falso baseado numa resposta. Ex: Sim ou Não
        public static bool ToBoolean(string value, string answerTrue)
        {
            if (string.IsNullOrEmpty(value.ToString()))
                return false;

            if (value.ToString().Trim().Equals(answerTrue.Trim()))
                return true;
            else
                return false;
        }

        //Extensão para facilitar a conversão de inteiros
        public static int ToInt32(this object obj)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Verifica qual é o separador e retorna um array com os valores
        /// </summary>
        public static string[] StringIntoArray(string value)
        {
            string[] _array = null;
            if (value.Contains(";"))
                _array = value.Split(";");
            else if (value.Contains(","))
                _array = value.Split(",");
            else if (value.Contains("/"))
                _array = value.Split("/");
            else if (value.Contains("-"))
                _array = value.Split("-");
            else
                _array = new string[] { value };

            return _array;
        }

        /// <summary>
        /// Extrai somente os números de uma string
        /// </summary>
        public static string GetOnlyNumbers(string value)
        {
            return new String(value.Where(Char.IsDigit).ToArray());
        }

        /// <summary>
        /// Extrai somente os caracteres de uma string
        /// </summary>
        public static string GetOnlyCharacteres(string value)
        {
            return new String(value.Where(x => !char.IsDigit(x)).ToArray());
        }

        /// <summary>
        /// Retorna uma máscara a partir de um tipo
        /// </summary>
        public static string GetMask(eMaskType maskType)
        {
            switch (maskType)
            {
                case eMaskType.cpf:
                    return "000.000.000-00";
                case eMaskType.cnpj:
                    return "00.000.000/0000-00";
                case eMaskType.cep:
                    return "00000-000";
                case eMaskType.data:
                    return "00/00/0000";
                case eMaskType.phoneWithDDD:
                    return "(00) 0000-0000";
                case eMaskType.phoneWithoutDDD:
                    return "0000-0000";
                case eMaskType.cellPhoneWithDDD:
                    return "(00) 00000-0000";
                case eMaskType.cellPhoneWithoutDDD:
                    return "00000-0000";

            }

            return "";
        }

        /// <summary>
        /// Retorna os número convertidos de A a Z
        /// </summary>
        public static string GetNumbersFromLetterAZ(string word, bool uniqueLetter)
        {
            StringBuilder _numbers = new StringBuilder();
            int total = 0;
            word = word.ToUpper();

            if (uniqueLetter)
                total = 1;
            else
                total = word.Length;

            for (int i = 0; i < total; i++)
            {
                char character = word.ElementAt(i);
                if (char.IsDigit(character))
                    _numbers.Append(character).Append(" ");
                else
                    _numbers.Append((character.ToInt32() - 64).ToString()).Append(" ");
            }

            return _numbers.ToString();
        }

        /// <summary>
        /// Retorna os número convertidos de A a Z
        /// </summary>
        public static string GetNumbersFromLetterZA(string word, bool uniqueLetter)
        {
            StringBuilder _numbers = new StringBuilder();
            int total = 0;
            word = word.ToUpper();

            if (uniqueLetter)
                total = 1;
            else
                total = word.Length;

            for (int i = 0; i < total; i++)
            {
                char character = word.ElementAt(i);
                if (char.IsDigit(character))
                    _numbers.Append(character).Append(" ");
                else
                    _numbers.Append(91 - character.ToInt32()).Append(" ");
            }

            return _numbers.ToString();
        }

        /// <summary>
        /// Retorna uma possível palavra a partir de uma sequência numérica
        /// </summary>
        public static string GetLettersFromNumberAZ(string numberSequence, bool uniqueNumber)
        {
            StringBuilder _word = new StringBuilder();
            numberSequence = numberSequence.ToLower();

            if (uniqueNumber)
            {
                if (numberSequence.Where(Char.IsDigit).Count() == 0)
                    _word.Append(numberSequence).Append(" ");
                else
                    _word.Append(Convert.ToChar(numberSequence.ToInt32() + 64)).Append(" ");//Converte de acordo com a tabela ASCII
            }
            else
            {
                for (int i = 0; i < numberSequence.Length; i++)
                {
                    char character = numberSequence.ElementAt(i);

                    if (!char.IsDigit(character))
                        _word.Append(character).Append(" ");
                    else
                        _word.Append(Convert.ToChar(character.ToString().ToInt32() + 64)).Append(" ");//Converte de acordo com a tabela ASCII
                }
            }

            return _word.ToString();
        }

        /// <summary>
        /// Retorna uma possível palavra a partir de uma sequência numérica
        /// </summary>
        public static string GetLettersFromNumberZA(string numberSequence, bool uniqueNumber)
        {
            StringBuilder _word = new StringBuilder();
            numberSequence = numberSequence.ToLower();

            if (uniqueNumber)
            {
                if (numberSequence.Where(Char.IsDigit).Count() == 0)
                    _word.Append(numberSequence).Append(" ");
                else
                    _word.Append(Convert.ToChar(91 - numberSequence.ToInt32())).Append(" ");
            }
            else
            {
                for (int i = 0; i < numberSequence.Length; i++)
                {
                    char character = numberSequence.ElementAt(i);

                    if (!char.IsDigit(character))
                        _word.Append(character).Append(" ");
                    else
                        _word.Append(Convert.ToChar(91 - character.ToString().ToInt32())).Append(" ");
                }
            }

            return _word.ToString();
        }

        #endregion
    }

}