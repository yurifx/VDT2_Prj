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
    public class FrotaViagemRepositorio
        {
        public static List<FrotaViagem> Listar(string _tipoTransportador, int _transportadorId, VDT2.Models.Configuracao configuracao)
            {
            //TODO: -> Receber dados do config

            List<FrotaViagem> _listaFrotaViagem;

            string nomeStoredProcedure = "FrotaViagem_lst";

            SqlParameter parmTipo = new SqlParameter("@p_Tipo", SqlDbType.Char)
                {
                Value = _tipoTransportador
                };

            SqlParameter parmTranportadorId = new SqlParameter("@p_Tranportador_ID", SqlDbType.Int)
                {
                Value = _transportadorId
                };

            SqlParameter parmAtivos = new SqlParameter("@p_Ativos", SqlDbType.Bit)
                {
                Value = 1
                };

            SqlParameter[] parametros = new SqlParameter[]
            {
                parmTipo,
                parmTranportadorId,
                parmAtivos
            };

            // Monta a chamada à stored procedure
            string chamada = $"{nomeStoredProcedure} {parmTipo.ParameterName} , {parmTranportadorId.ParameterName}, {parmAtivos.ParameterName}";

            try
                {
                using (var contexto = new GeralDbContext(configuracao))
                    {
                    //_listaLocaisInspecao = contexto.LocalInspecao.FromSql("Select * from LocalInspecao").ToList();
                    _listaFrotaViagem = contexto.FrotaViagem.FromSql(chamada, parametros).ToList();
                    //An exception of type 'System.InvalidOperationException' occurred in Microsoft.EntityFrameworkCore.dll but was not handled in user code
                    //Additional information: The required column 'LocalCheckPoint_ID' was not present in the results of a 'FromSql' operation.
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
            return _listaFrotaViagem;
            }


        public static int Inserir(int transportadorId, string nome, VDT2.Models.Configuracao configuracao)
            {
            string nomeStoredProcedure = "FrotaViagem_Ins";

            SqlParameter parmtransportadorId = new SqlParameter("@p_Transportador_ID", SqlDbType.VarChar)
                {
                Value = transportadorId
                };

            SqlParameter parmNome = new SqlParameter("@p_Nome", SqlDbType.VarChar)
                {
                Value = nome
                };

            SqlParameter parmFrotaViagemId = new SqlParameter("@p_FrotaViagem_ID", SqlDbType.Int)
                {
                Value = 1,
                Direction = ParameterDirection.Output
                };

            SqlParameter[] parametros = new SqlParameter[]
            {
                parmtransportadorId,
                parmNome,
                parmFrotaViagemId
            };

            string chamada = $"{nomeStoredProcedure} {parmtransportadorId.ParameterName}, {parmNome.ParameterName}, {parmFrotaViagemId.ParameterName} out";

            using (var contexto = new GeralDbContext(configuracao))
                {
                contexto.Database.ExecuteSqlCommand(chamada, parametros);
                //recebe retorno
                var _frotaViagemId = (int)parmFrotaViagemId.Value;
                return _frotaViagemId;
                }

            }

        }
    }

