// <copyright file="UploadImagens.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Amauri Rodrigues & Yuri Vasconcelos</author>
// <email>amauri.rodrigues@grupoasserth.com.br | yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>Classe referente a camada de negócios - UploadImagens</summary>

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VDT2.DAL;
using VDT2.Models;


/// <summary>
/// Camada de negócios - UploadImagens
/// </summary>
namespace VDT2.BLL
{
    public class UploadImagens
    {
        /// <summary>
        /// Realizar upload de arquivos
        /// </summary>
        /// <param name="avaria_Id">ID da avaria - </param>
        /// <param name="files">Arquivos para upload</param>
        /// <param name="configuracao">Configuracao</param>
        /// <returns>Conseguiu realizar upload? True, false?</returns>
        public static bool UploadImagensAvaria(int avaria_Id, ICollection<IFormFile> files, Configuracao configuracao)
        {
            try
            {
                string serverpath = configuracao.PastaFotos;
                string extensao = "";

                //recebe todos dados da avaria e inspeção
                Models.InspAvaria inspAvaria = DAL.InspAvaria.Listar(avaria_Id, configuracao);
                Models.Inspecao inspecao = DAL.Inspecao.ListarPorId(inspAvaria.Inspecao_ID, configuracao);

                //preenche dados para formar o path de upload
                string _cliente_id = Convert.ToString(inspecao.Cliente_ID);
                string _inspecao_id = Convert.ToString(inspecao.Inspecao_ID);
                string _inspecao = Convert.ToString((int)DAL.InspVeiculo.ListarPorId(inspAvaria.InspVeiculo_ID, configuracao).Inspecao_ID);
                string _inspVeiculo = Convert.ToString(inspAvaria.InspVeiculo_ID);
                string _inspAvaria = Convert.ToString(inspAvaria.InspAvaria_ID);
                string _ano = inspecao.Data.ToString("yyyy");
                string _mesdia = inspecao.Data.ToString("MMdd");

                //concatena todos os dados de path
                var path = Path.Combine(configuracao.PastaFotos, "Imagens", "Avarias", _cliente_id, _ano, _mesdia, _inspecao, _inspVeiculo, _inspAvaria);


                //1- Cria a pasta no servidor desta avaria 
                if (!Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                };

                //verifica se existem arquivos nesta pasta
                string[] arquivos = System.IO.Directory.GetFiles(path);

                int arquivoMaiorNumeracao = 0;
                foreach (var arquivo in arquivos)
                {
                    int numeroArquivo = 0;

                    if (int.TryParse(Path.GetFileNameWithoutExtension(arquivo), out numeroArquivo))
                    {
                        if (numeroArquivo > arquivoMaiorNumeracao)
                        {
                            arquivoMaiorNumeracao = numeroArquivo;
                        }
                    }
                }

                //Arquivos recebidos do POST do usuário
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {

                        var tipoImagem = file.ContentType;
                        switch (tipoImagem)
                        {
                            case "image/jpeg":
                                {
                                    extensao = ".jpeg";
                                    break;
                                }
                            case "image/jpg":
                                {
                                    extensao = ".jpg";
                                    break;
                                }

                            case "image/png":
                                {
                                    extensao = ".png";
                                    break;
                                }

                            case "image/gif":
                                {
                                    extensao = ".gif";
                                    break;
                                }
                        }

                        string arquivoatual = String.Concat(Convert.ToString(arquivoMaiorNumeracao + 1), extensao);
                        arquivoMaiorNumeracao = arquivoMaiorNumeracao + 1;
                        using (var fileStream = new FileStream(Path.Combine(path, arquivoatual), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                            arquivoatual = arquivoatual + 1;
                        }
                    }
                }
                return true; //sem erros
            }
            catch (Exception ex)
            {
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar a UploadArquivos | Avaria: {avaria_Id} ",
                        Excecao = ex
                    });
                return false;
            }
        }

        /// <summary>
        /// Lista todas as imagens da avaria informada
        /// </summary>
        /// <param name="avaria_Id">ID da avaria</param>
        /// <param name="configuracao">Configuração</param>
        /// <returns>Lista de Avarias</returns>
        public static List<ImagemAvaria> Listar(int avaria_Id, Configuracao configuracao)
        {
            List<ImagemAvaria> listaimagens = new List<ImagemAvaria>();
            string serverpath = configuracao.PastaFotos;
            try
            {
                //Recebe todos os dados da Avaria e da Inspeção
                Models.InspAvaria inspAvaria = DAL.InspAvaria.Listar(avaria_Id, configuracao);
                var inspecao = DAL.Inspecao.ListarPorId(inspAvaria.Inspecao_ID, configuracao);

                //preenche dados para formar o path de upload
                string _cliente_id = Convert.ToString(inspecao.Cliente_ID);
                string _inspecao_id = Convert.ToString(inspecao.Inspecao_ID);
                string _inspecao = Convert.ToString((int)DAL.InspVeiculo.ListarPorId(inspAvaria.InspVeiculo_ID, configuracao).Inspecao_ID);
                string _inspVeiculo = Convert.ToString(inspAvaria.InspVeiculo_ID);
                string _inspAvaria = Convert.ToString(inspAvaria.InspAvaria_ID);
                string _ano = inspecao.Data.ToString("yyyy");
                string _mesdia = inspecao.Data.ToString("MMdd");

                //concatena todos os dados/strings do caminho
                var path = Path.Combine(configuracao.PastaFotos, "Imagens", "Avarias", _cliente_id, _ano, _mesdia, _inspecao, _inspVeiculo, _inspAvaria);

                //Recebe todos arquivos desta pasta
                string[] arquivos = System.IO.Directory.GetFiles(path);
                foreach (var arquivo in arquivos)
                {
                    try
                    {
                        ImagemAvaria imagem = new ImagemAvaria();
                        var imgsrc = Path.GetFileName(arquivo);
                        if (imgsrc != "")
                        {
                            imagem.Imagem = imgsrc;
                            imagem.Path = arquivo;
                            listaimagens.Add(imagem);
                        }
                    }
                    catch (Exception ex)
                    {
                        Diag.Log.Grava(
                            new Diag.LogItem()
                            {
                                Nivel = Diag.Nivel.Aviso,
                                Mensagem = $"Não conseguiu receber lista de imagens da avaria | Avaria: {_inspAvaria} | Veiculo: {_inspVeiculo} | Inspecao: {_inspecao}",
                                Excecao = ex
                            });
                        break;
                    }
                }
                return listaimagens;
            }

            catch (Exception ex)
            {
                Diag.Log.Grava(
                            new Diag.LogItem()
                            {
                                Nivel = Diag.Nivel.Aviso,
                                Mensagem = $"Não conseguiu receber lista de imagens da avaria - Exception {ex}",
                                Excecao = ex
                            });
                listaimagens.Add(new ImagemAvaria { Erro = true, MensagemErro = "Erro ao listar imagens, tente novamente mais tarde ou entre em contato com o suporte técnico" });
                return listaimagens;
            }
        }

        /// <summary>
        /// Deletar Imagem da Avaria
        /// </summary>
        /// <param name="inspAvaria_ID">ID da avaria</param>
        /// <param name="imagem">nome da imagem | ex: 1.jpg</param>
        /// <param name="configuracao">Configuração</param>
        public static void DeletarImagem(int inspAvaria_ID, string imagem, Configuracao configuracao)
        {
            try
            {
                string serverpath = configuracao.PastaFotos;
                Models.InspAvaria inspAvaria = DAL.InspAvaria.Listar(inspAvaria_ID, configuracao);
                var inspecao = DAL.Inspecao.ListarPorId(inspAvaria.Inspecao_ID, configuracao);

                string _cliente_id = Convert.ToString(inspecao.Cliente_ID);
                string _inspecao_id = Convert.ToString(inspecao.Inspecao_ID);
                string _inspecao = Convert.ToString((int)DAL.InspVeiculo.ListarPorId(inspAvaria.InspVeiculo_ID, configuracao).Inspecao_ID);
                string _inspVeiculo = Convert.ToString(inspAvaria.InspVeiculo_ID);
                string _inspAvaria = Convert.ToString(inspAvaria.InspAvaria_ID);

                string _ano = inspecao.Data.ToString("yyyy");
                string _mesdia = inspecao.Data.ToString("MMdd");

                var path = Path.Combine(configuracao.PastaFotos, "Imagens", "Avarias", _cliente_id, _ano, _mesdia, _inspecao, _inspVeiculo, _inspAvaria);
                var pathArquivo = Path.Combine(path, imagem);

                if (System.IO.File.Exists(pathArquivo))
                {
                    try
                    {
                        System.IO.File.Delete(pathArquivo);
                    }
                    catch (System.IO.IOException ex)
                    {
                        Diag.Log.Grava(
                            new Diag.LogItem()
                            {
                                Nivel = Diag.Nivel.Aviso,
                                Mensagem = $"Não conseguiu deletar imagem/foto: {inspAvaria_ID} | Imagem: {imagem}",
                                Excecao = ex
                            });
                        Console.WriteLine(ex.Message);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Aviso,
                        Mensagem = $"Não conseguiu receber lista de imagens da avaria | Avaria: {inspAvaria_ID} | Imagem: {imagem}",
                        Excecao = ex
                    });
                throw;
            }
        }

    }

}
