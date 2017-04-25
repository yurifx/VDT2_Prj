// <copyright file="Configuracao.cs" company="Bureau Veritas">
// Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Amauri Rodrigues</author>
// <email>amauri.rodrigues@grupoasserth.com.br</email>
// <date>2017-02-19</date>
// <summary>Configuração geral do aplicativo (dados carregados de appsettings.json)</summary>

namespace VDT2.Models
{
    /// <summary>
    /// Configuração geral do aplicativo (dados carregados de appsettings.json)
    /// </summary>
    public class Configuracao
    {
        /// <summary>
        /// Pasta para gravar os arquivos de packing-list e loading-list
        /// </summary>
        public string PastaUploadListas { get; set; }

        /// <summary>
        /// String de conexão para acessar a base de dados
        /// </summary>
        public string ConnectionStringVDT { get; set; }


        //Pasta contendo arquivos de fotos do sistema
        public string PastaFotos { get; set; }


        //Paste temporária para geração de arquivos Excel
        public string PastaExcel { get; set; }
    }
}
