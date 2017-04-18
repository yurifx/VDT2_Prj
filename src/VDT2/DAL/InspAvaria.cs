// <copyright file="InspAvaria.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Amauri Rodrigues & Yuri Vasconcelos</author>
// <email>amauri.rodrigues@grupoasserth.com.br | yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>Classe referente a camada de acesso aos dados - InspAvaria</summary>


//Dependencias
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;

/// <summary>
/// Camada de acesso aos dados do banco de dados - InspAvaria
/// </summary>
namespace VDT2.DAL
{
    public class InspAvaria
    {
        /// <summary>
        /// Insere um novo registro no banco de dados via procedure InspAvaria_Ins
        /// </summary>
        /// <param name="inspAvaria">Dados da avaria a ser gravada</param>
        /// <param name="configuracao">string de conexão do banco de dados</param>
        /// <returns></returns>
        public static Models.InspAvaria Inserir(Models.InspAvaria inspAvaria, VDT2.Models.Configuracao configuracao)
        {
            string nomeStoredProcedure = "InspAvaria_Ins";

            try
            {
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

                SqlParameter parmDanoOrigem = new SqlParameter("@p_DanoOrigem", SqlDbType.Bit)
                {
                    Value = inspAvaria.DanoOrigem
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
                    parmDanoOrigem,
                    parmInspAvaria_ID
                };

                string chamada = $"{nomeStoredProcedure} {parmInspVeiculo_ID.ParameterName}, { parmAvArea_ID.ParameterName}, { parmAvDano_ID.ParameterName}, { parmAvSeveridade_ID.ParameterName}, { parmAvQuadrante_ID.ParameterName}, { parmAvGravidade_ID.ParameterName}, { parmAvCondicao_ID.ParameterName},     { parmFabricaTransporte.ParameterName}, {parmDanoOrigem.ParameterName}, {parmInspAvaria_ID.ParameterName} out";

                using (var contexto = new GeralDbContext(configuracao))
                {
                    contexto.Database.ExecuteSqlCommand(chamada, parametros);

                    inspAvaria.InspAvaria_ID = (int)parmInspAvaria_ID.Value;

                    #region gravalogInformacao
                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"InspAvaria.Inserir realizado com sucesso:  Dados registrados | inspAvaria_ID: {inspAvaria.InspAvaria_ID} | Inspecao_ID: {inspAvaria.Inspecao_ID},  Veiculo_ID: {inspAvaria.InspVeiculo_ID}, Área: {inspAvaria.AvArea_ID}, Condição: {inspAvaria.AvCondicao_ID}, Dano: {inspAvaria.AvDano_ID}, Gravidade: {inspAvaria.AvGravidade_ID}, Quadrante: {inspAvaria.AvQuadrante_ID}, Severidade {inspAvaria.AvSeveridade_ID}"
                        });
                    #endregion

                    return inspAvaria;
                }
            }
            catch (System.Exception ex)
            {
                #region gravalogErro
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar a procedure {nomeStoredProcedure} | Dados informados: Inspecao_ID: {inspAvaria.Inspecao_ID},  Veiculo_ID: {inspAvaria.InspVeiculo_ID}, Área: {inspAvaria.AvArea_ID}, Condição: {inspAvaria.AvCondicao_ID}, Dano: {inspAvaria.AvDano_ID}, Gravidade: {inspAvaria.AvGravidade_ID}, Quadrante: {inspAvaria.AvQuadrante_ID}, Severidade {inspAvaria.AvSeveridade_ID}",
                        Excecao = ex
                    });
                #endregion  

                throw;
            }
        }

        /// <summary>
        /// Lista avarias do cliente e chassi informado via procedure InspAvaria_LstVin
        /// </summary>
        /// <param name="Cliente_ID">ID do cliente</param>
        /// <param name="VIN_6">Chassi</param>
        /// <param name="configuracao">string de conexão com o banco de dados</param>
        /// <returns>Retorna todos os dados da Avaria informada</returns>
        public static List<Models.InspAvaria> Listar(int Cliente_ID, string VIN_6, VDT2.Models.Configuracao configuracao)
        {
            List<Models.InspAvaria> avarias = new List<Models.InspAvaria>();

            string nomeStoredProcedure = "InspAvaria_LstVin";

            try
            {
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

                using (var contexto = new GeralDbContext(configuracao))
                {
                    avarias = contexto.InspAvaria.FromSql(chamada, parametros).ToList();

                    #region gravalogInformacao
                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"InspAvaria.Listar realizado com sucesso - Registros encontrados: {avarias.Count()}"
                        });
                    #endregion

                    return avarias;
                }
            }
            catch (System.Exception ex)
            {
                #region gravalogErro
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar a procedure {nomeStoredProcedure} | Cliente_ID {Cliente_ID}, VIN_6 {VIN_6}",
                        Excecao = ex
                    });
                #endregion
                throw;
            }
        }

        /// <summary>
        /// Lista avarias por ID via procedure InspAvaria_Sel
        /// </summary>
        /// <param name="inspAvaria_ID">Id da avaria</param>
        /// <param name="configuracao">string de conexão com o banco de dados</param>
        /// <returns>Todos os dados da InspAvaria</returns>
        public static Models.InspAvaria Listar(int inspAvaria_ID, Configuracao configuracao)
        {
            Models.InspAvaria inspAvaria = new Models.InspAvaria();
            string nomeStoredProcedure = "InspAvaria_Sel";
            try
            {
                SqlParameter parmInspAvaria_ID = new SqlParameter("@p_InspAvaria_ID", SqlDbType.Int)
                {
                    Value = inspAvaria_ID
                };

                SqlParameter[] parametros = new SqlParameter[]
                {
                    parmInspAvaria_ID
                };

                string chamada = $"{nomeStoredProcedure} {parmInspAvaria_ID.ParameterName}";

                using (var contexto = new GeralDbContext(configuracao))

                {
                    inspAvaria = contexto.InspAvaria.FromSql(chamada, parametros).FirstOrDefault();

                    #region gravalogInformacao
                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"InspAvaria.Listar realizado com sucesso. Registro encontrado: InspAvaria_ID: {inspAvaria.InspAvaria_ID}"
                        });
                    #endregion

                    return inspAvaria;
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
        /// Atualiza os dados no banco de dados - InspAvaria_Upd
        /// </summary>
        /// <param name="inspAvaria">ID e informações da avaria a ser atualizada</param>
        /// <param name="configuracao">string de conexão com o banco de dados</param>
        public static Models.InspAvaria Update(Models.InspAvaria inspAvaria, Configuracao configuracao)
        {
            string nomeStoredProcedure = "InspAvaria_Upd";

            try
            {
                SqlParameter parmInspAvaria_ID = new SqlParameter("@p_InspAvaria_ID", SqlDbType.Int)
                {
                    Value = inspAvaria.InspAvaria_ID
                };

                SqlParameter parmAvArea_ID = new SqlParameter("@p_AvArea_ID", SqlDbType.Int)
                {
                    Value = inspAvaria.AvArea_ID
                };

                SqlParameter parmAvDano_ID = new SqlParameter("@p_AvDano_ID", SqlDbType.Int)
                {
                    Value = inspAvaria.AvDano_ID
                };

                SqlParameter parmAvSeveridade_ID = new SqlParameter("@p_AvSeveridade_ID", SqlDbType.Int)
                {
                    Value = inspAvaria.AvSeveridade_ID
                };

                SqlParameter parmAvQuadrante_ID = new SqlParameter("@p_AvQuadrante_ID", SqlDbType.Int)
                {
                    Value = inspAvaria.AvQuadrante_ID
                };

                SqlParameter parmAvGravidade_ID = new SqlParameter("@p_AvGravidade_ID", SqlDbType.Int)
                {
                    Value = inspAvaria.AvGravidade_ID
                };

                SqlParameter parmAvCondicao_ID = new SqlParameter("@p_AvCondicao_ID", SqlDbType.Int)
                {
                    Value = inspAvaria.AvCondicao_ID
                };

                SqlParameter parmFabricaTransporte = new SqlParameter("@p_FabricaTransporte", SqlDbType.Char)
                {
                    Value = inspAvaria.FabricaTransporte
                };

                SqlParameter parmDanoOrigem = new SqlParameter("@p_DanoOrigem", SqlDbType.Bit)
                {
                    Value = inspAvaria.DanoOrigem
                };

                SqlParameter parmCusto = new SqlParameter("@p_Custo", SqlDbType.Decimal)
                {
                    Value = DBNull.Value
                };

                if (inspAvaria.Custo != 0 && inspAvaria.Custo != null)
                {
                    parmCusto.Value = inspAvaria.Custo;
                }
                else
                {
                    parmCusto.Value = DBNull.Value;
                }

                SqlParameter[] parametros = new SqlParameter[]
                {
                    parmInspAvaria_ID,
                    parmAvArea_ID,
                    parmAvDano_ID,
                    parmAvSeveridade_ID,
                    parmAvQuadrante_ID,
                    parmAvGravidade_ID,
                    parmAvCondicao_ID,
                    parmFabricaTransporte,
                    parmDanoOrigem,
                    parmCusto
                };

                string chamada = $"{nomeStoredProcedure} {parmInspAvaria_ID.ParameterName}, {parmAvArea_ID.ParameterName}, {parmAvDano_ID.ParameterName}, {parmAvSeveridade_ID.ParameterName}, {parmAvQuadrante_ID.ParameterName}, {parmAvGravidade_ID.ParameterName}, {parmAvCondicao_ID.ParameterName}, {parmFabricaTransporte.ParameterName}, {parmDanoOrigem.ParameterName}, {parmCusto.ParameterName}";

                using (var contexto = new GeralDbContext(configuracao))
                {
                    contexto.Database.ExecuteSqlCommand(chamada, parametros);

                    #region gravalogInformacao
                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"InspAvaria.Update realizado com sucesso:  Dados atualizados | inspAvaria_ID: {inspAvaria.InspAvaria_ID} | Inspecao_ID: {inspAvaria.Inspecao_ID},  Veiculo_ID: {inspAvaria.InspVeiculo_ID}, Área: {inspAvaria.AvArea_ID}, Condição: {inspAvaria.AvCondicao_ID}, Dano: {inspAvaria.AvDano_ID}, Gravidade: {inspAvaria.AvGravidade_ID}, Quadrante: {inspAvaria.AvQuadrante_ID}, Severidade {inspAvaria.AvSeveridade_ID}"
                        });
                    #endregion

                    return inspAvaria;
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
        /// Lista avarias por ID - InspAvaria_Sel
        /// </summary>
        /// <param name="inspAvaria_ID">Id da avaria</param>
        /// <param name="configuracao">string de conexão com o banco de dados</param>
        /// <returns>InspAvaria</returns>
        public static List<Models.InspAvaria_Conf> InspAvariaConf(int cliente_ID, int localInspecao_ID, int localCheckPoint_ID, DateTime data, Configuracao configuracao)
        {
            List<Models.InspAvaria_Conf> listaAvarias_conf = new List<Models.InspAvaria_Conf>();
            string nomeStoredProcedure = "InspAvaria_Conf";
            try
            {
                SqlParameter parmClienteID = new SqlParameter("@p_Cliente_ID", SqlDbType.Int)
                {
                    Value = cliente_ID
                };

                SqlParameter parmLocalInspecao_ID = new SqlParameter("@p_LocalInspecao_ID", SqlDbType.Int)
                {
                    Value = localInspecao_ID
                };

                SqlParameter parmLocalCheckPoint_ID = new SqlParameter("@p_LocalCheckPoint_ID", SqlDbType.Int)
                {
                    Value = localCheckPoint_ID
                };

                SqlParameter parmData = new SqlParameter("@p_Data", SqlDbType.DateTime2)
                {
                    Value = data
                };

                SqlParameter[] parametros = new SqlParameter[]
                    {
                        parmClienteID,
                        parmLocalInspecao_ID,
                        parmLocalCheckPoint_ID,
                        parmData
                    };

                string chamada = $"{nomeStoredProcedure} {parmClienteID.ParameterName}, {parmLocalInspecao_ID.ParameterName}, {parmLocalCheckPoint_ID.ParameterName}, {parmData.ParameterName}  ";

                using (var contexto = new GeralDbContext(configuracao))

                {
                    listaAvarias_conf = contexto.InspAvaria_Conf.FromSql(chamada, parametros).ToList();

                    #region gravalogInformacao
                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"InspAvaria.InspAvariaConf realizado com sucesso - Registros encontrados {listaAvarias_conf.Count()}"
                        });
                    #endregion

                    return listaAvarias_conf;
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

        public static void Deletar(int InspAvaria_ID, Configuracao configuracao)
        {
            const string nomeStoredProcedure = "InspAvaria_Del";

            try
            {

                SqlParameter parmInspAvaria_ID = new SqlParameter("@p_InspAvaria_ID", SqlDbType.Int)
                {
                    Value = InspAvaria_ID
                };

                SqlParameter[] parametros = new SqlParameter[]
                {
                    parmInspAvaria_ID
                };

                string chamada = $"{nomeStoredProcedure} {parmInspAvaria_ID.ParameterName}";

                using (var contexto = new GeralDbContext(configuracao))
                {
                    contexto.Database.ExecuteSqlCommand(chamada, parametros);
                    Diag.Log.Grava(new Diag.LogItem
                    {
                        Nivel = Diag.Nivel.Informacao,
                        Mensagem = $"Avaria Deletada: {InspAvaria_ID}"
                    });
                }
            }


            catch (Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem
                {
                    Nivel = Diag.Nivel.Erro,
                    Mensagem = $"Erro ao deletar avaria: {InspAvaria_ID}",
                    Excecao = ex
                });
                throw;
            }

        }

        public static List<InspAvaria_Cons> Consultar(Models.InspAvaria_Cons InspAvaria_Cons, Configuracao configuracao)
        {
            List<Models.InspAvaria_Cons> listaAvarias_cons = new List<Models.InspAvaria_Cons>();
            string nomeStoredProcedure = "InspAvaria_Cons";
            try
            {
                //Cliente
                SqlParameter parmClienteID = new SqlParameter("@p_Cliente_ID", SqlDbType.VarChar)
                {
                    Value = InspAvaria_Cons.Cliente_ID
                };

                //Chassi
                SqlParameter parmChassi = new SqlParameter("@p_Chassi", SqlDbType.VarChar)
                {
                    Value = DBNull.Value
                };

                if (!String.IsNullOrEmpty(InspAvaria_Cons.Chassi))
                {
                    parmChassi.Value = InspAvaria_Cons.Chassi;
                }


                //Local Inspeção
                SqlParameter parmLocalInspecao = new SqlParameter("@p_LocalInspecao", SqlDbType.VarChar)
                {
                    Value = InspAvaria_Cons.LocalInspecao
                };


                //Local CheckPoint
                SqlParameter parmLocalCheckPoint = new SqlParameter("@p_LocalCheckPoint", SqlDbType.VarChar)
                {
                    Value = InspAvaria_Cons.LocalCheckPoint
                };

                //Transportador
                SqlParameter parmTransportador = new SqlParameter("@p_Transportador", SqlDbType.VarChar)
                {
                    Value = InspAvaria_Cons.Transportador
                };


                //Lote
                SqlParameter parmLote = new SqlParameter("@p_Lote", SqlDbType.VarChar)
                {
                    Value = DBNull.Value
                };

                if (!String.IsNullOrEmpty(InspAvaria_Cons.Lote))
                {
                    parmLote.Value = InspAvaria_Cons.Lote;
                }


                //Marca
                SqlParameter parmMarca = new SqlParameter("@p_Marca", SqlDbType.VarChar)
                {
                    Value = InspAvaria_Cons.Marca
                };


                //Modelo
                SqlParameter parmModelo = new SqlParameter("@p_Modelo", SqlDbType.VarChar)
                {
                    Value = InspAvaria_Cons.Modelo
                };

                //Area
                SqlParameter parmArea = new SqlParameter("@p_Area", SqlDbType.VarChar)
                {
                    Value = InspAvaria_Cons.Area
                };

                //Condicao
                SqlParameter parmCondicao = new SqlParameter("@p_Condicao", SqlDbType.VarChar)
                {
                    Value = InspAvaria_Cons.Condicao
                };

                //Dano
                SqlParameter parmDano = new SqlParameter("@p_Dano", SqlDbType.VarChar)
                {
                    Value = InspAvaria_Cons.Dano
                };

                //Quadrante
                SqlParameter parmQuadrante = new SqlParameter("@p_Quadrante", SqlDbType.VarChar)
                {
                    Value = InspAvaria_Cons.Quadrante
                };

                //Gravidade
                SqlParameter parmGravidade = new SqlParameter("@p_Gravidade", SqlDbType.VarChar)
                {
                    Value = InspAvaria_Cons.Gravidade
                };

                //Severidade
                SqlParameter parmSeveridade = new SqlParameter("@p_Severidade", SqlDbType.VarChar)
                {
                    Value = InspAvaria_Cons.Severidade
                };

                //Fábrica ou Transporte? Qual tipo de defeito |F| |T| ou *
                SqlParameter parmTipoDefeito = new SqlParameter("@p_TipoDefeito", SqlDbType.VarChar)
                {
                    Value = InspAvaria_Cons.FabricaTransporte
                };

                //Dano de Origem? |0| |1|
                SqlParameter parmDanoOrigem = new SqlParameter("@p_DanoOrigem", SqlDbType.VarChar)
                {
                    Value = InspAvaria_Cons.DanoOrigem
                };


                //Tipo de Transportador
                SqlParameter parmTransportadorTipo = new SqlParameter("@p_TransportadorTipo", SqlDbType.VarChar)
                {
                    Value = InspAvaria_Cons.TransportadorTipo
                };


                //FrotaViagem
                SqlParameter parmFrotaViagem = new SqlParameter("@p_FrotaViagem", SqlDbType.VarChar)
                {
                    Value = DBNull.Value
                };

                if (!String.IsNullOrEmpty(InspAvaria_Cons.FrotaViagem))
                {
                    parmFrotaViagem.Value = InspAvaria_Cons.FrotaViagem;
                }

                //Nome Navio
                SqlParameter parmNavio = new SqlParameter("@p_Navio", SqlDbType.VarChar)
                {
                    Value = DBNull.Value
                };

                if (!String.IsNullOrEmpty(InspAvaria_Cons.Navio))
                {
                    parmNavio.Value = InspAvaria_Cons.Navio;
                }                                                                 

                SqlParameter parmDataInicio = new SqlParameter("@p_DataInicio", SqlDbType.Date)
                {
                    Value = InspAvaria_Cons.DataInicio
                };

                SqlParameter parmDataFinal = new SqlParameter("@p_DataFinal", SqlDbType.Date)
                {
                    Value = InspAvaria_Cons.DataFinal
                };

                SqlParameter[] parametros = new SqlParameter[]
                    {
                        parmClienteID,
                        parmChassi,
                        parmLocalInspecao,
                        parmLocalCheckPoint,
                        parmTransportador,
                        parmLote,
                        parmMarca,
                        parmModelo,
                        parmArea,
                        parmCondicao,
                        parmDano,
                        parmQuadrante,
                        parmGravidade,
                        parmSeveridade,
                        parmTipoDefeito,
                        parmDanoOrigem,
                        parmTransportadorTipo,
                        parmFrotaViagem,
                        parmNavio,
                        parmDataInicio,
                        parmDataFinal
                    };

                string chamada = 
                    $"{nomeStoredProcedure} " +
                    $"{parmClienteID.ParameterName}, " +
                    $"{parmChassi.ParameterName}, " +
                    $"{parmLocalInspecao.ParameterName}," +
                    $"{parmLocalCheckPoint.ParameterName}, " +
                    $"{parmTransportador.ParameterName}, " +
                    $"{parmLote.ParameterName}, " +
                    $"{parmMarca.ParameterName}, " +
                    $"{parmModelo.ParameterName}, " +
                    $"{parmArea.ParameterName}, " +
                    $"{parmCondicao.ParameterName}," +
                    $"{parmDano.ParameterName}," +
                    $"{parmQuadrante.ParameterName}, " +
                    $"{parmGravidade.ParameterName}," +
                    $"{parmSeveridade.ParameterName}," +
                    $"{parmTipoDefeito.ParameterName}," +
                    $"{parmDanoOrigem.ParameterName}," +
                    $"{parmTransportadorTipo.ParameterName}," +
                    $"{parmFrotaViagem.ParameterName}," +
                    $"{parmNavio.ParameterName}," +
                    $"{parmDataInicio.ParameterName}," +
                    $"{parmDataFinal.ParameterName}";

                using (var contexto = new GeralDbContext(configuracao))

                {
                    listaAvarias_cons = contexto.InspAvaria_Cons.FromSql(chamada, parametros).ToList();

                    #region gravalogInformacao
                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"InspAvaria.InspAvariaCons realizado com sucesso - Registros encontrados {listaAvarias_cons.Count()}"
                        });
                    #endregion

                    return listaAvarias_cons;
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




    }
}