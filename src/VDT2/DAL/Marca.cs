// <copyright file="Marca.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Grupo Asserth</author>
// <email>yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>Classe referente a camada de acesso aos dados - Marca</summary>


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
/// Marca - Camada de acesso aos dados 
/// </summary>
namespace VDT2.DAL
{
    public class Marca
    {
        /// <summary>
        /// Lista todas as marcas que podem ser selecionadas pelo cliente_id informado 
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="configuracao">String de conexão - Configuração do banco de dados</param>
        /// <returns></returns>
        public static List<Models.Marca> Listar(int clientId, VDT2.Models.Configuracao configuracao)
        {
            List<Models.Marca> listaMarcas = new List<Models.Marca>();

            string nomeStoredProcedure = "Marca_Lst";

            try
            {
                SqlParameter parmClienteID = new SqlParameter("@p_Cliente_ID", SqlDbType.Int)
                {
                    Value = clientId
                };

                SqlParameter parmAtivos = new SqlParameter("@p_Ativos", SqlDbType.Bit)
                {
                    Value = true
                };

                SqlParameter[] parametros = new SqlParameter[]
                {
                    parmClienteID,
                    parmAtivos
                };

                string chamada = $"{nomeStoredProcedure} {parmClienteID.ParameterName}, {parmAtivos.ParameterName}";

                using (var contexto = new GeralDbContext(configuracao))
                {
                    listaMarcas = contexto.Marca.FromSql(chamada, parametros).ToList();

                    #region gravalogResultado
                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"Marca.Listar realizado com sucesso:  Registros encontrados: {listaMarcas.Count()}"
                        });
                    #endregion

                    return listaMarcas;
                }
            }

            catch (System.Exception ex)
            {
                listaMarcas.Add(new Models.Marca { Erro = true, MensagemErro = "Erro ao listar marcas DAL" });
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar a procedure {nomeStoredProcedure} | ClienteID: {clientId}",
                        Excecao = ex
                    });
                return listaMarcas;
            }
        }

        /// <summary>
        /// Insere a marca no banco de dados - tabela Marca
        /// </summary>
        /// <param name="cliente_ID">Cliente_ID</param>
        /// <param name="nomeNovaMarca">Nome da nova marca</param>
        /// <param name="configuracao">String de conexão - Configuração - bdd</param>
        /// <returns>ID da nova marca registrada no banco de dados</returns>
        public static int Inserir(int cliente_ID, string nomeNovaMarca, Configuracao configuracao)
        {
            string nomeStoredProcedure = "Marca_Ins";

            try
            {
                SqlParameter parmCliente_ID = new SqlParameter("@p_Cliente_ID", SqlDbType.VarChar)
                {
                    Value = cliente_ID
                };

                SqlParameter parmNome = new SqlParameter("@p_Nome", SqlDbType.VarChar)
                {
                    Value = nomeNovaMarca
                };

                SqlParameter parmAtivo = new SqlParameter("@p_Ativo", SqlDbType.VarChar)
                {
                    Value = 1
                };

                SqlParameter parmMarca_ID = new SqlParameter("@p_Marca_ID", SqlDbType.Int)
                {
                    Value = 1,
                    Direction = ParameterDirection.Output
                };

                SqlParameter[] parametros = new SqlParameter[]
                {
                    parmCliente_ID,
                    parmNome,
                    parmAtivo,
                    parmMarca_ID
                };

                string chamada = $"{nomeStoredProcedure} {parmCliente_ID.ParameterName}, {parmNome.ParameterName}, {parmAtivo.ParameterName}, {parmMarca_ID.ParameterName} out";

                using (var contexto = new GeralDbContext(configuracao))
                {
                    contexto.Database.ExecuteSqlCommand(chamada, parametros);

                    //recebe retorno
                    var id = (int)parmMarca_ID.Value;

                    #region gravalogInsert
                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"Marca.Inserir realizado com sucesso. Marca_ID {id}"
                        });
                    #endregion  
                    return id;
                }
            }

            catch (System.Exception ex)
            {
                #region gravalogerro
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar a procedure {nomeStoredProcedure} | Cliente_ID: {cliente_ID}, nomeNovaMarca: {nomeNovaMarca}",
                        Excecao = ex
                    });
                throw;
                #endregion
            }
        }
    }
}
