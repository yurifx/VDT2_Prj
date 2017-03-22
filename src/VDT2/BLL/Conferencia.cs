using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VDT2.Models;


namespace VDT2.BLL
{
    public class Conferencia
    {

        public static int IntegrarArquivoPackingList(ICollection<IFormFile> files, Configuracao configuracao)
        {
            try
            {
                var caminho = configuracao.PastaUploadListas;
                string nomeArquivo = files.FirstOrDefault().FileName;

                return 0;
            }
            catch (Exception ex)
            {
                //gravar log {ex}
                return -1;
            }
        }


        public static int IntegrarArquivoLoadingList(int ListaVeiculo_ID, char tipo, ICollection<IFormFile> files, Configuracao configuracao)
        {   
            try
            {
                string path = "";
                string serverpath = configuracao.PastaUploadListas;
                var file = files.FirstOrDefault();

                if (tipo == 'F')
                {
                    path = Path.Combine(serverpath, "Arquivos", "PackingList", Convert.ToString(ListaVeiculo_ID), file.FileName);
                }
                else
                {
                    path = Path.Combine(serverpath, "Arquivos", "LoadingList", Convert.ToString(ListaVeiculo_ID),  file.FileName);
                }
                


                string[] linhas = System.IO.File.ReadAllLines(path);

                foreach (var linha in linhas)
                {
                    Models.ListaVeiculosVin VeiculoVin = new Models.ListaVeiculosVin { ListaVeiculos_ID = ListaVeiculo_ID, VIN = linha };
                    DAL.ListaVeiculosVin.Inserir(VeiculoVin, configuracao);
                }


                var caminho = configuracao.PastaUploadListas;

                return 0;
            }
            catch (Exception ex)
            {
                //gravar log {ex}
                return -1;
            }
        }

    }
}
