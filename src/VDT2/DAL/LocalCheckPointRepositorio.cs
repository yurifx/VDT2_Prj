using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;

namespace VDT2.DAL
{
    public class LocalCheckPointRepositorio
    {
        public static List<LocalCheckPoint> Listar(int usuarioId, VDT2.Models.Configuracao configuracao)
            {
            List<LocalCheckPoint> _listaLocaisCheckPoint;
            
            string nomeStoredProcedure = "LocalCheckPoint_Lst";

            SqlParameter parmUsuarioId = new SqlParameter("@p_Usuario_ID", SqlDbType.Int)
                {
                Value = usuarioId
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

            // Monta a chamada à stored procedure
            //yuri markup
            string chamada = $"{nomeStoredProcedure} {parmUsuarioId.ParameterName} , {parmAtivos.ParameterName}";

            try
                {
                using (var contexto = new GeralDbContext(configuracao))
                    {

                    // _listaLocaisCheckPoint = contexto.LocalCheckPoint.FromSql("Select * from LocalCheckPoint").ToList();
                    _listaLocaisCheckPoint = contexto.LocalCheckPoint.FromSql(chamada, parametros).ToList();
                    //An exception of type 'System.InvalidOperationException' occurred in Microsoft.EntityFrameworkCore.dll but was not handled in user code
                    //Additional information: The required column 'LocalCheckPoint_ID' was not present in the results of a 'FromSql' operation.

                    return _listaLocaisCheckPoint;
                    }
                }
            catch (System.Exception ex)
                {

                Diag.Log.Grava(
                    new Diag.LogItem()
                        {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar a procedure {nomeStoredProcedure}",
                        Excecao = ex
                        });
                throw;
                }

            }
        
        }
}
