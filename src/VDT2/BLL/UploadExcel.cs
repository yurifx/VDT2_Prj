// <copyright file="UploadExcel.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Amauri Rodrigues & Yuri Vasconcelos</author>
// <email>amauri.rodrigues@grupoasserth.com.br | yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>Classe referente a camada de negócios - UploadExcel</summary>

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;

/// <summary>
/// Não estamos utilizando via Excel. _Caso necessário pode deletar esta classe
/// </summary>
namespace VDT2.BLL
{
    public class UploadExcel
    {

        /// <summary>
        /// Salva o arquivo excel no diretorio
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="files"></param>
        /// <param name="configuracao"></param>
        /// <returns></returns>
        public static int SalvarArquivo(char tipo, ICollection<IFormFile> files, Configuracao configuracao)
        {
            try
            {
                string path = "";
                string serverpath = configuracao.PastaUploadListas;
                var file = files.FirstOrDefault();

                if (tipo == 'F')
                {
                     path = Path.Combine(serverpath, "Arquivos", "PackingList");
                }
                else {
                     path = Path.Combine(serverpath, "Arquivos", "LoadingList");
                }


                //1- Cria a pasta no servidor, caso não exista
                if (!Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                };

                using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                #region gravalogErro
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Erro ao realizar upload do arquivo Excel, erro: {ex}"
                    });
                #endregion
                return -1;
            }
        }
    }
}
