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
    public class TransportadorRepositorio
        {
        public static List<Transportador> Listar(int ativos, VDT2.Models.Configuracao configuracao)
            {
            List<Transportador> _listaTransportador;

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

            try
                {
                using (var contexto = new GeralDbContext(configuracao))
                    {
                    var _teste = contexto.Transportador.FromSql("Select * from Transportador").ToList();
                    _listaTransportador = contexto.Transportador.FromSql(chamada, parametros).ToList();
                    return _listaTransportador;
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
