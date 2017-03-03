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
    public class InspVeiculoRepositorio
        {
        public static int Inserir(InspVeiculo _inspVeiculo, VDT2.Models.Configuracao configuracao)
            {
            // List<Inspecao> _listaInspecao;

            string nomeStoredProcedure = "InspVeiculo_Ins";

            SqlParameter parmInspecao_ID = new SqlParameter("@p_Inspecao_ID", SqlDbType.Int)
                {
                Value = _inspVeiculo.Inspecao_ID
                };

            SqlParameter parmMarca_ID = new SqlParameter("@p_Marca_ID", SqlDbType.Int)
                {
                Value = _inspVeiculo.Marca_ID
                };

            SqlParameter parmModelo_ID = new SqlParameter("@p_Modelo_ID", SqlDbType.Int)
                {
                Value = _inspVeiculo.Modelo_ID
                };

            //CHASSI COM 6 DÍGITOS
            SqlParameter parmVIN_6 = new SqlParameter("@p_VIN_6", SqlDbType.Char)
                {
                Value = _inspVeiculo.VIN_6
                };

            //CHASSI COM 17 DÍGITOS, NECESSÁRIO LÓGICA PARA PEGAR ESSA NUMERAÇÃO
            SqlParameter parmVIN = new SqlParameter("@p_VIN", SqlDbType.Char)
                {
                //  Value = _inspVeiculo.VIN
                Value = DBNull.Value
                };

            SqlParameter parmObservacoes = new SqlParameter("@Observacoes", SqlDbType.Char)
                {
                Value = _inspVeiculo.Observacoes
                };

            SqlParameter parmInspVeiculo_ID = new SqlParameter("@p_InspVeiculo_ID", SqlDbType.Int)
                {
                Value = 1,
                Direction = ParameterDirection.Output
                };

            SqlParameter[] parametros = new SqlParameter[]
            {
                parmInspecao_ID,
                parmMarca_ID,
                parmModelo_ID,
                parmVIN_6,
                parmVIN,
                parmObservacoes,
                parmInspVeiculo_ID

            };

            string chamada = $"{nomeStoredProcedure} {parmInspecao_ID.ParameterName}, { parmMarca_ID.ParameterName}, { parmModelo_ID.ParameterName}, { parmVIN_6.ParameterName}, { parmVIN.ParameterName}, { parmObservacoes.ParameterName}, {parmInspVeiculo_ID.ParameterName} out";

            try
                {
                using (var contexto = new GeralDbContext(configuracao))
                    {

                    //var _teste = contexto.Cliente.FromSql("Select * from Cliente").ToList();
                    contexto.Database.ExecuteSqlCommand(chamada, parametros);

                    //recebendo o retorno do parametro_id igual exemplo Amauri
                    var _inspVeiculoId = (int)parmInspVeiculo_ID.Value;

                    return _inspVeiculoId;
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

        public static int Existe(int Inspecao_ID, string VIN_6, VDT2.Models.Configuracao configuracao)
            {
            string nomeStoredProcedure = "InspVeiculo_Existe";

            SqlParameter parmInspecao_ID = new SqlParameter("@p_Inspecao_ID", SqlDbType.Int)
                {
                Value = Inspecao_ID
                };
            //CHASSI COM 6 DÍGITOS
            SqlParameter parmVIN_6 = new SqlParameter("@p_VIN_6", SqlDbType.Char)
                {
                Value = VIN_6
                };

            //Chassi com 17 dígitos
            SqlParameter parmVIN = new SqlParameter("@p_VIN", SqlDbType.Char)
                {
                Value = DBNull.Value
                };

            SqlParameter parmInspVeiculo_ID = new SqlParameter("@p_InspVeiculo_ID", SqlDbType.Int)
                {
                Value = 1,
                Direction = ParameterDirection.Output
                };

            SqlParameter[] parametros = new SqlParameter[]
          {
                parmInspecao_ID,
                parmVIN_6,
                parmVIN,
                parmInspVeiculo_ID
          };
            string chamada = $"{nomeStoredProcedure} {parmInspecao_ID.ParameterName}, {parmVIN_6.ParameterName}, {parmVIN.ParameterName}, {parmInspVeiculo_ID.ParameterName} out";
            try
                {
                using (var contexto = new GeralDbContext(configuracao))

                    {
                    contexto.Database.ExecuteSqlCommand(chamada, parametros);

                    string Veiculo_ID = Convert.ToString(parmInspVeiculo_ID.Value);

                    if (!(Veiculo_ID == ""))
                        {
                        var _inspVeiculoId = Convert.ToInt32(Veiculo_ID);
                        return _inspVeiculoId;
                        }
                    else { return 0; }

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


        public static List<InspVeiculo> Listar(int Inspecao_ID, VDT2.Models.Configuracao configuracao)
            {
            List<InspVeiculo> veiculo;
            string nomeStoredProcedure = "InspVeiculo_Lst";

            SqlParameter parmInspecao_ID = new SqlParameter("@p_Inspecao_ID", SqlDbType.Int)
                {
                Value = Inspecao_ID
                };
            
            
            SqlParameter[] parametros = new SqlParameter[]
          {
                parmInspecao_ID

          };
            string chamada = $"{nomeStoredProcedure} {parmInspecao_ID.ParameterName}";
            try
                {
                using (var contexto = new GeralDbContext(configuracao))

                    {
                    veiculo = contexto.InspVeiculo.FromSql(chamada, parametros).ToList();
                    return veiculo;
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

