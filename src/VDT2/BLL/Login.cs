// <copyright file="Login.cs" company="Bureau Veritas">
// Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Amauri Rodrigues</author>
// <email>amauri.rodrigues@grupoasserth.com.br</email>
// <date>2017-02-19</date>
// <summary>Autenticação de usuário</summary>

using System.Collections.Generic;
using System.Linq;

namespace VDT2.BLL
{
    /// <summary>
    /// Autenticação de usuário
    /// </summary>
    public static class Login
    {
        /// <summary>
        /// Consulta a base de dados para autenticar o usuário
        /// </summary>
        /// <param name="usuarioId">Identificação do usário (tbUsr.Login)</param>
        /// <param name="senha">Senha do usuário</param>
        /// <param name="configuracao">Configuração geral do aplicativo</param>
        /// <returns>Dados do usuário</returns>
        public static Models.Usuario EfetuaLogin(string usuarioId, string senha, VDT2.Models.Configuracao configuracao) {

            Models.Usuario dadosUsuario = DAL.Login.EfetuaLogin(usuarioId, configuracao);

            if (dadosUsuario == null) {
                return null;
            }

            if (senha != dadosUsuario.Senha) {
                dadosUsuario.Autenticou = false;
            }
            else {
                dadosUsuario.Autenticou = true;
            }

            return dadosUsuario;
        }

        /// <summary>
        /// Dados do usuário, extraídos do 'cookie'
        /// </summary>
        /// <param name="dadosUsuarioCookie"></param>
        /// <returns></returns>
        public static ViewModels.LoginViewModel ExtraiDadosUsuario(IEnumerable<System.Security.Claims.Claim> dadosUsuarioCookie) {

            ViewModels.LoginViewModel dadosUsuario = null;

            if (dadosUsuarioCookie.Count() > 0) {

                string login = dadosUsuarioCookie.Where(x => x.Type == "Login").Select(x => x.Value).FirstOrDefault();

                string nome = dadosUsuarioCookie.Where(x => x.Type == "Nome").Select(x => x.Value).FirstOrDefault();

                string usuarioIdTmp = dadosUsuarioCookie.Where(x => x.Type == "Usuario_ID").Select(x => x.Value).FirstOrDefault();

                int usuarioId = int.Parse(usuarioIdTmp);

                string inspetorTmp = dadosUsuarioCookie.Where(x => x.Type == "Inspetor").Select(x => x.Value).FirstOrDefault();
                bool inspetor = (inspetorTmp == "1");

                dadosUsuario = new ViewModels.LoginViewModel()
                {
                    Autenticado = true,
                    Identificacao = login,
                    Nome = nome,
                    UsuarioId = usuarioId,
                    Inspetor = inspetor
                };
            }

            return dadosUsuario;
        }
    }
}
