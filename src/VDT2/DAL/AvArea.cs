// <copyright file="AvArea.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Yuri Vasconcelos</author>
// <email>yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>Classe referente a camada de acesso aos dados - AvArea</summary>

///Dependencias
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;


/// <summary>
/// Camada de acesso aos dados AvArea
/// </summary>
namespace VDT2.DAL
{
    public class AvArea
    {

        /// <summary>
        /// Lista areas referentes ao Cliente
        /// </summary>
        /// <param name="Cliente_ID"></param>
        /// <param name="configuracao"></param>
        /// <returns>Lista de areas</returns>
        public static List<Models.AvArea> Listar(int Cliente_ID, VDT2.Models.Configuracao configuracao)
        {
            List<Models.AvArea> listaAreas = new List<Models.AvArea>();
            string nomeStoredProcedure = "AvArea_Lst";
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
                    listaAreas = contexto.AvArea.FromSql(chamada, parametros).OrderBy(x => x.AvArea_ID).ToList();

                    #region gravalogInformacao
                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"AvArea.Listar Realizado com sucesso. Registros encontrados - {listaAreas.Count()}"
                        });
                    #endregion

                    return listaAreas;
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
    }
}
