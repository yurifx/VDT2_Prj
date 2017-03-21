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
    public class AvGravidade
    {
        public static List<Models.AvGravidade> Listar(int Cliente_ID, VDT2.Models.Configuracao configuracao)
            {
            List<Models.AvGravidade> listaGravidades = new List<Models.AvGravidade>();
            string nomeStoredProcedure = "AvGravidade_Lst";

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
                    listaGravidades = contexto.AvGravidade.FromSql(chamada, parametros).OrderBy(x => x.AvGravidade_ID).ToList();
                    return listaGravidades;
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
