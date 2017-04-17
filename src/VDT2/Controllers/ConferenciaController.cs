// <copyright file="ConferenciaController.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Amauri Rodrigues & Yuri Vasconcelos</author>
// <email>amauri.rodrigues@grupoasserth.com.br | yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>Controllers de Conferência</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VDT2.ViewModels;
using VDT2.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

namespace VDT2.Controllers
{
    public class ConferenciaController : Controller
    {

        private VDT2.Models.Configuracao configuracao { get; set; }
        private string tempErro = "Erro ao processar informação, tente novamente mais tarde";

        /// <summary>
        /// Construtor da classe
        /// <para>Recebe a configuração do aplicativo, usando Dependency Injection</para>
        /// </summary>
        /// <param name="settings">Configuração geral do aplicativo, carregada de appsettings.json</param>
        public ConferenciaController(IOptions<VDT2.Models.Configuracao> settings)
        {
            this.configuracao = settings.Value;
        }

        /// <summary>
        /// Inicio da Conferência. Carrega os dados dos dropdownlists [select options]
        /// </summary>
        /// <returns></returns>
        public IActionResult NovaConferencia()
        {
            if (TempData.Count() > 0)
            {
                ViewData["MensagemErro"] = TempData["Erro"];
            }
            #region gravalogInformacao
            Diag.Log.Grava(
            new Diag.LogItem()
            {
                Nivel = Diag.Nivel.Informacao,
                Mensagem = $"Action acionada: NovaConferencia | Sem Parametros",
            });
            #endregion

            const string _mensagemLogin = "Usuário não identificado, faça login novamente";
            ConferenciaIndexViewModel conferenciaVM = new ConferenciaIndexViewModel();

            var dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);
            if (dadosUsuario != null)
            {
                var identificacao = this.Request.Cookies["Usr"];
                if (identificacao != null)
                {
                    var objUsuario = JsonConvert.DeserializeObject<Models.Usuario>(identificacao);
                    dadosUsuario.Usuario = objUsuario;

                    //Testes EM_ERRO
                    //dadosUsuario.Usuario.Locais = null;
                    //dadosUsuario = null;

                    conferenciaVM.ListaCliente = BLL.Inspecao.ListarClientes(dadosUsuario.UsuarioId, configuracao);

                    conferenciaVM.ListaLocalInspecao = BLL.Inspecao.ListarLocaisInspecao(dadosUsuario.UsuarioId, configuracao, dadosUsuario.Usuario.Locais);

                    conferenciaVM.ListaLocalCheckPoint = BLL.Inspecao.ListarLocalCheckPoint(dadosUsuario.UsuarioId, configuracao);

                    #region EM_ERRO
                    if (conferenciaVM.ListaCliente.FirstOrDefault().Erro == true)
                    {
                        ViewData["MensagemErro"] += conferenciaVM.ListaCliente.FirstOrDefault().MensagemErro;
                    }

                    if (conferenciaVM.ListaLocalInspecao.FirstOrDefault().Erro == true)
                    {
                        ViewData["MensagemErro"] += conferenciaVM.ListaLocalInspecao.FirstOrDefault().MensagemErro;
                    }

                    if (conferenciaVM.ListaLocalCheckPoint.FirstOrDefault().Erro == true)
                    {
                        ViewData["MensagemErro"] += conferenciaVM.ListaLocalCheckPoint.FirstOrDefault().MensagemErro;
                    }
                    #endregion
                }
                else
                {
                    ViewData["MensagemErro"] = _mensagemLogin;
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewData["MensagemErro"] = _mensagemLogin;
                return RedirectToAction("Index", "Home");
            }



            return View("NovaConferencia", conferenciaVM);
        }

        /// <summary>
        /// Listagem de veículos e suas respectivas avarias 2a tela.
        /// </summary>
        /// <param name="conferenciaVM">Dados da conferência</param>
        /// <returns></returns>
        public IActionResult ConferenciaListarVeiculos(ConferenciaIndexViewModel conferenciaVM)
        {
            #region gravalogInformacao
            try
            {
                Diag.Log.Grava(
                new Diag.LogItem()
                {
                    Nivel = Diag.Nivel.Informacao,
                    Mensagem = $"Action acionada: ConferenciaListarVeiculos | Parametros: {conferenciaVM.TextoLog()}"
                });
            }
            catch { }
            #endregion

            try
            {
                const string _mensagemLogin = "Erro ao identificar usuário, tente novamente mais tarde ou faça um novo login";
                const string _mensagemErro = "Não foi possível listar, por favor tente novamente mais tarde ou entre em contato com o suporte técnico";
                ListarConferenciaAvariaViewModel listarConferenciaAvariaVM = new ListarConferenciaAvariaViewModel();
                listarConferenciaAvariaVM.InspAvaria_Conf = new Models.InspAvaria_Conf();

                #region recebeDadosUsuario
                var dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);
                if (dadosUsuario == null)
                {
                    ViewData["MensagemErro"] = _mensagemLogin;
                    return RedirectToAction("Index", "Home");
                }

                var identificacao = this.Request.Cookies["Usr"];

                if (identificacao == null)
                {
                    ViewData["MensagemErro"] = _mensagemLogin;
                    return RedirectToAction("Index", "Home");
                }


                var objUsuario = JsonConvert.DeserializeObject<Models.Usuario>(identificacao);
                listarConferenciaAvariaVM.Usuario = objUsuario;
                dadosUsuario.Usuario = objUsuario;

                ViewData["UsuarioNome"] = dadosUsuario.Nome;
                ViewData["UsuarioIdentificacao"] = dadosUsuario.Identificacao;
                #endregion

                if (conferenciaVM != null)
                {
                    if (!(conferenciaVM.Cliente_ID == 0 || conferenciaVM.LocalInspecao_ID == 0 || conferenciaVM.LocalCheckPoint_ID == 0 || conferenciaVM.Data == null))
                    {
                        listarConferenciaAvariaVM.Pendencias = BLL.InspecaoVeiculo.IntegrarVIN(conferenciaVM.Cliente_ID, conferenciaVM.LocalInspecao_ID, conferenciaVM.LocalCheckPoint_ID, conferenciaVM.Data, configuracao);

                        //Lista todas as avarias dos referentes ao cliente/localInspecao e LocalCheckPoint informados
                        listarConferenciaAvariaVM.ListaInspAvaria_Conf = BLL.InspAvariaConf.ListarAvarias_Conf(conferenciaVM.Cliente_ID, conferenciaVM.LocalInspecao_ID, conferenciaVM.LocalCheckPoint_ID, conferenciaVM.Data, configuracao);


                        //Realiza a concatenação de inspeções para mandar p/ View
                        StringBuilder sbInspecao = new StringBuilder();
                        int auxInspecao = 0;
                        int inspecaoAtual = 0;
                        if (listarConferenciaAvariaVM.ListaInspAvaria_Conf.Count() > 0)
                        {
                            foreach (var item in listarConferenciaAvariaVM.ListaInspAvaria_Conf)
                            { 
                                //Lógica utilizada para não receber valores duplicados
                                auxInspecao = item.Inspecao_ID;
                                if (auxInspecao != inspecaoAtual)
                                {
                                    sbInspecao.Append($"{item.Inspecao_ID};");
                                    inspecaoAtual = item.Inspecao_ID;
                                }
                            }
                        }

                        listarConferenciaAvariaVM.ConcatInspecoes = sbInspecao.ToString();

                        if (listarConferenciaAvariaVM.ListaInspAvaria_Conf == null)
                        {
                            ViewData["MensagemErro"] = _mensagemErro;
                        }

                        //preenche dados do cabecalho da proxima view
                        listarConferenciaAvariaVM.InspAvaria_Conf.Data = conferenciaVM.Data;
                        listarConferenciaAvariaVM.InspAvaria_Conf.LocalNome = BLL.Inspecao.ListarLocaisInspecao(dadosUsuario.UsuarioId, configuracao, listarConferenciaAvariaVM.Usuario.Locais).Where(p => p.LocalInspecao_ID == conferenciaVM.LocalInspecao_ID).FirstOrDefault().Nome;
                        listarConferenciaAvariaVM.InspAvaria_Conf.CheckPointNome = BLL.Inspecao.ListarLocalCheckPoint(dadosUsuario.UsuarioId, configuracao).Where(p => p.LocalCheckPoint_ID == conferenciaVM.LocalCheckPoint_ID).FirstOrDefault().Nome_Pt;

                    }
                    else
                    {
                        ViewData["MensagemErro"] = _mensagemErro;
                    }
                }
                else
                {
                    ViewData["MensagemErro"] = _mensagemErro;
                }

                //Caso tudo ok, retorna a view;
                return View("ListarConferenciaAvarias", listarConferenciaAvariaVM);
            }

            catch (Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem
                {
                    Nivel = Diag.Nivel.Informacao,
                    Excecao = ex,
                    Mensagem = $"Erro ao solicitar operacao, ConferenciaListarVeiculos : { ex }"
                });

                TempData["Erro"] = tempErro;
                return RedirectToAction("NovaConferencia");
            }

        }


        /// <summary>
        /// Realiza edição das avarias
        /// </summary>
        /// <param name="inspAvaria_ID">ID da avaria</param>
        /// <returns></returns>
        public IActionResult EditarAvarias(int inspAvaria_ID, int inspVeiculo_ID)
        {
            #region gravalogInformacao
            try
            {
                Diag.Log.Grava(
                new Diag.LogItem()
                {
                    Nivel = Diag.Nivel.Informacao,
                    Mensagem = $"Action acoinada: EditarAvarias | Parametros: InspAvaria_ID {inspAvaria_ID}"
                });
            }
            catch { }
            #endregion

            try
            {
                ConferenciaEditarAvariasViewModel conferenciaEditarAvariasVM = new ConferenciaEditarAvariasViewModel();

                #region dadosUsuario
                var dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);
                if (dadosUsuario == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var identificacao = this.Request.Cookies["Usr"];
                    if (identificacao != null)
                    {
                        var objUsuario = JsonConvert.DeserializeObject<Models.Usuario>(identificacao);
                        conferenciaEditarAvariasVM.Usuario = objUsuario;
                    }
                }
                #endregion

                if (inspVeiculo_ID != 0)
                {
                    conferenciaEditarAvariasVM.InspVeiculo = BLL.InspecaoVeiculo.ListarPorId(inspVeiculo_ID, configuracao);

                    conferenciaEditarAvariasVM.Inspecao = BLL.Inspecao.ListarPorId(conferenciaEditarAvariasVM.InspVeiculo.Inspecao_ID, configuracao);

                    //Marcas
                    conferenciaEditarAvariasVM.ListaMarcas = BLL.InspecaoVeiculo.ListaMarca(conferenciaEditarAvariasVM.Inspecao.Cliente_ID, configuracao);

                    //Modelos
                    conferenciaEditarAvariasVM.ListaModelos = BLL.InspecaoVeiculo.ListaModelo(conferenciaEditarAvariasVM.Inspecao.Cliente_ID, configuracao);

                    #region EM_ERRO
                    //Marca
                    if (conferenciaEditarAvariasVM.ListaMarcas.FirstOrDefault().Text == "ERRO")
                    {
                        ViewData["MensagemErro"] = "Erro ao listar Marcas, tente novamente mais tarde";
                    }

                    //Modelos
                    if (conferenciaEditarAvariasVM.ListaModelos.FirstOrDefault().Text == "ERRO")
                    {
                        ViewData["MensagemErro"] = "Erro ao listar Modelos, tente novamente mais tarde";
                    }

                    if (inspAvaria_ID != 0)
                    {
                        conferenciaEditarAvariasVM.InspAvaria = BLL.Avarias.ListarPorId(inspAvaria_ID, configuracao);

                        //preenche dados próxima view

                        //Areas
                        conferenciaEditarAvariasVM.ListaAreas = BLL.Avarias.ListarAreas(conferenciaEditarAvariasVM.Inspecao.Cliente_ID, configuracao);

                        //Condicoes
                        conferenciaEditarAvariasVM.ListaCondicoes = BLL.Avarias.ListarCondicoes(conferenciaEditarAvariasVM.Inspecao.Cliente_ID, configuracao);

                        //Danos
                        conferenciaEditarAvariasVM.ListaDanos = BLL.Avarias.ListarDanos(conferenciaEditarAvariasVM.Inspecao.Cliente_ID, configuracao);

                        //Gravidades
                        conferenciaEditarAvariasVM.ListaGravidades = BLL.Avarias.ListarGravidades(conferenciaEditarAvariasVM.Inspecao.Cliente_ID, configuracao);

                        //Quadrantes
                        conferenciaEditarAvariasVM.ListaQuadrantes = BLL.Avarias.ListarQuadrantes(conferenciaEditarAvariasVM.Inspecao.Cliente_ID, configuracao);


                        //Severidades
                        conferenciaEditarAvariasVM.ListaSeveridades = BLL.Avarias.ListarSeveridades(conferenciaEditarAvariasVM.Inspecao.Cliente_ID, configuracao);

                        //Areas
                        if (conferenciaEditarAvariasVM.ListaAreas.FirstOrDefault().Nome_Pt == "ERRO")
                        {
                            ViewData["MensagemErro"] = "Erro ao listar Marcas, tente novamente mais tarde";
                        }

                        //Condicoes
                        if (conferenciaEditarAvariasVM.ListaCondicoes.FirstOrDefault().Nome_Pt == "ERRO")
                        {
                            ViewData["MensagemErro"] = "Erro ao listar Condicoes, tente novamente mais tarde";
                        }

                        //Danos
                        if (conferenciaEditarAvariasVM.ListaDanos.FirstOrDefault().Nome_Pt == "ERRO")
                        {
                            ViewData["MensagemErro"] = "Erro ao listar Danos, tente novamente mais tarde";
                        }


                        //Gravidades
                        if (conferenciaEditarAvariasVM.ListaGravidades.FirstOrDefault().Nome_Pt == "ERRO")
                        {
                            ViewData["MensagemErro"] = "Erro ao listar Gravidades, tente novamente mais tarde";
                        }

                        //Quadrantes
                        if (conferenciaEditarAvariasVM.ListaQuadrantes.FirstOrDefault().Nome_Pt == "ERRO")
                        {
                            ViewData["MensagemErro"] = "Erro ao listar Quadrantes, tente novamente mais tarde";
                        }


                        //Severidades
                        if (conferenciaEditarAvariasVM.ListaSeveridades.FirstOrDefault().Nome_Pt == "ERRO")
                        {
                            ViewData["MensagemErro"] = "Erro ao listar Severidades, tente novamente mais tarde";
                        }
                        #endregion
                    }
                }

                else
                {
                    ViewData["MensagemErro"] = "Erro editar avaria por favor tente novamente mais tarde ou entre em contato com o suporte técnico";
                    return RedirectToAction("Index", "Home");
                }


                //Testes EM_ERRO
                //conferenciaEditarAvariasVM = null; //teste1;
                //conferenciaEditarAvariasVM.ListaAreas = null;
                return View("EditarAvariasConferencia", conferenciaEditarAvariasVM);
            }
            catch (Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem
                {
                    Nivel = Diag.Nivel.Informacao,
                    Excecao = ex,
                    Mensagem = $"Erro ao solicitar operacao, EditarAvarias : { ex }"
                });

                return RedirectToAction("NovaConferencia");
            }
        }


        /// <summary>
        /// Salva avaria no banco de dados
        /// </summary>
        /// <param name="conferenciaEditarAvariasVM"></param>
        /// <returns></returns>
        public IActionResult SalvarAvaria(ConferenciaEditarAvariasViewModel conferenciaEditarAvariasVM)
        {
            const string _mensagemLogin = "Erro ao verificar dados do usuário tente novamente mais tarde ou entre em contato com o suporte técnico";

            #region recebeDadosUsuario
            var dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);
            if (dadosUsuario == null)
            {
                ViewData["MensagemErro"] = _mensagemLogin;
                return RedirectToAction("Index", "Home");
            }

            var identificacao = this.Request.Cookies["Usr"];

            if (identificacao == null)
            {
                ViewData["MensagemErro"] = _mensagemLogin;
                return RedirectToAction("Index", "Home");
            }

            var objUsuario = JsonConvert.DeserializeObject<Models.Usuario>(identificacao);
            dadosUsuario.Usuario = objUsuario;

            if (dadosUsuario.Usuario.AlteraInspecao != true)
            {
                ViewData["MensagemErro"] = _mensagemLogin;
                return RedirectToAction("Index", "Home");
            }
            #endregion

            #region gravalogInformacao
            try
            {
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Informacao,
                        Mensagem = $"Action acionada: SalvarAvaria - Parametros: {conferenciaEditarAvariasVM.TextoLog()}"
                    });
            }
            catch { }
            #endregion

            try
            {
                //realiza updates
                if (conferenciaEditarAvariasVM.InspVeiculo != null)
                {
                    conferenciaEditarAvariasVM.InspVeiculo = BLL.InspecaoVeiculo.Update(conferenciaEditarAvariasVM.InspVeiculo, configuracao);
                }

                if (conferenciaEditarAvariasVM.InspAvaria != null)
                {
                    conferenciaEditarAvariasVM.InspAvaria = BLL.Avarias.Update(conferenciaEditarAvariasVM.InspAvaria, configuracao);
                    if (conferenciaEditarAvariasVM.InspAvaria.Erro == true || conferenciaEditarAvariasVM.InspVeiculo.Erro == true)
                    {
                        ViewData["MensagemErro"] = "Erro ao atualizar dados da avaria, tente novamente mais tarde ou entre em contato com o suporte";
                        return RedirectToAction("Home", "Index");
                    }

                }
                else
                {
                    ViewData["MensagemSucesso"] = "Dados atualizados com sucesso.";
                }

                //lista ocorrencias
                //faço essa requisição pois no update do inspVeiculo não retorna o Inspecao_ID
                conferenciaEditarAvariasVM.InspVeiculo = BLL.InspecaoVeiculo.ListarPorId(conferenciaEditarAvariasVM.InspVeiculo.InspVeiculo_ID, configuracao);
                conferenciaEditarAvariasVM.Inspecao = BLL.Inspecao.ListarPorId(conferenciaEditarAvariasVM.InspVeiculo.Inspecao_ID, configuracao);

                List<InspAvaria_Conf> listaInspAvaria_Conf = new List<InspAvaria_Conf>();

                ListarConferenciaAvariaViewModel listarConferenciaAvariaVM = new ListarConferenciaAvariaViewModel();
                listarConferenciaAvariaVM.InspAvaria_Conf = new Models.InspAvaria_Conf();

                listarConferenciaAvariaVM.ListaInspAvaria_Conf = BLL.InspAvariaConf.ListarAvarias_Conf(conferenciaEditarAvariasVM.Inspecao.Cliente_ID, conferenciaEditarAvariasVM.Inspecao.LocalInspecao_ID, conferenciaEditarAvariasVM.Inspecao.LocalCheckPoint_ID, conferenciaEditarAvariasVM.Inspecao.Data, configuracao);
                listarConferenciaAvariaVM.Pendencias = BLL.InspecaoVeiculo.IntegrarVIN(conferenciaEditarAvariasVM.Inspecao.Cliente_ID, conferenciaEditarAvariasVM.Inspecao.LocalInspecao_ID, conferenciaEditarAvariasVM.Inspecao.LocalCheckPoint_ID, conferenciaEditarAvariasVM.Inspecao.Data, configuracao);

                if (listarConferenciaAvariaVM.ListaInspAvaria_Conf.Count() > 0)
                {
                    listarConferenciaAvariaVM.InspAvaria_Conf.Data = conferenciaEditarAvariasVM.Inspecao.Data;
                    listarConferenciaAvariaVM.InspAvaria_Conf.LocalNome = listarConferenciaAvariaVM.ListaInspAvaria_Conf.FirstOrDefault().LocalNome;
                    listarConferenciaAvariaVM.InspAvaria_Conf.CheckPointNome = listarConferenciaAvariaVM.ListaInspAvaria_Conf.FirstOrDefault().CheckPointNome;
                }
                else
                {
                    ViewData["MensagemErro"] = "Erro ao listar dados do veículo, tente novamente mais tarde ou entre em contato com o suporte técnico";
                }

                return View("ListarConferenciaAvarias", listarConferenciaAvariaVM);
            }
            catch (Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem
                {
                    Nivel = Diag.Nivel.Erro,
                    Excecao = ex,
                    Mensagem = $"Erro ao processar informação tente novamente mais tarde, SalvarAvaria {ex}"
                });

                TempData["Erro"] = tempErro;
                return RedirectToAction("NovaConferencia");
            }

        }

        /// <summary>
        /// Visualizar fotos da avaria - Botão Fotos
        /// </summary>
        /// <param name="inspAvaria_ID"></param>
        /// <returns></returns>
        public IActionResult VisualizarFotos(int inspAvaria_ID)
        {
            const string _mensagemLogin = "Erro ao processar solicitação, usuário não identificado, tente novamente mais tarde";
            ConferenciaVisualizarAvariasViewModel visualizarAvariasVM = new ConferenciaVisualizarAvariasViewModel();

            #region gravalogInformacao
            try
            {
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Informacao,
                        Mensagem = $"Action acionada: VisualizarFotos | Parametro InspAvaria_ID: {inspAvaria_ID}"
                    });
            }
            catch { }
            #endregion

            #region recebeDadosUsuario
            var dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);
            if (dadosUsuario == null)
            {
                ViewData["MensagemErro"] = _mensagemLogin;
                return RedirectToAction("Index", "Home");
            }

            var identificacao = this.Request.Cookies["Usr"];

            if (identificacao == null)
            {
                ViewData["MensagemErro"] = _mensagemLogin;
                return RedirectToAction("Index", "Home");
            }

            var objUsuario = JsonConvert.DeserializeObject<Models.Usuario>(identificacao);
            dadosUsuario.Usuario = objUsuario;
            #endregion

            try
            {
                visualizarAvariasVM.Usuario = dadosUsuario.Usuario;
                visualizarAvariasVM.InspAvaria = new Models.InspAvaria();
                visualizarAvariasVM.InspAvaria = BLL.Avarias.ListarPorId(inspAvaria_ID, configuracao);
                visualizarAvariasVM.ListaImagemAvarias = BLL.UploadImagens.Listar(inspAvaria_ID, configuracao);

                return View("VisualizarFotos", visualizarAvariasVM);
            }
            catch (Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem
                {
                    Nivel = Diag.Nivel.Erro,
                    Excecao = ex,
                    Mensagem = $"Erro ao processar informação tente novamente mais tarde, VisualizarFotos; {ex}"
                });

                TempData["Erro"] = tempErro;
                return RedirectToAction("NovaConferencia");
            }

        }
        /// <summary>
        /// Grava fotos da avaria
        /// </summary>
        /// <param name="inspAvaria_ID">ID da avaria</param>
        /// <param name="files">Fotos referentes a avaria</param>
        /// <returns></returns>
        public IActionResult SalvarFotos(int inspAvaria_ID, ICollection<IFormFile> files)
        {

            #region gravalogInformacao
            Diag.Log.Grava(
                new Diag.LogItem()
                {
                    Nivel = Diag.Nivel.Informacao,
                    Mensagem = $"Action acionada: SalvarFotos | Parametro InspAvaria_ID: {inspAvaria_ID}"
                });
            #endregion

            try
            {
                bool uploadImagem = false;
                ConferenciaEditarAvariasViewModel conferenciaEditarAvariasVM = new ConferenciaEditarAvariasViewModel();
                ListarConferenciaAvariaViewModel listarConferenciaAvariaVM = new ListarConferenciaAvariaViewModel();
                listarConferenciaAvariaVM.InspAvaria_Conf = new Models.InspAvaria_Conf(); ;

                uploadImagem = BLL.UploadImagens.UploadImagensAvaria(inspAvaria_ID, files, configuracao);
                if (uploadImagem == false)
                {
                    ViewData["MensagemErro"] = "Erro ao realizar upload de imagens da avaria";
                }
                else
                {
                    ViewData["MensagemSucesso"] = "Fotos atualizadas com sucesso";
                }

                //Carrega dados proxima View
                conferenciaEditarAvariasVM.InspAvaria = BLL.Avarias.ListarPorId(inspAvaria_ID, configuracao);
                conferenciaEditarAvariasVM.InspVeiculo = BLL.InspecaoVeiculo.ListarPorId(conferenciaEditarAvariasVM.InspAvaria.InspVeiculo_ID, configuracao);
                conferenciaEditarAvariasVM.Inspecao = BLL.Inspecao.ListarPorId(conferenciaEditarAvariasVM.InspAvaria.Inspecao_ID, configuracao);
                if (conferenciaEditarAvariasVM.InspAvaria.Erro == true || conferenciaEditarAvariasVM.InspVeiculo.Erro == true || conferenciaEditarAvariasVM.Inspecao.Erro == true)
                {
                    ViewData["MensagemErro"] = "Erro ao listar dados, tente novamente mais tarde ou entre em contato com o suporte";
                }

                listarConferenciaAvariaVM.ListaInspAvaria_Conf = BLL.InspAvariaConf.ListarAvarias_Conf(conferenciaEditarAvariasVM.Inspecao.Cliente_ID, conferenciaEditarAvariasVM.Inspecao.LocalInspecao_ID, conferenciaEditarAvariasVM.Inspecao.LocalCheckPoint_ID, conferenciaEditarAvariasVM.Inspecao.Data, configuracao);

                listarConferenciaAvariaVM.InspAvaria_Conf.Data = conferenciaEditarAvariasVM.Inspecao.Data;
                listarConferenciaAvariaVM.InspAvaria_Conf.LocalNome = listarConferenciaAvariaVM.ListaInspAvaria_Conf.FirstOrDefault().LocalNome;
                listarConferenciaAvariaVM.InspAvaria_Conf.CheckPointNome = listarConferenciaAvariaVM.ListaInspAvaria_Conf.FirstOrDefault().CheckPointNome;

                return View("ListarConferenciaAvarias", listarConferenciaAvariaVM);
            }
            catch (Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem
                {
                    Nivel = Diag.Nivel.Erro,
                    Excecao = ex,
                    Mensagem = $"Erro ao processar informação tente novamente mais tarde, SalvarFotos; {ex}"
                });

                TempData["Erro"] = tempErro;
                return RedirectToAction("NovaConferencia");
            }
        }

        /// <summary>
        /// Clique do usuário no botão Loading list
        /// </summary>
        /// <returns>View</returns>
        public IActionResult LoadingListInicio()
        {

            #region gravalogInformacao
            try
            {
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Informacao,
                        Mensagem = $"Action acionada: LoadingListInicio | Sem Parametros"
                    });
            }
            catch { }
            #endregion

            string _mensagemLogin = "Erro ao validar dados do usuário, faça login novamente";
            ViewModels.LoginViewModel dadosUsuario = null;

            #region recebeDadosUsuario
            dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);
            if (dadosUsuario == null)
            {
                ViewData["MensagemErro"] = _mensagemLogin;
                return RedirectToAction("Index", "Home");
            }

            var identificacao = this.Request.Cookies["Usr"];

            if (identificacao == null)
            {
                ViewData["MensagemErro"] = _mensagemLogin;
                return RedirectToAction("Index", "Home");
            }

            var objUsuario = JsonConvert.DeserializeObject<Models.Usuario>(identificacao);
            dadosUsuario.Usuario = objUsuario;

            ViewData["UsuarioNome"] = dadosUsuario.Nome;
            ViewData["UsuarioIdentificacao"] = dadosUsuario.Identificacao;
            #endregion

            try
            {
                ConferenciaLoadingListViewModel conferenciaLoadingListVM = new ConferenciaLoadingListViewModel();
                conferenciaLoadingListVM.ListaCliente = BLL.Inspecao.ListarClientes(dadosUsuario.UsuarioId, configuracao);
                conferenciaLoadingListVM.ListaLocalInspecao = BLL.Inspecao.ListarLocaisInspecao(dadosUsuario.UsuarioId, configuracao, dadosUsuario.Usuario.Locais);
                conferenciaLoadingListVM.ListaLocalCheckPoint = BLL.Inspecao.ListarLocalCheckPoint(dadosUsuario.UsuarioId, configuracao);

                #region EM_ERRO
                if (conferenciaLoadingListVM.ListaCliente.FirstOrDefault().Erro == true)
                {
                    ViewData["MensagemErro"] = (conferenciaLoadingListVM.ListaCliente.FirstOrDefault().MensagemErro);
                }
                if (conferenciaLoadingListVM.ListaLocalInspecao.FirstOrDefault().Erro == true)
                {
                    ViewData["MensagemErro"] = conferenciaLoadingListVM.ListaLocalInspecao.FirstOrDefault().MensagemErro;
                }

                if (conferenciaLoadingListVM.ListaLocalCheckPoint.FirstOrDefault().Erro == true)
                {
                    ViewData["MensagemErro"] = conferenciaLoadingListVM.ListaLocalCheckPoint.FirstOrDefault().MensagemErro;
                }

                #endregion


                return View("LoadingListInicio", conferenciaLoadingListVM);
            }
            catch (Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem
                {
                    Nivel = Diag.Nivel.Erro,
                    Excecao = ex,
                    Mensagem = $"Erro ao processar informação tente novamente mais tarde, LoadingListInicio; {ex}"
                });

                TempData["Erro"] = tempErro;
                return RedirectToAction("NovaConferencia");
            }
        }

        /// <summary>
        /// Clique do usuário no botão packinglist
        /// </summary>
        /// <returns></returns>
        public IActionResult PackingListInicio()
        {
            #region gravalogInformacao
            try
            {
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Informacao,
                        Mensagem = $"Action acionada: PackingListInicio | Sem Parametros"
                    });
            }
            catch { }
            #endregion

            try
            {
                //Verifica dados do usuário
                string _mensagemLogin = "Erro ao validar dados do usuário, por favor faça login novamente";
                ViewModels.LoginViewModel dadosUsuario = new ViewModels.LoginViewModel();

                #region recebeDadosUsuario
                dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);
                if (dadosUsuario == null)
                {
                    ViewData["MensagemErro"] = _mensagemLogin;
                    return RedirectToAction("Index", "Home");
                }

                var identificacao = this.Request.Cookies["Usr"];

                if (identificacao == null)
                {
                    ViewData["MensagemErro"] = _mensagemLogin;
                    return RedirectToAction("Index", "Home");
                }

                var objUsuario = JsonConvert.DeserializeObject<Models.Usuario>(identificacao);
                dadosUsuario.Usuario = objUsuario;

                ViewData["UsuarioNome"] = dadosUsuario.Nome;
                ViewData["UsuarioIdentificacao"] = dadosUsuario.Identificacao;
                #endregion

                ConferenciaPackingListViewModel conferenciaPackingListVM = new ConferenciaPackingListViewModel();

                conferenciaPackingListVM.ListaCliente = BLL.Inspecao.ListarClientes(dadosUsuario.UsuarioId, configuracao);

                if (conferenciaPackingListVM.ListaCliente.FirstOrDefault().Erro == true)
                {
                    ViewData["MensagemErro"] = "Erro ao listar Clientes, tente novamente mais tarde ou entre em contato com o suporte técnico.";
                }


                conferenciaPackingListVM.ListaLocalInspecao = BLL.Inspecao.ListarLocaisInspecao(dadosUsuario.UsuarioId, configuracao, dadosUsuario.Usuario.Locais);

                if (conferenciaPackingListVM.ListaLocalInspecao.FirstOrDefault().Erro == true)
                {
                    ViewData["MensagemErro"] = conferenciaPackingListVM.ListaLocalInspecao.FirstOrDefault().MensagemErro;
                }

                conferenciaPackingListVM.ListaLocalCheckPoint = BLL.Inspecao.ListarLocalCheckPoint(dadosUsuario.UsuarioId, configuracao);

                if (conferenciaPackingListVM.ListaLocalCheckPoint.FirstOrDefault().Erro == true)
                {
                    ViewData["MensagemErro"] = conferenciaPackingListVM.ListaLocalCheckPoint.FirstOrDefault().MensagemErro;
                }

                return View("PackingListInicio", conferenciaPackingListVM);
            }

            catch (Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem
                {
                    Nivel = Diag.Nivel.Erro,
                    Excecao = ex,
                    Mensagem = $"Erro ao processar informação tente novamente mais tarde, LoadingListInicio; {ex}"
                });

                TempData["Erro"] = tempErro;
                return RedirectToAction("NovaConferencia");
            }
        }

        /// <summary>
        /// Salva as informações de cabeçalho da LoadingList Enviada pelo usuário
        /// </summary>
        /// <param name="conferenciaLoadingListVM"></param>
        /// <param name="files">Arquivo contendo os dados em txt.</param>
        /// <returns></returns>
        public IActionResult LoadingListSalvar(ConferenciaLoadingListViewModel conferenciaLoadingListVM, ICollection<IFormFile> files)
        {

            #region gravalogInformacao

            Diag.Log.Grava(
                new Diag.LogItem()
                {
                    Nivel = Diag.Nivel.Informacao,
                    Mensagem = $"Action acionada: LoadingListSalvar | Parametros {conferenciaLoadingListVM.TextoLog()}"
                });
            #endregion

            bool salvou = false;
            bool inseriuArquivo = false;
            bool integrou = false;
            string _mensagemErro = "Erro ao gravar arquivo. Tente novamente mais tarde ou entre em contato com service desk";

            //Verifica dados do usuário
            string _mensagemLogin = "Erro ao validar dados do usuário, por favor faça login novamente";
            ViewModels.LoginViewModel dadosUsuario = new ViewModels.LoginViewModel();

            #region recebeDadosUsuario
            dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);
            if (dadosUsuario == null)
            {
                ViewData["MensagemErro"] = _mensagemLogin;
                return RedirectToAction("Index", "Home");
            }

            var identificacao = this.Request.Cookies["Usr"];

            if (identificacao == null)
            {
                ViewData["MensagemErro"] = _mensagemLogin;
                return RedirectToAction("Index", "Home");
            }

            var objUsuario = JsonConvert.DeserializeObject<Models.Usuario>(identificacao);

            dadosUsuario.Usuario = objUsuario;

            ViewData["UsuarioNome"] = dadosUsuario.Nome;
            ViewData["UsuarioIdentificacao"] = dadosUsuario.Identificacao;
            #endregion

            try
            {
                conferenciaLoadingListVM.ListaCliente = BLL.Inspecao.ListarClientes(dadosUsuario.UsuarioId, configuracao);
                if (conferenciaLoadingListVM.ListaCliente.FirstOrDefault().Erro == true)
                {
                    ViewData["MensagemErro"] = conferenciaLoadingListVM.ListaCliente.FirstOrDefault().MensagemErro;
                }

                conferenciaLoadingListVM.ListaLocalInspecao = BLL.Inspecao.ListarLocaisInspecao(dadosUsuario.UsuarioId, configuracao, dadosUsuario.Usuario.Locais);
                if (conferenciaLoadingListVM.ListaLocalInspecao.FirstOrDefault().Erro == true)
                {
                    ViewData["MensagemErro"] = conferenciaLoadingListVM.ListaLocalInspecao.FirstOrDefault().MensagemErro;
                }


                conferenciaLoadingListVM.ListaLocalCheckPoint = BLL.Inspecao.ListarLocalCheckPoint(dadosUsuario.UsuarioId, configuracao);
                if (conferenciaLoadingListVM.ListaLocalCheckPoint.FirstOrDefault().Erro == true)
                {
                    ViewData["MensagemErro"] = conferenciaLoadingListVM.ListaLocalCheckPoint.FirstOrDefault().MensagemErro;
                }

                //Verifica o arquivo enviado. 
                if (files.Count() > 0)
                {
                    if (files.FirstOrDefault().ContentType == "text/plain")
                    {
                        Models.ListaVeiculos listaVeiculos = new ListaVeiculos
                        {
                            Cliente_ID = conferenciaLoadingListVM.Cliente_ID,
                            DataHoraInclusao = DateTime.Now,
                            LocalInspecao_ID = conferenciaLoadingListVM.LocalInspecao_ID,
                            LocalCheckPoint_ID = conferenciaLoadingListVM.LocalCheckPoint_ID,
                            NomeArquivo = files.FirstOrDefault().FileName,
                            Tipo = 'L',
                            Usuario_ID = dadosUsuario.UsuarioId
                        };

                        listaVeiculos = BLL.Conferencia.InserirListaVeiculos(listaVeiculos, configuracao);

                        if (listaVeiculos.ListaVeiculo_ID != 0)
                        {
                            inseriuArquivo = true;

                            salvou = BLL.UploadTxt.SalvarArquivo(listaVeiculos.ListaVeiculo_ID, 'L', files, configuracao);

                            if (salvou)
                            {
                                integrou = BLL.Conferencia.IntegrarArquivoLoadingPackingList(listaVeiculos.ListaVeiculo_ID, 'L', files, configuracao);

                                if (integrou)
                                {
                                    ViewData["MensagemSucesso"] = "Upload realizado com sucessso";
                                    return View("LoadingListInicio", conferenciaLoadingListVM);
                                }
                            }
                        }

                        if (!salvou || !integrou || !inseriuArquivo)
                        {
                            ViewData["MensagemErro"] = _mensagemErro;
                        }
                    }
                    else if (files.FirstOrDefault().ContentType != "text/plain")
                    {
                        ViewData["MensagemErro"] = _mensagemErro;
                    }
                }
                else if (files.Count() >= 0)
                {
                    ViewData["MensagemErro"] = _mensagemErro;
                }

                return View("LoadingListInicio", conferenciaLoadingListVM);
            }
            catch (Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem
                {
                    Nivel = Diag.Nivel.Erro,
                    Excecao = ex,
                    Mensagem = $"Erro ao processar informação tente novamente mais tarde, LoadingListSalvar; {ex}"
                });
                TempData["Erro"] = tempErro;
                return RedirectToAction("NovaConferencia");
            }
        }

        /// <summary>
        /// Salvar PackingList Informado pelo usuároi
        /// </summary>
        /// <param name="conferenciaPackingListVM"></param>
        /// <param name="files">Arquivo contendo dados da PackingList</param>
        /// <returns></returns>
        public IActionResult PackingListSalvar(ConferenciaPackingListViewModel conferenciaPackingListVM, ICollection<IFormFile> files)
        {
            #region gravalogInformacao
            try
            {
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Informacao,
                        Mensagem = $"Action acionada: PackingListSalvar | Parametros {conferenciaPackingListVM.TextoLog()}"
                    });
            }
            catch { }
            #endregion

            try
            {
                bool salvou = false;
                bool inseriuArquivo = false;
                bool integrou = false;

                //Verifica dados do usuário
                string _mensagemLogin = "Erro ao validar dados do usuário, por favor faça login novamente";
                ViewModels.LoginViewModel dadosUsuario = new ViewModels.LoginViewModel();

                #region recebeDadosUsuario
                dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);
                if (dadosUsuario == null)
                {
                    ViewData["MensagemErro"] = _mensagemLogin;
                    return RedirectToAction("Index", "Home");
                }

                var identificacao = this.Request.Cookies["Usr"];

                if (identificacao == null)
                {
                    ViewData["MensagemErro"] = _mensagemLogin;
                    return RedirectToAction("Index", "Home");
                }

                var objUsuario = JsonConvert.DeserializeObject<Models.Usuario>(identificacao);
                dadosUsuario.Usuario = objUsuario;

                ViewData["UsuarioNome"] = dadosUsuario.Nome;
                ViewData["UsuarioIdentificacao"] = dadosUsuario.Identificacao;
                #endregion

                conferenciaPackingListVM.ListaCliente = BLL.Inspecao.ListarClientes(dadosUsuario.UsuarioId, configuracao);
                conferenciaPackingListVM.ListaLocalInspecao = BLL.Inspecao.ListarLocaisInspecao(dadosUsuario.UsuarioId, configuracao, dadosUsuario.Usuario.Locais);
                conferenciaPackingListVM.ListaLocalCheckPoint = BLL.Inspecao.ListarLocalCheckPoint(dadosUsuario.UsuarioId, configuracao);

                if (files.Count() > 0)
                {
                    if (files.FirstOrDefault().ContentType == "text/plain")
                    {
                        Models.ListaVeiculos listaVeiculos = new ListaVeiculos
                        {
                            Cliente_ID = conferenciaPackingListVM.Cliente_ID,
                            Usuario_ID = dadosUsuario.UsuarioId,
                            DataHoraInclusao = DateTime.Now,
                            LocalInspecao_ID = conferenciaPackingListVM.LocalInspecao_ID,
                            LocalCheckPoint_ID = conferenciaPackingListVM.LocalCheckPoint_ID,
                            NomeArquivo = files.FirstOrDefault().FileName,
                            Tipo = 'P'
                        };

                        listaVeiculos = BLL.Conferencia.InserirListaVeiculos(listaVeiculos, configuracao);

                        if (listaVeiculos.ListaVeiculo_ID != 0)
                        {
                            inseriuArquivo = true;

                            salvou = BLL.UploadTxt.SalvarArquivo(listaVeiculos.ListaVeiculo_ID, 'P', files, configuracao);

                            if (salvou)
                            {
                                integrou = BLL.Conferencia.IntegrarArquivoLoadingPackingList(listaVeiculos.ListaVeiculo_ID, 'P', files, configuracao);

                                if (integrou)
                                {
                                    ViewData["MensagemSucesso"] = "Upload realizado com sucessso";
                                }
                            }
                        }

                        if (!salvou || !integrou || !inseriuArquivo)
                        {
                            ViewData["MensagemErro"] = "Erro ao realizar upload de arquivo.Tente novamente mais tarde ou entre em contato com service desk";
                        }
                    }
                    else if (files.FirstOrDefault().ContentType != "text/plain")
                    {
                        ViewData["MensagemErro"] = "Arquivo inválido, por favor, faça upload de um arquivo .txt";
                    }
                }
                else if (files.Count() == 0)
                {
                    ViewData["MensagemErro"] = "Nenhum arquivo selecionado, por favor tente novamente mais tarde";
                }


                return View("PackingListInicio", conferenciaPackingListVM);
            }
            catch (Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem
                {
                    Nivel = Diag.Nivel.Erro,
                    Excecao = ex,
                    Mensagem = $"Erro ao processar informação tente novamente mais tarde, PackingListSalvar; {ex}"
                });
                TempData["Erro"] = tempErro;
                return RedirectToAction("NovaConferencia");
            }

        }

        /// <summary>
        /// Action method utilizado para deletar avarias
        /// </summary>
        /// <param name="avaria_ID">id da avaria</param>
        /// <returns>Retorna a view anterior</returns>
        public IActionResult DeletarAvaria(int avaria_ID)
        {

            //Inicializações
            ConferenciaEditarAvariasViewModel conferenciaEditarAvariasVM = new ConferenciaEditarAvariasViewModel();
            ListarConferenciaAvariaViewModel listarConferenciaAvariaVM = new ListarConferenciaAvariaViewModel();
            listarConferenciaAvariaVM.InspAvaria_Conf = new Models.InspAvaria_Conf();


            //Preciso guardar os dados da avaria antes de deletar. Pois utilizarei esses dados para pegar informações do veículo e inspeção.
            conferenciaEditarAvariasVM.InspAvaria = BLL.Avarias.ListarPorId(avaria_ID, configuracao);

            ///*Teste Erro */
            //conferenciaEditarAvariasVM.InspAvaria.Erro = true;
            //conferenciaEditarAvariasVM.InspAvaria.InspAvaria_ID = 0;
            ///*Fim teste Erro*/


            //Verifica se está com erro;
            if (!conferenciaEditarAvariasVM.InspAvaria.Erro)
            {
                conferenciaEditarAvariasVM.InspVeiculo = BLL.InspecaoVeiculo.ListarPorId(conferenciaEditarAvariasVM.InspAvaria.InspVeiculo_ID, configuracao);
                conferenciaEditarAvariasVM.Inspecao = BLL.Inspecao.ListarPorId(conferenciaEditarAvariasVM.InspAvaria.Inspecao_ID, configuracao);
            }

            //Deleta a avaria
            bool deletou = BLL.Avarias.Deletar(avaria_ID, configuracao);
            if (deletou)
            {
                ViewData["MensagemSucesso"] = "Avaria deletada com sucesso";
            }


            //Carrega dados proxima View

            if (conferenciaEditarAvariasVM.InspAvaria.Erro == true || conferenciaEditarAvariasVM.InspVeiculo.Erro == true || conferenciaEditarAvariasVM.Inspecao.Erro == true)
            {
                ViewData["MensagemErro"] = "Erro ao listar dados, tente novamente mais tarde ou entre em contato com o suporte";
            }

            if (!conferenciaEditarAvariasVM.InspAvaria.Erro)
            {
                listarConferenciaAvariaVM.ListaInspAvaria_Conf = BLL.InspAvariaConf.ListarAvarias_Conf(conferenciaEditarAvariasVM.Inspecao.Cliente_ID, conferenciaEditarAvariasVM.Inspecao.LocalInspecao_ID, conferenciaEditarAvariasVM.Inspecao.LocalCheckPoint_ID, conferenciaEditarAvariasVM.Inspecao.Data, configuracao);
            }

            /*Teste Erro*/
            //listarConferenciaAvariaVM.ListaInspAvaria_Conf = null; // teste
            /*Fim Teste Erro*/


            if (listarConferenciaAvariaVM.ListaInspAvaria_Conf != null)
            {
                listarConferenciaAvariaVM.InspAvaria_Conf.Data = conferenciaEditarAvariasVM.Inspecao.Data;
                listarConferenciaAvariaVM.InspAvaria_Conf.LocalNome = listarConferenciaAvariaVM.ListaInspAvaria_Conf.FirstOrDefault().LocalNome;
                listarConferenciaAvariaVM.InspAvaria_Conf.CheckPointNome = listarConferenciaAvariaVM.ListaInspAvaria_Conf.FirstOrDefault().CheckPointNome;
            }
            else
            {
                Diag.Log.Grava(new Diag.LogItem
                {
                    Nivel = Diag.Nivel.Informacao,
                    Mensagem = "Erro ao processar informação tente novamente mais tarde, ConferenciaController | DeletarAvaria"
                });
                TempData["Erro"] = tempErro;
                return RedirectToAction("NovaConferencia");
            }

            return View("ListarConferenciaAvarias", listarConferenciaAvariaVM);
        }


        public IActionResult Voltar(string nomeView)
        {
            #region gravalogInformacao
            try
            {
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Informacao,
                        Mensagem = "Action acionada: Voltar | Sem Parametros"
                    });
            }
            catch { }
            #endregion
            return View("NovaConferencia");
        }

        /// <summary>
        /// Realiza as consultas de avarias
        /// </summary>
        /// <returns>Retorna tela inicial de consultas</returns>
        public IActionResult NovaConsulta()
        {
            Diag.Log.Grava(new Diag.LogItem
            {
                Nivel = Diag.Nivel.Informacao,
                Mensagem = "",

            });

            string _mensagemErroLogin = "Erro ao receber Dados do usuário";

            #region recebeDadosUsuario
            var dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);
            if (dadosUsuario == null)
            {
                ViewData["MensagemErro"] = _mensagemErroLogin;
                return RedirectToAction("Index", "Home");
            }

            var identificacao = this.Request.Cookies["Usr"];

            if (identificacao == null)
            {
                ViewData["MensagemErro"] = _mensagemErroLogin;
                return RedirectToAction("Index", "Home");
            }

            var objUsuario = JsonConvert.DeserializeObject<Models.Usuario>(identificacao);
            dadosUsuario.Usuario = objUsuario;

            #endregion

            var clientes = BLL.Inspecao.ListarClientes(dadosUsuario.UsuarioId, configuracao);

            if (clientes.Count() > 1)
            {
                return View("NovaConsulta", clientes);
            }
            else if (clientes.Count() == 0)
            {
                {
                    return ConsultaCliente(clientes.FirstOrDefault().Cliente_ID);
                }
            }
            else
            {
                Diag.Log.Grava(new Diag.LogItem
                {
                    Mensagem = "Erro ao listar clientes - Action NovaConsulta / Controller Conferência",
                    Nivel = Diag.Nivel.Erro
                });

                TempData["MensagemErro"] = "Erro ao listar Clientes";
                return RedirectToAction("Index", "Home");

            }
        }


        /// <summary>
        /// Verifica os dados do cliente e exibe a próxima view (Consulta) contendo informações pertinentes a este cliente
        /// </summary>
        /// <param name="Cliente_ID"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ConsultaCliente(int Cliente_ID)
        {

            Diag.Log.Grava(new Diag.LogItem
            {
                Nivel = Diag.Nivel.Informacao,
                Mensagem = $"Action acionada: ConsultaCliente | Parametros {Cliente_ID}"
            });


            string _mensagemErroLogin = "Erro ao receber Dados do usuário";

            #region recebeDadosUsuario
            var dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);
            if (dadosUsuario == null)
            {
                ViewData["MensagemErro"] = _mensagemErroLogin;
                return RedirectToAction("Index", "Home");
            }

            var identificacao = this.Request.Cookies["Usr"];

            if (identificacao == null)
            {
                ViewData["MensagemErro"] = _mensagemErroLogin;
                return RedirectToAction("Index", "Home");
            }

            var objUsuario = JsonConvert.DeserializeObject<Models.Usuario>(identificacao);
            dadosUsuario.Usuario = objUsuario;
            #endregion

            ViewModels.ConsultaViewModel consultaVM = new ViewModels.ConsultaViewModel();

            //Não é necessário pois não vou informar o nome do cliente. Apenas o ID / Caso precise informar na tela, pegar abaixo:
            //consultaVM.Cliente = BLL.Inspecao.ListarClientes(dadosUsuario.UsuarioId, configuracao).Where(p => p.Cliente_ID == id).FirstOrDefault();

            consultaVM.Cliente = new Models.Cliente { Cliente_ID = Cliente_ID };
            consultaVM.ListaLocalInspecao = BLL.Inspecao.ListarLocaisInspecao(dadosUsuario.UsuarioId, configuracao, dadosUsuario.Usuario.Locais);
            consultaVM.ListaLocalCheckPoint = BLL.Inspecao.ListarLocalCheckPoint(dadosUsuario.UsuarioId, configuracao);
            consultaVM.ListaMarca = BLL.InspecaoVeiculo.ListaMarca(Cliente_ID, configuracao);
            consultaVM.ListaModelo = BLL.InspecaoVeiculo.ListaModelo(Cliente_ID, configuracao);
            consultaVM.ListaArea = BLL.Avarias.ListarAreas(Cliente_ID, configuracao);
            consultaVM.ListaCondicao = BLL.Avarias.ListarCondicoes(Cliente_ID, configuracao);
            consultaVM.ListaDano = BLL.Avarias.ListarDanos(Cliente_ID, configuracao);
            consultaVM.ListaQuadrante = BLL.Avarias.ListarQuadrantes(Cliente_ID, configuracao);
            consultaVM.ListaGravidade = BLL.Avarias.ListarGravidades(Cliente_ID, configuracao);
            consultaVM.ListaSeveridade = BLL.Avarias.ListarSeveridades(Cliente_ID, configuracao);
            consultaVM.ListaTransportador = BLL.Inspecao.ListarTransportadores(Cliente_ID, configuracao);

            return View("Consulta", consultaVM);
        }

        /// <summary>
        /// Realiza a listagem de dados referente aos parametros informados pelo usuário
        /// </summary>
        /// <param name="consultaVM"></param>
        /// <returns></returns>
        public IActionResult ListarConsulta(ConsultaViewModel consultaVM)
        {
            Diag.Log.Grava(new Diag.LogItem
            {
                Nivel = Diag.Nivel.Informacao,
                Mensagem = $"Action acionada: ListarConsulta"
            });

            ConferenciaConsultaVeiculosViewModel consultaVeiculosVM = new ConferenciaConsultaVeiculosViewModel();

            consultaVeiculosVM.ListaInspAvaria_Cons = BLL.InspAvariaCons.ConsultarVeiculos(consultaVM, configuracao);

            consultaVeiculosVM.QuantidadeInspecionada = consultaVeiculosVM.ListaInspAvaria_Cons.Count();
            consultaVeiculosVM.VeiculosSemAvaria = consultaVeiculosVM.ListaInspAvaria_Cons.Where(p => String.IsNullOrEmpty(p.Area_Pt)).Count();
            consultaVeiculosVM.VeiculosComAvaria = consultaVeiculosVM.QuantidadeInspecionada - consultaVeiculosVM.VeiculosSemAvaria;

            if (consultaVeiculosVM.QuantidadeInspecionada != 0)
            {
                string PercentualComAvaria = ((decimal)(consultaVeiculosVM.VeiculosComAvaria) / (decimal)(consultaVeiculosVM.QuantidadeInspecionada) * 100).ToString("N2");
                consultaVeiculosVM.PercentualAvariado = Convert.ToDecimal(PercentualComAvaria);
                consultaVeiculosVM.PercentualSemAvaria = 100 - consultaVeiculosVM.PercentualAvariado;
            }
            return View("ConsultaVeiculos", consultaVeiculosVM);
        }

        /// <summary>
        /// Publicar Veículos
        /// </summary>
        /// <param name="teste"></param>
        /// <returns></returns>
        public IActionResult Publicar(string concatInspecoes)
        {
            Diag.Log.Grava(new Diag.LogItem
            {
                Nivel = Diag.Nivel.Informacao,
                Mensagem = $"Action acionada: Publicar | Parametros: ConcatInspecoes: {concatInspecoes}"
            });


            string _mensagemErroLogin = "Erro ao receber Dados do usuário";

            #region recebeDadosUsuario
            var dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);
            if (dadosUsuario == null)
            {
                ViewData["MensagemErro"] = _mensagemErroLogin;
                return RedirectToAction("Index", "Home");
            }

            var identificacao = this.Request.Cookies["Usr"];

            if (identificacao == null)
            {
                ViewData["MensagemErro"] = _mensagemErroLogin;
                return RedirectToAction("Index", "Home");
            }

            var objUsuario = JsonConvert.DeserializeObject<Models.Usuario>(identificacao);
            dadosUsuario.Usuario = objUsuario;
            #endregion

            var publicou = BLL.Inspecao.Publicar(dadosUsuario.UsuarioId, concatInspecoes, configuracao);

            ViewData["MensagemSucesso"] = "Publicação realizada com sucesso.";

            return View();
        }
    }
}

