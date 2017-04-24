// <copyright file="Transportador.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Grupo Asserth</author>
// <email>yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>Classe referente a camada de acesso aos dados - Transportador</summary>


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
/// Camada de acesso ao banco de dados - Transportador
/// </summary>
namespace VDT2.DAL
{
    public class Transportador
    {
        /// <summary>
        /// Listagem de transportadores 
        /// </summary>
        /// <param name="ativos"></param>
        /// <param name="configuracao"></param>
        /// <returns>Lista de transportadores</returns>
        public static List<Models.Transportador> Listar(int ativos, VDT2.Models.Configuracao configuracao)
        {
            List<Models.Transportador> listaTransportador = new List<Models.Transportador>();
            string nomeStoredProcedure = "Transportador_Lst";

            try
            {
                SqlParameter parmAtivos = new SqlParameter("@p_Ativos", SqlDbType.Bit)
                {
                    Value = ativos
                };

                SqlParameter[] parametros = new SqlParameter[]
                {
                    parmAtivos
                };

                string chamada = $"{nomeStoredProcedure} {parmAtivos.ParameterName}";

                using (var contexto = new GeralDbContext(configuracao))
                {
                    listaTransportador = contexto.Transportador.FromSql(chamada, parametros).ToList();

                    #region gravalogResultado
                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"Transportador.Listar realizado com sucesso:  Registros encontrados: {listaTransportador.Count()}"
                        });
                    #endregion  
                    return listaTransportador;
                }
            }

            catch (System.Exception ex)
            {
                #region gravalogErro
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar a procedure {nomeStoredProcedure} Parametros: ativos {ativos}",
                        Excecao = ex
                    });
                #endregion
                throw;
            }
        }

        /// <summary>
        /// Listagem de transportador via ID
        /// </summary>
        /// <param name="transportador_id">id do transportador</param>
        /// <param name="configuracao">string de conexão com o banco de dados</param>
        /// <returns></returns>
        public static Models.Transportador ListarPorId(int transportador_id, VDT2.Models.Configuracao configuracao)
        {
            Models.Transportador transportador = new Models.Transportador();
            string nomeStoredProcedure = "Transportador_Sel";

            SqlParameter parmTransportador_ID = new SqlParameter("@p_Transportador_ID", SqlDbType.Int)
            {
                Value = transportador_id
            };

            SqlParameter[] parametros = new SqlParameter[]
            {
                parmTransportador_ID
            };

            string chamada = $"{nomeStoredProcedure} {parmTransportador_ID.ParameterName}";

            try
            {
                using (var contexto = new GeralDbContext(configuracao))
                {
                    transportador = contexto.Transportador.FromSql(chamada, parametros).FirstOrDefault();

                    #region gravalogResultado
                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"Transportador.ListarPordId realizado com sucesso:  Transportador_ID: {transportador_id}"
                        });
                    #endregion  

                    return transportador;
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
                throw;
            }

        }
    }
}
