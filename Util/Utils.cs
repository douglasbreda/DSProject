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
        public static string GetNumbersFromLetterAZ(string word)
        {
            StringBuilder _numbers = new StringBuilder();
            List<string> _alphabet = new List<string>() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

            for (int i = 0; i < word.Length; i++)
            {
                char character = word.ElementAt(i);
                if (char.IsDigit(character))
                    _numbers.Append(character).Append(" ");
                else
                    _numbers.Append(_alphabet.IndexOf(character.ToString()) + 1).Append(" ");
            }

            return _numbers.ToString();
        }

        /// <summary>
        /// Retorna os número convertidos de A a Z
        /// </summary>
        public static string GetNumbersFromLetterZA(string word)
        {
            StringBuilder _numbers = new StringBuilder();
            List<string> _alphabet = new List<string>() { "z", "y", "x", "w", "v", "u", "t", "s", "r", "q", "p", "o", "n", "m", "l", "k", "j", "i", "h", "g", "f", "e", "d", "c", "b", "a" };

            for (int i = 0; i < word.Length; i++)
            {
                char character = word.ElementAt(i);
                if (char.IsDigit(character))
                    _numbers.Append(character).Append(" ");
                else
                    _numbers.Append(_alphabet.IndexOf(character.ToString()) + 1).Append(" ");
            }

            return _numbers.ToString();
        }
    }
}