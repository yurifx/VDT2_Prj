// <copyright file="ListaVeiculosVin.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Amauri Rodrigues & Yuri Vasconcelos</author>
// <email>amauri.rodrigues@grupoasserth.com.br</email>
// <date>2017-02-19</date>
// <summary>Classe de acesso aos dados - ListaVeiculosVin</summary>


//Dependencias
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;

/// <summary>
/// Camada de acesso aos dados - ListaVeiculosVin
/// </summary>
namespace VDT2.DAL
{
    public class ListaVeiculosVin
    {
        /// <summary>
        /// Insere o registro do VIN com 17 dígitos na tabela ListaVeículosVin_Ins via arquivo recebido pelo usuário
        /// </summary>
        /// <param name="VeiculoVIN"></param>
        /// <param name="configuracao"></param>
        /// <returns></returns>
        public static Models.ListaVeiculosVin Inserir(Models.ListaVeiculosVin VeiculoVIN, int Lote_ID, Configuracao configuracao)
        {
            string nomeStoredProcedure = "ListaVeiculosVin_Ins";
            try
            {
                SqlParameter parmListaVeiculos_ID = new SqlParameter("@p_ListaVeiculos_ID", SqlDbType.Int)
                {
                    Value = VeiculoVIN.ListaVeiculos_ID
                };

                SqlParameter parmVIN = new SqlParameter("@p_VIN", SqlDbType.Char)
                {
                    Value = VeiculoVIN.VIN
                };


                SqlParameter parmLoteID = new SqlParameter("@p_Lote_ID", SqlDbType.Char)
                {
                    Value = Lote_ID
                };

                SqlParameter parmListaVeiculosVin_ID = new SqlParameter("@p_ListaVeiculosVin_ID", SqlDbType.Int)
                {
                    Value = 1,
                    Direction = ParameterDirection.Output
                };

                SqlParameter[] parametros = new SqlParameter[]
                {
                    parmListaVeiculos_ID,
                    parmVIN,
                    parmLoteID,
                    parmListaVeiculosVin_ID
                };

                string chamada = $"{nomeStoredProcedure} {parmListaVeiculos_ID.ParameterName}, {parmVIN.ParameterName}, {parmLoteID.ParameterName}, {parmListaVeiculosVin_ID.ParameterName} out";

                using (var contexto = new GeralDbContext(configuracao))
                {
                    contexto.Database.ExecuteSqlCommand(chamada, parametros);

                    #region gravalogInformacao
                    Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Informacao,
                        Mensagem = $"ListaVeiculosVin.Inserir registrado om sucesso"
                    });
                    #endregion

                    return VeiculoVIN;
                }
            }

            catch (Exception ex)
            {
                #region gravalogErro
                Diag.Log.Grava(
               new Diag.LogItem()
               {
                   Nivel = Diag.Nivel.Erro,
                   Mensagem = $"Erro ao executar ListaVeiculos.Inserir: Erro:  {ex}"
               });
                #endregion
                throw;
            }
        }
    }
}


