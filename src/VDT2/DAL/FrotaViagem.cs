// <copyright file="FrotaViagem.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Grupo Asserth</author>
// <email>yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>Classe referente a camada de acesso aos dados - FrotaViagem</summary>


using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;


/// <summary>
/// Camada de acesso aos dados  FrotaViagem
/// </summary>
namespace VDT2.DAL
{
    public class FrotaViagem
    {

        /// <summary>
        /// Realiza a listagem de frotaviagem no banco de dados executando a procedure: FrotaViagem_lst
        /// </summary>
        /// <param name="tipoTransportador"></param>
        /// <param name="transportadorId"></param>
        /// <param name="configuracao"></param>
        /// <returns>Retorna uma lista com todas as frotas viagens</returns>
        public static List<Models.FrotaViagem> Listar(string tipoTransportador, int transportadorId, VDT2.Models.Configuracao configuracao)
        {

            List<Models.FrotaViagem> listaFrotaViagem;

            string nomeStoredProcedure = "FrotaViagem_Lst";

            try
            {
                SqlParameter parmTipo = new SqlParameter("@p_Tipo", SqlDbType.Char)
                {
                    Value = tipoTransportador
                };

                SqlParameter parmTranportadorId = new SqlParameter("@p_Tranportador_ID", SqlDbType.Int)
                {
                    Value = transportadorId
                };

                SqlParameter parmAtivos = new SqlParameter("@p_Ativos", SqlDbType.Bit)
                {
                    Value = 1
                };

                SqlParameter[] parametros = new SqlParameter[]
                {
                    parmTipo,
                    parmTranportadorId,
                    parmAtivos
                };

                string chamada = $"{nomeStoredProcedure} {parmTipo.ParameterName} , {parmTranportadorId.ParameterName}, {parmAtivos.ParameterName}";

                using (var contexto = new GeralDbContext(configuracao))
                {
                    listaFrotaViagem = contexto.FrotaViagem.FromSql(chamada, parametros).ToList();

                    #region gravalogResultado
                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"FrotaViagem.Listar realizado com sucesso:  Registros encontrados: {listaFrotaViagem.Count()}"
                        });
                    #endregion  
                }
            }

            catch (System.Exception ex)
            {
                #region gravalogErro
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar a procedure {nomeStoredProcedure}",
                        Excecao = ex
                    });
                throw;
                #endregion
            }
            return listaFrotaViagem;
        }

        /// <summary>
        /// Realiza a inserção de uma nova FrotaViagem no banco de dados executando a procedure: FrotaViagem_Ins
        /// </summary>
        /// <param name="transportadorId"></param>
        /// <param name="nome"></param>
        /// <param name="configuracao"></param>
        /// <returns>retorna o ID do novo registro referente ao frotaviagem no banco de dados</returns>
        public static int Inserir(int transportadorId, string nome, VDT2.Models.Configuracao configuracao)
        {
            int frotaViagemId = 0;
            string nomeStoredProcedure = "FrotaViagem_Ins";

            try
            {
                SqlParameter parmtransportadorId = new SqlParameter("@p_Transportador_ID", SqlDbType.VarChar)
                {
                    Value = transportadorId
                };

                SqlParameter parmNome = new SqlParameter("@p_Nome", SqlDbType.VarChar)
                {
                    Value = nome
                };

                SqlParameter parmFrotaViagemId = new SqlParameter("@p_FrotaViagem_ID", SqlDbType.Int)
                {
                    Value = 1,
                    Direction = ParameterDirection.Output
                };

                SqlParameter[] parametros = new SqlParameter[]
                {
                    parmtransportadorId,
                    parmNome,
                    parmFrotaViagemId
                };

                string chamada = $"{nomeStoredProcedure} {parmtransportadorId.ParameterName}, {parmNome.ParameterName}, {parmFrotaViagemId.ParameterName} out";

                using (var contexto = new GeralDbContext(configuracao))
                {

                    contexto.Database.ExecuteSqlCommand(chamada, parametros);

                    frotaViagemId = (int)parmFrotaViagemId.Value;

                    #region gravalogInformacao
                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"FrotaViagemRepositorio.Inserir realizado com sucesso:  Dados atualizados | FrotaViagem_ID: {frotaViagemId}"
                        });
                    #endregion
                }
            }

            catch (System.Exception ex)
            {
                #region gravalogErro
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar a procedure {nomeStoredProcedure} | Parametros: transportadorId {transportadorId}  | nome {nome}",
                        Excecao = ex
                    });
                throw;
                #endregion
            }

            return frotaViagemId;
        }

        /// <summary>
        /// Metodo para selecionar um frotaviagem do banco de dados
        /// </summary>
        /// <param name="frota_ID"></param>
        /// <param name="configuracao"></param>
        /// <returns>Retorna todos os dados da frotaviagem informada</returns>
        public static Models.FrotaViagem Selecionar(int? frota_ID, VDT2.Models.Configuracao configuracao)
        {
            Models.FrotaViagem frotaViagem = new Models.FrotaViagem();
            string nomeStoredProcedure = "FrotaViagem_Sel";

            try
            {
                SqlParameter parmFrotaViagem_ID = new SqlParameter("@p_FrotaViagem_ID", SqlDbType.Int)
                {
                    Value = frota_ID
                };

                SqlParameter[] parametros = new SqlParameter[]
                {
                    parmFrotaViagem_ID
                };

                string chamada = $"{nomeStoredProcedure} {parmFrotaViagem_ID.ParameterName}";

                using (var contexto = new GeralDbContext(configuracao))

                {
                    frotaViagem = contexto.FrotaViagem.FromSql(chamada, parametros).FirstOrDefault();

                    return frotaViagem;
                }

            }
            catch (System.Exception ex)
            {

                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar a procedure {nomeStoredProcedure}",
                        Excecao = ex
                    });
                throw;
            }
        }

    }
}

