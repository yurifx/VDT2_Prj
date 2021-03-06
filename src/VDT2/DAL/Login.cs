﻿// <copyright file="Login.cs" company="Bureau Veritas">
// Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Amauri Rodrigues</author>
// <email>amauri.rodrigues@grupoasserth.com.br</email>
// <date>2017-02-19</date>
// <summary>Classe de acesso aos dados do usuário - login</summary>

using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace VDT2.DAL
{
    /// <summary>
    /// Classe de acesso aos dados do usuário - login
    /// </summary>
    public static class Login
    {
        /// <summary>
        /// Consulta a base de dados para autenticar o usuário
        /// </summary>
        /// <param name="usuarioLogin">Identificação (nome de login) do usuário (Usuario.Login)</param>
        /// <param name="configuracao">Configuração geral do aplicativo</param>
        /// <returns></returns>
        public static Models.Usuario EfetuaLogin(string usuarioLogin, VDT2.Models.Configuracao configuracao)
        {
            Diag.Log.Grava(new Diag.LogItem()
               {
                   Nivel = Diag.Nivel.Informacao,
                   Mensagem = $"DAL acionada. Efetua login, parametros informados: usuarioLogin {usuarioLogin}, ConnString {configuracao.ConnectionStringVDT}",
               });

            Models.Usuario dadosUsuario = null;

            string nomeStoredProcedure = "ChkLogin";

            SqlParameter parmUsuarioId = new SqlParameter("@p_Login", SqlDbType.VarChar)
            {
                Value = usuarioLogin
            };

            SqlParameter[] parametros = new SqlParameter[]
            {
                parmUsuarioId
            };

            // Monta a chamada à stored procedure
            string chamada = $"{nomeStoredProcedure} {parmUsuarioId.ParameterName}";
            try
            {
                using (var contexto = new GeralDbContext(configuracao))
                {

                    contexto.Database.OpenConnection();

                    Diag.Log.Grava(new Diag.LogItem { Mensagem = $"Conseguiu conectar com o bd - {chamada}", Nivel = Diag.Nivel.Informacao });

                    dadosUsuario = contexto.Usuario.FromSql(chamada, parametros).FirstOrDefault();

                    if (dadosUsuario != null)
                    {
                        Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"Login.EfetuaLogin realizado com sucesso:  Login: {dadosUsuario.Login}" });
                    }
                    else
                    {
                        Diag.Log.Grava(new Diag.LogItem() { Nivel = Diag.Nivel.Aviso, Mensagem = $"Não encontrou registros com o usuário informado {usuarioLogin}" });
                    }

                }
            }
            catch (System.Exception ex)
            {

                Diag.Log.Grava(new Diag.LogItem() { Nivel = Diag.Nivel.Erro, Mensagem = $"Não conseguiu executar a procedure {nomeStoredProcedure} - Usuário:{usuarioLogin}", Excecao = ex });
                throw;
            }
            return dadosUsuario;
        }
    }
}
