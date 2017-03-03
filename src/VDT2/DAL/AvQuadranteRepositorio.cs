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
    public class AvQuadranteRepositorio
    {
        public static List<AvQuadrante> Listar(int clientId, VDT2.Models.Configuracao configuracao)
            {
            List<AvQuadrante> AvQuadrante;
            //TODO: -> Receber dados do config
            
            string nomeStoredProcedure = "AvQuadrante_Lst";

            SqlParameter parmClienteID = new SqlParameter("@p_Cliente_ID", SqlDbType.Int)
                {
                Value = clientId
                };


            SqlParameter[] parametros = new SqlParameter[]
             {
                 parmClienteID
           };

            string chamada = $"{nomeStoredProcedure} {parmClienteID.ParameterName}";

            try
                {
                using (var contexto = new GeralDbContext(configuracao))
                    {
                    //todo - Arrumar para pegar via Procedure
                    //listaMarcas = contexto.Marca.ToList();
                    //var _teste = contexto.Cliente.FromSql("Select * from Cliente").ToList();
                    AvQuadrante = contexto.AvQuadrante.FromSql(chamada, parametros).ToList();
                    return AvQuadrante;
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
