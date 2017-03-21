using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VDT2.BLL
    {
    public static class Extensoes
        {

        /// <summary>
        /// remove espaços e altera tudo para letras maiusculas
        /// </summary>
        /// <param name="str"></param>
        /// <returns>retorna a string sem espaços e com letras upperkey</returns>
        public static string RemoveEspacosUpperKey(this string str)
            {
            if (str != null)
                {
                return str.Trim().ToUpper();
                }
            else
                {
                return "ERRO";
                }
            }


        public static int WordCount(this string str)
            {
            return str.Split(new char[] { ' ', '.', '?' },
                    StringSplitOptions.RemoveEmptyEntries).Length;
            }

        public static int QtdLetra(this string palavra, char letra)

            {
            return palavra.Where(c => c.Equals(letra)).Count();
            }
        }
    }
