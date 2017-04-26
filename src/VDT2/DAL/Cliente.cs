// <copyright file="Cliente.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Grupo Asserth</author>
// <email>yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>Classe referente a camada de acesso aos dados - Cliente</summary>


//dependencias
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;


/// <summary>
/// Camada de acesso aos dados Cliente
/// </summary>
namespace VDT2.DAL
{
    public class Cliente
    {

        /// <summary>
        /// Realiza a listagem de clientes no banco de dados
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <param name="configuracao"></param>
        /// <returns>Retorna a lista de clientes encontradas no bdd</returns>
        public static List<Models.Cliente> Listar(int usuarioID, VDT2.Models.Configuracao configuracao)
        {
            string nomeStoredProcedure = "Cliente_Lst";

            List<Models.Cliente> listaCliente = new List<Models.Cliente>();
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

                string chamada = $"{nomeStoredProcedure} {parmUsuarioId.ParameterName}, {parmAtivos.ParameterName}";

                using (var contexto = new GeralDbContext(configuracao))
                {
                    listaCliente = contexto.Cliente.FromSql(chamada, parametros).ToList();

                    #region gravalogResultado
                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"Cliente.Listar realizado com sucesso:  Registros encontrados: {listaCliente.Count()}"
                        });
                    #endregion  

                    return listaCliente;
                }
            }

            catch (System.Exception ex)
            {

                #region gravalogErro
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar a procedure {nomeStoredProcedure} | Parametros: UsuarioId =  {usuarioID}",
                        Excecao = ex
                    });

                #endregion
                throw;
            }

        }
    }
}
