using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;



namespace VDT2.BLL
{
    public class GerarExcelConsultas
    {

        /// <summary>
        /// Gerar excel referente as consultas
        /// </summary>
        /// <param name="NomeUsuario">Nome do usuário que solicitou o arquivo</param>
        /// <param name="DadosConsulta">Dados da consulta</param>
        /// <param name="configuracao">Configuracao</param>
        /// <param name="Scheme">Request.Scheme</param>
        /// <param name="Host">Request.Host</param>
        /// <returns>URL contendo o diretório do arquivo excel</returns>
        public string GerarExcel(string NomeUsuario, List<InspAvaria_Cons> DadosConsulta, Configuracao configuracao, string Scheme, Microsoft.AspNetCore.Http.HostString Host)
        {
            {
                try
                {
                    string caminhoServidor = System.IO.Path.GetTempPath();

                    //if (!Directory.Exists(caminhoServidor))
                    //{
                    //    Directory.CreateDirectory(caminhoServidor);
                    //}

                    string sFileName = $"{NomeUsuario}_RelatorioConsulta.xlsx";

                    string URL = $"{Scheme}/{Host}/{sFileName}";

                    FileInfo file = new FileInfo(Path.Combine(caminhoServidor, sFileName));
                    if (file.Exists)
                    {
                        file.Delete();
                        file = new FileInfo(Path.Combine(caminhoServidor, sFileName));
                    }
                    using (ExcelPackage package = new ExcelPackage(file))
                    {
                        // Adiciona um novo worksheet (planilha) no excel
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("DadosConsulta");

                        //Cabeçalhos
                        worksheet.Cells[1, 1].Value = "Data";
                        worksheet.Cells[1, 2].Value = "VIN";
                        worksheet.Cells[1, 3].Value = "VIN_6";
                        worksheet.Cells[1, 4].Value = "Local";
                        worksheet.Cells[1, 5].Value = "CheckPoint";
                        worksheet.Cells[1, 6].Value = "Transportador";
                        worksheet.Cells[1, 7].Value = "Frota|Viagem";
                        worksheet.Cells[1, 8].Value = "Navio";
                        worksheet.Cells[1, 9].Value = "Lote";
                        worksheet.Cells[1, 10].Value = "Marca";
                        worksheet.Cells[1, 11].Value = "Modelo";
                        worksheet.Cells[1, 12].Value = "Área";
                        worksheet.Cells[1, 13].Value = "Condição";
                        worksheet.Cells[1, 14].Value = "Dano";
                        worksheet.Cells[1, 15].Value = "Gravidade";
                        worksheet.Cells[1, 16].Value = "Quadrante";
                        worksheet.Cells[1, 17].Value = "Severidade";
                        worksheet.Cells[1, 18].Value = "HorasReparo";
                        worksheet.Cells[1, 19].Value = "Custo";


                        //Muda o estilo do header
                        for (int i = 1; i < 20; i++)
                        {
                            worksheet.Cells[1, i].Style.Font.Bold = true;
                        }

                        //Adicionar os valores nos campos;
                        for (int i = 0; i < DadosConsulta.Count; i++)
                        {
                            worksheet.Cells[i + 2, 1].Value = DadosConsulta[i].Data.ToString("dd-MM-yyyy");
                            worksheet.Cells[i + 2, 2].Value = DadosConsulta[i].VIN;
                            worksheet.Cells[i + 2, 3].Value = DadosConsulta[i].VIN_6;
                            worksheet.Cells[i + 2, 4].Value = DadosConsulta[i].LocalNome;
                            worksheet.Cells[i + 2, 5].Value = DadosConsulta[i].CheckPointNome;
                            worksheet.Cells[i + 2, 6].Value = DadosConsulta[i].TransportadorNome;
                            worksheet.Cells[i + 2, 7].Value = DadosConsulta[i].FrotaViagemNome;
                            worksheet.Cells[i + 2, 8].Value = DadosConsulta[i].NavioNome;
                            worksheet.Cells[i + 2, 9].Value = DadosConsulta[i].LoteNome;
                            worksheet.Cells[i + 2, 10].Value = DadosConsulta[i].MarcaNome;
                            worksheet.Cells[i + 2, 11].Value = DadosConsulta[i].ModeloNome;
                            worksheet.Cells[i + 2, 12].Value = DadosConsulta[i].Area_Pt;
                            worksheet.Cells[i + 2, 13].Value = DadosConsulta[i].Condicao_Pt;
                            worksheet.Cells[i + 2, 14].Value = DadosConsulta[i].Dano_Pt;
                            worksheet.Cells[i + 2, 15].Value = DadosConsulta[i].Gravidade_Pt;
                            worksheet.Cells[i + 2, 16].Value = DadosConsulta[i].Quadrante_Pt;
                            worksheet.Cells[i + 2, 17].Value = DadosConsulta[i].Severidade_Pt;
                            worksheet.Cells[i + 2, 18].Value = DadosConsulta[i].HorasReparo;
                            worksheet.Cells[i + 2, 19].Value = DadosConsulta[i].Custo;
                        }

                        //Salvar
                        package.Save();

                        return URL;
                    }
                }
                catch (Exception ex)
                {
                    //TODO Gravar no log excessão;
                    return ex.Message.ToString();
                }





            }
        }
    }
}