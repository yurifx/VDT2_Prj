using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;

namespace VDT2.BLL
{
    public class UploadExcel
    {
        public static int SalvarArquivo(ICollection<IFormFile> files, Configuracao configuracao) {
            try { 
            string serverpath = configuracao.PastaUploadListas;
            var file = files.FirstOrDefault();

            var path = Path.Combine(serverpath, "Arquivos");

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
            catch(Exception ex)
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
