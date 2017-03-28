using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;


/// <summary>
/// Camada de acesso aos dados ListaVeiculos
/// </summary>
namespace VDT2.DAL
{
    public class ListaVeiculos
    {
        /// <summary>
        /// Insere a lista de veiculos - VIN17  no banco de dados via prcedure ListaVeiculos_Ins
        /// </summary>
        /// <param name="listaVeiculos">Dados referentes a Lista</param>
        /// <param name="configuracao">Dados da configuração informada</param>
        /// <returns></returns>
        public static Models.ListaVeiculos Inserir(Models.ListaVeiculos listaVeiculos, Configuracao configuracao)
        {
            string nomeStoredProcedure = "ListaVeiculos_Ins";

            try
            {
                SqlParameter parmClienteID = new SqlParameter("@p_Cliente_ID", SqlDbType.Int)
                {
                    Value = listaVeiculos.Cliente_ID
                };

                SqlParameter parmUsuarioID = new SqlParameter("@p_Usuario_ID", SqlDbType.Int)
                {
                    Value = listaVeiculos.Usuario_ID
                };

                SqlParameter parmNomeArquivo = new SqlParameter("@p_NomeArquivo", SqlDbType.VarChar)
                {
                    Value = listaVeiculos.NomeArquivo
                };

                SqlParameter parmLocalInspecaoID = new SqlParameter("@p_LocalInspecao_ID", SqlDbType.Int)
                {
                    Value = listaVeiculos.LocalInspecao_ID
                };

                SqlParameter parmTipo = new SqlParameter("@p_Tipo", SqlDbType.Char)
                {
                    Value = listaVeiculos.Tipo
                };

                SqlParameter parmListaVeiculo_ID = new SqlParameter("@p_ListaVeiculo_ID", SqlDbType.Int)
                {
                    Value = 1,
                    Direction = ParameterDirection.Output
                };


                SqlParameter[] parametros = new SqlParameter[]
                {
                    parmClienteID,
                    parmUsuarioID,
                    parmNomeArquivo,
                    parmLocalInspecaoID,
                    parmTipo,
                    parmListaVeiculo_ID
                };

                string chamada = $"{nomeStoredProcedure} {parmClienteID.ParameterName}, {parmUsuarioID.ParameterName}, {parmNomeArquivo.ParameterName}, {parmLocalInspecaoID.ParameterName}, {parmTipo.ParameterName}, {parmListaVeiculo_ID.ParameterName} out";

                using (var contexto = new GeralDbContext(configuracao))
                {
                    contexto.Database.ExecuteSqlCommand(chamada, parametros);

                    listaVeiculos.ListaVeiculo_ID = (int)parmListaVeiculo_ID.Value;

                    #region gravalogInformacao
                    Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Informacao,
                        Mensagem = $"ListaVeiculos.Inserir registrado com sucesso - ListaVeiculo_ID = {listaVeiculos.ListaVeiculo_ID}"
                    });
                    #endregion

                    return listaVeiculos;
                }

            }

            catch (Exception ex)
            {
                #region gravalogErro
                Diag.Log.Grava(
                   new Diag.LogItem()
                   {
                       Nivel = Diag.Nivel.Erro,
                       Mensagem = $"Erro ao executar ListaVeiculos.Inserir: Erro:  {ex}"
                   });
                #endregion

                return listaVeiculos;
            }
        }
    }
}
