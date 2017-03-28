// <copyright file="AvQuadrante.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Amauri Rodrigues & Yuri Vasconcelos</author>
// <email>amauri.rodrigues@grupoasserth.com.br | yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>Classe referente a camada de acesso aos dados - AvQuadrante</summary>

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;


/// <summary>
/// Camada de acesso aos dados AvQuadrante
/// </summary>
namespace VDT2.DAL
{
    public class AvQuadrante
    {
        /// <summary>
        /// Lista todos os Quadrantes existentes no banco de dados
        /// </summary>
        /// <param name="Cliente_ID"></param>
        /// <param name="configuracao"></param>
        /// <returns></returns>
        public static List<Models.AvQuadrante> Listar(int Cliente_ID, VDT2.Models.Configuracao configuracao)
        {
            List<Models.AvQuadrante> listaQuadrantes = new List<Models.AvQuadrante>();
            string nomeStoredProcedure = "AvQuadrante_Lst";

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
                    listaQuadrantes = contexto.AvQuadrante.FromSql(chamada, parametros).OrderBy(x => x.AvQuadrante_ID).ToList();

                    #region gravalogInformacao
                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"AvQuadrante.Listar Realizado com sucesso. Registros encontrados - {listaQuadrantes.Count()}"
                        });
                    #endregion

                    return listaQuadrantes;
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
