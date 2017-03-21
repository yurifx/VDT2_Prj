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
    public class AvCondicao
        {
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
                    return listaAvCondicao;
                    }
                }
            catch (System.Exception ex)
                {
                    new Diag.LogItem()
                        {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar a procedure {nomeStoredProcedure}",
                        Excecao = ex
                        };
                throw;
                }
            }
        }
    }
