using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;

namespace VDT2.BLL
{
    public class UploadTxt
    {
/// <summary>
/// Salva o arquivo que o usuário enviou no input
/// </summary>
/// <param name="ListaVeiculo_ID">Após a inserção do cabeçalho no banco de dados, enviar o seu ID</param>
/// <param name="tipo">Tipo: "P" - PackingList,  "L" - LoadingList</param>
/// <param name="files">arquivo do usuário</param>
/// <param name="configuracao">configurações do appsettings</param>
/// <returns></returns>
        public static bool SalvarArquivo(int ListaVeiculo_ID, char tipo, ICollection<IFormFile> files, Configuracao configuracao)
        {
            try
            {
                string path = "";
                string serverpath = configuracao.PastaUploadListas;
                var file = files.FirstOrDefault();

                //Cria local para gravação do arquivo
                if (tipo == 'P')
                {
                    path = Path.Combine(serverpath, "Arquivos", "PackingList", Convert.ToString(ListaVeiculo_ID));
                }
                else
                {
                    path = Path.Combine(serverpath, "Arquivos", "LoadingList", Convert.ToString(ListaVeiculo_ID));
                }


                //1- Cria a pasta no servidor, caso não exista
                if (!Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                };

                using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                    return true;
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
                return false;
            }
        }
    }
}
