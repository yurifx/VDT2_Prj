// <copyright file="AvDano.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Yuri Vasconcelos</author>
// <email>yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>Classe referente a camada de acesso aos dados - AvDano</summary>


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
/// Camada de acesso a dados: AvDano
/// </summary>
namespace VDT2.DAL
{
    public class AvDano
    {

        /// <summary>
        /// Realiza a listagem dos danos referentes ao cliente informado
        /// </summary>
        /// <param name="Cliente_ID"></param>
        /// <param name="configuracao"></param>
        /// <returns></returns>
        public static List<Models.AvDano> Listar(int Cliente_ID, VDT2.Models.Configuracao configuracao)
        {
            List<Models.AvDano> listaDanos = new List<Models.AvDano>();
            string nomeStoredProcedure = "AvDano_Lst";

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
                    listaDanos = contexto.AvDano.FromSql(chamada, parametros).OrderBy(x => x.AvDano_ID).ToList();

                    #region gravalogInformacao
                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"AvDano.Listar Realizado com sucesso. Registros encontrados - {listaDanos.Count()}"
                        });
                    #endregion

                    return listaDanos;
                }
            }

            catch (System.Exception ex)
            {
                #region gravalogerro
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
