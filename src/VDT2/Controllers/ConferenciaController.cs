// <copyright file="ConferenciaController.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Amauri Rodrigues & Yuri Vasconcelos</author>
// <email>amauri.rodrigues@grupoasserth.com.br | yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>Controllers de Conferencia</summary>

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

namespace VDT2.Controllers
{
    public class ConferenciaController : Controller
    {

        private VDT2.Models.Configuracao configuracao { get; set; }

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
            ConferenciaIndexViewModel conferenciaVM = new ConferenciaIndexViewModel();
            const string _mensagemLogin = "Usuário não identificado, faça login novamente";

            #region dadosUsuario
            var dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);
            if (dadosUsuario != null)
            {
                var identificacao = this.Request.Cookies["Usr"];
                if (identificacao != null)
                {
                    var objUsuario = JsonConvert.DeserializeObject<Models.Usuario>(identificacao);
                    dadosUsuario.Usuario = objUsuario;

                    conferenciaVM.ListaCliente = BLL.Inspecao.ListarClientes(dadosUsuario.UsuarioId, configuracao);

                    conferenciaVM.ListaLocalInspecao = BLL.Inspecao.ListarLocaisInspecao(dadosUsuario.UsuarioId, configuracao, dadosUsuario.Usuario.Locais);

                    conferenciaVM.ListaLocalCheckPoint = BLL.Inspecao.ListarLocalCheckPoint(dadosUsuario.UsuarioId, configuracao);

                    #region EM_ERRO
                    if (conferenciaVM.ListaCliente.FirstOrDefault().Erro == true)
                    {
                        ViewData["MensagemErro"] = conferenciaVM.ListaCliente.FirstOrDefault().MensagemErro;
                    }

                    if (conferenciaVM.ListaLocalInspecao.FirstOrDefault().Erro == true)
                    {
                        ViewData["MensagemErro"] = conferenciaVM.ListaLocalInspecao.FirstOrDefault().MensagemErro;
                    }

                    if (conferenciaVM.ListaLocalCheckPoint.FirstOrDefault().Erro == true)
                    {
                        ViewData["MensagemErro"] = conferenciaVM.ListaLocalCheckPoint.FirstOrDefault().MensagemErro;
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

            #endregion

            return View("NovaConferencia", conferenciaVM);
        }

        /// <summary>
        /// Listagem de veículos e suas respectivas avarias 2a tela.
        /// </summary>
        /// <param name="conferenciaVM">Dados da conferência</param>
        /// <returns></returns>
        public IActionResult ConferenciaListarVeiculos(ConferenciaIndexViewModel conferenciaVM)
        {
            string _mensagemLogin = "Erro ao identificar usuário, tente novamente mais tarde ou faça um novo login";
            ListarConferenciaAvariaViewModel listarConferenciaAvariaVM = new ListarConferenciaAvariaViewModel();
            List<InspAvaria_Conf> listaInspAvaria_Conf = new List<InspAvaria_Conf>();
            
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
            
            bool Integrou = BLL.InspecaoVeiculo.IntegrarVIN(conferenciaVM.Cliente_ID, conferenciaVM.LocalInspecao_ID, configuracao);

            listarConferenciaAvariaVM.InspAvaria_Conf = new Models.InspAvaria_Conf();

            listarConferenciaAvariaVM.InspAvaria_Conf.Data = conferenciaVM.Data;

            listarConferenciaAvariaVM.InspAvaria_Conf.LocalNome = BLL.Inspecao.ListarLocaisInspecao(dadosUsuario.UsuarioId, configuracao, listarConferenciaAvariaVM.Usuario.Locais).Where(p => p.LocalInspecao_ID == conferenciaVM.LocalInspecao_ID).FirstOrDefault().Nome;

            listarConferenciaAvariaVM.InspAvaria_Conf.CheckPointNome = BLL.Inspecao.ListarLocalCheckPoint(dadosUsuario.UsuarioId, configuracao).Where(p => p.LocalCheckPoint_ID == conferenciaVM.LocalCheckPoint_ID).FirstOrDefault().Nome_Pt;

            listarConferenciaAvariaVM.ListaInspAvaria_Conf = BLL.InspAvariaConf.ListarAvarias_Conf(conferenciaVM.Cliente_ID, conferenciaVM.LocalInspecao_ID, conferenciaVM.LocalCheckPoint_ID, conferenciaVM.Data, configuracao);

            //listarConferenciaAvariaVM.listaInspAvaria_Conf = null; //Teste1
            //listarConferenciaAvariaVM.listaInspAvaria_Conf.Clear(); //teste2

            return View("ListarConferenciaAvarias", listarConferenciaAvariaVM);
        }


        /// <summary>
        /// Realiza edição das avarias
        /// </summary>
        /// <param name="inspAvaria_ID">ID da avaria</param>
        /// <returns></returns>
        public IActionResult EditarAvarias(int inspAvaria_ID)
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

            conferenciaEditarAvariasVM.InspAvaria = BLL.Avarias.ListarPorId(inspAvaria_ID, configuracao);

            conferenciaEditarAvariasVM.InspVeiculo = BLL.InspecaoVeiculo.ListarPorId(conferenciaEditarAvariasVM.InspAvaria.InspVeiculo_ID, configuracao);

            conferenciaEditarAvariasVM.Inspecao = BLL.Inspecao.ListarPorId(conferenciaEditarAvariasVM.InspVeiculo.Inspecao_ID, configuracao);

            //preenche dados próxima view
            
            //Marcas
            conferenciaEditarAvariasVM.ListaMarcas = BLL.InspecaoVeiculo.ListaMarca(conferenciaEditarAvariasVM.Inspecao.Cliente_ID, configuracao);

            //Modelos
            conferenciaEditarAvariasVM.ListaModelos = BLL.InspecaoVeiculo.ListaModelo(conferenciaEditarAvariasVM.Inspecao.Cliente_ID, configuracao);

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

            //TODO: Validações de Erro

            return View("EditarAvariasConferencia", conferenciaEditarAvariasVM);
        }


        /// <summary>
        /// Salva avaria no banco de dados
        /// </summary>
        /// <param name="conferenciaEditarAvariasVM"></param>
        /// <returns></returns>
        public IActionResult SalvarAvaria(ConferenciaEditarAvariasViewModel conferenciaEditarAvariasVM)
        {
            //realiza updates
            conferenciaEditarAvariasVM.InspAvaria = BLL.Avarias.Update(conferenciaEditarAvariasVM.InspAvaria, configuracao);
            conferenciaEditarAvariasVM.InspVeiculo = BLL.InspecaoVeiculo.Update(conferenciaEditarAvariasVM.InspVeiculo, configuracao);

            //lista ocorrencias
            conferenciaEditarAvariasVM.InspVeiculo = BLL.InspecaoVeiculo.ListarPorId(conferenciaEditarAvariasVM.InspVeiculo.InspVeiculo_ID, configuracao);
            conferenciaEditarAvariasVM.Inspecao = BLL.Inspecao.ListarPorId(conferenciaEditarAvariasVM.InspVeiculo.Inspecao_ID, configuracao);

            if (conferenciaEditarAvariasVM.InspAvaria.Erro == false && conferenciaEditarAvariasVM.InspAvaria.Erro == false)
            {
                ViewData["MensagemSucesso"] = $"Dados atualizados com sucesso. Chassi: {conferenciaEditarAvariasVM.InspVeiculo.VIN_6},  Avaria:  {conferenciaEditarAvariasVM.InspAvaria.InspAvaria_ID}";
            }

            List<InspAvaria_Conf> listaInspAvaria_Conf = new List<InspAvaria_Conf>();

            ListarConferenciaAvariaViewModel listarConferenciaAvariaVM = new ListarConferenciaAvariaViewModel();
            listarConferenciaAvariaVM.ListaInspAvaria_Conf = BLL.InspAvariaConf.ListarAvarias_Conf(conferenciaEditarAvariasVM.Inspecao.Cliente_ID, conferenciaEditarAvariasVM.Inspecao.LocalInspecao_ID, conferenciaEditarAvariasVM.Inspecao.LocalCheckPoint_ID, conferenciaEditarAvariasVM.Inspecao.Data, configuracao);
            listarConferenciaAvariaVM.InspAvaria_Conf = new Models.InspAvaria_Conf();

            listarConferenciaAvariaVM.InspAvaria_Conf.Data = conferenciaEditarAvariasVM.Inspecao.Data;
            listarConferenciaAvariaVM.InspAvaria_Conf.LocalNome = listarConferenciaAvariaVM.ListaInspAvaria_Conf.FirstOrDefault().LocalNome;
            listarConferenciaAvariaVM.InspAvaria_Conf.CheckPointNome = listarConferenciaAvariaVM.ListaInspAvaria_Conf.FirstOrDefault().CheckPointNome;

            return View("ListarConferenciaAvarias", listarConferenciaAvariaVM);

        }

        /// <summary>
        /// Visualizar fotos da avaria - Botão Fotos
        /// </summary>
        /// <param name="inspAvaria_ID"></param>
        /// <returns></returns>
        public IActionResult VisualizarFotos(int inspAvaria_ID)
        {
            ConferenciaVisualizarAvariasViewModel visualizarAvariasVM = new ConferenciaVisualizarAvariasViewModel();

            visualizarAvariasVM.InspAvaria = new Models.InspAvaria();
            visualizarAvariasVM.InspAvaria = BLL.Avarias.ListarPorId(inspAvaria_ID, configuracao);

            visualizarAvariasVM.ListaImagemAvarias = BLL.UploadImagens.Listar(inspAvaria_ID, configuracao);


            return View("VisualizarFotos", visualizarAvariasVM);

        }
        /// <summary>
        /// Grava fotos da avaria
        /// </summary>
        /// <param name="inspAvaria_ID">ID da avaria</param>
        /// <param name="files">Fotos referentes a avaria</param>
        /// <returns></returns>
        public IActionResult SalvarFotos(int inspAvaria_ID, ICollection<IFormFile> files)
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

            conferenciaEditarAvariasVM.InspAvaria = BLL.Avarias.ListarPorId(inspAvaria_ID, configuracao);

            conferenciaEditarAvariasVM.InspVeiculo = BLL.InspecaoVeiculo.ListarPorId(conferenciaEditarAvariasVM.InspAvaria.InspVeiculo_ID, configuracao);

            conferenciaEditarAvariasVM.Inspecao = BLL.Inspecao.ListarPorId(conferenciaEditarAvariasVM.InspAvaria.Inspecao_ID, configuracao);

            listarConferenciaAvariaVM.ListaInspAvaria_Conf = BLL.InspAvariaConf.ListarAvarias_Conf(conferenciaEditarAvariasVM.Inspecao.Cliente_ID, conferenciaEditarAvariasVM.Inspecao.LocalInspecao_ID, conferenciaEditarAvariasVM.Inspecao.LocalCheckPoint_ID, conferenciaEditarAvariasVM.Inspecao.Data, configuracao);

            listarConferenciaAvariaVM.InspAvaria_Conf.Data = conferenciaEditarAvariasVM.Inspecao.Data;

            listarConferenciaAvariaVM.InspAvaria_Conf.LocalNome = listarConferenciaAvariaVM.ListaInspAvaria_Conf.FirstOrDefault().LocalNome;

            listarConferenciaAvariaVM.InspAvaria_Conf.CheckPointNome = listarConferenciaAvariaVM.ListaInspAvaria_Conf.FirstOrDefault().CheckPointNome;

            return View("ListarConferenciaAvarias", listarConferenciaAvariaVM);
        }

        /// <summary>
        /// Clique do usuário no botão Loading list
        /// </summary>
        /// <returns>View</returns>
        public IActionResult LoadingListInicio()
        {
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

            ConferenciaLoadingListViewModel conferenciaLoadingListVM = new ConferenciaLoadingListViewModel();
            conferenciaLoadingListVM.ListaCliente = BLL.Inspecao.ListarClientes(dadosUsuario.UsuarioId, configuracao);
            conferenciaLoadingListVM.ListaLocalInspecao = BLL.Inspecao.ListarLocaisInspecao(dadosUsuario.UsuarioId, configuracao, dadosUsuario.Usuario.Locais);
            #region EM_ERRO
            if (conferenciaLoadingListVM.ListaCliente.FirstOrDefault().Erro == true)
            {
                ViewData["MensagemErro"] = (conferenciaLoadingListVM.ListaCliente.FirstOrDefault().MensagemErro);
            }
            if (conferenciaLoadingListVM.ListaLocalInspecao.FirstOrDefault().Erro == true)
            {
                ViewData["MensagemErro"] = conferenciaLoadingListVM.ListaLocalInspecao.FirstOrDefault().MensagemErro;
            }
            #endregion

            return View("LoadingListInicio", conferenciaLoadingListVM);
        }

        /// <summary>
        /// Clique do usuário no botão packinglist
        /// </summary>
        /// <returns></returns>
        public IActionResult PackingListInicio()
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

            return View("PackingListInicio", conferenciaPackingListVM);

        }

        /// <summary>
        /// Salva as informações de cabeçalho da LoadingList Enviada pelo usuário
        /// </summary>
        /// <param name="conferenciaLoadingListVM"></param>
        /// <param name="files">Arquivo contendo os dados em txt.</param>
        /// <returns></returns>
        public IActionResult LoadingListSalvar(ConferenciaLoadingListViewModel conferenciaLoadingListVM, ICollection<IFormFile> files)
        {
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

        /// <summary>
        /// Salvar PackingList Informado pelo usuároi
        /// </summary>
        /// <param name="conferenciaPackingListVM"></param>
        /// <param name="files">Arquivo contendo dados da PackingList</param>
        /// <returns></returns>
        public IActionResult PackingListSalvar(ConferenciaPackingListViewModel conferenciaPackingListVM, ICollection<IFormFile> files)
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

            if (files.Count() > 0)
            {
                if (files.FirstOrDefault().ContentType == "text/plain")
                {
                    Models.ListaVeiculos listaVeiculos = new ListaVeiculos
                    {
                        Cliente_ID = conferenciaPackingListVM.Cliente_ID,
                        DataHoraInclusao = DateTime.Now,
                        LocalInspecao_ID = conferenciaPackingListVM.LocalInspecao_ID,
                        NomeArquivo = files.FirstOrDefault().FileName,
                        Tipo = 'P',
                        Usuario_ID = dadosUsuario.UsuarioId
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

        public IActionResult Voltar(string nomeView)
        {
            return RedirectToAction("NovaConferencia", "Conferencia");
        }

    }
}
