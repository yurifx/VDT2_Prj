// <copyright file="AvCondicao.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Grupo Asserth</author>
// <email>yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>Classe referente a camada de acesso aos dados - AvCondicao</summary>

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
/// Camada de acesso a dados: AvCondicao
/// </summary>
namespace VDT2.DAL
{
    public class AvCondicao
    {

        /// <summary>
        /// Lista as condicoes do cliente informado
        /// </summary>
        /// <param name="Cliente_ID"></param>
        /// <param name="configuracao"></param>
        /// <returns>Lista de Condicoes</returns>
        public static List<Models.AvCondicao> Listar(int Cliente_ID, VDT2.Models.Configuracao configuracao)
        {
            List<Models.AvCondicao> listaAvCondicao = new List<Models.AvCondicao>();
            string nomeStoredProcedure = "AvCondicao_Lst";

            try
            {
                SqlParameter parmClienteID = new SqlParameter("@p_Cliente_ID", SqlDbType.Int)
                {
                    Value = Cliente_ID
                };


                SqlParameter[] parametros = new SqlParameter[]
                {
                    parmClienteID
                };

                string chamada = $"{nomeStoredProcedure} {parmClienteID.ParameterName}";
                using (var contexto = new GeralDbContext(configuracao))
                {
                    listaAvCondicao = contexto.AvCondicao.FromSql(chamada, parametros).OrderBy(x => x.AvCondicao_ID).ToList();

                    #region gravalogInformacao
                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"AvCondicao.Listar Realizado com sucesso. Registros encontrados - {listaAvCondicao.Count()}"
                        });
                    #endregion

                    return listaAvCondicao;
                }
            }
            catch (System.Exception ex)
            {
                #region gravalogErro
                new Diag.LogItem()
                {
                    Nivel = Diag.Nivel.Erro,
                    Mensagem = $"Não conseguiu executar a procedure {nomeStoredProcedure}",
                    Excecao = ex
                };
                #endregion  
                throw;
            }
        }
    }
}
