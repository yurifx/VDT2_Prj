// <copyright file="Inspecao.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Yuri Vasconcelos</author>
// <email>yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>Classe referente a camada de acesso aos dados - Inspecao</summary>


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
/// Camda de acesso aos dados - Inspecao
/// </summary>
namespace VDT2.DAL
{
    public class Inspecao
    {

        /// <summary>
        /// Insere uma nova inspecao no banco de dados - Inspecao_ins
        /// </summary>
        /// <param name="inspecao">Obj Inspecao contendo os dados da inspecao</param>
        /// <param name="configuracao">string de conexão com o banco de dados</param>
        /// <returns></returns>
        public static Models.Inspecao Inserir(Models.Inspecao inspecao, VDT2.Models.Configuracao configuracao)
        {
            string nomeStoredProcedure = "Inspecao_Ins";
            try
            {
                SqlParameter parmCliente_ID = new SqlParameter("@p_Cliente_ID", SqlDbType.Int)
                {
                    Value = inspecao.Cliente_ID
                };

                SqlParameter parmLocalInspecao_ID = new SqlParameter("@p_LocalInspecao_ID", SqlDbType.Int)
                {
                    Value = inspecao.LocalInspecao_ID
                };

                SqlParameter parmLocalCheckPointID = new SqlParameter("@p_LocalCheckPoint_ID", SqlDbType.Int)
                {
                    Value = inspecao.LocalCheckPoint_ID
                };

                SqlParameter parmTransportador_ID = new SqlParameter("@p_Transportador_ID", SqlDbType.Int)
                {
                    Value = inspecao.Transportador_ID
                };

                SqlParameter parmFrotaViagem_ID = new SqlParameter("@p_FrotaViagem_ID", SqlDbType.Int)
                {
                    Value = inspecao.FrotaViagem_ID
                };

                SqlParameter parmNavio_ID = null;

                if (!inspecao.Navio_ID.HasValue)
                {  //Verifica se é nulo, se for -> adiciona System.DbNull
                    parmNavio_ID = new SqlParameter("@p_Navio_ID", SqlDbType.Int)
                    {
                        Value = DBNull.Value
                    };
                }
                else
                {
                    parmNavio_ID = new SqlParameter("@p_Navio_ID", SqlDbType.Int)
                    {
                        Value = inspecao.Navio_ID.Value
                    };
                }

                SqlParameter parmUsuario_ID = new SqlParameter("@p_Usuario_ID", SqlDbType.Int)
                {
                    Value = 1
                };

                SqlParameter parmData = new SqlParameter("@p_Data", SqlDbType.DateTime)
                {
                    Value = System.DateTime.Today
                };

                SqlParameter parmInspecao_ID = new SqlParameter("@p_Inspecao_ID", SqlDbType.Int)
                {
                    Value = 1,
                    Direction = ParameterDirection.Output
                };

                SqlParameter[] parametros = new SqlParameter[]
                 {
                    parmCliente_ID,
                    parmLocalInspecao_ID,
                    parmLocalCheckPointID,
                    parmTransportador_ID,
                    parmFrotaViagem_ID,
                    parmNavio_ID,
                    parmUsuario_ID,
                    parmData,
                    parmInspecao_ID
                 };

                string chamada = $"{nomeStoredProcedure} {parmCliente_ID.ParameterName}, {parmLocalInspecao_ID.ParameterName}, {parmLocalCheckPointID.ParameterName}, {parmTransportador_ID.ParameterName}, {parmFrotaViagem_ID.ParameterName}, {parmNavio_ID.ParameterName}, {parmUsuario_ID.ParameterName}, {parmData.ParameterName}, {parmInspecao_ID.ParameterName} out";

                using (var contexto = new GeralDbContext(configuracao))
                {
                    contexto.Database.ExecuteSqlCommand(chamada, parametros);

                    inspecao.Inspecao_ID = (int)parmInspecao_ID.Value;

                    #region gravalogInformacao
                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"Inspecao.Inserir realizado com sucesso:  Dados inseridos | Inspecao_ID {inspecao.Inspecao_ID}, Cliente ID: {inspecao.Cliente_ID}, Local Inspeção: {inspecao.LocalInspecao_ID}, LocalCheckPoint: {inspecao.LocalCheckPoint_ID},  Transportador_ID {inspecao.Transportador_ID}, FrotaViagem: {inspecao.FrotaViagem_ID}, Navio ID: {inspecao.Navio_ID}"
                        });
                    #endregion

                    return inspecao;
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
                #endregion
                throw;
            }
        }

        /// <summary>
        /// Listar inspecao informando ID
        /// </summary>
        /// <param name="inspecao_ID">ID da inspeção</param>
        /// <param name="configuracao">string de conexão com o banco de dados</param>
        /// <returns>Models.Inspecao</returns>
        public static Models.Inspecao ListarPorId(int inspecao_ID, Configuracao configuracao)
        {
            Models.Inspecao inspecao = new Models.Inspecao();
            string nomeStoredProcedure = "Inspecao_Sel";

            try
            {
                SqlParameter parmInspecao_ID = new SqlParameter("@p_Inspecao_ID", SqlDbType.Int)
                {
                    Value = inspecao_ID
                };

                SqlParameter[] parametros = new SqlParameter[]
                {
                    parmInspecao_ID
                };

                string chamada = $"{nomeStoredProcedure} {parmInspecao_ID.ParameterName}";

                using (var contexto = new GeralDbContext(configuracao))

                {
                    inspecao = contexto.Inspecao.FromSql(chamada, parametros).FirstOrDefault();

                    #region gravalogInformacao
                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"Inspecao.ListarPorId realizado com sucesso:  Inspecao_ID = {inspecao_ID}"
                        });
                    #endregion

                    return inspecao;
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
                #endregion
                throw;
            }
        }
        /// <summary>
        /// Atualiza informações sobre a inspeção - Inspecao_Upd
        /// </summary>
        /// <param name="inspecao">Obj Inspecao contendo os dados da inspeção a ser atualizada</param>
        /// <param name="configuracao">string de conexão com o banco de dados</param>
        /// <returns>bool ERRO?</returns>
        public static Models.Inspecao Update(Models.Inspecao inspecao, VDT2.Models.Configuracao configuracao)
        {
            inspecao.Erro = false;
            inspecao.MensagemErro = "";

            string nomeStoredProcedure = "Inspecao_Upd";
            try
            {
                SqlParameter parmInspecao_ID = new SqlParameter("@p_Inspecao_ID", SqlDbType.Int)
                {
                    Value = inspecao.Inspecao_ID
                };

                SqlParameter parmCliente_ID = new SqlParameter("@p_Cliente_ID", SqlDbType.Int)
                {
                    Value = inspecao.Cliente_ID
                };

                SqlParameter parmLocalInspecao_ID = new SqlParameter("@p_LocalInspecao_ID", SqlDbType.Int)
                {
                    Value = inspecao.LocalInspecao_ID
                };

                SqlParameter parmLocalCheckPointID = new SqlParameter("@p_LocalCheckPoint_ID", SqlDbType.Int)
                {
                    Value = inspecao.LocalCheckPoint_ID
                };

                SqlParameter parmTransportador_ID = new SqlParameter("@p_Transportador_ID", SqlDbType.Int)
                {
                    Value = inspecao.Transportador_ID
                };

                SqlParameter parmFrotaViagem_ID = new SqlParameter("@p_FrotaViagem_ID", SqlDbType.Int)
                {
                    Value = inspecao.FrotaViagem_ID
                };

                SqlParameter parmNavio_ID = null;

                if (!inspecao.Navio_ID.HasValue)
                {
                    parmNavio_ID = new SqlParameter("@p_Navio_ID", SqlDbType.Int)
                    {
                        Value = DBNull.Value
                    };
                }
                else
                {
                    parmNavio_ID = new SqlParameter("@p_Navio_ID", SqlDbType.Int)
                    {
                        Value = inspecao.Navio_ID.Value
                    };
                }

                SqlParameter[] parametros = new SqlParameter[]
                {
                    parmInspecao_ID,
                    parmCliente_ID,
                    parmLocalInspecao_ID,
                    parmLocalCheckPointID,
                    parmTransportador_ID,
                    parmFrotaViagem_ID,
                    parmNavio_ID
                };

                string chamada = $"{nomeStoredProcedure} {parmInspecao_ID.ParameterName}, {parmCliente_ID.ParameterName}, {parmLocalInspecao_ID.ParameterName}, {parmLocalCheckPointID.ParameterName}, {parmTransportador_ID.ParameterName}, {parmFrotaViagem_ID.ParameterName}, {parmNavio_ID.ParameterName}";

                using (var contexto = new GeralDbContext(configuracao))
                {
                    contexto.Database.ExecuteSqlCommand(chamada, parametros);

                    #region gravalogInformacao
                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"InspecaoRepositorio.Update realizado com sucesso:  Dados atualizados | Inspecao_ID {inspecao.Inspecao_ID}, Cliente ID: {inspecao.Cliente_ID}, Local Inspeção: {inspecao.LocalInspecao_ID}, LocalCheckPoint: {inspecao.LocalCheckPoint_ID},  Transportador_ID {inspecao.Transportador_ID}, FrotaViagem: {inspecao.FrotaViagem_ID}, Navio ID: {inspecao.Navio_ID}"
                        });
                    #endregion

                    return inspecao;
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
                #endregion
                throw;
            }

        }


        /// <summary>
        /// Camada de acesso aos dados Publicar
        /// </summary>
        /// <param name="UsuarioId">Usuario que está publicando</param>
        /// <param name="Inspecoes">String concatenada contendo as inspeções delimitadas por ponto e virgula</param>
        /// <param name="configuracao"></param>
        /// <returns></returns>
        public static bool Publicar(int UsuarioId, string Inspecoes, Configuracao configuracao)
        {
            string nomeStoredProcedure = "Publicar";
            try
            {
                SqlParameter parmUsuarioID = new SqlParameter("@p_Usuario_ID", SqlDbType.Int)
                {
                    Value = UsuarioId
                };

                SqlParameter parmInspecoes = new SqlParameter("@p_Inspecoes", SqlDbType.VarChar)
                {
                    Value = Inspecoes
                };

                SqlParameter[] parametros = new SqlParameter[]
               {
                    parmUsuarioID,
                    parmInspecoes
               };

                string chamada = $"{nomeStoredProcedure} {parmUsuarioID.ParameterName}, {parmInspecoes.ParameterName}";

                using (var contexto = new GeralDbContext(configuracao))
                {
                    contexto.Database.ExecuteSqlCommand(chamada, parametros);

                    #region gravalogInformacao
                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"Inspecao.Publicar realizado com sucesso:  Dados atualizados"
                        });
                    #endregion
                    return true;
                }

            }
            catch (Exception ex)
            {
                #region gravalogErro
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar a procedure {nomeStoredProcedure}",
                        Excecao = ex
                    });
                #endregion
                throw;
            }


        }
    }
}
