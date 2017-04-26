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
        public string GerarExcelInspecao(string NomeUsuario, List<InspAvaria_Cons> DadosConsulta, Configuracao configuracao, string Scheme, Microsoft.AspNetCore.Http.HostString Host)
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
                    using (ExcelPackage arquivoExcel = new ExcelPackage(file))
                    {
                        // Adiciona um novo worksheet (planilha) no excel
                        ExcelWorksheet worksheet = arquivoExcel.Workbook.Worksheets.Add("DadosConsulta");

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
                        worksheet.Cells[1, 18].Value = "TipoAvaria";
                        worksheet.Cells[1, 19].Value = "HorasReparo";
                        worksheet.Cells[1, 20].Value = "Custo";


                        //Muda o estilo do header
                        for (int i = 1; i < 21; i++)
                        {
                            //Setando peso do texto
                            worksheet.Cells[1, i].Style.Font.Bold = true;

                            //setando tamanho
                            worksheet.Cells[1, i].Style.Font.Size = 12;

                            //setando cor do texto
                            worksheet.Cells[1, i].Style.Font.Color.SetColor(System.Drawing.Color.White);

                            //Setando bordas
                            //worksheet.Cells[1, i].Style.Border.Right.Style =
                            //    worksheet.Cells[1, i].Style.Border.Bottom.Style =
                            //    worksheet.Cells[1, i].Style.Border.Left.Style = 
                            //    worksheet.Cells[1, i].Style.Border.Top.Style =  ExcelBorderStyle.Thick;

                            //Setando background-color
                            worksheet.Cells[1, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[1, i].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DarkBlue);
                        }

                        //Altura da primeira linha
                        worksheet.Row(1).Height = 26;

                        //Alinhamento horizontal da primeira linha
                        worksheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                        //Alinhamento Vertical da primeira linha
                        worksheet.Row(1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                        //Alinhamento vertical e horizontal de todas as linhas
                        for (int j = 0; j < DadosConsulta.Count + 2; j++)
                        {
                            worksheet.Row(j + 1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            worksheet.Row(j + 1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        }


                        //largura das colunas
                        worksheet.Column(1).Width = 11; //Data
                        worksheet.Column(2).Width = 18; //VIN
                        worksheet.Column(3).Width = 10; //VIN_6
                        worksheet.Column(5).Width = 22; //CheckPoint
                        worksheet.Column(6).Width = 24; //Transportador
                        worksheet.Column(7).Width = 16; //FrotaViagem
                        worksheet.Column(8).Width = 12; //Navio
                        worksheet.Column(9).Width = 12; //Lote
                        worksheet.Column(10).Width = 12; //Marca
                        worksheet.Column(11).Width = 12; //Modelo
                        worksheet.Column(12).Width = 28; //Área
                        worksheet.Column(13).Width = 22; //Condição
                        worksheet.Column(14).Width = 16; //Dano
                        worksheet.Column(15).Width = 12; //Gravidade
                        worksheet.Column(16).Width = 16; //Quadrante
                        worksheet.Column(17).Width = 24; //Severidade
                        worksheet.Column(18).Width = 12; //TipoAvaria
                        worksheet.Column(19).Width = 12; //HorasReparo
                        worksheet.Column(20).Width = 12; //Custo


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
                            worksheet.Cells[i + 2, 18].Value = DadosConsulta[i].FabricaTransporte;
                            worksheet.Cells[i + 2, 19].Value = DadosConsulta[i].HorasReparo;
                            worksheet.Cells[i + 2, 20].Value = DadosConsulta[i].Custo;
                        }

                        //Salvar
                        arquivoExcel.Save();

                        return URL;
                    }
                }
                catch (Exception ex)
                {

                    Diag.Log.Grava(new Diag.LogItem
                    {
                        Mensagem = $"Erro ao Gerar Arquivo Excel - {ex}",
                        Nivel = Diag.Nivel.Erro
                    });

                    return ex.Message.ToString();
                }





            }
        }
    }
}