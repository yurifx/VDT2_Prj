// <copyright file="Extensoes.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>GrupoAsserth</author>
// <email>yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>Extensions methods usados no projeto</summary>

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
