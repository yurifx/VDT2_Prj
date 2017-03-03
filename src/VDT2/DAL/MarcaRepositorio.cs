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
    public class MarcaRepositorio
        {
        public static List<Marca> Listar(int clientId, VDT2.Models.Configuracao configuracao)
            {
            List<Marca> listaMarcas;

            string nomeStoredProcedure = "Marca_Lst";

            SqlParameter parmClienteID = new SqlParameter("@p_Cliente_ID", SqlDbType.Int)
                {
                Value = clientId
                };

            SqlParameter parmAtivos = new SqlParameter("@p_Ativos", SqlDbType.Bit)
                {
                Value = true
                };

            SqlParameter[] parametros = new SqlParameter[]
             {
                 parmClienteID,
                 parmAtivos
            };

            string chamada = $"{nomeStoredProcedure} {parmClienteID.ParameterName} , {parmAtivos.ParameterName}";
            try
                {
                using (var contexto = new GeralDbContext(configuracao))
                    {
                    //todo - Arrumar para pegar via Procedure
                    //listaMarcas = contexto.Marca.ToList();
                    //var _teste = contexto.Cliente.FromSql("Select * from Cliente").ToList();
                    listaMarcas = contexto.Marca.FromSql(chamada, parametros).ToList();
                    return listaMarcas;
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
