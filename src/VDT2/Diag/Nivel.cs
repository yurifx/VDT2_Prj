// <copyright file="Nivel.cs" company="Bureau Veritas">
// Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Amauri Rodrigues</author>
// <email>amauri.rodrigues@grupoasserth.com.br</email>
// <date>2017-02-19</date>
// <summary>Enumeração dos níveis de LOG</summary>

using System.ComponentModel;

namespace VDT2.Diag
{
    /// <summary>
    /// Enumeração dos níveis de LOG
    /// </summary>
    public enum Nivel
    {
        /// <summary>
        /// 0=Indefinido
        /// </summary>
        [DescriptionAttribute("Indefinido")]
        Indefinido = 0,

        /// <summary>
        /// 1=Informacao
        /// </summary>
        [DescriptionAttribute("Informacao")]
        Informacao = 1,

        /// <summary>
        /// 2=Aviso
        /// </summary>
        [DescriptionAttribute("Aviso")]
        Aviso = 2,

        /// <summary>
        /// 3=Erro
        /// </summary>
        [DescriptionAttribute("Erro")]
        Erro = 3,

        /// <summary>
        /// 4=Critico
        /// </summary>
        [DescriptionAttribute("Critico")]
        Critico = 4
    }
}
