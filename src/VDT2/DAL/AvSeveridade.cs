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
    public class AvSeveridade
        {
        public static List<Models.AvSeveridade> Listar(int Cliente_ID, VDT2.Models.Configuracao configuracao)
            {

            List<Models.AvSeveridade> listaSeveridades = new List<Models.AvSeveridade>();
            string nomeStoredProcedure = "AvSeveridade_Lst";
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
                    listaSeveridades = contexto.AvSeveridade.FromSql(chamada, parametros).OrderBy(x => x.AvSeveridade_ID).ToList();
                    var contador = listaSeveridades.Count();
                    #region gravalogResultado
                    Diag.Log.Grava(
                        new Diag.LogItem()
                            {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"AvSeveridade.Listar() realizado com sucesso:  Registros encontrados: {contador}"
                            });
                    #endregion  
                    return listaSeveridades;
                    }
                }
            catch (System.Exception ex)
                {
                listaSeveridades.Add(new Models.AvSeveridade { Erro = true, MensagemErro = "Erro ao processar DAL - Severidade - Listar()" });
                #region gravalogerro
                Diag.Log.Grava(
                    new Diag.LogItem()
                        {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar a procedure {nomeStoredProcedure}",
                        Excecao = ex
                        });
                #endregion  
                return listaSeveridades;
                }
            }
        }
    }

