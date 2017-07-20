// <copyright file="InspAvariaConf.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>GrupoAsserth</author>
// <email>yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>Classe referente a camada de negócios - InspAvariaConf</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;

/// <summary>
/// Classe responsável por manipular dados da conferencia de avarias
/// </summary>
namespace VDT2.BLL
{
    public class InspAvariaConf
    {

        /// <summary>
        /// Realiza a listagem de avarias recebendo os dados para conferência 
        /// </summary>
        /// <param name="LocalInspecao_ID">Local Inspeção</param>
        /// <param name="LocalCheckPoint_ID">Local CheckPoint a ser listado</param>
        /// <param name="Data">Data da Avaria</param>
        /// <param name="configuracao"></param>
        /// <returns>Lista de Avarias</returns>
        public static List<Models.InspAvaria_Conf> ListarAvarias_Conf(int Cliente_ID, int LocalInspecao_ID, int LocalCheckPoint_ID, DateTime Data, Configuracao configuracao)
        {
            List<Models.InspAvaria_Conf> listaInspAvaria_Conf = new List<Models.InspAvaria_Conf>();

            try
            {
                listaInspAvaria_Conf = DAL.InspAvaria.InspAvariaConf(Cliente_ID, LocalInspecao_ID, LocalCheckPoint_ID, Data, configuracao);
                return listaInspAvaria_Conf;
            }

            catch (Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem() { Nivel = Diag.Nivel.Erro, Mensagem = $"Não conseguiu Executar ListarAvarias_Conf", Excecao = ex });

                return listaInspAvaria_Conf;
            }
        }


        /// <summary>
        /// Realiza a listagem do Summary de avarias/veículos
        /// </summary>
        /// <param name="cliente_ID"></param>
        /// <param name="localInspecao_ID"></param>
        /// <param name="localCheckPoint_ID"></param>
        /// <param name="data"></param>
        /// <param name="configuracao"></param>
        /// <returns></returns>
        public static List<Models.Conferencia_Summary> ListarConferenciaSummary (int cliente_ID, int localInspecao_ID, int localCheckPoint_ID, DateTime data, Configuracao configuracao)
        {

            Diag.Log.Grava(new Diag.LogItem { Mensagem = $"ListarConferenciaSummary | parametros informados | Cliente_ID: {cliente_ID} | LocalInspecao_ID: {localInspecao_ID} | LocalCheckPoint: {localCheckPoint_ID} | data: {data}", Nivel = Diag.Nivel.Informacao });
            try
            {
                List<Models.Conferencia_Summary> listaConferenciaSummary = new List<Conferencia_Summary>();
                listaConferenciaSummary = DAL.Conferencia_Summary.Consultar(cliente_ID, localInspecao_ID, localCheckPoint_ID, data, configuracao);

                Diag.Log.Grava(new Diag.LogItem { Mensagem = $"ListarConferenciaSummary realizado com sucesso: {listaConferenciaSummary.Count()}", Nivel = Diag.Nivel.Informacao });
                return listaConferenciaSummary;

            }
            catch (Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem() { Nivel = Diag.Nivel.Erro, Mensagem = $"Não conseguiu Executar ListaConferenciaSummary", Excecao = ex });
                return null;
            }


        }
    }
}
