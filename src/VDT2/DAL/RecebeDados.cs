using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;

namespace VDT2.DAL
    //DELETAR ESTA CLASSE ****** 24.02.2017
    {
    public class RecebeDados
        {

        public static List<Cliente> Cliente(int usuarioID)
            {
            List<Cliente> _listaCliente;

            //TODO: -> Receber dados do config
            Configuracao configuracao = new Configuracao();
            configuracao.ConnectionStringVDT = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=VDT2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            configuracao.PastaUploadListas = "C\\TMP";

            string nomeStoredProcedure = "Cliente_Lst";

            SqlParameter parmUsuarioId = new SqlParameter("@p_Usuario_ID", SqlDbType.Int)
                {
                Value = usuarioID
                };

            SqlParameter parmAtivos = new SqlParameter("@p_Ativos", SqlDbType.Bit)
                {
                Value = 0
                };

            SqlParameter[] parametros = new SqlParameter[]
            {
                parmUsuarioId,
                parmAtivos

            };

            string chamada = $"{nomeStoredProcedure} {parmUsuarioId.ParameterName}, {parmAtivos.ParameterName}";

            using (var contexto = new GeralDbContext(configuracao))
                {

                //var _teste = contexto.Cliente.FromSql("Select * from Cliente").ToList();
                _listaCliente = contexto.Cliente.FromSql(chamada, parametros).ToList();


                return _listaCliente;
                }

            }
        
        public static List<LocalInspecao> LocaisIspecao()
            {
            //TODO: -> Receber dados do config

            List<LocalInspecao> _listaLocaisInspecao;

            Configuracao configuracao = new Configuracao();
            configuracao.ConnectionStringVDT = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=VDT2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            configuracao.PastaUploadListas = "C\\TMP";

            string nomeStoredProcedure = "LocalInspecao_lst";

            SqlParameter parmUsuarioId = new SqlParameter("@p_Usuario_ID", SqlDbType.Int)
                {
                Value = 1
                };

            SqlParameter parmAtivos = new SqlParameter("@p_Ativos", SqlDbType.Bit)
                {
                Value = 1
                };

            SqlParameter[] parametros = new SqlParameter[]
            {
                parmUsuarioId,
                parmAtivos

            };

            // Monta a chamada à stored procedure
            //yuri markup
            string chamada = $"{nomeStoredProcedure} {parmUsuarioId.ParameterName} , {parmAtivos.ParameterName}";


            
            using (var contexto = new GeralDbContext(configuracao))
                {
                //_listaLocaisInspecao = contexto.LocalInspecao.FromSql("Select * from LocalInspecao").ToList();
                 _listaLocaisInspecao = contexto.LocalInspecao.FromSql(chamada, parametros).ToList();
                //An exception of type 'System.InvalidOperationException' occurred in Microsoft.EntityFrameworkCore.dll but was not handled in user code
                //Additional information: The required column 'LocalCheckPoint_ID' was not present in the results of a 'FromSql' operation.
                }
            return _listaLocaisInspecao;
            }


        public static List<LocalCheckPoint> LocaisCheckPoint(int usuarioId)
            {
            List<LocalCheckPoint> _listaLocaisCheckPoint;

            //TODO: -> Receber dados do config
            Configuracao configuracao = new Configuracao();
            configuracao.ConnectionStringVDT = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=VDT2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            configuracao.PastaUploadListas = "C\\TMP";



            string nomeStoredProcedure = "LocalCheckPoint_Lst";

            SqlParameter parmUsuarioId = new SqlParameter("@p_Usuario_ID", SqlDbType.Int)
                {
                Value = usuarioId
                };

            SqlParameter parmAtivos = new SqlParameter("@p_Ativos", SqlDbType.Bit)
                {
                Value = 0
                };

            SqlParameter[] parametros = new SqlParameter[]
            {
                parmUsuarioId,
                parmAtivos

            };

            // Monta a chamada à stored procedure
            //yuri markup
            string chamada = $"{nomeStoredProcedure} {parmUsuarioId.ParameterName} , {parmAtivos.ParameterName}";

            using (var contexto = new GeralDbContext(configuracao))
                {

                // _listaLocaisCheckPoint = contexto.LocalCheckPoint.FromSql("Select * from LocalCheckPoint").ToList();
                _listaLocaisCheckPoint = contexto.LocalCheckPoint.FromSql(chamada, parametros).ToList();
                //An exception of type 'System.InvalidOperationException' occurred in Microsoft.EntityFrameworkCore.dll but was not handled in user code
                //Additional information: The required column 'LocalCheckPoint_ID' was not present in the results of a 'FromSql' operation.

                return _listaLocaisCheckPoint;
                }

            }

        public static List<Transportador> Transportador(int ativos)
            {
            List<Transportador> _listaTransportador;

            //TODO: -> Receber dados do config
            Configuracao configuracao = new Configuracao();
            configuracao.ConnectionStringVDT = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=VDT2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            configuracao.PastaUploadListas = "C\\TMP";



            string nomeStoredProcedure = "Transportador_Lst";

            SqlParameter parmAtivos = new SqlParameter("@p_Ativos", SqlDbType.Bit)
                {
                Value = 0
                };

            SqlParameter[] parametros = new SqlParameter[]
            {
                parmAtivos
            };
            
            string chamada = $"{nomeStoredProcedure} {parmAtivos.ParameterName}";

            using (var contexto = new GeralDbContext(configuracao))
                {

                var _teste = contexto.Transportador.FromSql("Select * from Transportador").ToList();
                _listaTransportador = contexto.Transportador.FromSql(chamada, parametros).ToList();
                

                return _listaTransportador;
                }

            }


        }
    }
