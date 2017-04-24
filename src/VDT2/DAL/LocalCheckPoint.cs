// <copyright file="LocalCheckPoint.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Grupo Asserth</author>
// <email>yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>Classe referente a camada de acesso aos dados - LocalCheckPoint</summary>

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
/// Camada de acesso aos dados do banco de dados - LocalCheckPoint
/// </summary>
namespace VDT2.DAL
{
    public class LocalCheckPoint
    {
        /// <summary>
        /// Lista todo os checkpoints disponíveis para o usuário informado
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <param name="configuracao"></param>
        /// <returns>Lista de locais disponíveis para o cliente</returns>
        public static List<Models.LocalCheckPoint> Listar(int usuarioID, VDT2.Models.Configuracao configuracao)
        {
            List<Models.LocalCheckPoint> listaLocaisCheckPoint = new List<Models.LocalCheckPoint>();

            string nomeStoredProcedure = "LocalCheckPoint_Lst";

            try
            {
                SqlParameter parmUsuarioId = new SqlParameter("@p_Usuario_ID", SqlDbType.Int)
                {
                    Value = usuarioID
                };

                SqlParameter parmAtivos = new SqlParameter("@p_Ativos", SqlDbType.Bit)
                {
                    Value = 0
                };

                SqlParameter[] parametros = new SqlParameter[]
                {
                    parmUsuarioId,
                    parmAtivos
                };

                string chamada = $"{nomeStoredProcedure} {parmUsuarioId.ParameterName} , {parmAtivos.ParameterName}";

                using (var contexto = new GeralDbContext(configuracao))
                {
                    listaLocaisCheckPoint = contexto.LocalCheckPoint.FromSql(chamada, parametros).ToList();

                    var contador = listaLocaisCheckPoint.Count();

                    #region gravalogResultado
                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"LocalCheckPoint.Listar realizado com sucesso:  Registros encontrados: {contador}"
                        });
                    #endregion  

                    return listaLocaisCheckPoint;
                }
            }

            catch (System.Exception ex)
            {
                #region gravalogErro
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar a procedure {nomeStoredProcedure} | Parametros: usuarioId {usuarioID}",
                        Excecao = ex
                    });
                #endregion

                throw;
            }
        }

    }
}
