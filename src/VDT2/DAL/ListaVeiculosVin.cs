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
    public class ListaVeiculosVin
    {
        public static Models.ListaVeiculosVin Inserir(Models.ListaVeiculosVin VeiculoVIN, Configuracao configuracao)
        {
            string nomeStoredProcedure = "ListaVeiculosVin_Ins";
            try
            {
                SqlParameter parmListaVeiculos_ID = new SqlParameter("@p_ListaVeiculos_ID", SqlDbType.Int)
                {
                    Value = VeiculoVIN.ListaVeiculos_ID
                };

                SqlParameter parmVIN = new SqlParameter("@p_VIN", SqlDbType.Char)
                {
                    Value = VeiculoVIN.VIN
                };


                SqlParameter parmListaVeiculosVin_ID = new SqlParameter("@p_ListaVeiculosVin_ID", SqlDbType.Int)
                {
                    Value = 1,
                    Direction = ParameterDirection.Output
                };



                SqlParameter[] parametros = new SqlParameter[]
                    {
                    parmListaVeiculos_ID,
                    parmVIN,
                    parmListaVeiculosVin_ID
                    };

                string chamada = $"{nomeStoredProcedure} {parmListaVeiculos_ID.ParameterName}, {parmVIN.ParameterName}, {parmListaVeiculosVin_ID.ParameterName} out";

                using (var contexto = new GeralDbContext(configuracao))
                {
                    contexto.Database.ExecuteSqlCommand(chamada, parametros);
                  //  VeiculoVIN.ListaVeiculosVin_ID = (int)parmListaVeiculosVin_ID.Value;
                    return VeiculoVIN;
                }

            }

            catch (Exception ex)
            {
                #region gravalogErro
                Diag.Log.Grava(
               new Diag.LogItem()
               {
                   Nivel = Diag.Nivel.Erro,
                   Mensagem = $"Erro ao executar ListaVeiculos.Inserir: Erro:  {ex}"
               });
                #endregion
                throw;
            }



        }
    }
}


