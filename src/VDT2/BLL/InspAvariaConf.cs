﻿using System;
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
        public static List<Models.InspAvaria_Conf> ListarAvarias_Conf(int LocalInspecao_ID, int LocalCheckPoint_ID, DateTime Data, Configuracao configuracao)
        {
            List<Models.InspAvaria_Conf> listaInspAvaria_Conf = new List<Models.InspAvaria_Conf>();

            try { 
            listaInspAvaria_Conf = DAL.InspAvaria.InspAvariaConf(LocalInspecao_ID, LocalCheckPoint_ID, Data, configuracao);
            return listaInspAvaria_Conf;
            }

            catch
            {
                //fazer tratamento de erro
                return listaInspAvaria_Conf;
            }
        }
    }
}