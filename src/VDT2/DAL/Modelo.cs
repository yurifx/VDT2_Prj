using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;


/// <summary>
/// Camada de acesso aos dados - Modelo
/// </summary>
namespace VDT2.DAL
{
    public class Modelo
    {
        /// <summary>
        /// Lista todos os modelos do banco de dados para o cliente_id informado
        /// </summary>
        /// <param name="cliente_ID">id do cliente</param>
        /// <param name="configuracao">string de conexão com o banco de dados</param>
        /// <returns>Lista de modelos</returns>
        public static List<Models.Modelo> Listar(int cliente_ID, VDT2.Models.Configuracao configuracao)
        {
            List<Models.Modelo> listaModelo = new List<Models.Modelo>();

            string nomeStoredProcedure = "Modelo_Lst";
            string _mensagemErro = "Erro ao listar marcas DAL";

            try
            {
                SqlParameter parmClienteID = new SqlParameter("@p_Cliente_ID", SqlDbType.Int)
                {
                    Value = cliente_ID
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

                using (var contexto = new GeralDbContext(configuracao))
                {
                    listaModelo = contexto.Modelo.FromSql(chamada, parametros).ToList();

                    #region gravalogInformacao
                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"Modelo.Listar() realizado com sucesso:  Registros encontrados: {listaModelo.Count()}"
                        });
                    #endregion  

                    return listaModelo;
                }
            }
            catch (System.Exception ex)
            {
                listaModelo.Add(new Models.Modelo { Erro = true, MensagemErro = _mensagemErro });

                #region gravalogErro
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar a procedure {nomeStoredProcedure}",
                        Excecao = ex
                    });
                #endregion

                return listaModelo;
            }
        }

        /// <summary>
        /// Insere um novo modelo no banco de dados
        /// </summary>
        /// <param name="cliente_ID">ID do cliente</param>
        /// <param name="nomeNovoModelo">Nome do novo Modelo</param>
        /// <param name="configuracao">strnig de conexão com o banco de dados</param>
        /// <returns></returns>
        public static int Inserir(int cliente_ID, string nomeNovoModelo, Configuracao configuracao)
        {
            string nomeStoredProcedure = "Modelo_Ins";
            try
            {
                SqlParameter parmCliente_ID = new SqlParameter("@p_Cliente_ID", SqlDbType.Int)
                {
                    Value = cliente_ID
                };

                SqlParameter parmNome = new SqlParameter("@p_Nome", SqlDbType.VarChar)
                {
                    Value = nomeNovoModelo
                };

                SqlParameter parmAtivo = new SqlParameter("@p_Ativo", SqlDbType.Int)
                {
                    Value = 1
                };

                SqlParameter parmModelo_ID = new SqlParameter("@p_Modelo_ID", SqlDbType.Int)
                {
                    Value = 1,
                    Direction = ParameterDirection.Output
                };

                SqlParameter[] parametros = new SqlParameter[]
                {
                    parmCliente_ID,
                    parmNome,
                    parmAtivo,
                    parmModelo_ID
                };

                string chamada = $"{nomeStoredProcedure} {parmCliente_ID.ParameterName}, {parmNome.ParameterName}, {parmAtivo.ParameterName}, {parmModelo_ID.ParameterName} out";

                using (var contexto = new GeralDbContext(configuracao))
                {
                    contexto.Database.ExecuteSqlCommand(chamada, parametros);

                    var id = (int)parmModelo_ID.Value;

                    #region gravalogInformacao
                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = $"Modelo.Inserir realizado com sucesso:  Dados atualizados | Modelo_ID {id}"
                        });
                    #endregion  

                    return id;
                }
            }
            catch (System.Exception ex)
            {
                #region gravalogErro
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar a procedure {nomeStoredProcedure} | ClienteID {cliente_ID}, Nome Modelo: {nomeNovoModelo}",
                        Excecao = ex
                    });
                throw;
                #endregion
            }
        }
    }
}
