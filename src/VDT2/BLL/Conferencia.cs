// <copyright file="Conferencia.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Grupo Asserth</author>
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
        public static Models.ListaVeiculos InserirListaVeiculos(Models.ListaVeiculos listaVeiculos, DateTime dataLista, Configuracao configuracao)
        {
            try
            {
                listaVeiculos = DAL.ListaVeiculos.Inserir(listaVeiculos, dataLista, configuracao);
                return listaVeiculos;
            }
            catch (Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Erro, Mensagem = $"Não conseguiu executar a InserirListaVeiculos", Excecao = ex });

                listaVeiculos.ListaVeiculo_ID = 0;

                return listaVeiculos;
            }
        }

        /// <summary>
        /// Realiza integração do arquivo de Loading ou PackingList
        /// </summary>
        /// <param name="ListaVeiculo_ID"></param>
        /// <param name="Tipo">Tipo: "P" = Packing, "L" = Loading</param>
        /// <param name="files">Arquivo enviado</param>
        /// <param name="configuracao"></param>
        /// <returns></returns>
        public static bool IntegrarArquivoLista(int ListaVeiculo_ID, string Tipo, ICollection<IFormFile> files, Configuracao configuracao)
        {
            try
            {
                string path = "";
                string serverpath = configuracao.PastaUploadListas;
                var file = files.FirstOrDefault();
                string mesdia = DateTime.Now.ToString("MMdd");
                string ano = DateTime.Now.ToString("yyyy");

                if (Tipo == "P")
                {
                    path = Path.Combine(serverpath, "Arquivos", "PackingList", ano, mesdia, Convert.ToString(ListaVeiculo_ID), file.FileName);
                }

                else if (Tipo == "L")
                {
                    path = Path.Combine(serverpath, "Arquivos", "LoadingList", ano, mesdia, Convert.ToString(ListaVeiculo_ID), file.FileName);
                }

                else if (Tipo == "D")
                {
                    path = Path.Combine(serverpath, "Arquivos", "DischargingList", ano, mesdia, Convert.ToString(ListaVeiculo_ID), file.FileName);
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
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Erro, Mensagem = $"Não conseguiu executar IntegrarArquivoLoadingList", Excecao = ex });
                return false;
            }
        }

    }
}
