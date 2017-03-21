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
    public class AvDano
        {

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
                    var contador = listaDanos.Count();
                    #region gravalogResultado
                    Diag.Log.Grava(
                        new Diag.LogItem()
                            {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"AvDano.Listar() realizado com sucesso:  Registros encontrados: {contador}"
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
