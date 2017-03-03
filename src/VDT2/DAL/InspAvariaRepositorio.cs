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
    public class InspAvariaRepositorio
        {
        public static int Inserir(InspAvaria inspAvaria, VDT2.Models.Configuracao configuracao)
            {
            string nomeStoredProcedure = "InspAvaria_Ins";

            SqlParameter parmInspVeiculo_ID = new SqlParameter("@p_InspVeiculo_ID", SqlDbType.Int)
                {
                Value = inspAvaria.InspVeiculo_ID
                };

            SqlParameter parmAvArea_ID = new SqlParameter("@p_AvArea_ID", SqlDbType.Int)
                {
                Value = inspAvaria.AvArea_ID
                };

            SqlParameter parmAvCondicao_ID = new SqlParameter("@p_AvCondicao_ID", SqlDbType.Int)
                {
                Value = inspAvaria.AvCondicao_ID
                };


            SqlParameter parmAvDano_ID = new SqlParameter("@p_AvDano_ID", SqlDbType.Int)
                {
                Value = inspAvaria.AvDano_ID
                };

            SqlParameter parmAvGravidade_ID = new SqlParameter("@p_AvGravidade_ID", SqlDbType.Int)
                {
                Value = inspAvaria.AvGravidade_ID
                };

            SqlParameter parmAvQuadrante_ID = new SqlParameter("@p_AvQuadrante_ID", SqlDbType.Int)
                {
                Value = inspAvaria.AvQuadrante_ID
                };
            SqlParameter parmAvSeveridade_ID = new SqlParameter("@p_AvSeveridade_ID", SqlDbType.Int)
                {
                Value = inspAvaria.AvSeveridade_ID
                };

            SqlParameter parmFabricaTransporte = new SqlParameter("@p_FabricaTransporte", SqlDbType.Char)
                {
                Value = inspAvaria.FabricaTransporte
                };

            SqlParameter parmInspAvaria_ID = new SqlParameter("@p_InspAvaria_ID", SqlDbType.Int)
                {
                Value = 1,
                Direction = ParameterDirection.Output
                };

            SqlParameter[] parametros = new SqlParameter[]
            {
                parmInspVeiculo_ID,
                parmAvArea_ID,
                parmAvCondicao_ID,
                parmAvDano_ID,
                parmAvGravidade_ID,
                parmAvQuadrante_ID,
                parmAvSeveridade_ID,
                parmFabricaTransporte,
                parmInspAvaria_ID

            };

            string chamada = $"{nomeStoredProcedure} {parmInspVeiculo_ID.ParameterName}, { parmAvArea_ID.ParameterName}, { parmAvCondicao_ID.ParameterName}, { parmAvDano_ID.ParameterName}, { parmAvGravidade_ID.ParameterName}, { parmAvQuadrante_ID.ParameterName},{ parmAvSeveridade_ID.ParameterName}, { parmFabricaTransporte.ParameterName},{parmInspAvaria_ID.ParameterName} out";

            try
                {
                using (var contexto = new GeralDbContext(configuracao))
                    {
                    //var _teste = contexto.Cliente.FromSql("Select * from Cliente").ToList();
                    contexto.Database.ExecuteSqlCommand(chamada, parametros);

                    //recebendo o retorno do parametro_id igual exemplo Amauri
                    var inspVeiculoId = (int)parmInspAvaria_ID.Value;

                    return inspVeiculoId;
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

        public static List<InspAvaria> Listar(int Cliente_ID, string VIN_6, VDT2.Models.Configuracao configuracao)
            {
            List<InspAvaria> avarias;

            string nomeStoredProcedure = "InspAvaria_LstVin";

            SqlParameter parmCliente_ID = new SqlParameter("@p_Cliente_ID", SqlDbType.Bit)
                {
                Value = Cliente_ID
                };

            SqlParameter parmVIN_6 = new SqlParameter("@p_VIN_6", SqlDbType.Char)
                {
                Value = VIN_6
                };

            SqlParameter parmVIN = new SqlParameter("@p_VIN", SqlDbType.Char)
                {
                Value = 0
                };

            SqlParameter[] parametros = new SqlParameter[]
            {
                parmCliente_ID,
                parmVIN_6,
                parmVIN
            };

            string chamada = $"{nomeStoredProcedure}  {parmCliente_ID.ParameterName}, {parmVIN_6.ParameterName}, {parmVIN.ParameterName}";
            try
                {
                using (var contexto = new GeralDbContext(configuracao))
                    {
                    avarias = contexto.InspAvaria.FromSql(chamada, parametros).ToList();
                    return avarias;
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
