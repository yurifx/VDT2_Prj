// <copyright file="AvSeveridade.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Grupo Asserth</author>
// <email>yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>Classe referente a camada de acesso aos dados - AvSeveridade</summary>

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;


/// <summary>
/// Camada de acesso aos dados AvSeveridade
/// </summary>
namespace VDT2.DAL
{
    public class AvSeveridade
    {

        /// <summary>
        /// Realiza a listagem de severidades referentes ao cliente informado
        /// </summary>
        /// <param name="Cliente_ID"></param>
        /// <param name="configuracao"></param>
        /// <returns>Retorna a lista de severidades</returns>
        public static List<Models.AvSeveridade> Listar(int Cliente_ID, VDT2.Models.Configuracao configuracao)
        {

            List<Models.AvSeveridade> listaSeveridades = new List<Models.AvSeveridade>();

            string nomeStoredProcedure = "AvSeveridade_Lst";

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
                    listaSeveridades = contexto.AvSeveridade.FromSql(chamada, parametros).OrderBy(x => x.AvSeveridade_ID).ToList();

                    #region gravalogResultado
                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"AvSeveridade.Listar realizado com sucesso:  Registros encontrados: {listaSeveridades.Count()}"
                        });
                    #endregion  

                    return listaSeveridades;
                }
            }
            catch (System.Exception ex)
            {
                listaSeveridades.Add(new Models.AvSeveridade { Erro = true, MensagemErro = "Erro ao processar DAL - Severidade - Listar()" });

                #region gravalogerro
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar a procedure {nomeStoredProcedure}",
                        Excecao = ex
                    });
                #endregion  

                return listaSeveridades;
            }
        }
    }
}

