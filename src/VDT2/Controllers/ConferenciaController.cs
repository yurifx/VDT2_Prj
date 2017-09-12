// <copyright file="ConferenciaController.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Grupo Asserth</author>
// <email>yuri.vasconcelos@grupoasserth.com.br</email>
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
using VDT2.BLL;
using System.IO;

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
                ViewData["Mensagem"] = TempData["Mensagem"];
            }

            Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"Action acionada: NovaConferencia | Sem Parametros" });


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

            Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"Action acionada: ConferenciaListarVeiculos" });

            const string _mensagemLogin = "Erro ao identificar usuário, tente novamente mais tarde ou faça um novo login";
            const string _mensagemErro = "Não foi possível listar, por favor tente novamente mais tarde ou entre em contato com o suporte técnico";
            ListarConferenciaAvariaViewModel listarConferenciaAvariaVM = new ListarConferenciaAvariaViewModel();
            listarConferenciaAvariaVM.InspAvaria_Conf = new Models.InspAvaria_Conf();

            try
            {

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


                if (conferenciaVM != null)
                {
                    if (!(conferenciaVM.Cliente_ID == 0 || conferenciaVM.LocalInspecao_ID == 0 || conferenciaVM.LocalCheckPoint_ID == 0 || conferenciaVM.Data == null))
                    {
                        listarConferenciaAvariaVM = RecebeDadosConferenciaTelaConferencia(listarConferenciaAvariaVM, conferenciaVM.Cliente_ID, conferenciaVM.LocalInspecao_ID, conferenciaVM.LocalCheckPoint_ID, conferenciaVM.Data, configuracao);

                        //Realiza a concatenação de inspeções para mandar p/ View
                        StringBuilder sbInspecao = new StringBuilder();
                        if (listarConferenciaAvariaVM.ListaInspAvaria_Conf.Count() > 0)
                        {
                            HashSet<int> controle = new HashSet<int>();

                            foreach (var item in listarConferenciaAvariaVM.ListaInspAvaria_Conf)
                            {
                                //Lógica utilizada para não receber valores duplicados
                                if (!controle.Contains(item.Inspecao_ID))
                                {
                                    sbInspecao.Append($"{item.Inspecao_ID};");
                                    controle.Add(item.Inspecao_ID);
                                }
                            }
                        }

                        listarConferenciaAvariaVM.ConcatInspecoes = sbInspecao.ToString();

                        if (listarConferenciaAvariaVM.ListaInspAvaria_Conf == null)
                        {
                            ViewData["MensagemErro"] = _mensagemErro;
                        }

                        //Preenche dados do cabecalho da proxima view
                        listarConferenciaAvariaVM.InspAvaria_Conf.Data = conferenciaVM.Data;
                        listarConferenciaAvariaVM.InspAvaria_Conf.LocalNome = BLL.Inspecao.ListarLocaisInspecao(dadosUsuario.UsuarioId, configuracao, listarConferenciaAvariaVM.Usuario.Locais).Where(p => p.LocalInspecao_ID == conferenciaVM.LocalInspecao_ID).FirstOrDefault().Nome;

                        var dadosCheckPoint = BLL.Inspecao.ListarLocalCheckPoint(dadosUsuario.UsuarioId, configuracao).Where(p => p.LocalCheckPoint_ID == conferenciaVM.LocalCheckPoint_ID).FirstOrDefault();

                        listarConferenciaAvariaVM.InspAvaria_Conf.CheckPointNome = dadosCheckPoint.Nome_Pt;
                        listarConferenciaAvariaVM.InspAvaria_Conf.Operacao = dadosCheckPoint.Operacao;
                        listarConferenciaAvariaVM.InspAvaria_Conf.TransportadorTipo = dadosCheckPoint.Tipo;
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
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Excecao = ex, Mensagem = "Erro ao solicitar operacao, ConferenciaListarVeiculos" });

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
            Diag.Log.Grava(new Diag.LogItem() { Nivel = Diag.Nivel.Informacao, Mensagem = $"Action acoinada: EditarAvarias | Parametros: InspAvaria_ID: {inspAvaria_ID}" });

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


                return View("EditarAvariasConferencia", conferenciaEditarAvariasVM);
            }
            catch (Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Excecao = ex, Mensagem = $"Erro ao solicitar operacao, EditarAvarias" });

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

            Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"Action acionada: SalvarAvaria" });

            DateTime dataEnviada;
            DateTime dataAntiga = new DateTime();

            try
            {
                //realiza updates
                //Na versão atual somente alteramos a DATA, nenhum outro campo da inspeção
                var inspecaoInicial = new Models.Inspecao();

                if (conferenciaEditarAvariasVM.Inspecao.Inspecao_ID != 0)
                {
                    dataEnviada = conferenciaEditarAvariasVM.Inspecao.Data;
                    inspecaoInicial = BLL.Inspecao.ListarPorId(conferenciaEditarAvariasVM.Inspecao.Inspecao_ID, configuracao);
                    dataAntiga = inspecaoInicial.Data;

                    if (dataEnviada != dataAntiga)
                    {
                        inspecaoInicial.Data = dataEnviada;//Modifico aqui a data da inspeção que será alterada
                        conferenciaEditarAvariasVM.Inspecao = BLL.Inspecao.Update(inspecaoInicial, configuracao);
                        if (conferenciaEditarAvariasVM.Inspecao.Erro == true)
                        {
                            ViewData["MensagemErro"] = "Erro ao atualizar data da inspeção, tente novamente mais tarde ou entre em contato com o suporte";
                            return RedirectToAction("Home", "Index");
                        }
                    };
                }

                //Atualiza Veículo
                if (conferenciaEditarAvariasVM.InspVeiculo != null)
                {
                    conferenciaEditarAvariasVM.InspVeiculo = BLL.InspecaoVeiculo.Update(conferenciaEditarAvariasVM.InspVeiculo, configuracao);
                }

                if (conferenciaEditarAvariasVM.InspAvaria != null)
                {
                    //Atualiza Avaria
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

                //lista ocorrencias! 
                //faço essa requisição pois no update do inspVeiculo não retorna o Inspecao_ID
                conferenciaEditarAvariasVM.InspVeiculo = BLL.InspecaoVeiculo.ListarPorId(conferenciaEditarAvariasVM.InspVeiculo.InspVeiculo_ID, configuracao);

                List<InspAvaria_Conf> listaInspAvaria_Conf = new List<InspAvaria_Conf>();

                ListarConferenciaAvariaViewModel listarConferenciaAvariaVM = new ListarConferenciaAvariaViewModel();

                listarConferenciaAvariaVM.InspAvaria_Conf = new Models.InspAvaria_Conf();

                //Recebe pendências, integra vin, recebe os veículos
                listarConferenciaAvariaVM = RecebeDadosConferenciaTelaConferencia(listarConferenciaAvariaVM, inspecaoInicial.Cliente_ID, inspecaoInicial.LocalInspecao_ID, inspecaoInicial.LocalCheckPoint_ID, dataAntiga, configuracao);

                //Estes dados serão passados para view
                listarConferenciaAvariaVM.InspAvaria_Conf.Data = dataAntiga;

                if (listarConferenciaAvariaVM.ListaInspAvaria_Conf.Count() > 0)
                {
                    listarConferenciaAvariaVM.InspAvaria_Conf.LocalNome = listarConferenciaAvariaVM.ListaInspAvaria_Conf.FirstOrDefault().LocalNome;

                    var dadosCheckPoint = listarConferenciaAvariaVM.ListaInspAvaria_Conf.FirstOrDefault();

                    listarConferenciaAvariaVM.InspAvaria_Conf.CheckPointNome = dadosCheckPoint.CheckPointNome;

                    listarConferenciaAvariaVM.InspAvaria_Conf.Operacao = dadosCheckPoint.Operacao;

                    listarConferenciaAvariaVM.InspAvaria_Conf.TransportadorTipo = dadosCheckPoint.TransportadorTipo;
                }
                else
                {
                    ViewData["MensagemErro"] = "Não há registros nesta data.";
                }

                return View("ListarConferenciaAvarias", listarConferenciaAvariaVM);
            }

            catch (Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Erro, Excecao = ex, Mensagem = $"Erro ao processar informação tente novamente mais tarde, SalvarAvaria {ex}" });

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

            Diag.Log.Grava(new Diag.LogItem() { Nivel = Diag.Nivel.Informacao, Mensagem = $"Action acionada: VisualizarFotos | Parametro InspAvaria_ID: {inspAvaria_ID}" });

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
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Erro, Excecao = ex, Mensagem = $"Erro ao processar informação tente novamente mais tarde, VisualizarFotos; {ex}" });

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

            Diag.Log.Grava(new Diag.LogItem() { Nivel = Diag.Nivel.Informacao, Mensagem = $"Action acionada: SalvarFotos | Parametro InspAvaria_ID: {inspAvaria_ID}" });

            try
            {
                bool uploadImagem = false;

                ConferenciaEditarAvariasViewModel conferenciaEditarAvariasVM = new ConferenciaEditarAvariasViewModel();

                ListarConferenciaAvariaViewModel listarConferenciaAvariaVM = new ListarConferenciaAvariaViewModel();

                listarConferenciaAvariaVM.InspAvaria_Conf = new Models.InspAvaria_Conf();

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


                listarConferenciaAvariaVM = RecebeDadosConferenciaTelaConferencia(listarConferenciaAvariaVM, conferenciaEditarAvariasVM.Inspecao.Cliente_ID, conferenciaEditarAvariasVM.Inspecao.LocalInspecao_ID, conferenciaEditarAvariasVM.Inspecao.LocalCheckPoint_ID, conferenciaEditarAvariasVM.Inspecao.Data, configuracao);

                listarConferenciaAvariaVM.InspAvaria_Conf.Data = conferenciaEditarAvariasVM.Inspecao.Data;

                listarConferenciaAvariaVM.InspAvaria_Conf.LocalNome = listarConferenciaAvariaVM.ListaInspAvaria_Conf.FirstOrDefault().LocalNome;

                listarConferenciaAvariaVM.InspAvaria_Conf.CheckPointNome = listarConferenciaAvariaVM.ListaInspAvaria_Conf.FirstOrDefault().CheckPointNome;

                listarConferenciaAvariaVM.InspAvaria_Conf.TransportadorTipo = listarConferenciaAvariaVM.ListaInspAvaria_Conf.FirstOrDefault().TransportadorTipo;

                return View("ListarConferenciaAvarias", listarConferenciaAvariaVM);
            }
            catch (Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Erro, Excecao = ex, Mensagem = $"Erro ao processar informação tente novamente mais tarde, SalvarFotos; {ex}" });

                TempData["Erro"] = tempErro;

                return RedirectToAction("NovaConferencia");
            }
        }


        /// <summary>
        /// 1- Busca Pendências e Integra Vin
        /// 2- Lista todas as avarias
        /// 3- Lista o report (Summary)
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="cliente_ID"></param>
        /// <param name="localInspecao_ID"></param>
        /// <param name="localCheckPoint_ID"></param>
        /// <param name="data"></param>
        /// <param name="configuracao"></param>
        /// <returns></returns>
        private ListarConferenciaAvariaViewModel RecebeDadosConferenciaTelaConferencia(ListarConferenciaAvariaViewModel obj, int cliente_ID, int localInspecao_ID, int localCheckPoint_ID, DateTime data, Configuracao configuracao)
        {
            try
            {

                obj.Pendencias = BLL.InspecaoVeiculo.IntegrarVIN(cliente_ID, localInspecao_ID, localCheckPoint_ID, data, configuracao);

                obj.ListaInspAvaria_Conf = BLL.InspAvariaConf.ListarAvarias_Conf(cliente_ID, localInspecao_ID, localCheckPoint_ID, data, configuracao);

                obj.ConferenciaSummary = BLL.InspAvariaConf.ListarConferenciaSummary(cliente_ID, localInspecao_ID, localCheckPoint_ID, data, configuracao);

                return obj;
            }
            catch (Exception ex)

            {
                Diag.Log.Grava(new Diag.LogItem { Excecao = ex, Mensagem = "Erro ao receber dados conferencia - Tela inspeção", Nivel = Diag.Nivel.Erro });
                return obj;
            }
        }

        /// <summary>
        /// Clique do usuário no botão packinglist
        /// </summary>
        /// <returns></returns>
        public IActionResult SalvarListaInicio()
        {
            Diag.Log.Grava(new Diag.LogItem() { Nivel = Diag.Nivel.Informacao, Mensagem = $"Action acionada: SalvarListaInicio | Sem Parametros" });

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

                ConferenciaListaViewModel conferenciaListaVM = new ConferenciaListaViewModel();

                conferenciaListaVM.ListaCliente = BLL.Inspecao.ListarClientes(dadosUsuario.UsuarioId, configuracao);

                if (conferenciaListaVM.ListaCliente.FirstOrDefault().Erro == true)
                {
                    ViewData["MensagemErro"] = "Erro ao listar Clientes, tente novamente mais tarde ou entre em contato com o suporte técnico.";
                }


                conferenciaListaVM.ListaLocalInspecao = BLL.Inspecao.ListarLocaisInspecao(dadosUsuario.UsuarioId, configuracao, dadosUsuario.Usuario.Locais);

                if (conferenciaListaVM.ListaLocalInspecao.FirstOrDefault().Erro == true)
                {
                    ViewData["MensagemErro"] = conferenciaListaVM.ListaLocalInspecao.FirstOrDefault().MensagemErro;
                }

                conferenciaListaVM.ListaLocalCheckPoint = BLL.Inspecao.ListarLocalCheckPoint(dadosUsuario.UsuarioId, configuracao);

                if (conferenciaListaVM.ListaLocalCheckPoint.FirstOrDefault().Erro == true)
                {
                    ViewData["MensagemErro"] = conferenciaListaVM.ListaLocalCheckPoint.FirstOrDefault().MensagemErro;
                }

                return View("EnviarLista", conferenciaListaVM);
            }

            catch (Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Erro, Excecao = ex, Mensagem = $"Erro ao processar informação tente novamente mais tarde, LoadingListInicio; {ex}" });

                TempData["Erro"] = tempErro;

                return RedirectToAction("NovaConferencia");
            }
        }

        /// <summary>
        /// Salvar Lista Informado pelo usuário
        /// </summary>
        /// <param name="conferenciaListaVM">Model contendo os dados enviados</param>
        /// <param name="files">Arquivo contendo dados da Lista, em formato txt</param>
        /// <returns></returns>
        public IActionResult SalvarLista(ConferenciaListaViewModel conferenciaListaVM, ICollection<IFormFile> files)
        {

            Diag.Log.Grava(new Diag.LogItem() { Nivel = Diag.Nivel.Informacao, Mensagem = $"Action acionada: SalvarLista, Controller - Conferência | Parametros {conferenciaListaVM.TextoLog()}" });

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

                conferenciaListaVM.ListaCliente = BLL.Inspecao.ListarClientes(dadosUsuario.UsuarioId, configuracao);
                conferenciaListaVM.ListaLocalInspecao = BLL.Inspecao.ListarLocaisInspecao(dadosUsuario.UsuarioId, configuracao, dadosUsuario.Usuario.Locais);
                conferenciaListaVM.ListaLocalCheckPoint = BLL.Inspecao.ListarLocalCheckPoint(dadosUsuario.UsuarioId, configuracao);

                if (files.Count() > 0)
                {
                    if (files.FirstOrDefault().ContentType == "text/plain")
                    {
                        Models.ListaVeiculos listaVeiculos = new ListaVeiculos
                        {
                            Cliente_ID = conferenciaListaVM.Cliente_ID,
                            Usuario_ID = dadosUsuario.UsuarioId,
                            DataHoraInclusao = DateTime.Now,
                            LocalInspecao_ID = conferenciaListaVM.LocalInspecao_ID,
                            LocalCheckPoint_ID = conferenciaListaVM.LocalCheckPoint_ID,
                            NomeArquivo = files.FirstOrDefault().FileName,
                            TipoLista = conferenciaListaVM.TipoLista,
                            Lote = conferenciaListaVM.Lote
                        };

                        listaVeiculos = BLL.Conferencia.InserirListaVeiculos(listaVeiculos, configuracao);

                        if (listaVeiculos.ListaVeiculo_ID != 0)
                        {
                            inseriuArquivo = true;

                            salvou = BLL.UploadTxt.SalvarArquivo(listaVeiculos.ListaVeiculo_ID, listaVeiculos.TipoLista, files, configuracao);

                            if (salvou)
                            {
                                integrou = BLL.Conferencia.IntegrarArquivoLista(listaVeiculos.ListaVeiculo_ID, listaVeiculos.TipoLista, files, configuracao);

                                if (integrou)
                                {
                                    ViewData["MensagemSucesso"] = "Upload realizado com sucessso";
                                }
                            }
                        }

                        if (!salvou || !integrou || !inseriuArquivo)
                            ViewData["MensagemErro"] = "Erro ao realizar upload de arquivo.Tente novamente mais tarde ou entre em contato com service desk";

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

                return View("EnviarLista", conferenciaListaVM);

            }
            catch (Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Erro, Excecao = ex, Mensagem = $"Erro ao processar informação tente novamente mais tarde, PackingListSalvar; {ex}" });

                TempData["Erro"] = tempErro;

                return RedirectToAction("NovaConferencia");
            }

        }

        /// <summary>
        /// Metodo utilizado para deletar avarias
        /// </summary>
        /// <param name="avaria_ID">id da avaria</param>
        /// <returns>Retorna a view anterior</returns>
        public IActionResult DeletarAvaria(int avaria_ID)
        {
            Diag.Log.Grava(new Diag.LogItem
            {
                Nivel = Diag.Nivel.Informacao,
                Mensagem = $"Action acionada: DeletarAvaria - avaria_ID: {avaria_ID}"
            });


            //Inicializações
            ConferenciaEditarAvariasViewModel conferenciaEditarAvariasVM = new ConferenciaEditarAvariasViewModel();
            ListarConferenciaAvariaViewModel listarConferenciaAvariaVM = new ListarConferenciaAvariaViewModel();
            listarConferenciaAvariaVM.InspAvaria_Conf = new Models.InspAvaria_Conf();


            //Preciso guardar os dados da avaria antes de deletar. Pois utilizarei esses dados para pegar informações do veículo e inspeção.
            conferenciaEditarAvariasVM.InspAvaria = BLL.Avarias.ListarPorId(avaria_ID, configuracao);

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
                listarConferenciaAvariaVM.Pendencias = BLL.InspecaoVeiculo.IntegrarVIN(conferenciaEditarAvariasVM.Inspecao.Cliente_ID, conferenciaEditarAvariasVM.Inspecao.LocalInspecao_ID, conferenciaEditarAvariasVM.Inspecao.LocalCheckPoint_ID, conferenciaEditarAvariasVM.Inspecao.Data, configuracao);

                listarConferenciaAvariaVM.ListaInspAvaria_Conf = BLL.InspAvariaConf.ListarAvarias_Conf(conferenciaEditarAvariasVM.Inspecao.Cliente_ID, conferenciaEditarAvariasVM.Inspecao.LocalInspecao_ID, conferenciaEditarAvariasVM.Inspecao.LocalCheckPoint_ID, conferenciaEditarAvariasVM.Inspecao.Data, configuracao);

                listarConferenciaAvariaVM.ConferenciaSummary = BLL.InspAvariaConf.ListarConferenciaSummary(conferenciaEditarAvariasVM.Inspecao.Cliente_ID, conferenciaEditarAvariasVM.Inspecao.LocalInspecao_ID, conferenciaEditarAvariasVM.Inspecao.LocalCheckPoint_ID, conferenciaEditarAvariasVM.Inspecao.Data, configuracao);
            }


            if (listarConferenciaAvariaVM.ListaInspAvaria_Conf != null)
            {
                listarConferenciaAvariaVM.InspAvaria_Conf.Data = conferenciaEditarAvariasVM.Inspecao.Data;

                listarConferenciaAvariaVM.InspAvaria_Conf.LocalNome = listarConferenciaAvariaVM.ListaInspAvaria_Conf.FirstOrDefault().LocalNome;

                listarConferenciaAvariaVM.InspAvaria_Conf.CheckPointNome = listarConferenciaAvariaVM.ListaInspAvaria_Conf.FirstOrDefault().CheckPointNome;

                listarConferenciaAvariaVM.InspAvaria_Conf.TransportadorTipo = listarConferenciaAvariaVM.ListaInspAvaria_Conf.FirstOrDefault().TransportadorTipo;
            }
            else
            {
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Erro, Mensagem = "Erro ao processar informação tente novamente mais tarde, ConferenciaController | DeletarAvaria" });

                TempData["Erro"] = tempErro;

                return RedirectToAction("NovaConferencia");
            }

            return View("ListarConferenciaAvarias", listarConferenciaAvariaVM);
        }

        /// <summary>
        /// Action method utilizado para deletar Veículos inspecionados
        /// </summary>
        /// <param name="id">ID do veículo</param>
        /// <returns></returns>
        public IActionResult DeletarVeiculo(int id)
        {
            Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"Action acionada: DeletarVeiculo - Veiculo-ID {id}" });

            //Inicializações
            ConferenciaEditarAvariasViewModel conferenciaEditarVM = new ConferenciaEditarAvariasViewModel();

            ListarConferenciaAvariaViewModel listarConferenciaAvariaVM = new ListarConferenciaAvariaViewModel();

            listarConferenciaAvariaVM.InspAvaria_Conf = new Models.InspAvaria_Conf();

            //Preciso guardar os dados do veiculo antes de deletar. Pois utilizarei esses dados para pegar informações da inspeção.
            conferenciaEditarVM.InspVeiculo = BLL.InspecaoVeiculo.ListarPorId(id, configuracao);


            //Verifica se está com erro;
            if (conferenciaEditarVM.InspVeiculo != null)
            {

                if (!conferenciaEditarVM.InspVeiculo.Erro)
                {
                    conferenciaEditarVM.Inspecao = BLL.Inspecao.ListarPorId(conferenciaEditarVM.InspVeiculo.Inspecao_ID, configuracao);
                }

                else
                {
                    Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Erro, Mensagem = $"Não foi possível consultar dados do veículo informado: Veículo_ID {id}" });

                    TempData["Erro"] = "Erro ao consultar dado do veículo tente novamente mais tarde";
                }
            }

            //Deleta veículo
            bool deletou = BLL.InspecaoVeiculo.DeletarVeiculo(id, configuracao);
            if (deletou)
            {
                ViewData["MensagemSucesso"] = "Veículo deletado com sucesso";
            }
            else
            {
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Erro, Mensagem = $"Action DeletarVeiculo - Não conseguiu deletar veículo: Veículo-ID {id}" });

                ViewData["MensagemSucesso"] = "Não conseguiu deletar veículo, verifique suas avarias";
            }


            //Verifica se retornou erro
            if (conferenciaEditarVM.InspVeiculo.Erro == true || conferenciaEditarVM.Inspecao.Erro == true)
            {
                ViewData["MensagemErro"] = "Erro ao listar dados, tente novamente mais tarde ou entre em contato com o suporte";

                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Erro, Mensagem = $"Erro ao listar dados: Erro - ConferenciaEditarVM.InspVeiculo.Erro == true : Veículo-ID {id}" });
            }


            listarConferenciaAvariaVM.Pendencias = BLL.InspecaoVeiculo.IntegrarVIN(conferenciaEditarVM.Inspecao.Cliente_ID, conferenciaEditarVM.Inspecao.LocalInspecao_ID, conferenciaEditarVM.Inspecao.LocalCheckPoint_ID, conferenciaEditarVM.Inspecao.Data, configuracao);

            listarConferenciaAvariaVM.ListaInspAvaria_Conf = BLL.InspAvariaConf.ListarAvarias_Conf(conferenciaEditarVM.Inspecao.Cliente_ID, conferenciaEditarVM.Inspecao.LocalInspecao_ID, conferenciaEditarVM.Inspecao.LocalCheckPoint_ID, conferenciaEditarVM.Inspecao.Data, configuracao);

            listarConferenciaAvariaVM.ConferenciaSummary = BLL.InspAvariaConf.ListarConferenciaSummary(conferenciaEditarVM.Inspecao.Cliente_ID, conferenciaEditarVM.Inspecao.LocalInspecao_ID, conferenciaEditarVM.Inspecao.LocalCheckPoint_ID, conferenciaEditarVM.Inspecao.Data, configuracao);

            if (listarConferenciaAvariaVM.ListaInspAvaria_Conf != null && listarConferenciaAvariaVM.ListaInspAvaria_Conf.Count() > 0)
            {
                listarConferenciaAvariaVM.InspAvaria_Conf.Data = conferenciaEditarVM.Inspecao.Data;
                listarConferenciaAvariaVM.InspAvaria_Conf.LocalNome = listarConferenciaAvariaVM.ListaInspAvaria_Conf.FirstOrDefault().LocalNome;
                listarConferenciaAvariaVM.InspAvaria_Conf.CheckPointNome = listarConferenciaAvariaVM.ListaInspAvaria_Conf.FirstOrDefault().CheckPointNome;
                listarConferenciaAvariaVM.InspAvaria_Conf.TransportadorTipo = listarConferenciaAvariaVM.ListaInspAvaria_Conf.FirstOrDefault().TransportadorTipo;
            }
            else
            {
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = "Erro ao processar informação tente novamente mais tarde, ConferenciaController | DeletarAvaria" });

                TempData["Erro"] = tempErro;

                return RedirectToAction("NovaConferencia");
            }

            return View("ListarConferenciaAvarias", listarConferenciaAvariaVM);
        }

        /// <summary>
        /// Action method para voltar a tela anterior
        /// </summary>
        /// <param name="nomeView">Nome da view de retorno</param>
        /// <returns></returns>
        public IActionResult Voltar(string nomeView)
        {
            Diag.Log.Grava(new Diag.LogItem() { Nivel = Diag.Nivel.Informacao, Mensagem = "Action acionada: Voltar | Sem Parametros" });
            return View("NovaConferencia");
        }

        /// <summary>
        /// Realiza as consultas de avarias
        /// </summary>
        /// <returns>Retorna tela inicial de consultas</returns>
        public IActionResult NovaConsulta()
        {

            Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = "Action acionada: Nova Consulta | Sem parametros" });

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
            else if (clientes.Count() == 1)
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

            Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"Action acionada: ConsultaCliente | Parametros {Cliente_ID}" });

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

            Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"Action acionada: ListarConsulta - Controller: ConferenciaController" });

            string _mensagemErroLogin = "Erro ao ListarConsulta";

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

            ConferenciaConsultaVeiculosViewModel consultaVeiculosVM = new ConferenciaConsultaVeiculosViewModel();

            try
            {
                consultaVeiculosVM.ListaInspAvaria_Cons = BLL.InspAvariaCons.ConsultarVeiculos(consultaVM, configuracao);
                consultaVeiculosVM.ListaInspAvaria_Summary = BLL.InspAvariaCons.ConsultarSumario(consultaVM, configuracao);

                var dados = BLL.InspAvariaCons.RecebeDadosUsuario(consultaVM);

                //Objeto serializado para enviar a view
                consultaVeiculosVM.FiltroRealizado = JsonConvert.SerializeObject(dados);

                consultaVeiculosVM.QuantidadeInspecionada = consultaVeiculosVM.ListaInspAvaria_Summary.Where(p => p.ID == 1).FirstOrDefault().Total;
                consultaVeiculosVM.VeiculosComAvaria = consultaVeiculosVM.ListaInspAvaria_Summary.Where(p => p.ID == 2).FirstOrDefault().Total;
                consultaVeiculosVM.VeiculosSemAvaria = consultaVeiculosVM.ListaInspAvaria_Summary.Where(p => p.ID == 3).FirstOrDefault().Total;
                consultaVeiculosVM.QuantidadeAvarias = consultaVeiculosVM.ListaInspAvaria_Summary.Where(p => p.ID == 4).FirstOrDefault().Total;
                consultaVeiculosVM.QuantidadeAvariasTransporte = consultaVeiculosVM.ListaInspAvaria_Summary.Where(p => p.ID == 5).FirstOrDefault().Total;
                consultaVeiculosVM.QuantidadeAvariasFabrica = consultaVeiculosVM.ListaInspAvaria_Summary.Where(p => p.ID == 6).FirstOrDefault().Total;

                if (consultaVeiculosVM.QuantidadeInspecionada != 0)
                {
                    string PercentualComAvaria = ((decimal)(consultaVeiculosVM.VeiculosComAvaria) / (decimal)(consultaVeiculosVM.QuantidadeInspecionada) * 100).ToString("N2");
                    consultaVeiculosVM.PercentualAvariado = Convert.ToDecimal(PercentualComAvaria);
                    consultaVeiculosVM.PercentualSemAvaria = 100 - consultaVeiculosVM.PercentualAvariado;
                }
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "Erro ao realizar consulta. Tente novamente mais tarde ou entre em contato com o ServiceDesk";

                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Erro, Mensagem = $"Erro ao consultar IActionResult -  {ex}" });

                return RedirectToAction("Index", "Home");
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
            try
            {
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"Action acionada: Publicar | Parametros: ConcatInspecoes: {concatInspecoes}" });

                const string _mensagemErroLogin = "Erro ao identificar usuário tente novamente mais tarde.";

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

                if (publicou)
                {
                    TempData["Mensagem"] = "Publicação realizada com sucesso.";
                }
                else
                {
                    TempData["Mensagem"] = "Não foi possível realizar publicação, tente novamente mais tarde";
                }

                return RedirectToAction("NovaConferencia");
            }
            catch (Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem { Mensagem = $"Erro ao Publicar Inspeção - Conferência Controller - Action Publicar", Nivel = Diag.Nivel.Erro, Excecao = ex });
                TempData["MensagemErro"] = "Erro ao Publicar Inspeção, tente novamente mais tarde ou entre em contato com o suporte técnico";

                return RedirectToAction("NovaConferencia");
            }
        }

        /// <summary>
        /// Realiza a exportação do arquivo excel
        /// </summary>
        /// <param name="dados">string contendo os dados da consulta realizada</param>
        /// <returns></returns>
        public FileResult ExportarExcel(string dados)
        {
            Diag.Log.Grava(new Diag.LogItem
            {
                Nivel = Diag.Nivel.Informacao,
                Mensagem = $"Action acionada: Exportar Excel | Parametros: Dados: {dados}"
            });

            #region recebeDadosUsuario
            var dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);
            if (dadosUsuario == null)
            {
                return null;
            }

            var identificacao = this.Request.Cookies["Usr"];

            if (identificacao == null)
            {
                return null;

            }

            var objUsuario = JsonConvert.DeserializeObject<Models.Usuario>(identificacao);
            dadosUsuario.Usuario = objUsuario;
            #endregion

            try
            {
                InspAvaria_Cons inspAvaria_Cons = new InspAvaria_Cons();

                inspAvaria_Cons = JsonConvert.DeserializeObject<Models.InspAvaria_Cons>(dados);

                var ListaInspAvaria_Cons = DAL.InspAvaria.Consultar(inspAvaria_Cons, configuracao);

                GerarExcelConsultas gerarExcel = new GerarExcelConsultas();

                gerarExcel.GerarExcelInspecao(dadosUsuario.Nome, ListaInspAvaria_Cons, configuracao, Request.Scheme, Request.Host);

                var path = Path.Combine(System.IO.Path.GetTempPath(), $"{dadosUsuario.Nome}_RelatorioConsulta.xlsx");

                using (var fs = new FileStream(path, FileMode.Open))
                {
                    //Conversão para Byte
                    byte[] arquivoEmBytes = new byte[fs.Length];
                    fs.Read(arquivoEmBytes, 0, arquivoEmBytes.Length);
                    FileContentResult Arquivo = new FileContentResult(arquivoEmBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    Arquivo.FileDownloadName = $"{dadosUsuario.Nome}_RelatorioConsulta_{System.DateTime.Now.ToString("ddMMyyy")}.xlsx";
                    return Arquivo;
                }
            }
            catch (Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Erro, Mensagem = $"Erro ao gerar Exportar Excel " });

                throw new Exception("Não foi possível processar informação - Tente novamente mais tarde ou entre em contato com o suporte técnico ", ex);
            }
        }


        public string VisualizarFotosConsulta(int inspAvaria_ID)
        {
            try
            {
                string retorno = "";
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = "Action acionada - Visualizar Fotos Consulta" });
                var fotos = BLL.UploadImagens.Listar(inspAvaria_ID, configuracao);

                foreach (var item in fotos)
                {
                    //retorno += $"<img src={item.Path}></img>";
                    retorno += $@"<img class='img-thumbnail' style='max-width: 500px; width: 500px; margin-left:30px;margin-bottom:20px;' src='../Inspecao/Foto?imagem={item.Imagem}&inspAvaria_ID={inspAvaria_ID}' />";
                }

                return retorno;
                //return "<p>oi do controller</p>";

            }
            catch (Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem { Excecao = ex, Mensagem = "Erro ao visualizar fotos da consulta", Nivel = Diag.Nivel.Erro });
                return "";

            }
        }
    }
}

