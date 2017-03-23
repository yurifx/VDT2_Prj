using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;

/// <summary>
/// Camada de acesso aos dados InspVeículo
/// </summary>
namespace VDT2.DAL
{
    public class InspVeiculo
    {
        /// <summary>
        /// Inserção de dados do veículo - inspVeiculo 
        /// </summary>
        /// <param name="inspVeiculo">Obj contendo os dados do veículo a ser inserido no banco de dados</param>
        /// <param name="configuracao">strnig de conexão com o banco de dados</param>
        /// <returns></returns>
        public static Models.InspVeiculo Inserir(Models.InspVeiculo inspVeiculo, VDT2.Models.Configuracao configuracao)
        {
            string nomeStoredProcedure = "InspVeiculo_Ins";
            try
            {
                SqlParameter parmInspecao_ID = new SqlParameter("@p_Inspecao_ID", SqlDbType.Int)
                {
                    Value = inspVeiculo.Inspecao_ID
                };

                SqlParameter parmMarca_ID = new SqlParameter("@p_Marca_ID", SqlDbType.Int)
                {
                    Value = inspVeiculo.Marca_ID
                };

                SqlParameter parmModelo_ID = new SqlParameter("@p_Modelo_ID", SqlDbType.Int)
                {
                    Value = inspVeiculo.Modelo_ID
                };

                //CHASSI COM 6 DÍGITOS
                SqlParameter parmVIN_6 = new SqlParameter("@p_VIN_6", SqlDbType.Char)
                {
                    Value = inspVeiculo.VIN_6
                };


                SqlParameter parmVIN = new SqlParameter("@p_VIN", SqlDbType.Char)
                {
                    //  Value = inspVeiculo.VIN
                    Value = DBNull.Value
                };

                SqlParameter parmObservacoes = new SqlParameter("@Observacoes", SqlDbType.Char)
                {
                    Value = inspVeiculo.Observacoes
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

                using (var contexto = new GeralDbContext(configuracao))
                {
                    contexto.Database.ExecuteSqlCommand(chamada, parametros);
                    inspVeiculo.InspVeiculo_ID = (int)parmInspVeiculo_ID.Value;
                    #region gravalogInformacao
                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"BLL.InspVeiculo.Inserir realizado com sucesso:  Dados atualizados | inspVeiculo_ID: {inspVeiculo.InspVeiculo_ID}"
                        });
                    #endregion
                    return inspVeiculo;
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
        /// <summary>
        /// Verifica se já existe um registro no banco de dados com os parametros informados -  InspVeiculo_Existe
        /// </summary>
        /// <param name="Inspecao_ID">ID do veículo</param>
        /// <param name="VIN_6">Chassi com 6 dígitos</param>
        /// <param name="configuracao">String de conexão com o banco de dados</param>
        /// <returns>int Número do inspVeículo_ID</returns>
        public static int Existe(int Inspecao_ID, string VIN_6, VDT2.Models.Configuracao configuracao)
        {
            string nomeStoredProcedure = "InspVeiculo_Existe";
            try
            {
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

                using (var contexto = new GeralDbContext(configuracao))

                {
                    contexto.Database.ExecuteSqlCommand(chamada, parametros);
                    string Veiculo_ID = Convert.ToString(parmInspVeiculo_ID.Value);
                    if (!(Veiculo_ID == ""))
                    {
                        var _inspVeiculoId = Convert.ToInt32(Veiculo_ID);
                        #region gravalogResultado
                        Diag.Log.Grava(
                            new Diag.LogItem()
                            {
                                Nivel = Diag.Nivel.Informacao,
                                Mensagem = $"InspVeiculo.Existe() realizado com sucesso:  id encontrado: {_inspVeiculoId}"
                            });
                        #endregion
                        return _inspVeiculoId;
                    }
                    else
                    {
                        return 0;
                    }
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
                return -1;
            }
        }

        /// <summary>
        /// Lista todos os dados do veículo via ID -InspVeiculo_Sel
        /// </summary>
        /// <param name="InspVeiculo_ID">ID do veículo a ser buscado</param>
        /// <param name="configuracao">string de conexão com o banco de dados</param>
        /// <returns></returns>
        public static Models.InspVeiculo ListarPorId(int InspVeiculo_ID, VDT2.Models.Configuracao configuracao)
        {
            Models.InspVeiculo veiculo;
            string nomeStoredProcedure = "InspVeiculo_Sel";
            try
            {
                SqlParameter parmInspecao_ID = new SqlParameter("@p_Inspecao_ID", SqlDbType.Int)
                {
                    Value = InspVeiculo_ID
                };

                SqlParameter[] parametros = new SqlParameter[]
                    {
                parmInspecao_ID
                    };

                string chamada = $"{nomeStoredProcedure} {parmInspecao_ID.ParameterName}";

                using (var contexto = new GeralDbContext(configuracao))

                {
                    veiculo = contexto.InspVeiculo.FromSql(chamada, parametros).FirstOrDefault();
                    return veiculo;
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
                throw;
                #endregion
            }
        }
        /// <summary>
        /// Atualização dos dados  - InspVeiculo_Upd
        /// </summary>
        /// <param name="inspVeiculo">Obj contendo os dados do veículo a ser atualizado</param>
        /// <param name="configuracao">string de conexão com o banco de dados</param>
        public static Models.InspVeiculo Update(Models.InspVeiculo inspVeiculo, VDT2.Models.Configuracao configuracao)
        {
            string nomeStoredProcedure = "InspVeiculo_Upd";
            try
            {
                SqlParameter parmInspVeiculo_ID = new SqlParameter("@p_Inspecao_ID", SqlDbType.Int)
                {
                    Value = inspVeiculo.InspVeiculo_ID
                };

                SqlParameter parmMarca_ID = new SqlParameter("@p_Marca_ID", SqlDbType.Int)
                {
                    Value = inspVeiculo.Marca_ID
                };

                SqlParameter parmModelo_ID = new SqlParameter("@p_Modelo_ID", SqlDbType.Int)
                {
                    Value = inspVeiculo.Modelo_ID
                };

                //CHASSI COM 6 DÍGITOS
                SqlParameter parmVIN_6 = new SqlParameter("@p_VIN_6", SqlDbType.Char)
                {
                    Value = inspVeiculo.VIN_6
                };

                //CHASSI COM 17 DÍGITOS, NECESSÁRIO LÓGICA PARA PEGAR ESSA NUMERAÇÃO
                SqlParameter parmVIN = new SqlParameter("@p_VIN", SqlDbType.Char)
                {
                    //  Value = _inspVeiculo.VIN
                    Value = DBNull.Value
                };

                SqlParameter parmObservacoes = new SqlParameter("@Observacoes", SqlDbType.Char)
                {
                    Value = inspVeiculo.Observacoes
                };

                SqlParameter[] parametros = new SqlParameter[]
                {
                parmInspVeiculo_ID,
                parmMarca_ID,
                parmModelo_ID,
                parmVIN_6,
                parmVIN,
                parmObservacoes
                };

                string chamada = $"{nomeStoredProcedure} {parmInspVeiculo_ID.ParameterName}, { parmMarca_ID.ParameterName}, { parmModelo_ID.ParameterName}, { parmVIN_6.ParameterName}, { parmVIN.ParameterName}, { parmObservacoes.ParameterName}";

                using (var contexto = new GeralDbContext(configuracao))
                {
                    contexto.Database.ExecuteSqlCommand(chamada, parametros);
                    #region gravalogInformacao
                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"BLL.InspVeiculo.Inserir realizado com sucesso:  Dados atualizados | inspVeiculo_ID: {inspVeiculo.InspVeiculo_ID}"
                        });
                    #endregion
                    return inspVeiculo;
                }
            }

            catch (System.Exception ex)
            {
                #region gravaloErro
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar a procedure {nomeStoredProcedure} | Parametros: InspVeiculo: {inspVeiculo.InspVeiculo_ID}",
                        Excecao = ex
                    });
                #endregion  
                throw;
            }
        }

        public static bool IntegrarVIN(int Cliente_ID, int LocalInspecao_ID, Configuracao configuracao)
        {

            string nomeStoredProcedure = "IntegraVinVeiculos";
            try
            {
                SqlParameter parmCliente_ID = new SqlParameter("@p_Cliente_ID", SqlDbType.Int)
                {
                    Value = Cliente_ID
                };

                SqlParameter parmLocalInspecao_ID = new SqlParameter("@p_LocalInspecao_ID", SqlDbType.Int)
                {
                    Value = LocalInspecao_ID
                };

                SqlParameter[] parametros = new SqlParameter[]
                {
                        parmCliente_ID,
                        parmLocalInspecao_ID
                };

                string chamada = $"{nomeStoredProcedure} {parmCliente_ID.ParameterName}, { parmLocalInspecao_ID.ParameterName}";

                using (var contexto = new GeralDbContext(configuracao))
                {
                    contexto.Database.ExecuteSqlCommand(chamada, parametros);
                    #region gravalogInformacao
                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"BLL.InspVeiculo.IntegrarVIN realizado com sucesso"
                        });
                    #endregion
                    return true;
                }
            }
            catch (Exception ex)
            {
                #region gravaLogErro
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"BLL.InspVeiculo.ReceberVin Não realizado, Erro: {ex}"
                    });
                #endregion
                return false;
            }

        }

    }



}

