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
    public class ClienteRepositorio
        {
        public static List<Cliente> Listar(int usuarioID, VDT2.Models.Configuracao configuracao)
            {
            List<Cliente> _listaCliente;

            //TODO: -> Receber dados do config
//            Configuracao configuracao = new Configuracao();
  //          configuracao.ConnectionStringVDT = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=VDT2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            configuracao.PastaUploadListas = "C\\TMP";

            string nomeStoredProcedure = "Cliente_Lst";

            SqlParameter parmUsuarioId = new SqlParameter("@p_Usuario_ID", SqlDbType.Int)
                {
                Value = usuarioID
                };

            SqlParameter parmAtivos = new SqlParameter("@p_Ativos", SqlDbType.Bit)
                {
                Value = 0
                //Value = System.DBNull.Value
                
                };

            SqlParameter[] parametros = new SqlParameter[]
            {
                parmUsuarioId,
                parmAtivos

            };

            string chamada = $"{nomeStoredProcedure} {parmUsuarioId.ParameterName}, {parmAtivos.ParameterName}";

            try
                {
                using (var contexto = new GeralDbContext(configuracao))
                    {

                    //var _teste = contexto.Cliente.FromSql("Select * from Cliente").ToList();
                    _listaCliente = contexto.Cliente.FromSql(chamada, parametros).ToList();


                    return _listaCliente;
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
