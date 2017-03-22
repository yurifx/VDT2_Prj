﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VDT2.Models;


namespace VDT2.BLL
{
    public class Conferencia
    {

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


                var caminho = configuracao.PastaUploadListas;

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
