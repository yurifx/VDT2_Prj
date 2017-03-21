﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;

/// <summary>
/// camada de acesso aos dados - Navio
/// </summary>
namespace VDT2.DAL
    {
    public class Navio
        {
        /// <summary>
        /// Lista todos os navios do banco de dados
        /// </summary>
        /// <param name="configuracao">string de conexão com o banco de dados - Configuração</param>
        /// <returns>Retorna uma lista de Navio</returns>
        public static List<Models.Navio> ListarTodos(VDT2.Models.Configuracao configuracao)
            {
            List<Models.Navio> listaNavio;
            string nomeStoredProcedure = "Navio_Lst";
            try
                {
                SqlParameter parmAtivos = new SqlParameter("@p_Ativos", SqlDbType.Bit)
                    {
                    Value = 0
                    };

                SqlParameter[] parametros = new SqlParameter[]
                    {
                    parmAtivos
                    };

                string chamada = $"{nomeStoredProcedure}  {parmAtivos.ParameterName}";

                using (var contexto = new GeralDbContext(configuracao))
                    {
                    listaNavio = contexto.Navio.FromSql(chamada, parametros).ToList();
                    var contador = listaNavio.Count();
                    #region gravalogResultado
                    Diag.Log.Grava(
                        new Diag.LogItem()
                            {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"Navio.ListarTodos realizado com sucesso:  Registros encontrados: {contador}"
                            });
                    #endregion  
                    return listaNavio;
                    }
                }
            catch (System.Exception ex)
                {
                #region logravaErro
                Diag.Log.Grava(
                    new Diag.LogItem()
                        {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar a procedure {nomeStoredProcedure} | Parametros - configuracao",
                        Excecao = ex
                        });
                throw;
                #endregion  
                }
            }

/// <summary>
/// Insere um novo registro no banco de dados com o nome do navio informado
/// </summary>
/// <param name="nomeNavio">nome do navio</param>
/// <param name="configuracao">string de conexão com o banco de dados</param>
/// <returns>id do navio inserido</returns>
        public static int Inserir(string nomeNavio, Configuracao configuracao)
            {
            string nomeStoredProcedure = "Navio_Ins";
            try
                {
                SqlParameter parmNome = new SqlParameter("@p_Nome", SqlDbType.VarChar)
                    {
                    Value = nomeNavio
                    };
                SqlParameter parmNavioId = new SqlParameter("@p_Navio_Id", SqlDbType.Int)
                    {
                    Value = 1,
                    Direction = ParameterDirection.Output
                    };


                SqlParameter[] parametros = new SqlParameter[]
                {
                parmNome,
                parmNavioId
                };

                string chamada = $"{nomeStoredProcedure} {parmNome.ParameterName}, {parmNavioId.ParameterName} out";
                using (var contexto = new GeralDbContext(configuracao))
                    {
                    contexto.Database.ExecuteSqlCommand(chamada, parametros);
                    var navioId = (int)parmNavioId.Value;
                    #region gravalogInformacao
                    Diag.Log.Grava(
                        new Diag.LogItem()
                            {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"BLL.Navio.Inserir realizado com sucesso:  Dados atualizados | Navio_ID: {navioId}"
                            });
                    #endregion
                    return navioId;
                    }
                }
            catch (System.Exception ex)
                {
                #region gravalogErro
                Diag.Log.Grava(
                    new Diag.LogItem()
                        {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar a procedure {nomeStoredProcedure} | Nome Navio: {nomeNavio}",
                        Excecao = ex
                        });
                throw;
                #endregion  
                }

            }
/// <summary>
/// Consulta ID do navio - Caso exista, retorna o valor. Caso não exista, insere no banco de dados
/// </summary>
/// <param name="nomeNavio">Nome do navio a ser procurado</param>
/// <param name="configuracao">String de conexão com o banco de dados</param>
/// <returns></returns>
        public static int ConsultaIdNavio(string nomeNavio, VDT2.Models.Configuracao configuracao)
            {
            int navioID = 0;
            try
                {
                if (nomeNavio != null)
                    {
                    //Primeiro, verifica se existe este navio no bdd
                    var navio_db = Navio.ListarTodos(configuracao).Where(p => p.Nome == nomeNavio).FirstOrDefault();

                    if (navio_db == null) //caso nao tenha achado, insere este como novo.
                        {
                        //insere e recebe o ID do novo registro
                        navioID = Navio.Inserir(nomeNavio, configuracao);
                        }
                    else { navioID = navio_db.Navio_ID; } //caso exista, só pega o seu id
                    }
                }
            catch (System.Exception ex)
                {
                Diag.Log.Grava(
                    new Diag.LogItem()
                        {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar a Consulta: ConsultaIdNavio | Parametros: Navio: {nomeNavio}",
                        Excecao = ex
                        });
                throw;
                }
            return navioID;
            }

        /// <summary>
        /// Consulta todos os dados do Navio informando seu ID
        /// </summary>
        /// <param name="navio_ID">id do navio procurado</param>
        /// <param name="configuracao">string de conexão com o banco de dados</param>
        /// <returns></returns>
        public static Models.Navio ConsultaNavioPorId(int? navio_ID, VDT2.Models.Configuracao configuracao)
            {
            Models.Navio navio = new Models.Navio();
            string nomeStoredProcedure = "Navio_Sel";
            try
                {
                SqlParameter parmNavio_ID = new SqlParameter("@p_Navio_ID", SqlDbType.Int)
                    {
                    Value = navio_ID
                    };

                SqlParameter[] parametros = new SqlParameter[]
              {
                parmNavio_ID

              };
                string chamada = $"{nomeStoredProcedure} {parmNavio_ID.ParameterName}";

                using (var contexto = new GeralDbContext(configuracao))

                    {
                    navio = contexto.Navio.FromSql(chamada, parametros).FirstOrDefault();
                    return navio;
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