// <copyright file="InspAvariaConf.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Amauri Rodrigues & Yuri Vasconcelos</author>
// <email>amauri.rodrigues@grupoasserth.com.br | yuri.vasconcelos@grupoasserth.com.br</email>
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
                #region gravalogErro
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu Executar ListarAvarias_Conf. Erro: {ex}",
                        Excecao = ex
                    });
                #endregion
                return listaInspAvaria_Conf;
            }
        }
    }
}
