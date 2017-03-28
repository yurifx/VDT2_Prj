using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;


/// <summary>
/// Camada de acesso aos dados LocalInspecao
/// </summary>
namespace VDT2.DAL
{
    public class LocalInspecao
    {
        /// <summary>
        /// Lista Locais de inspeção no bd;
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <param name="configuracao">String de conexão com o bdd</param>
        /// <returns></returns>
        public static List<Models.LocalInspecao> Listar(int usuarioID, VDT2.Models.Configuracao configuracao)
        {
            List<Models.LocalInspecao> listaLocaisInspecao = new List<Models.LocalInspecao>();

            string nomeStoredProcedure = "LocalInspecao_lst";

            try
            {
                SqlParameter parmUsuarioId = new SqlParameter("@p_Usuario_ID", SqlDbType.Int)
                {
                    Value = usuarioID
                };

                SqlParameter parmAtivos = new SqlParameter("@p_Ativos", SqlDbType.Bit)
                {
                    Value = 1
                };

                SqlParameter[] parametros = new SqlParameter[]
                {
                    parmUsuarioId,
                    parmAtivos
                };

                string chamada = $"{nomeStoredProcedure} {parmUsuarioId.ParameterName} , {parmAtivos.ParameterName}";

                using (var contexto = new GeralDbContext(configuracao))
                {
                    listaLocaisInspecao = contexto.LocalInspecao.FromSql(chamada, parametros).ToList();

                    #region gravalogResultado
                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"LocalInspecao.Listar realizado com sucesso:  Registros encontrados: {listaLocaisInspecao.Count()}"
                        });
                    #endregion
                }

                return listaLocaisInspecao;
            }

            catch (System.Exception ex)
            {
                #region gravalogErro
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar a procedure {nomeStoredProcedure} | Parametros: _usuarioId: {usuarioID}",
                        Excecao = ex
                    });
                #endregion
                throw;
            }
        }
    }
}
