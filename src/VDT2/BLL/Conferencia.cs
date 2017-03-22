using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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


        public static int IntegrarArquivoLoadingList(char tipo, ICollection<IFormFile> files, Configuracao configuracao)
        {
            try
            {
                var file = files.FirstOrDefault();
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
