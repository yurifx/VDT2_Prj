// <copyright file="Conferencia.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Amauri Rodrigues & Yuri Vasconcelos</author>
// <email>amauri.rodrigues@grupoasserth.com.br | yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>Camada de negócios referentes a Conferencia</summary>

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VDT2.Models;

/// <summary>
/// Camada de negócios - Conferência de avarias
/// </summary>
namespace VDT2.BLL
{
    public class Conferencia
    {
        /// <summary>
        /// Insere a lista de veículos no banco de dados.
        /// </summary>
        /// <param name="listaVeiculos"></param>
        /// <param name="configuracao"></param>
        /// <returns></returns>
        public static Models.ListaVeiculos InserirListaVeiculos(Models.ListaVeiculos listaVeiculos, Configuracao configuracao)
        {

            try
            {
                listaVeiculos = DAL.ListaVeiculos.Inserir(listaVeiculos, configuracao);
                return listaVeiculos;
            }
            catch (Exception ex)
            {
                #region gravalogErro
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar a InserirListaVeiculos: Exception:  {ex}",
                        Excecao = ex
                    });
                #endregion  
                listaVeiculos.ListaVeiculo_ID = 0;
                return listaVeiculos;
            }
        }
        
/// <summary>
/// Realiza integração do arquivo de Loading ou PackingList
/// </summary>
/// <param name="ListaVeiculo_ID"></param>
/// <param name="tipo">Tipo: "P" = Packing, "L" = Loading</param>
/// <param name="files">Arquivo enviado</param>
/// <param name="configuracao"></param>
/// <returns></returns>
        public static bool IntegrarArquivoLoadingPackingList(int ListaVeiculo_ID, char tipo, ICollection<IFormFile> files, Configuracao configuracao)
        {
            try
            {
                string path = "";
                string serverpath = configuracao.PastaUploadListas;
                var file = files.FirstOrDefault();
                string mesdia = DateTime.Now.ToString("MMdd");
                string ano = DateTime.Now.ToString("yyyy");

                if (tipo == 'P')
                {
                    path = Path.Combine(serverpath, "Arquivos", "PackingList", ano, mesdia,  Convert.ToString(ListaVeiculo_ID), file.FileName);
                }
                else
                {
                    path = Path.Combine(serverpath, "Arquivos", "LoadingList", ano, mesdia, Convert.ToString(ListaVeiculo_ID), file.FileName);
                }

                string[] linhas = System.IO.File.ReadAllLines(path);

                foreach (var linha in linhas)
                {
                    if (linha.Length == 17)
                    {
                        Models.ListaVeiculosVin VeiculoVin = new Models.ListaVeiculosVin { ListaVeiculos_ID = ListaVeiculo_ID, VIN = linha };
                        DAL.ListaVeiculosVin.Inserir(VeiculoVin, configuracao);
                    }
                }
                
                return true;
            }
            catch (Exception ex)
            {
                #region gravalogErro
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar IntegrarArquivoLoadingList",
                        Excecao = ex
                    });
                #endregion
                return false;
            }
        }

    }
}
