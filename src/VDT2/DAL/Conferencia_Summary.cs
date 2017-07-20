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
    public class Conferencia_Summary
    {
        public static List<Models.Conferencia_Summary> Consultar(int cliente_ID, int localInspecao_ID, int LocalCheckPoint_Id, DateTime data, Configuracao configuracao)
        {
            List<Models.Conferencia_Summary> listaConferencia = new List<Models.Conferencia_Summary>();
            try
            {
                string nomeStoredProcedure = "Conferencia_Summary";

                SqlParameter parmCliente_ID = new SqlParameter("@p_Cliente_ID", SqlDbType.Int)
                {
                    Value = cliente_ID
                };

                SqlParameter parmLocalInspecao_ID = new SqlParameter("@p_LocalInspecao_ID", SqlDbType.Int)
                {
                    Value = localInspecao_ID
                };


                SqlParameter parmLocalCheckPoint_ID = new SqlParameter("@p_LocalCheckPoint_ID", SqlDbType.Int)
                {
                    Value = LocalCheckPoint_Id
                };


                SqlParameter parmData = new SqlParameter("@p_Data", SqlDbType.DateTime)
                {
                    Value = data
                };

                SqlParameter[] parametros = new SqlParameter[]
                {
                    parmCliente_ID,
                    parmLocalInspecao_ID,
                    parmLocalCheckPoint_ID,
                    parmData
                };

                string chamada = $"{nomeStoredProcedure} {parmCliente_ID.ParameterName} , {parmLocalInspecao_ID.ParameterName}, {parmLocalCheckPoint_ID.ParameterName}, {parmData.ParameterName}";


                using (var contexto = new GeralDbContext(configuracao))
                {
                    listaConferencia = contexto.Conferencia_Summary.FromSql(chamada, parametros).ToList();
                    return listaConferencia;
                }
            }
            catch (Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem { Excecao = ex, Nivel = Diag.Nivel.Erro, Mensagem = "Erro ao consultar - DAL.Conferencia_Summary." });
                return null;
            }


        }

    }
}
