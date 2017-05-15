// <copyright file="ConferenciaController.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Grupo Asserth</author>
// <email>yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>Controllers de Conferência</summary>

using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDT2.Models;


/// <summary>
/// Classe responsável por gerar Excel
/// </summary>
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
        public bool GerarExcelInspecao(string NomeUsuario, List<InspAvaria_Cons> DadosConsulta, Configuracao configuracao, string Scheme, Microsoft.AspNetCore.Http.HostString Host)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"NomeUsuario  - {NomeUsuario}");
            sb.Append($"DataInicio  - {DadosConsulta.FirstOrDefault().DataInicio}");
            sb.Append($"DataFinal  - {DadosConsulta.FirstOrDefault().DataInicio}");
            sb.Append($"Scheme -  {Scheme}");
                        
            Diag.Log.Grava(new Diag.LogItem
            {
                Mensagem = sb.ToString(),
                Nivel = Diag.Nivel.Informacao
            });

            try
            {
                string caminhoServidor = System.IO.Path.GetTempPath();

                string sFileName = $"{NomeUsuario}_RelatorioConsulta.xlsx";

                string URL = $"{Scheme}/{Host}/{sFileName}";

                FileInfo file = new FileInfo(Path.Combine(caminhoServidor, sFileName));
                if (file.Exists)
                {
                    try
                    {
                        file.Delete();
                        file = new FileInfo(Path.Combine(caminhoServidor, sFileName));
                        Diag.Log.Grava(new Diag.LogItem
                        {
                            Mensagem = $"Arquivo temporário existente no servidor, realizou delete.",
                            Nivel = Diag.Nivel.Informacao
                        });

                    }
                    catch (Exception ex)
                    {
                        Diag.Log.Grava(new Diag.LogItem
                        {
                            Mensagem = $"Erro ao Gerar Arquivo Excel - {ex}",
                            Nivel = Diag.Nivel.Erro
                        });
                        return false;
                    }
                }


                //Criando oarquivo excel
                using (ExcelPackage arquivoExcel = new ExcelPackage(file))
                {
                    // Adiciona um novo worksheet (planilha) no excel
                    ExcelWorksheet worksheet = arquivoExcel.Workbook.Worksheets.Add("DadosConsulta");

                    //Cabeçalhos
                    worksheet.Cells["A1"].Value = "Data";
                    worksheet.Cells["B1"].Value = "VIN";
                    worksheet.Cells["C1"].Value = "VIN_6";
                    worksheet.Cells["D1"].Value = "Local";
                    worksheet.Cells["E1"].Value = "CheckPoint";
                    worksheet.Cells["F1"].Value = "Transportador";
                    worksheet.Cells["G1"].Value = "Navio";
                    worksheet.Cells["H1"].Value = "Frota | Viagem";
                    worksheet.Cells["I1"].Value = "Lote";
                    worksheet.Cells["J1"].Value = "Marca";
                    worksheet.Cells["K1"].Value = "Modelo";
                    worksheet.Cells["L1"].Value = "Área";
                    worksheet.Cells["M1"].Value = "Local";
                    worksheet.Cells["N1"].Value = "Lado";
                    worksheet.Cells["O1"].Value = "Dano";
                    worksheet.Cells["P1"].Value = "Severidade";
                    worksheet.Cells["Q1"].Value = "Quadrante";
                    worksheet.Cells["R1"].Value = "Gravidade";
                    worksheet.Cells["S1"].Value = "Condição";
                    worksheet.Cells["T1"].Value = "Tipo Avaria";
                    worksheet.Cells["U1"].Value = "Horas Reparo";
                    worksheet.Cells["V1"].Value = "Custo Reparo";
                    worksheet.Cells["W1"].Value = "Substituição Peça";
                    worksheet.Cells["X1"].Value = "Valor Peça";
                    worksheet.Cells["Y1"].Value = "Custo Total";


                    //Setando tipo do texto
                    worksheet.Cells["A1:Y1"].Style.Font.Bold = true;

                    //setando tamanho
                    worksheet.Cells["A1:Y1"].Style.Font.Size = 12;

                    //setando cor do texto
                    worksheet.Cells["A1:Y1"].Style.Font.Color.SetColor(System.Drawing.Color.White);

                    //Setando background-color
                    worksheet.Cells["A1:Y1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells["A1:Y1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DarkBlue);

                    //Alinhamento
                    worksheet.Cells["A1:Y300"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells["A1:Y300"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                    //Altura da primeira linha
                    worksheet.Row(1).Height = 26;


                    //Largura das colunas
                    worksheet.Column(1).Width = 11; 
                    worksheet.Column(2).Width = 20; 
                    worksheet.Column(3).Width = 10; 
                    worksheet.Column(4).Width = 22; 
                    worksheet.Column(5).Width = 24; 
                    worksheet.Column(6).Width = 16; 
                    worksheet.Column(7).Width = 12; 
                    worksheet.Column(8).Width = 20; 
                    worksheet.Column(9).Width = 12; 
                    worksheet.Column(10).Width = 16;
                    worksheet.Column(11).Width = 28;
                    worksheet.Column(12).Width = 16;
                    worksheet.Column(13).Width = 16;
                    worksheet.Column(14).Width = 16;
                    worksheet.Column(15).Width = 16;
                    worksheet.Column(16).Width = 26;
                    worksheet.Column(17).Width = 20;
                    worksheet.Column(18).Width = 18;
                    worksheet.Column(19).Width = 18;
                    worksheet.Column(20).Width = 22;
                    worksheet.Column(21).Width = 18;
                    worksheet.Column(22).Width = 16;
                    worksheet.Column(23).Width = 20;
                    worksheet.Column(24).Width = 18;
                    worksheet.Column(25).Width = 18;

                    worksheet.Cells["Y2:Y1000"].Style.Numberformat.Format = "R$#,##0.00";
                    worksheet.Cells["X2:X1000"].Style.Numberformat.Format = "R$#,##0.00";
                    worksheet.Cells["V2:V1000"].Style.Numberformat.Format = "R$#,##0.00";



                    //Adicionar os valores nos campos;
                    for (int i = 0; i < DadosConsulta.Count; i++)
                    {
                        worksheet.Cells[i + 2, 1].Value = DadosConsulta[i].Data.ToString("dd-MM-yyyy");
                        worksheet.Cells[i + 2, 2].Value = DadosConsulta[i].VIN;
                        worksheet.Cells[i + 2, 3].Value = DadosConsulta[i].VIN_6;
                        worksheet.Cells[i + 2, 4].Value = DadosConsulta[i].LocalNome;
                        worksheet.Cells[i + 2, 5].Value = DadosConsulta[i].CheckPointNome;
                        worksheet.Cells[i + 2, 6].Value = DadosConsulta[i].TransportadorNome;
                        worksheet.Cells[i + 2, 7].Value = DadosConsulta[i].NavioNome;
                        worksheet.Cells[i + 2, 8].Value = DadosConsulta[i].FrotaViagemNome;
                        worksheet.Cells[i + 2, 9].Value = DadosConsulta[i].LoteNome;
                        worksheet.Cells[i + 2, 10].Value = DadosConsulta[i].MarcaNome;
                        worksheet.Cells[i + 2, 11].Value = DadosConsulta[i].ModeloNome;
                        worksheet.Cells[i + 2, 12].Value = DadosConsulta[i].Area_Pt;
                        worksheet.Cells[i + 2, 13].Value = DadosConsulta[i].Local_Pt;

                        if (String.IsNullOrEmpty(DadosConsulta[i].Lado_Pt))
                        {
                            worksheet.Cells[i + 2, 14].Value = "N/A";
                        }
                        else
                        {
                            worksheet.Cells[i + 2, 14].Value = DadosConsulta[i].Lado_Pt;
                        }

                        worksheet.Cells[i + 2, 15].Value = DadosConsulta[i].Dano_Pt;
                        worksheet.Cells[i + 2, 16].Value = DadosConsulta[i].Severidade_Pt;
                        worksheet.Cells[i + 2, 17].Value = DadosConsulta[i].Quadrante_Pt;
                        worksheet.Cells[i + 2, 18].Value = DadosConsulta[i].Gravidade_Pt;
                        worksheet.Cells[i + 2, 19].Value = DadosConsulta[i].Condicao_Pt;


                        if (DadosConsulta[i].FabricaTransporte =="F")
                        {
                            worksheet.Cells[i + 2, 20].Value = "Fábrica";
                        }
                        else if (DadosConsulta[i].FabricaTransporte == "T")
                        {
                            worksheet.Cells[i + 2, 20].Value = "Transporte";
                        }

                        worksheet.Cells[i + 2, 21].Value = DadosConsulta[i].HorasReparo;
                        worksheet.Cells[i + 2, 22].Value = DadosConsulta[i].CustoReparo;

                        if (DadosConsulta[i].SubstituicaoPeca == true)
                        {
                            worksheet.Cells[i + 2, 23].Value = "Sim";
                        }
                        else
                        {
                            worksheet.Cells[i + 2, 23].Value = "Não";

                        }


                        worksheet.Cells[i + 2, 24].Value = DadosConsulta[i].ValorPeca;
                        worksheet.Cells[i + 2, 25].Value = DadosConsulta[i].CustoTotal;
                    }

                    //Salvar arquivo Excel
                    try
                    {
                        arquivoExcel.Save();
                    }
                    catch (Exception ex)
                    {
                        Diag.Log.Grava(new Diag.LogItem
                        {
                            Mensagem = $"Erro ao Salvar Arquivo Excel - {ex}",
                            Nivel = Diag.Nivel.Erro
                        });
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {

                Diag.Log.Grava(new Diag.LogItem
                {
                    Mensagem = $"Erro ao Gerar Arquivo Excel - {ex}",
                    Nivel = Diag.Nivel.Erro
                });

                return false;
            }
        }
    }

}