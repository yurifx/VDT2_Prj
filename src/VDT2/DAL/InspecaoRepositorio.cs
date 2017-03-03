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
    public class InspecaoRepositorio
        {
        public static int Inserir(Inspecao _inspecao, VDT2.Models.Configuracao configuracao)
            {
            // List<Inspecao> _listaInspecao;

            string nomeStoredProcedure = "Inspecao_Ins";


            SqlParameter parmCliente_ID = new SqlParameter("@p_Cliente_ID", SqlDbType.Int)
                {
                Value = _inspecao.Cliente_ID
                };

            SqlParameter parmLocalInspecao_ID = new SqlParameter("@p_LocalInspecao_ID", SqlDbType.Int)
                {
                Value = _inspecao.LocalInspecao_ID
                };

            SqlParameter parmLocalCheckPointID = new SqlParameter("@p_LocalCheckPoint_ID", SqlDbType.Int)
                {
                Value = _inspecao.LocalCheckPoint_ID
                };

            SqlParameter parmTransportador_ID = new SqlParameter("@p_Transportador_ID", SqlDbType.Int)
                {
                Value = _inspecao.Transportador_ID
                };

            SqlParameter parmFrotaViagem_ID = new SqlParameter("@p_FrotaViagem_ID", SqlDbType.Int)
                {
                Value = _inspecao.FrotaViagem_ID
                };

            SqlParameter parmNavio_ID = null;

            if (!_inspecao.Navio_ID.HasValue)
                {  //Verifica se é nulo, se for -> adiciona System.DbNull
                parmNavio_ID = new SqlParameter("@p_Navio_ID", SqlDbType.Int)
                    {
                    Value = DBNull.Value
                    };
                }
            else
                {
                parmNavio_ID = new SqlParameter("@p_Navio_ID", SqlDbType.Int)
                    {
                    Value = _inspecao.Navio_ID.Value
                    };
                }


            SqlParameter parmUsuario_ID = new SqlParameter("@p_Usuario_ID", SqlDbType.Int)
                {
                Value = 1
                };

            SqlParameter parmData = new SqlParameter("@p_Data", SqlDbType.DateTime)
                {
                Value = System.DateTime.Today

                };

            SqlParameter parmInspecao_ID = new SqlParameter("@p_Inspecao_ID", SqlDbType.Int)
                {
                Value = 1,
                Direction = ParameterDirection.Output
                };


            SqlParameter[] parametros = new SqlParameter[]
            {
                parmCliente_ID,
                parmLocalInspecao_ID,
                parmLocalCheckPointID,
                parmTransportador_ID,
                parmFrotaViagem_ID,
                parmNavio_ID,
                parmUsuario_ID,
                parmData,
                parmInspecao_ID
            };


            string chamada = $"{nomeStoredProcedure} {parmCliente_ID.ParameterName}, {parmLocalInspecao_ID.ParameterName}, {parmLocalCheckPointID.ParameterName}, {parmTransportador_ID.ParameterName}, {parmFrotaViagem_ID.ParameterName}, {parmNavio_ID.ParameterName}, {parmUsuario_ID.ParameterName}, {parmData.ParameterName}, {parmInspecao_ID.ParameterName} out";

            try
                {
                using (var contexto = new GeralDbContext(configuracao))
                    {

                    //var _teste = contexto.Cliente.FromSql("Select * from Cliente").ToList();
                    contexto.Database.ExecuteSqlCommand(chamada, parametros);

                    //recebendo o retorno do parametro_id igual exemplo Amauri
                    var _inspecaoId = (int)parmInspecao_ID.Value;

                    return _inspecaoId;
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
        public static Inspecao ListarPorId(int InspecaoId)
            {
            Configuracao configuracao = new Configuracao();
            configuracao.ConnectionStringVDT = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=VDT2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            configuracao.PastaUploadListas = "C\\TMP";

            Inspecao _inspecao;

            using (var contexto = new GeralDbContext(configuracao))
                {
                _inspecao = contexto.Inspecao.ToList().Where(i => i.Inspecao_ID == InspecaoId).FirstOrDefault();
                }


            return _inspecao;
            }

        }


    }
