// <copyright file="ListaVeiculos.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Amauri Rodrigues & Yuri Vasconcelos</author>
// <email>amauri.rodrigues@grupoasserth.com.br | yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>Classe referente a camada de acesso aos dados - ListaVeiculos</summary>


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
/// Camada de acesso aos dados ListaVeiculos
/// </summary>
namespace VDT2.DAL
{
    public class ListaVeiculos
    {
        /// <summary>
        /// Insere a lista de veiculos - VIN17  no banco de dados via prcedure ListaVeiculos_Ins
        /// </summary>
        /// <param name="listaVeiculos">Dados referentes a Lista</param>
        /// <param name="configuracao">Dados da configuração informada</param>
        /// <returns></returns>
        public static Models.ListaVeiculos Inserir(Models.ListaVeiculos listaVeiculos, Configuracao configuracao)
        {
            string nomeStoredProcedure = "ListaVeiculos_Ins";

            try
            {
                SqlParameter parmClienteID = new SqlParameter("@p_Cliente_ID", SqlDbType.Int)
                {
                    Value = listaVeiculos.Cliente_ID
                };

                SqlParameter parmUsuarioID = new SqlParameter("@p_Usuario_ID", SqlDbType.Int)
                {
                    Value = listaVeiculos.Usuario_ID
                };

                SqlParameter parmNomeArquivo = new SqlParameter("@p_NomeArquivo", SqlDbType.VarChar)
                {
                    Value = listaVeiculos.NomeArquivo
                };

                SqlParameter parmLocalInspecaoID = new SqlParameter("@p_LocalInspecao_ID", SqlDbType.Int)
                {
                    Value = listaVeiculos.LocalInspecao_ID
                };

                SqlParameter parmLocalCheckPointID = new SqlParameter("@p_LocalCheckPoint_ID", SqlDbType.Int)
                {
                    Value = listaVeiculos.LocalCheckPoint_ID
                };

                SqlParameter parmTipo = new SqlParameter("@p_Tipo", SqlDbType.Char)
                {
                    Value = listaVeiculos.Tipo
                };

                SqlParameter parmLote = new SqlParameter("@p_Lote", SqlDbType.Char)
                {
                    Value = listaVeiculos.Lote
                };


                SqlParameter parmListaVeiculo_ID = new SqlParameter("@p_ListaVeiculo_ID", SqlDbType.Int)
                {
                    Value = 1,
                    Direction = ParameterDirection.Output
                };

                SqlParameter parmLoteID = new SqlParameter("@p_Lote_ID", SqlDbType.Int)
                {
                    Value = 1,
                    Direction = ParameterDirection.Output
                };


                SqlParameter[] parametros = new SqlParameter[]
                {
                    parmClienteID,
                    parmUsuarioID,
                    parmNomeArquivo,
                    parmLocalInspecaoID,
                    parmLocalCheckPointID,
                    parmTipo,
                    parmLote,
                    parmListaVeiculo_ID,
                    parmLoteID
                };

                string chamada = $"{nomeStoredProcedure} {parmClienteID.ParameterName}, {parmUsuarioID.ParameterName}, {parmNomeArquivo.ParameterName}, {parmLocalInspecaoID.ParameterName}, {parmLocalCheckPointID.ParameterName}, {parmTipo.ParameterName}, {parmLote.ParameterName}, {parmListaVeiculo_ID.ParameterName} out, {parmLoteID.ParameterName} out";

                using (var contexto = new GeralDbContext(configuracao))
                {
                    contexto.Database.ExecuteSqlCommand(chamada, parametros);

                    listaVeiculos.ListaVeiculo_ID = (int)parmListaVeiculo_ID.Value;
                    listaVeiculos.Lote_ID = (int)parmLoteID.Value;

                    #region gravalogInformacao
                    Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Informacao,
                        Mensagem = $"ListaVeiculos.Inserir registrado com sucesso - ListaVeiculo_ID = {listaVeiculos.ListaVeiculo_ID}"
                    });
                    #endregion

                    return listaVeiculos;
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

                return listaVeiculos;
            }
        }
    }
}
