// <copyright file="HomeController.cs" company="Bureau Veritas">
// Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Amauri Rodrigues</author>
// <email>amauri.rodrigues@grupoasserth.com.br</email>
// <date>2017-02-19</date>
// <summary>Controllers inicial - login</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;  // Para usar IOptions
using VDT2.DAL;
using VDT2.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace VDT2.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize(ActiveAuthenticationSchemes = VDT2.BLL.Globais.NomeCookieAutenticacao)]
    public class HomeController : Controller
    {
        /// <summary>
        /// Configuração geral do aplicativo, carregada de appsettings.json
        /// </summary>
        private VDT2.Models.Configuracao configuracao { get; set; }

        /// <summary>
        /// Construtor da classe
        /// <para>Recebe a configuração do aplicativo, usando Dependency Injection</para>
        /// </summary>
        /// <param name="settings">Configuração geral do aplicativo, carregada de appsettings.json</param>
        public HomeController(IOptions<VDT2.Models.Configuracao> settings)
        {

            this.configuracao = settings.Value;
        }

        /// <summary>
        /// Método acionado depois que o usuário já foi autenticado
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public IActionResult Index(string returnUrl)

        {

            // Testes - INICIO
            if (this.HttpContext.User == null)
            {

                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Informacao,
                        Mensagem = "this.HttpContext.User == null"
                    });
            }
            else if (this.HttpContext.User.Identity == null)
            {

                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Informacao,
                        Mensagem = "this.HttpContext.User.Identity == null"
                    });
            }
            else if (this.HttpContext.User.Identity.Name == null)
            {

                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Informacao,
                        Mensagem = "this.HttpContext.User.Identity.Name == null"
                    });
            }
            else
            {
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Informacao,
                        Mensagem = "this.HttpContext.User.Identity.Name:" + this.HttpContext.User.Identity.Name
                    });
            }

            if (this.HttpContext.User.Identity != null)
            {
                var identityAux = (System.Security.Claims.ClaimsIdentity)User.Identity;

                if (identityAux == null)
                {

                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = "identityAux == null"
                        });
                }
                else
                {
                    IEnumerable<System.Security.Claims.Claim> claimsAux = identityAux.Claims;

                    if (claimsAux == null)
                    {

                        Diag.Log.Grava(
                            new Diag.LogItem()
                            {
                                Nivel = Diag.Nivel.Informacao,
                                Mensagem = "claimsAux == null"
                            });
                    }
                    else
                    {
                        Diag.Log.Grava(
                            new Diag.LogItem()
                            {
                                Nivel = Diag.Nivel.Informacao,
                                Mensagem = "identityAux.Claims:"
                            });

                        foreach (var x in claimsAux)
                        {
                            string s = $"  Type: {x.Type} Value:{x.Value}";
                            if (x.Properties != null && x.Properties.Count > 0)
                            {
                                foreach (var p in x.Properties)
                                {
                                    s += $" Key:{p.Key} Value:{p.Value}";
                                }
                            }

                            Diag.Log.Grava(
                                new Diag.LogItem()
                                {
                                    Nivel = Diag.Nivel.Informacao,
                                    Mensagem = s
                                });
                        }
                    }
                }
            }

            if (this.HttpContext.User.Claims == null)
            {

                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Informacao,
                        Mensagem = "this.HttpContext.User.Claims == null"
                    });
            }
            else
            {
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Informacao,
                        Mensagem = "this.HttpContext.User.Claims:"
                    });

                foreach (var x in this.HttpContext.User.Claims)
                {
                    string s = $"  Type: {x.Type} Value:{x.Value}";
                    if (x.Properties != null && x.Properties.Count > 0)
                    {
                        foreach (var p in x.Properties)
                        {
                            s += $" Key:{p.Key} Value:{p.Value}";
                        }
                    }

                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = s
                        });
                }
            }
            // Testes - FIM

            ViewModels.LoginViewModel dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);

            if (dadosUsuario == null)
            {

                // Testes - INICIO
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = "dadosUsuario == null"
                    });
                // Testes - FIM

                return View();
            }

            // Testes - INICIO
            Diag.Log.Grava(
                new Diag.LogItem()
                {
                    Nivel = Diag.Nivel.Informacao,
                    Mensagem = $"Login:{dadosUsuario.Identificacao}"
                });
            // Testes - FIM

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            //Recebe Dados Cookies - Teste Yuri

            var identificacao = this.Request.Cookies["Usr"];
            if (identificacao != null)
            {
                var objUsuario = JsonConvert.DeserializeObject<Models.Usuario>(identificacao);
                dadosUsuario.Usuario = objUsuario;
            }


            ViewData["UsuarioNome"] = dadosUsuario.Nome;
            ViewData["UsuarioIdentificacao"] = dadosUsuario.Identificacao;

            return View(dadosUsuario);
        }

        /// <summary>
        /// Método acionado eventualmente, se o usuário adicionar "/Login" no fim o URL.
        /// <para>
        /// Redireciona para a view "Index"
        /// </para>
        /// </summary>
        /// <returns></returns>
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public IActionResult Login()
        {
            return View("Index");
        }


        /// <summary>
        /// Método principal de autenticação
        /// </summary>
        /// <param name="dados"></param>
        /// <returns></returns>
        [HttpPost, Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public async Task<JsonResult> Login(ViewModels.LoginViewModel dados)
        {

            /* http://stackoverflow.com/questions/35202804/submitting-a-razor-form-using-jquery-ajax-in-mvc6-using-the-built-in-functionali
             * data-ajax="true" data-ajax-method="POST" 
             * 
             * +------------------------+-----------------------------+
             * |      AjaxOptions       |       HTML attribute        |
             * +------------------------+-----------------------------+
             * | Confirm                | data-ajax-confirm           |
             * | HttpMethod             | data-ajax-method            |
             * | InsertionMode          | data-ajax-mode              |
             * | LoadingElementDuration | data-ajax-loading-duration  |
             * | LoadingElementId       | data-ajax-loading           |
             * | OnBegin                | data-ajax-begin             |
             * | OnComplete             | data-ajax-complete          |
             * | OnFailure              | data-ajax-failure           |
             * | OnSuccess              | data-ajax-success           |
             * | UpdateTargetId         | data-ajax-update            |
             * | Url                    | data-ajax-url               |
             * +------------------------+-----------------------------+
             */

            /* https://docs.asp.net/en/latest/security/authentication/cookie.html
             * http://dotnetthoughts.net/cookie-authentication-in-asp-net-5/
             * https://github.com/leastprivilege/AspNetCoreSecuritySamples/blob/master/Cookies/src/AspNetCoreAuthentication/Startup.cs
             */

            Diag.Log.Grava(
                new Diag.LogItem()
                {
                    Nivel = Diag.Nivel.Informacao,
                    Mensagem = $"Usuário: {dados.Identificacao}"
                });

            string msg = string.Empty;

            if (string.IsNullOrEmpty(dados.Identificacao) || string.IsNullOrEmpty(dados.Senha))
            {
                msg = "Você deve informar sua identificação e sua senha";
            }
            else
            {

                Models.Usuario dadosUsuario = AutenticaUsuario(dados.Identificacao, dados.Senha, this.configuracao);
              
                if (dadosUsuario != null)
                {
                    //Grava cookie do usuário: Teste Yuri
                    var jsonDadosUsuario = JsonConvert.SerializeObject(dadosUsuario);
                    Response.Cookies.Append(
                        "Usr",
                        jsonDadosUsuario,
                        new CookieOptions()
                        {
                            Path = "/",
                            HttpOnly = false,
                            Secure = false
                        }
                    );
                    //


                    List<System.Security.Claims.Claim> claims = new List<System.Security.Claims.Claim>(6);
                    claims.Add(new System.Security.Claims.Claim("Login", dadosUsuario.Login, System.Security.Claims.ClaimValueTypes.String));
                    claims.Add(new System.Security.Claims.Claim("Nome", dadosUsuario.Nome, System.Security.Claims.ClaimValueTypes.String));
                    claims.Add(new System.Security.Claims.Claim("Usuario_ID", dadosUsuario.Usuario_ID.ToString(), System.Security.Claims.ClaimValueTypes.String));
                    claims.Add(new System.Security.Claims.Claim("Inspetor", (dadosUsuario.Inspetor ? "1" : "0"), System.Security.Claims.ClaimValueTypes.String));

                    var userIdentity = new System.Security.Claims.ClaimsIdentity("SuperSecureLogin");
                    userIdentity.AddClaims(claims);

                    var userPrincipal = new System.Security.Claims.ClaimsPrincipal(userIdentity);

                    await HttpContext.Authentication.SignInAsync(
                        VDT2.BLL.Globais.NomeCookieAutenticacao,
                        userPrincipal,
                        new Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties
                        {
                            ExpiresUtc = DateTime.UtcNow.AddMinutes(VDT2.BLL.Globais.ExpiracaoCookieAutenticacao),
                            IsPersistent = false,
                            AllowRefresh = false
                        });

                    Diag.Log.Grava(
                        new Diag.LogItem()
                        {
                            Nivel = Diag.Nivel.Informacao,
                            Mensagem = "Executou SignInAsync"
                        });
                    msg = "OK:";
                }
                else
                {
                    msg = "Identificação ou senha inválidos";
                }
            }

            return Json(msg);
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete(".AspNetCore.VDT_AuthCookie");
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Autenticação do usuário
        /// </summary>
        /// <param name="login"></param>
        /// <param name="senha"></param>
        /// <param name="configuracao"></param>
        /// <returns></returns>
        private static Models.Usuario AutenticaUsuario(string login, string senha, VDT2.Models.Configuracao configuracao)
        {

            VDT2.Models.Usuario dadosUsuario = VDT2.BLL.Login.EfetuaLogin(login, senha, configuracao);

            if (dadosUsuario == null)
            {

                // TODO: Gravar no LOG
                return null; // Não conseguiu autenticar o usuário - o usuário não foi encontrado na tabela tbUsrId ou ocorreu um erro na consulta à base de dados
            }

            if (!dadosUsuario.Autenticou)
            {
                Diag.Log.Grava(
                  new Diag.LogItem()
                  {
                      Nivel = Diag.Nivel.Informacao,
                      Mensagem = $"Não conseguiu autenticar usuário, senha inválida | Login: {login}",
                  });

                return null;  // Não conseguiu autenticar o usuário - senha inválida
            }

            return dadosUsuario;
        }

    }
}
