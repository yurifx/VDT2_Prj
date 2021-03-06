﻿// <copyright file="InspecaoController.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Grupo Asserth</author>
// <email>yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>Controllers de Inspecao</summary>


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VDT2.DAL;
using VDT2.ViewModels;
using Microsoft.AspNetCore.Http;
using VDT2.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Rendering;
using VDT2.BLL;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Newtonsoft.Json;
using System.Text;

namespace VDT2.Controllers
{
    public class InspecaoController : Controller
    {
        private VDT2.Models.Configuracao Configuracao { get; set; }

        /// <summary>
        /// Construtor da classe
        /// <para>Recebe a configuração do aplicativo, usando Dependency Injection</para>
        /// </summary>
        /// <param name="settings">Configuração geral do aplicativo, carregada de appsettings.json</param>
        public InspecaoController(IOptions<VDT2.Models.Configuracao> settings, IHostingEnvironment environment)
        {
            this.Configuracao = settings.Value;
        }

        /// <summary>
        /// Index()
        /// <para>Inicialização da inspeção - Nova Inspeção</para>
        /// </summary>
        /// <returns></returns>
        public IActionResult NovaInspecao()
        {

            Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"Action acionada: NovaInspecao" });

            string _mensagemLogin = "Erro ao validar usuário, por favor realize o login novamente";

            InspecaoDadosGeraisViewModel inspecaoDadosGeraisVM = new InspecaoDadosGeraisViewModel();

            ViewModels.LoginViewModel dadosUsuario = null;

            dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);
            if (dadosUsuario != null)
            {
                ViewData["UsuarioNome"] = dadosUsuario.Nome;
                ViewData["UsuarioIdentificacao"] = dadosUsuario.Identificacao;

                var identificacao = this.Request.Cookies["Usr"];

                if (identificacao != null)
                {
                    var objUsuario = JsonConvert.DeserializeObject<Models.Usuario>(identificacao);
                    dadosUsuario.Usuario = objUsuario;

                    inspecaoDadosGeraisVM.Data = System.DateTime.Today;

                    inspecaoDadosGeraisVM.ListaCliente = BLL.Inspecao.ListarClientes(dadosUsuario.UsuarioId, Configuracao);

                    inspecaoDadosGeraisVM.ListaLocalInspecao = BLL.Inspecao.ListarLocaisInspecao(dadosUsuario.UsuarioId, Configuracao, dadosUsuario.Usuario.Locais);

                    inspecaoDadosGeraisVM.ListaLocalCheckPoint = BLL.Inspecao.ListarLocalCheckPoint(dadosUsuario.UsuarioId, Configuracao);

                    inspecaoDadosGeraisVM.ListaTransportador = BLL.Inspecao.ListarTransportadores(dadosUsuario.UsuarioId, Configuracao);

                    #region EM_ERRO
                    if (inspecaoDadosGeraisVM.ListaLocalInspecao.FirstOrDefault().Erro == true)
                    {
                        ViewData["MensagemErro"] = inspecaoDadosGeraisVM.ListaLocalInspecao.FirstOrDefault().MensagemErro;
                    }
                    if (inspecaoDadosGeraisVM.ListaCliente.FirstOrDefault().Erro == true)
                    {
                        ViewData["MensagemErro"] = inspecaoDadosGeraisVM.ListaCliente.FirstOrDefault().MensagemErro;
                    }
                    if (inspecaoDadosGeraisVM.ListaLocalCheckPoint.FirstOrDefault().Erro == true)
                    {
                        ViewData["MensagemErro"] = inspecaoDadosGeraisVM.ListaLocalCheckPoint.FirstOrDefault().MensagemErro;
                    }
                    if (inspecaoDadosGeraisVM.ListaTransportador.FirstOrDefault().Erro == true)
                    {
                        ViewData["MensagemErro"] = inspecaoDadosGeraisVM.ListaTransportador.FirstOrDefault().MensagemErro;
                    }
                    #endregion

                    return View("NovaInspecao", inspecaoDadosGeraisVM);
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
        }

        /// <summary>
        /// Insere ou atualiza dados de uma nova inspeção ou atualiza inspeção que já está no banco;
        /// </summary>
        /// <param name="inspecaoDadosGeraisVM">Dados do formulário de cadastro de inspeção</param>
        /// <param name="botaoEnviar">Botao acionado pelo usuário - View - NovaInspecao </param>
        /// <returns>Retorna a View (Veiculo)</returns>
        [HttpPost]
        public IActionResult InserirDadosCabecalhoInspecao(InspecaoDadosGeraisViewModel inspecaoDadosGeraisVM, string botaoEnviar)
        {
            Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"Action acionada: InserirDadosCabecalhoInspecao | Parametros: Cliente_ID: {inspecaoDadosGeraisVM.Cliente_ID}, Local Inspeção: {inspecaoDadosGeraisVM.LocalInspecao_ID}, LocalCheckPoint: {inspecaoDadosGeraisVM.LocalCheckPoint_ID}, Transportador[Id_Tipo] {inspecaoDadosGeraisVM.IdTipo}, NomeNavio: {inspecaoDadosGeraisVM.NomeNavio}, FrotaViagemNome {inspecaoDadosGeraisVM.FrotaViagemNome}, Em Edição: {inspecaoDadosGeraisVM.Edicao}", });

            string _mensagemLogin = "Erro ao validar usuário, por favor realize o login novamente";

            InspecaoVeiculoViewModel InspecaoVeiculoVM = new InspecaoVeiculoViewModel();
            InspecaoVeiculoVM.InspVeiculo = new Models.InspVeiculo();

            //Verifica dados do usuário
            ViewModels.LoginViewModel dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);

            if (dadosUsuario == null)
            {
                ViewData["MensagemErro"] = _mensagemLogin;

                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"Dados do usuário estão nulos, InserirDadosCabecalhoInspecao" });

                return RedirectToAction("Index", "Home");
            }

            var identificacao = this.Request.Cookies["Usr"];

            if (identificacao == null)
            {
                ViewData["MensagemErro"] = _mensagemLogin;

                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"Identificacao está nulo, InserirDadosCabecalhoInspecao" });

                return RedirectToAction("Index", "Home");
            }

            var objUsuario = JsonConvert.DeserializeObject<Models.Usuario>(identificacao);

            //Aqui insere Navio, FrotaViagem no banco de dados, além de receber os dados do transportador
            InspecaoVeiculoVM.Inspecao = BLL.Inspecao.MontaDadosInspecao(inspecaoDadosGeraisVM, Configuracao);

            //Atualiza dados
            if (inspecaoDadosGeraisVM.Edicao == 1)
            {
                InspecaoVeiculoVM.Inspecao.Inspecao_ID = inspecaoDadosGeraisVM.Inspecao_ID;

                if (InspecaoVeiculoVM.Inspecao != null)
                {
                    if (InspecaoVeiculoVM.Inspecao.Inspecao_ID != 0)
                    {
                        InspecaoVeiculoVM.Inspecao.Data = InspecaoVeiculoVM.Inspecao.Data;
                        InspecaoVeiculoVM.Inspecao = BLL.Inspecao.Update(InspecaoVeiculoVM.Inspecao, Configuracao);

                        if (InspecaoVeiculoVM.Inspecao.Erro == true)
                        {
                            ViewData["MensagemErro"] = InspecaoVeiculoVM.Inspecao.MensagemErro;
                        }
                        else
                        {
                            ViewData["MensagemSucesso"] = "Inspecao Atualizada com sucesso";
                        }
                    }
                }
            }

            //Insere
            else if (inspecaoDadosGeraisVM.Edicao == 0)
            {
                //Verifica se é um reload do formulário anterior
                string formsubmit = inspecaoDadosGeraisVM.Cliente_ID + inspecaoDadosGeraisVM.LocalInspecao_ID + inspecaoDadosGeraisVM.LocalCheckPoint_ID + inspecaoDadosGeraisVM.FrotaViagemNome + inspecaoDadosGeraisVM.IdTipo;

                var ultimosubmit = this.HttpContext.Session.GetString("formsubmit");

                if (formsubmit != ultimosubmit)
                {
                    this.HttpContext.Session.SetString("formsubmit", formsubmit);
                }
                else
                {
                    Diag.Log.Grava(new Diag.LogItem() { Nivel = Diag.Nivel.Informacao, Mensagem = $"Não foi possível finalizar solicitação. Dados são identicos aos enviados anteriormente - Prevent (f5)" });
                    TempData["MensagemErro"] = "Não é possível atualizar dados identicos aos informados anteriormente!";
                    return RedirectToAction("NovaInspecao");
                }


                InspecaoVeiculoVM.Inspecao = BLL.Inspecao.Inserir(InspecaoVeiculoVM.Inspecao, Configuracao);
                if (InspecaoVeiculoVM.Inspecao != null)
                {
                    if (InspecaoVeiculoVM.Inspecao.Erro == true)
                    {
                        #region EM_ERRO
                        TempData["MensagemErro"] = InspecaoVeiculoVM.Inspecao.MensagemErro;
                        inspecaoDadosGeraisVM.ListaCliente = BLL.Inspecao.ListarClientes(dadosUsuario.UsuarioId, Configuracao);
                        inspecaoDadosGeraisVM.ListaLocalInspecao = BLL.Inspecao.ListarLocaisInspecao(dadosUsuario.UsuarioId, Configuracao, objUsuario.Locais);
                        inspecaoDadosGeraisVM.ListaLocalCheckPoint = BLL.Inspecao.ListarLocalCheckPoint(dadosUsuario.UsuarioId, Configuracao);
                        inspecaoDadosGeraisVM.ListaTransportador = BLL.Inspecao.ListarTransportadores(dadosUsuario.UsuarioId, Configuracao);
                        return View("NovaInspecao", inspecaoDadosGeraisVM);
                        #endregion
                    }
                    else
                    {
                        ViewData["MensagemSucesso"] = $"Dados gravados com sucesso! Inspeção: {InspecaoVeiculoVM.Inspecao.Inspecao_ID}";
                    }
                }
            }

            //Preenche Dados para próxima View
            InspecaoVeiculoVM.Marca = BLL.InspecaoVeiculo.ListaMarca(inspecaoDadosGeraisVM.Cliente_ID, Configuracao);

            InspecaoVeiculoVM.Modelo = BLL.InspecaoVeiculo.ListaModelo(inspecaoDadosGeraisVM.Cliente_ID, Configuracao);

            #region EM_ERRO
            if (InspecaoVeiculoVM.Modelo.FirstOrDefault().Text == "ERRO")
            {
                ViewData["MensagemErro"] += "Erro ao listar Modelos, tente novamente mais tarde ou entre em contato com o suporte técnico";
            }

            if (InspecaoVeiculoVM.Marca.FirstOrDefault().Text == "ERRO")
            {
                ViewData["MensagemErro"] += "Erro ao listar Marcas, tente novamente mais tarde ou entre em contato com o suporte técnico";
            }

            InspecaoVeiculoVM.InspVeiculo_ID = 0;
            #endregion

            return View("Veiculo", InspecaoVeiculoVM);
        }


        /// <summary>
        /// Inserir Veiculo - IActionResult - Controller.cs
        /// </summary>
        /// <param name="VeiculoViewModel">Veiculo Informado</param>
        /// <param name="tipoBotao">Tipo de requisição</param>
        /// <returns>RedirectToAction NovaInspecao, Controller "Home"</returns>
        [HttpPost]
        public IActionResult InserirVeiculo(ViewModels.InspecaoVeiculoViewModel VeiculoViewModel, int tipoBotao)
        {
            StringBuilder sbLog = new StringBuilder("Action acionada: InserirVeículo - Parametros", 350);
            sbLog.Append($" | Inspecao_ID: { VeiculoViewModel.Inspecao_ID}");
            sbLog.Append($" | VIN_6: { VeiculoViewModel.VIN_6}");
            sbLog.Append($" | Marca: { VeiculoViewModel.Marca_ID}");
            sbLog.Append($" | Modelo { VeiculoViewModel.Modelo_ID}");
            sbLog.Append($" | Modelo { VeiculoViewModel.Modelo_ID}");
            sbLog.Append($" | Observações { VeiculoViewModel.Observacoes}");

            Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = Convert.ToString(sbLog) });

            //Verifica dados do usuário
            ViewModels.LoginViewModel dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);
            if (dadosUsuario == null)
            {
                Diag.Log.Grava(new Diag.LogItem() { Nivel = Diag.Nivel.Informacao, Mensagem = $"Parametros: Nome:  {dadosUsuario.Nome} |  Autenticado: {dadosUsuario.Autenticado}" });
                return RedirectToAction("Index", "Home");
            }

            //Recebe os dados da View
            VeiculoViewModel.InspVeiculo = new Models.InspVeiculo
            {
                Inspecao_ID = VeiculoViewModel.Inspecao_ID,
                Marca_ID = VeiculoViewModel.Marca_ID,
                Modelo_ID = VeiculoViewModel.Modelo_ID,
                VIN_6 = VeiculoViewModel.VIN_6.RemoveEspacosUpperKey(),
                VIN = null,
                InspVeiculo_ID = VeiculoViewModel.InspVeiculo_ID, //recebe no postback
                Observacoes = VeiculoViewModel.Observacoes,
                Erro = false,
                MensagemErro = ""
            };

            VeiculoViewModel.Inspecao = BLL.Inspecao.ListarPorId(VeiculoViewModel.Inspecao_ID, Configuracao);

            if (VeiculoViewModel.Inspecao.Erro == true)
            {
                ViewData["MensagemErro"] = VeiculoViewModel.Inspecao.MensagemErro;
            }

            //Verifica se está em edição 
            if (VeiculoViewModel.Edicao == 1)
            {

                //Verifica o veículo no banco de dados
                var veiculoBD = BLL.InspecaoVeiculo.ListarPorId(VeiculoViewModel.InspVeiculo_ID, Configuracao);

                //Verifica se o chassi é diferente.
                if (veiculoBD.VIN_6 == VeiculoViewModel.VIN_6)
                {
                    //se forem iguais não há problemas, realiza update
                    VeiculoViewModel.InspVeiculo = BLL.InspecaoVeiculo.Update(VeiculoViewModel.InspVeiculo, Configuracao);
                }

                //Caso seja diferente, verifica se já existe o novo chassi no banco de dados;
                //Se já existir, proibir o update.
                else
                {
                    var existeID = BLL.InspecaoVeiculo.Existe(VeiculoViewModel.Inspecao_ID, VeiculoViewModel.VIN_6, Configuracao);

                    if (existeID > 0)
                    {
                        ViewData["MensagemErro"] = $"Não é possível inserir o veículo informado, veículo já encontra-se registrado nesta inspeção, veículo id: {existeID}";

                        VeiculoViewModel.InspVeiculo.Erro = true;

                        //Recebe dados novamente para retornar a página de inserção de veículos
                        VeiculoViewModel.Inspecao = BLL.Inspecao.ListarPorId(VeiculoViewModel.InspVeiculo.Inspecao_ID, Configuracao);

                        VeiculoViewModel.InspVeiculo = VeiculoViewModel.InspVeiculo;

                        VeiculoViewModel.Marca = BLL.InspecaoVeiculo.ListaMarca(VeiculoViewModel.Inspecao.Cliente_ID, Configuracao);

                        VeiculoViewModel.Modelo = BLL.InspecaoVeiculo.ListaModelo(VeiculoViewModel.Inspecao.Cliente_ID, Configuracao);

                        return View("Veiculo", VeiculoViewModel);

                    }
                    else
                    {
                        VeiculoViewModel.InspVeiculo = BLL.InspecaoVeiculo.Update(VeiculoViewModel.InspVeiculo, Configuracao);
                    }
                }


                if (VeiculoViewModel.InspVeiculo.Erro == true)
                #region EM_ERRO
                {
                    VeiculoViewModel.InspVeiculo.MensagemErro = VeiculoViewModel.InspVeiculo.MensagemErro;
                    ViewData["MensagemErro"] = VeiculoViewModel.InspVeiculo.MensagemErro;

                    //preenche dados para retornar para view;
                    ViewModels.InspecaoVeiculoViewModel InspecaoVeiculoVM = new ViewModels.InspecaoVeiculoViewModel();
                    InspecaoVeiculoVM.Inspecao = BLL.Inspecao.ListarPorId(VeiculoViewModel.InspVeiculo.Inspecao_ID, Configuracao);
                    InspecaoVeiculoVM.Marca = BLL.InspecaoVeiculo.ListaMarca(InspecaoVeiculoVM.Inspecao.Cliente_ID, Configuracao);
                    InspecaoVeiculoVM.Modelo = BLL.InspecaoVeiculo.ListaModelo(InspecaoVeiculoVM.Inspecao.Cliente_ID, Configuracao);

                    if (InspecaoVeiculoVM.Inspecao.Erro == true)
                    {
                        ViewData["MensagemErro"] += "  - Erro ao listar informações da inspeção";
                    }

                    if (InspecaoVeiculoVM.Marca.FirstOrDefault().Text == "ERRO")
                    {
                        ViewData["MensagemErro"] += "  - Erro ao listar Marcas";
                    }

                    if (InspecaoVeiculoVM.Modelo.FirstOrDefault().Text == "ERRO")
                    {
                        ViewData["MensagemErro"] += "   - Erro ao listar Modelos";
                    }

                    InspecaoVeiculoVM.Edicao = 1;
                    InspecaoVeiculoVM.InspVeiculo_ID = VeiculoViewModel.InspVeiculo.InspVeiculo_ID;
                    InspecaoVeiculoVM.VIN_6 = VeiculoViewModel.InspVeiculo.VIN_6;
                    InspecaoVeiculoVM.Marca_ID = VeiculoViewModel.InspVeiculo.Marca_ID;
                    InspecaoVeiculoVM.Modelo_ID = VeiculoViewModel.InspVeiculo.Modelo_ID;
                    InspecaoVeiculoVM.Observacoes = VeiculoViewModel.InspVeiculo.Observacoes;
                    return View("Veiculo", InspecaoVeiculoVM);
                }
                #endregion

                else if (VeiculoViewModel.InspVeiculo.Erro == false)
                {
                    ViewData["MensagemSucesso"] += "Dados do veículo atualizados com sucesso: " + VeiculoViewModel.InspVeiculo.VIN_6;
                    VeiculoViewModel.Edicao = 0;
                }
            }

            else //Insere
            {
                //Verifica se já existe o chassi
                VeiculoViewModel.InspVeiculo.InspVeiculo_ID = BLL.InspecaoVeiculo.Existe(VeiculoViewModel.InspVeiculo.Inspecao_ID, VeiculoViewModel.InspVeiculo.VIN_6, Configuracao);

                if (VeiculoViewModel.InspVeiculo.InspVeiculo_ID > 0)
                {
                    ViewData["MensagemErro"] = $"Não é possível inserir o veículo informado, veículo já encontra-se registrado nesta inspeção, veículo id: {VeiculoViewModel.InspVeiculo.InspVeiculo_ID}";

                    VeiculoViewModel.InspVeiculo.Erro = true;

                    //Recebe dados novamente para retornar a página de inserção de veículos
                    VeiculoViewModel.Inspecao = BLL.Inspecao.ListarPorId(VeiculoViewModel.InspVeiculo.Inspecao_ID, Configuracao);

                    VeiculoViewModel.InspVeiculo = VeiculoViewModel.InspVeiculo;

                    VeiculoViewModel.Marca = BLL.InspecaoVeiculo.ListaMarca(VeiculoViewModel.Inspecao.Cliente_ID, Configuracao);

                    VeiculoViewModel.Modelo = BLL.InspecaoVeiculo.ListaModelo(VeiculoViewModel.Inspecao.Cliente_ID, Configuracao);

                    return View("Veiculo", VeiculoViewModel);
                }

                //Caso não existe este chassi nesta inspeção, realiza a inserção
                else if (VeiculoViewModel.InspVeiculo.InspVeiculo_ID == 0)
                {
                    VeiculoViewModel.InspVeiculo = BLL.InspecaoVeiculo.Inserir(VeiculoViewModel.InspVeiculo, Configuracao);

                    if (VeiculoViewModel.InspVeiculo.Erro == true)
                    {
                        ViewData["MensagemErro"] += " Erro ao inserir dados do veículo, tente novamente mais tarde ou entre em contato com o suporte técnico";

                        VeiculoViewModel.InspVeiculo.Erro = true;

                        VeiculoViewModel.Marca = BLL.InspecaoVeiculo.ListaMarca(VeiculoViewModel.Inspecao.Cliente_ID, Configuracao);

                        VeiculoViewModel.Modelo = BLL.InspecaoVeiculo.ListaMarca(VeiculoViewModel.Inspecao.Cliente_ID, Configuracao);

                        return View("Veiculo", VeiculoViewModel);
                    }

                    else
                    {
                        ViewData["MensagemSucesso"] = "Veículo gravado com sucesso: " + VeiculoViewModel.InspVeiculo.VIN_6;
                    }
                }

                else if (VeiculoViewModel.InspVeiculo.InspVeiculo_ID == -1) //-1 (erro ao executar Existe)  | 0  - não existe  | >0 - já existe
                {
                    #region EM_ERRO
                    ViewData["MensagemErro"] += " Erro ao verificar se existe o chassi informado, por favor tente novamente mais tarde ou entre em contato com o suporte técnico";
                    VeiculoViewModel.Inspecao = BLL.Inspecao.ListarPorId(VeiculoViewModel.InspVeiculo.Inspecao_ID, Configuracao);
                    VeiculoViewModel.InspVeiculo = VeiculoViewModel.InspVeiculo;
                    VeiculoViewModel.InspVeiculo.Erro = true;
                    VeiculoViewModel.Marca = BLL.InspecaoVeiculo.ListaMarca(VeiculoViewModel.Inspecao.Cliente_ID, Configuracao);
                    VeiculoViewModel.Modelo = BLL.InspecaoVeiculo.ListaMarca(VeiculoViewModel.Inspecao.Cliente_ID, Configuracao);
                    return View("Veiculo", VeiculoViewModel);
                    #endregion
                }
            }

            // 1 - Apenas Registrar e continuar na mesma tela
            if (tipoBotao == 1)
            {
                VeiculoViewModel.Marca = BLL.InspecaoVeiculo.ListaMarca(VeiculoViewModel.Inspecao.Cliente_ID, Configuracao);
                VeiculoViewModel.Modelo = BLL.InspecaoVeiculo.ListaModelo(VeiculoViewModel.Inspecao.Cliente_ID, Configuracao);

                #region EM_ERRO
                if (VeiculoViewModel.Marca.FirstOrDefault().Text == "ERRO")
                {
                    ViewData["MensagemErro"] += "Erro ao listar Marcas, tente novamente mais tarde ou entre em contato com o suporte técnico";
                }

                if (VeiculoViewModel.Modelo.FirstOrDefault().Text == "ERRO")
                {
                    ViewData["MensagemErro"] += "Erro ao listar Modelos, tente novamente mais tarde ou entre em contato com o suporte técnico";
                }
                #endregion

                VeiculoViewModel.InspVeiculo_ID = 0;
                VeiculoViewModel.VIN_6 = "";
                VeiculoViewModel.Observacoes = "";
                VeiculoViewModel.Marca_ID = 0;
                VeiculoViewModel.Modelo_ID = 0;

                return View("Veiculo", VeiculoViewModel);
            }

            // 2- Registrar Avarias
            else if (tipoBotao == 2)
            {
                //preenche valores próxima view [RegistrarAvarias]
                InspecaoVeiculoRegistrarAvariasVM RegistrarAvariasViewModel = new InspecaoVeiculoRegistrarAvariasVM();
                RegistrarAvariasViewModel.dadosUsuario = dadosUsuario;
                RegistrarAvariasViewModel.Inspecao = VeiculoViewModel.Inspecao;
                RegistrarAvariasViewModel.InspVeiculo = VeiculoViewModel.InspVeiculo;

                //recebe listas
                RegistrarAvariasViewModel.avAreaLista = BLL.Avarias.ListarAreas(RegistrarAvariasViewModel.Inspecao.Cliente_ID, Configuracao);
                RegistrarAvariasViewModel.avCondicaoLista = BLL.Avarias.ListarCondicoes(RegistrarAvariasViewModel.Inspecao.Cliente_ID, Configuracao);
                RegistrarAvariasViewModel.avDanoRepositorioLista = BLL.Avarias.ListarDanos(RegistrarAvariasViewModel.Inspecao.Cliente_ID, Configuracao);
                RegistrarAvariasViewModel.avGravidadeLista = BLL.Avarias.ListarGravidades(RegistrarAvariasViewModel.Inspecao.Cliente_ID, Configuracao);
                RegistrarAvariasViewModel.avQuadranteLista = BLL.Avarias.ListarQuadrantes(RegistrarAvariasViewModel.Inspecao.Cliente_ID, Configuracao);
                RegistrarAvariasViewModel.avSeveridadeLista = BLL.Avarias.ListarSeveridades(RegistrarAvariasViewModel.Inspecao.Cliente_ID, Configuracao);

                #region EM_ERRO
                if (RegistrarAvariasViewModel.avAreaLista.Count() > 0)
                {
                    if (RegistrarAvariasViewModel.avAreaLista.FirstOrDefault().Erro == true)
                    {
                        ViewData["MensagemErro"] = RegistrarAvariasViewModel.avAreaLista.FirstOrDefault().MensagemErro;
                    }
                }
                else
                {
                    ViewData["MensagemErro"] = "Não há áreas para este cliente, por favor entre em contato com o suporte técnico";
                }


                if (RegistrarAvariasViewModel.avCondicaoLista.Count() > 0)
                {
                    if (RegistrarAvariasViewModel.avCondicaoLista.FirstOrDefault().Erro == true)
                    {
                        ViewData["MensagemErro"] = RegistrarAvariasViewModel.avCondicaoLista.FirstOrDefault().MensagemErro;
                    }
                }
                else
                {
                    ViewData["MensagemErro"] = "Não há condicoes para este cliente, por favor entre em contato com o suporte técnico";
                }

                if (RegistrarAvariasViewModel.avDanoRepositorioLista.Count() > 0)
                {
                    if (RegistrarAvariasViewModel.avDanoRepositorioLista.FirstOrDefault().Erro == true)
                    {
                        ViewData["MensagemErro"] = "Erro ao listar Danos, tente novamente mais tarde ou entre em contato com o suporte técnico";
                    }
                }
                else
                {
                    ViewData["MensagemErro"] = "Não há Danos para este cliente, por favor entre em contato com o suporte técnico";
                }


                if (RegistrarAvariasViewModel.avGravidadeLista.Count() > 0)
                {
                    if (RegistrarAvariasViewModel.avGravidadeLista.FirstOrDefault().Erro == true)
                    {
                        ViewData["MensagemErro"] = "Erro ao listar Gravidades, tente novamente mais tarde ou entre em contato com o suporte técnico";
                    }
                }
                else
                {
                    ViewData["MensagemErro"] = "Não há Gravidades para este cliente, por favor entre em contato com o suporte técnico";
                }

                if (RegistrarAvariasViewModel.avQuadranteLista.Count() > 0)
                {
                    if (RegistrarAvariasViewModel.avQuadranteLista.FirstOrDefault().Erro == true)
                    {
                        ViewData["MensagemErro"] = "Erro ao listar Quadrantes, tente novamente mais tarde ou entre em contato com o suporte técnico";
                    }
                }
                else
                {
                    ViewData["MensagemErro"] = "Não há Quadrantes para este cliente, por favor entre em contato com o suporte técnico";
                }

                if (RegistrarAvariasViewModel.avSeveridadeLista.Count() > 0)
                {
                    if (RegistrarAvariasViewModel.avSeveridadeLista.FirstOrDefault().Erro == true)
                    {
                        ViewData["MensagemErro"] = "Erro ao listar Severidades, tente novamente mais tarde ou entre em contato com o suporte técnico";
                    }
                }
                else
                {
                    ViewData["MensagemErro"] = "Não há Severidades para este cliente, por favor entre em contato com o suporte técnico";
                }
                #endregion

                RegistrarAvariasViewModel.listaAvarias = BLL.Avarias.Listar(RegistrarAvariasViewModel.Inspecao.Cliente_ID, VeiculoViewModel.VIN_6, Configuracao);

                #region EM_ERRO
                if (RegistrarAvariasViewModel.listaAvarias.Count > 0)
                {
                    if (RegistrarAvariasViewModel.listaAvarias.FirstOrDefault().Erro == true)
                    {
                        ViewData["MensagemErro"] = "Erro ao receber lista de avarias deste veículo, tente novamente mais tarde ou entre em contato com o suporte técnico";
                    }
                }
                #endregion

                return View("RegistrarAvarias", RegistrarAvariasViewModel);
            };
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Insere avaria no banco de dados
        /// </summary>
        /// <param name="registrarAvariasViewModel">ViewModel contendo informações pertinentes a avaria</param>
        /// <param name="tipoBotao"></param>
        /// <param name="files">Arquivos contendo as fotos da avaria</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult InserirAvaria(InspecaoVeiculoRegistrarAvariasVM registrarAvariasViewModel, int tipoBotao, ICollection<IFormFile> files)
        {
            StringBuilder sbLog = new StringBuilder("Action acionada: InserirAvaria - Parametros", 250);
            sbLog.Append($"  | InspecaoID: {registrarAvariasViewModel.Inspecao_ID}");
            sbLog.Append($"  | InspVeiculo_ID: {registrarAvariasViewModel.InspVeiculo_ID}");
            sbLog.Append($"  | Area_ID: {registrarAvariasViewModel.Area_ID}");
            sbLog.Append($"  | Condicao_ID: {registrarAvariasViewModel.Condicao_ID}");
            sbLog.Append($"  | Dano_ID: {registrarAvariasViewModel.Dano_ID}");
            sbLog.Append($"  | Gravidade_ID: {registrarAvariasViewModel.Gravidade_ID}");
            sbLog.Append($"  | Quadrante_ID: {registrarAvariasViewModel.Quadrante_ID}");
            sbLog.Append($"  | Severidade_ID: {registrarAvariasViewModel.Severidade_ID}");
            sbLog.Append($"  | FabricaTransporte: {registrarAvariasViewModel.Fabricatransporte}");

            Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = Convert.ToString(sbLog) });

            //verifica dados usuário
            #region dadosusuario
            ViewModels.LoginViewModel dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);
            if (dadosUsuario == null)
            {
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"Parametros: Nome:  {dadosUsuario.Nome} |  Autenticado: {dadosUsuario.Autenticado}" });
                return RedirectToAction("Index", "Home");
            }
            #endregion

            //recebe dados da View
            registrarAvariasViewModel.InspAvaria = new Models.InspAvaria
            {
                Inspecao_ID = registrarAvariasViewModel.Inspecao_ID,
                InspVeiculo_ID = registrarAvariasViewModel.InspVeiculo_ID,
                AvArea_ID = registrarAvariasViewModel.Area_ID,
                AvCondicao_ID = registrarAvariasViewModel.Condicao_ID,
                AvDano_ID = registrarAvariasViewModel.Dano_ID,
                AvGravidade_ID = registrarAvariasViewModel.Gravidade_ID,
                AvQuadrante_ID = registrarAvariasViewModel.Quadrante_ID,
                AvSeveridade_ID = registrarAvariasViewModel.Severidade_ID,
                FabricaTransporte = registrarAvariasViewModel.Fabricatransporte,
                DanoOrigem = false
            };

            registrarAvariasViewModel.Inspecao = BLL.Inspecao.ListarPorId(registrarAvariasViewModel.InspAvaria.Inspecao_ID, Configuracao);

            if (registrarAvariasViewModel.Inspecao.Erro == true)
            #region EM_ERRO
            {
                ViewData["MensagemErro"] += "Erro receber dados da inspeção, tente novamente mais tarde ou entre em contato com o suporte técnico";
                //interromper continuação. Não vai conseguir inserir avaria ??? TODO
            }
            #endregion


            //Insere Avaria
            registrarAvariasViewModel.InspAvaria = BLL.Avarias.Inserir(registrarAvariasViewModel.InspAvaria, Configuracao);

            if (registrarAvariasViewModel.InspAvaria.Erro == true)
            {
                ViewData["MensagemErro"] += "Erro ao registrar avaria, tente novamente mais tarde ou entre em contato com o suporte técnico";
            }
            else
            {
                ViewData["MensagemSucesso"] = "Avaria registrada com sucesso";

                //Faz Upload das imagens que o usuário inseriu
                bool realizouUpload = BLL.UploadImagens.UploadImagensAvaria(registrarAvariasViewModel.InspAvaria.InspAvaria_ID, files, Configuracao);
                if (realizouUpload == false)
                {
                    ViewData["MensagemErro"] += "Erro ao inserir fotos no sistema, tente novamente mais tarde ou entre em contato com o suporte técnico";
                }
            }

            registrarAvariasViewModel.dadosUsuario = dadosUsuario;
            registrarAvariasViewModel.InspVeiculo = BLL.InspecaoVeiculo.ListarPorId(registrarAvariasViewModel.InspVeiculo_ID, Configuracao);

            //Preenche listas
            registrarAvariasViewModel.avAreaLista = BLL.Avarias.ListarAreas(registrarAvariasViewModel.Inspecao.Cliente_ID, Configuracao);
            registrarAvariasViewModel.avCondicaoLista = BLL.Avarias.ListarCondicoes(registrarAvariasViewModel.Inspecao.Cliente_ID, Configuracao);
            registrarAvariasViewModel.avDanoRepositorioLista = BLL.Avarias.ListarDanos(registrarAvariasViewModel.Inspecao.Cliente_ID, Configuracao);
            registrarAvariasViewModel.avGravidadeLista = BLL.Avarias.ListarGravidades(registrarAvariasViewModel.Inspecao.Cliente_ID, Configuracao);
            registrarAvariasViewModel.avQuadranteLista = BLL.Avarias.ListarQuadrantes(registrarAvariasViewModel.Inspecao.Cliente_ID, Configuracao);
            registrarAvariasViewModel.avSeveridadeLista = BLL.Avarias.ListarSeveridades(registrarAvariasViewModel.Inspecao.Cliente_ID, Configuracao);

            #region EM_ERRO
            if (registrarAvariasViewModel.avAreaLista.FirstOrDefault().Erro == true)
            {
                ViewData["MensagemErro"] = "Erro ao listar Areas, tente novamente mais tarde ou entre em contato com o suporte técnico";
            }

            if (registrarAvariasViewModel.avCondicaoLista.FirstOrDefault().Erro == true)
            {
                ViewData["MensagemErro"] = "Erro ao listar Condicoes, tente novamente mais tarde ou entre em contato com o suporte técnico";
            }

            if (registrarAvariasViewModel.avDanoRepositorioLista.FirstOrDefault().Erro == true)
            {
                ViewData["MensagemErro"] = "Erro ao listar Danos, tente novamente mais tarde ou entre em contato com o suporte técnico";
            }

            if (registrarAvariasViewModel.avGravidadeLista.FirstOrDefault().Erro == true)
            {
                ViewData["MensagemErro"] = "Erro ao listar Gravidades, tente novamente mais tarde ou entre em contato com o suporte técnico";
            }


            if (registrarAvariasViewModel.avQuadranteLista.FirstOrDefault().Erro == true)
            {
                ViewData["MensagemErro"] = "Erro ao listar Quadrantes, tente novamente mais tarde ou entre em contato com o suporte técnico";
            }

            if (registrarAvariasViewModel.avSeveridadeLista.FirstOrDefault().Erro == true)
            {
                ViewData["MensagemErro"] = "Erro ao listar Severidades, tente novamente mais tarde ou entre em contato com o suporte técnico";
            }
            #endregion


            registrarAvariasViewModel.VIN_6 = registrarAvariasViewModel.InspVeiculo.VIN_6;

            registrarAvariasViewModel.listaAvarias = BLL.Avarias.Listar(registrarAvariasViewModel.Inspecao.Cliente_ID, registrarAvariasViewModel.VIN_6, Configuracao);

            if (registrarAvariasViewModel.listaAvarias.Count > 0)
            {
                if (registrarAvariasViewModel.listaAvarias.FirstOrDefault().Erro == true)
                {
                    ViewData["MensagemErro"] = "Erro ao listar avarias do veículo, tente novamente mais tarde ou entre em contato com o suporte técnico";
                }
            }

            return View("RegistrarAvarias", registrarAvariasViewModel);
        }

        /// <summary>
        /// Realiza visualização dos dados da avaria que foi clicada 
        /// </summary>
        /// <param name="VisualizarAvariasVM"></param>
        /// <param name="tipoBotao"></param>
        /// <returns>View("Avarias")</returns>
        [HttpPost]
        public IActionResult VisualizarAvarias(VisualizarAvariasViewModel VisualizarAvariasVM, int tipoBotao) //melhorar para pegar do viewmodel
        {
            StringBuilder sbLog = new StringBuilder("Action acionada: VisualizarAvarias - Parametros", 100);
            sbLog.Append($"  | Inspecao_ID: {VisualizarAvariasVM.Inspecao_ID}");
            sbLog.Append($"  | InspVeiculo_ID: {VisualizarAvariasVM.InspVeiculo_ID}");

            Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = Convert.ToString(sbLog) });

            //Dados Usuário
            ViewModels.LoginViewModel dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);

            if (dadosUsuario == null)
            {
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"Parametros: Nome:  {dadosUsuario.Nome} |  Autenticado: {dadosUsuario.Autenticado}" });
                return RedirectToAction("Index", "Home");
            }

            Models.Inspecao inspecao = new Models.Inspecao();

            Models.InspVeiculo inspVeiculo = new Models.InspVeiculo();

            inspecao = BLL.Inspecao.ListarPorId(VisualizarAvariasVM.Inspecao_ID, Configuracao);

            inspVeiculo = BLL.InspecaoVeiculo.ListarPorId(VisualizarAvariasVM.InspVeiculo_ID, Configuracao);

            VisualizarAvariasVM.Avarias = BLL.Avarias.Listar(inspecao.Cliente_ID, inspVeiculo.VIN_6, Configuracao);

            return View("Avarias", VisualizarAvariasVM);
        }


        /// <summary>
        /// Realiza Edição das avarias
        /// </summary>
        /// <param name="inspAvaria_ID_form_editar">ID da avaria</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditarAvarias(int inspAvaria_ID_form_editar)
        {

            Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"Action EditarAvarias acionada: Parametros: InspAvaria_ID {inspAvaria_ID_form_editar}" });

            InspecaoEditarAvariasViewModel EditarAvariasVM = new InspecaoEditarAvariasViewModel();

            EditarAvariasVM.InspAvaria = BLL.Avarias.ListarPorId(inspAvaria_ID_form_editar, Configuracao);

            EditarAvariasVM.InspVeiculo = BLL.InspecaoVeiculo.ListarPorId(EditarAvariasVM.InspAvaria.InspVeiculo_ID, Configuracao);

            EditarAvariasVM.Inspecao = BLL.Inspecao.ListarPorId(EditarAvariasVM.InspAvaria.Inspecao_ID, Configuracao);

            #region EM_ERRO
            if (EditarAvariasVM.InspAvaria != null)
            {

                if (EditarAvariasVM.InspAvaria.Erro == true)
                {
                    ViewData["MensagemErro"] = ViewData["MensagemErro"] = EditarAvariasVM.InspAvaria.MensagemErro;
                }
            }

            if (EditarAvariasVM.Inspecao != null)
            {
                if (EditarAvariasVM.Inspecao.Erro == true)
                {
                    ViewData["MensagemErro"] = "Erro ao listar dados da Inspeção, tente novamente mais tarde ou entre em contato com o suporte técnico";
                }
            }
            #endregion

            EditarAvariasVM.avAreaLista = BLL.Avarias.ListarAreas(EditarAvariasVM.Inspecao.Cliente_ID, Configuracao);
            EditarAvariasVM.avCondicaoLista = BLL.Avarias.ListarCondicoes(EditarAvariasVM.Inspecao.Cliente_ID, Configuracao);
            EditarAvariasVM.avDanoLista = BLL.Avarias.ListarDanos(EditarAvariasVM.Inspecao.Cliente_ID, Configuracao);
            EditarAvariasVM.avGravidadeLista = BLL.Avarias.ListarGravidades(EditarAvariasVM.Inspecao.Cliente_ID, Configuracao);
            EditarAvariasVM.avQuadranteLista = BLL.Avarias.ListarQuadrantes(EditarAvariasVM.Inspecao.Cliente_ID, Configuracao);
            EditarAvariasVM.avSeveridadeLista = BLL.Avarias.ListarSeveridades(EditarAvariasVM.Inspecao.Cliente_ID, Configuracao);

            #region EM_ERRO
            if (EditarAvariasVM.avAreaLista.FirstOrDefault().Erro == true)
            {
                ViewData["MensagemErro"] += EditarAvariasVM.avAreaLista.FirstOrDefault().MensagemErro;
            }

            if (EditarAvariasVM.avCondicaoLista.FirstOrDefault().Erro == true)
            {
                ViewData["MensagemErro"] += EditarAvariasVM.avCondicaoLista.FirstOrDefault().MensagemErro;
            }

            if (EditarAvariasVM.avDanoLista.FirstOrDefault().Erro == true)
            {
                ViewData["MensagemErro"] += EditarAvariasVM.avDanoLista.FirstOrDefault().MensagemErro;
            }

            if (EditarAvariasVM.avGravidadeLista.FirstOrDefault().Erro == true)
            {
                ViewData["MensagemErro"] += EditarAvariasVM.avGravidadeLista.FirstOrDefault().MensagemErro;
            }

            if (EditarAvariasVM.avQuadranteLista.FirstOrDefault().Erro == true)
            {
                ViewData["MensagemErro"] += EditarAvariasVM.avQuadranteLista.FirstOrDefault().MensagemErro;
            }

            if (EditarAvariasVM.avSeveridadeLista.FirstOrDefault().Erro == true)
            {
                ViewData["MensagemErro"] += EditarAvariasVM.avSeveridadeLista.FirstOrDefault().MensagemErro;
            }
            #endregion


            EditarAvariasVM.ImagemAvarias = BLL.UploadImagens.Listar(EditarAvariasVM.InspAvaria.InspAvaria_ID, Configuracao);
            if (EditarAvariasVM.ImagemAvarias.Count() > 0)
            {
                if (EditarAvariasVM.ImagemAvarias.FirstOrDefault().Erro == true)
                {
                    ViewData["MensagemErro"] = EditarAvariasVM.ImagemAvarias.FirstOrDefault().MensagemErro;
                }
            }

            return View(EditarAvariasVM);
        }


        /// <summary>
        /// Gravação da avaria no banco de dados - Contendo as fotos
        /// </summary>
        /// <param name="EditarAvariasVM">Dados da avaria</param>
        /// <param name="files">Fotos da avaria</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SalvarAvaria(InspecaoEditarAvariasViewModel EditarAvariasVM, ICollection<IFormFile> files)
        {

            #region gravalogInformacao
            try
            {
                StringBuilder sbLog = new StringBuilder("Action acionada: SalvarAvaria - Parametros", 150);
                sbLog.Append($"  | InspAvaria_ID: {EditarAvariasVM.InspAvaria_ID}");
                sbLog.Append($"  | Area_ID: {EditarAvariasVM.Area_ID}");
                sbLog.Append($"  | Condicao_ID: {EditarAvariasVM.Condicao_ID}");
                sbLog.Append($"  | Dano_ID: {EditarAvariasVM.Dano_ID}");
                sbLog.Append($"  | Gravidade_ID: {EditarAvariasVM.Gravidade_ID}");
                sbLog.Append($"  | Quadrante_ID: {EditarAvariasVM.Quadrante_ID}");
                sbLog.Append($"  | Severidade_ID: {EditarAvariasVM.Severidade_ID}");
                sbLog.Append($"  | FabricaTransporte: {EditarAvariasVM.Fabricatransporte}");

                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = Convert.ToString(sbLog) });
            }
            catch { }
            #endregion

            #region recebeDadosUsuario
            ViewModels.LoginViewModel dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);
            if (dadosUsuario == null)
            {
                return RedirectToAction("Index", "Home");
            }
            #endregion

            //recebe dados ViewModelo           
            EditarAvariasVM.InspAvaria = new Models.InspAvaria
            {
                InspAvaria_ID = EditarAvariasVM.InspAvaria_ID,
                AvArea_ID = EditarAvariasVM.Area_ID,
                AvCondicao_ID = EditarAvariasVM.Condicao_ID,
                AvDano_ID = EditarAvariasVM.Dano_ID,
                AvGravidade_ID = EditarAvariasVM.Gravidade_ID,
                AvQuadrante_ID = EditarAvariasVM.Quadrante_ID,
                AvSeveridade_ID = EditarAvariasVM.Severidade_ID,
                FabricaTransporte = EditarAvariasVM.Fabricatransporte,
                DanoOrigem = false
            };

            //Faz update dos dados da avaria
            EditarAvariasVM.InspAvaria = BLL.Avarias.Update(EditarAvariasVM.InspAvaria, Configuracao);
            if (EditarAvariasVM.InspAvaria.Erro == true)
            {
                ViewData["MensagemErro"] = EditarAvariasVM.InspAvaria.MensagemErro;
            }
            else
            {
                ViewData["MensagemSucesso"] = "Avaria atualizada com sucesso.";
            }

            //Insere a nova imagem referente a avaria
            bool uploadImagem = BLL.UploadImagens.UploadImagensAvaria(EditarAvariasVM.InspAvaria_ID, files, Configuracao);
            if (uploadImagem == false)
            {
                ViewData["MensagemErro"] = "Erro ao relaizar upload de imagens da avaria";
            }
            InspecaoVeiculoRegistrarAvariasVM registrarAvariasViewModel = new InspecaoVeiculoRegistrarAvariasVM();

            //Carrega os dados da proxima view.
            registrarAvariasViewModel.dadosUsuario = dadosUsuario;

            //carrega dados Avaria
            registrarAvariasViewModel.InspAvaria = BLL.Avarias.ListarPorId(EditarAvariasVM.InspAvaria_ID, Configuracao);

            //carrega dados do Veículo
            registrarAvariasViewModel.InspVeiculo = BLL.InspecaoVeiculo.ListarPorId(registrarAvariasViewModel.InspAvaria.InspVeiculo_ID, Configuracao);

            //carrega dados da Inspeção
            registrarAvariasViewModel.Inspecao = BLL.Inspecao.ListarPorId(registrarAvariasViewModel.InspAvaria.Inspecao_ID, Configuracao);

            //Lista
            registrarAvariasViewModel.avAreaLista = BLL.Avarias.ListarAreas(registrarAvariasViewModel.Inspecao.Cliente_ID, Configuracao);
            registrarAvariasViewModel.avCondicaoLista = BLL.Avarias.ListarCondicoes(registrarAvariasViewModel.Inspecao.Cliente_ID, Configuracao);
            registrarAvariasViewModel.avDanoRepositorioLista = BLL.Avarias.ListarDanos(registrarAvariasViewModel.Inspecao.Cliente_ID, Configuracao);
            registrarAvariasViewModel.avGravidadeLista = BLL.Avarias.ListarGravidades(registrarAvariasViewModel.Inspecao.Cliente_ID, Configuracao);
            registrarAvariasViewModel.avQuadranteLista = BLL.Avarias.ListarQuadrantes(registrarAvariasViewModel.Inspecao.Cliente_ID, Configuracao);
            registrarAvariasViewModel.avSeveridadeLista = BLL.Avarias.ListarSeveridades(registrarAvariasViewModel.Inspecao.Cliente_ID, Configuracao);

            #region EM_ERRO
            if (registrarAvariasViewModel.InspAvaria.Erro == true)
            {
                ViewData["MensagemErro"] = registrarAvariasViewModel.InspAvaria.MensagemErro;
            }

            if (registrarAvariasViewModel.Inspecao.Erro == true)
            {
                ViewData["MensagemErro"] = registrarAvariasViewModel.Inspecao.MensagemErro;
            }

            if (registrarAvariasViewModel.InspVeiculo.Erro == true)
            {
                ViewData["MensagemErro"] = registrarAvariasViewModel.InspVeiculo.MensagemErro;
            }

            if (registrarAvariasViewModel.avAreaLista.FirstOrDefault().Erro == true)
            {
                ViewData["MensagemErro"] = registrarAvariasViewModel.avAreaLista.FirstOrDefault().MensagemErro;
            }

            if (registrarAvariasViewModel.avCondicaoLista.FirstOrDefault().Erro == true)
            {
                ViewData["MensagemErro"] = registrarAvariasViewModel.avCondicaoLista.FirstOrDefault().MensagemErro;
            }

            if (registrarAvariasViewModel.avDanoRepositorioLista.FirstOrDefault().Erro == true)
            {
                ViewData["MensagemErro"] = registrarAvariasViewModel.avDanoRepositorioLista.FirstOrDefault().MensagemErro;
            }
            if (registrarAvariasViewModel.avGravidadeLista.FirstOrDefault().Erro == true)
            {
                ViewData["MensagemErro"] = registrarAvariasViewModel.avGravidadeLista.FirstOrDefault().MensagemErro;
            }
            if (registrarAvariasViewModel.avQuadranteLista.FirstOrDefault().Erro == true)
            {
                ViewData["MensagemErro"] = registrarAvariasViewModel.avQuadranteLista.FirstOrDefault().MensagemErro;
            }
            if (registrarAvariasViewModel.avSeveridadeLista.FirstOrDefault().Erro == true)
            {
                ViewData["MensagemErro"] = registrarAvariasViewModel.avSeveridadeLista.FirstOrDefault().MensagemErro;
            }
            #endregion

            //RECEBER UM VIN_6, dependendo da minha inspecao e meu InspecaoVeiculo_ID
            registrarAvariasViewModel.VIN_6 = registrarAvariasViewModel.InspVeiculo.VIN_6;

            registrarAvariasViewModel.UltimoVeiculo_InspVeiculo_ID = registrarAvariasViewModel.InspVeiculo.InspVeiculo_ID;
            registrarAvariasViewModel.listaAvarias = BLL.Avarias.Listar(registrarAvariasViewModel.Inspecao.Cliente_ID, registrarAvariasViewModel.VIN_6, Configuracao);
            if (registrarAvariasViewModel.listaAvarias.FirstOrDefault().Erro == true)
            {
                ViewData["MensagemErro"] = registrarAvariasViewModel.listaAvarias.FirstOrDefault().MensagemErro;
            }
            return View("RegistrarAvarias", registrarAvariasViewModel);
        }


        /// <summary>
        /// Edição de veículos / Botão Editar Veículo
        /// </summary>
        /// <param name="inspecaoVeiculoVM">Dados do veículo via ViewModel</param>
        /// <param name="tipobotao"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditarVeiculo(InspecaoVeiculoViewModel inspecaoVeiculoVM, int tipobotao)
        {

            Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"Action acionada: EditarVeiculo | Parametros: Edição: {inspecaoVeiculoVM.Edicao} | Veículo_ID {inspecaoVeiculoVM.Inspecao_ID}" });

            ViewModels.LoginViewModel dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);

            if (dadosUsuario == null)
            {
                ViewData["MensagemErro"] = "Por favor, faça login novamente, sua sessão expirou";
                return RedirectToAction("Index", "Home");
            }

            try
            {
                inspecaoVeiculoVM.Marca = BLL.InspecaoVeiculo.ListaMarca(inspecaoVeiculoVM.Inspecao.Cliente_ID, Configuracao);
                inspecaoVeiculoVM.Modelo = BLL.InspecaoVeiculo.ListaModelo(inspecaoVeiculoVM.Inspecao.Cliente_ID, Configuracao);
            }
            catch { }

            inspecaoVeiculoVM.Edicao = 1;

            inspecaoVeiculoVM.Inspecao = BLL.Inspecao.ListarPorId(inspecaoVeiculoVM.Inspecao_ID, Configuracao);

            if (inspecaoVeiculoVM.Inspecao.Erro == true)
            {
                ViewData["MensagemErro"] = "Não foi possível encontrar inspeção informada, tente novamente mais tarde";
                return View("Veiculo", inspecaoVeiculoVM);
            }


            if (inspecaoVeiculoVM.InspVeiculo_ID == 0)
            {
                ViewData["MensagemErro"] = "Nenhum veículo disponível para edição, tente novamente mais tarde ou entre em contato com o suporte técnico";
                return View("Veiculo", inspecaoVeiculoVM);
            }

            inspecaoVeiculoVM.InspVeiculo = BLL.InspecaoVeiculo.ListarPorId(inspecaoVeiculoVM.InspVeiculo_ID, Configuracao);

            if (inspecaoVeiculoVM.InspVeiculo.Erro == true)
            {
                ViewData["MensagemErro"] = inspecaoVeiculoVM.InspVeiculo.MensagemErro;
                inspecaoVeiculoVM.VIN_6 = "Erro";
                inspecaoVeiculoVM.Observacoes = "";
                inspecaoVeiculoVM.Marca_ID = 0;
                inspecaoVeiculoVM.Modelo_ID = 0;

            }
            else
            {
                inspecaoVeiculoVM.VIN_6 = inspecaoVeiculoVM.InspVeiculo.VIN_6;
                inspecaoVeiculoVM.Observacoes = inspecaoVeiculoVM.InspVeiculo.Observacoes;
                inspecaoVeiculoVM.Marca_ID = inspecaoVeiculoVM.InspVeiculo.Marca_ID;
                inspecaoVeiculoVM.Modelo_ID = inspecaoVeiculoVM.InspVeiculo.Modelo_ID;
            }

            inspecaoVeiculoVM.Marca = BLL.InspecaoVeiculo.ListaMarca(inspecaoVeiculoVM.Inspecao.Cliente_ID, Configuracao);
            inspecaoVeiculoVM.Modelo = BLL.InspecaoVeiculo.ListaModelo(inspecaoVeiculoVM.Inspecao.Cliente_ID, Configuracao);

            return View("Veiculo", inspecaoVeiculoVM);
        }


        /// <summary>
        /// Editar Inspeção
        /// </summary>
        /// <param name="inspecaoDadosGeraisVM"></param>
        /// <param name="tipobotao"></param>
        /// <returns>View ("NovaInspecao")</returns>
        [HttpPost]
        public IActionResult EditarInspecao(InspecaoDadosGeraisViewModel inspecaoDadosGeraisVM, int tipobotao)
        {

            string _mensagemLogin = "Erro ao validar dados do usuário, por favor faça login novamente";

            #region gravalogInformacao
            try
            {
                StringBuilder sbLog = new StringBuilder("Action acionada: EditarInspecao - Parametros", 250);
                sbLog.Append($"  | InspecaoID: {inspecaoDadosGeraisVM.Inspecao_ID}");
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = Convert.ToString(sbLog) });
            }
            catch { }
            #endregion

            #region recebeDadosUsuario
            ViewModels.LoginViewModel dadosUsuario = new ViewModels.LoginViewModel();
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

            //Preenche dados proxima view            
            inspecaoDadosGeraisVM.ListaCliente = BLL.Inspecao.ListarClientes(dadosUsuario.UsuarioId, Configuracao);
            inspecaoDadosGeraisVM.ListaLocalInspecao = BLL.Inspecao.ListarLocaisInspecao(dadosUsuario.UsuarioId, Configuracao, dadosUsuario.Usuario.Locais);
            inspecaoDadosGeraisVM.ListaLocalCheckPoint = BLL.Inspecao.ListarLocalCheckPoint(dadosUsuario.UsuarioId, Configuracao);
            inspecaoDadosGeraisVM.ListaTransportador = BLL.Inspecao.ListarTransportadores(dadosUsuario.UsuarioId, Configuracao);

            inspecaoDadosGeraisVM.Edicao = 1;

            //Erro ao receber dados da inspeção
            if (inspecaoDadosGeraisVM.Inspecao_ID == 0)
            {
                ViewData["MensagemErro"] = "Erro ao processar informação, tente novamente mais tarde ou entre em contato com o suporte";
                return View("NovaInspecao", inspecaoDadosGeraisVM);
            }


            inspecaoDadosGeraisVM.Inspecao = BLL.Inspecao.ListarPorId(inspecaoDadosGeraisVM.Inspecao_ID, Configuracao);

            //Erro ao receber dados da inspeção
            if (inspecaoDadosGeraisVM.Inspecao.Erro == true)
            {
                ViewData["MensagemErro"] = inspecaoDadosGeraisVM.Inspecao.MensagemErro;
                return View("NovaInspecao", inspecaoDadosGeraisVM);
            }

            //Modificar isso para fazer o model binding correto. [debug]
            inspecaoDadosGeraisVM.Data = inspecaoDadosGeraisVM.Inspecao.Data;
            inspecaoDadosGeraisVM.Inspecao_ID = inspecaoDadosGeraisVM.Inspecao.Inspecao_ID;
            inspecaoDadosGeraisVM.Cliente_ID = inspecaoDadosGeraisVM.Inspecao.Cliente_ID;
            inspecaoDadosGeraisVM.LocalInspecao_ID = inspecaoDadosGeraisVM.Inspecao.LocalInspecao_ID;
            inspecaoDadosGeraisVM.LocalCheckPoint_ID = inspecaoDadosGeraisVM.Inspecao.LocalCheckPoint_ID;
            inspecaoDadosGeraisVM.Transportador_ID = inspecaoDadosGeraisVM.Inspecao.Transportador_ID;

            inspecaoDadosGeraisVM.Transportador = BLL.Inspecao.ListarTransportadorPorId(inspecaoDadosGeraisVM.Inspecao.Transportador_ID, Configuracao);

            //Erro ao listar Transportador
            if (inspecaoDadosGeraisVM.Transportador.Erro == true)
            {
                ViewBag["MensagemErro"] = inspecaoDadosGeraisVM.Transportador.MensagemErro;
            }

            inspecaoDadosGeraisVM.TipoTransportador = inspecaoDadosGeraisVM.Transportador.Tipo;

            //Recebe Nome da Frota
            inspecaoDadosGeraisVM.IdTipo = inspecaoDadosGeraisVM.Transportador_ID + "_" + inspecaoDadosGeraisVM.TipoTransportador;
            inspecaoDadosGeraisVM.FrotaViagem = BLL.Inspecao.ConsultaFrotaViagemPorId(inspecaoDadosGeraisVM.Inspecao.FrotaViagem_ID, Configuracao);

            //Erro ao listar Frota
            if (inspecaoDadosGeraisVM.FrotaViagem.Erro == true)
            {
                ViewData["MensagemErro"] = inspecaoDadosGeraisVM.FrotaViagem.MensagemErro;
            }

            inspecaoDadosGeraisVM.FrotaViagemNome = inspecaoDadosGeraisVM.FrotaViagem.Nome;

            //Navio
            if (inspecaoDadosGeraisVM.Inspecao.Navio_ID != null)
            {
                var navio = BLL.Inspecao.ConsultaNavioPorId(inspecaoDadosGeraisVM.Inspecao.Navio_ID, Configuracao);
                if (navio.Erro == true)
                {
                    ViewData["MensagemErro"] = navio.MensagemErro;
                }

                inspecaoDadosGeraisVM.NomeNavio = navio.Nome;
            }

            #region EM_ERRO
            if (inspecaoDadosGeraisVM.ListaCliente.FirstOrDefault().Erro == true)
            {
                ViewData["MensagemErro"] = inspecaoDadosGeraisVM.ListaCliente.FirstOrDefault().MensagemErro;
            }

            if (inspecaoDadosGeraisVM.ListaLocalInspecao.FirstOrDefault().Erro == true)
            {
                ViewData["MensagemErro"] = inspecaoDadosGeraisVM.ListaLocalInspecao.FirstOrDefault().MensagemErro;
            }

            if (inspecaoDadosGeraisVM.ListaLocalCheckPoint.FirstOrDefault().Erro == true)
            {
                ViewData["MensagemErro"] = inspecaoDadosGeraisVM.ListaLocalCheckPoint.FirstOrDefault().MensagemErro;
            }

            if (inspecaoDadosGeraisVM.ListaTransportador.FirstOrDefault().Erro == true)
            {
                ViewData["MensagemErro"] = inspecaoDadosGeraisVM.ListaTransportador.FirstOrDefault().MensagemErro;
            }
            #endregion

            return View("NovaInspecao", inspecaoDadosGeraisVM);
        }

        /// <summary>
        /// Novo Veículo
        /// </summary>
        /// <param name="NovoVeiculo">Dados do novo veículo</param>
        /// <param name="tipobotao"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult NovoVeiculo(InspecaoVeiculoViewModel NovoVeiculo, int tipobotao)
        {
            string _mensagemLogin = "Erro ao validar usuário por favor faça login novamente";

            Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"Inspecao.NovoVeiculo | Parametros:  Edição: {NovoVeiculo.Edicao}, InspVeiculo_ID: {NovoVeiculo.InspVeiculo_ID}" });

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

            ViewData["UsuarioNome"] = dadosUsuario.Nome;
            ViewData["UsuarioIdentificacao"] = dadosUsuario.Identificacao;
            #endregion

            NovoVeiculo.Edicao = 0;

            if (NovoVeiculo.InspVeiculo_ID == 0)
            {
                ViewBag["MensagemErro"] = "Erro ao realizar solicitação, Veículo = 0, por favor tente novamente mais tarde ou entre em contato com o suporte técnico";
                return RedirectToAction("Index", "Home");
            }

            NovoVeiculo.Inspecao = BLL.Inspecao.ListarPorId(NovoVeiculo.Inspecao_ID, Configuracao);
            if (NovoVeiculo.Inspecao.Erro == true)
            {
                ViewData["MensagemErro"] = NovoVeiculo.Inspecao.MensagemErro;
            }

            NovoVeiculo.Marca = BLL.InspecaoVeiculo.ListaMarca(NovoVeiculo.Inspecao.Cliente_ID, Configuracao);
            NovoVeiculo.Modelo = BLL.InspecaoVeiculo.ListaModelo(NovoVeiculo.Inspecao.Cliente_ID, Configuracao);

            if (NovoVeiculo.Marca.Count() > 0)
            {
                if (NovoVeiculo.Marca.FirstOrDefault().Text == "Erro")
                {
                    ViewData["MensagemErro"] = "Erro ao listar Marcas";

                }
            }


            if (NovoVeiculo.Modelo.Count() > 0)
            {
                if (NovoVeiculo.Modelo.FirstOrDefault().Text == "Erro")
                {
                    ViewData["MensagemErro"] = "Erro ao listar Modelos";
                }
            }

            NovoVeiculo.InspVeiculo = BLL.InspecaoVeiculo.ListarPorId(NovoVeiculo.InspVeiculo_ID, Configuracao);

            if (NovoVeiculo.InspVeiculo.Erro == true)
            {
                ViewData["MensagemErro"] = NovoVeiculo.InspVeiculo.MensagemErro;
            }

            NovoVeiculo.VIN_6 = "";
            NovoVeiculo.Observacoes = "";
            NovoVeiculo.Marca_ID = 0;
            NovoVeiculo.Modelo_ID = 0;

            return View("Veiculo", NovoVeiculo);
        }

        /// <summary>
        /// Insere um novo modelo no banco de dados via ajax
        /// </summary>
        /// <param name="cliente_ID">ID do cliente</param>
        /// <param name="novoModeloNome">nome do novo modelo a ser inserido no banco de dados</param>
        /// <returns></returns>
        [HttpPost]
        public int InserirNovoModelo(string cliente_ID, string novoModeloNome)
        {
            Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"Controller: InserirNovoModelo | Cliente_ID: {cliente_ID}, NovoModelo_Nome: {novoModeloNome}" });

            int id = 0;

            try
            {
                id = BLL.Inspecao.InserirNovoModelo(Convert.ToInt32(cliente_ID), novoModeloNome, Configuracao);
                return id;
            }
            catch
            {
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Erro, Mensagem = $"Erro ao executar InspecaoController.InserirNovoModelo | Cliente_ID {cliente_ID}, modelo_nome {novoModeloNome}" });
                return -1;
            }
        }

        /// <summary>
        /// Recurso usado via AJAX para inserção de marcas no banco de dados
        /// </summary>
        /// <param name="cliente_ID">Id do cliente</param>
        /// <param name="novaMarcaNome">Nome da marca a ser inserida no banco de dados</param>
        /// <returns></returns>
        [HttpPost]
        public int InserirNovaMarca(string cliente_ID, string novaMarcaNome)
        {
            Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"Nova Marca Acionada: {cliente_ID}, inspAvaria_ID: {novaMarcaNome}" });

            int id = 0;
            try
            {
                id = BLL.Inspecao.InserirNovaMarca(Convert.ToInt32(cliente_ID), novaMarcaNome, Configuracao);
                return id;
            }
            catch
            {
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Erro, Mensagem = $"Erro ao executar InspecaoController.InserirNovaMarca() | Cliente_ID {cliente_ID}, marca_nome {novaMarcaNome}" });
                return -1;
            }

        }

        /// <summary>
        /// Foto solicitada - retorna um FileContentResult
        /// </summary>
        /// <param name="imagem"></param>
        /// <param name="inspAvaria_ID"></param>
        /// <returns>FileContentResult</returns>
        public FileResult Foto(string imagem, int inspAvaria_ID)
        {
            FileContentResult retorno = null;
            if (imagem != null)
            {
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"Recebe dados binarios: Foto | Imagem: {imagem}, inspAvaria_ID: {inspAvaria_ID}" });

                try

                {
                    string serverpath = Configuracao.PastaFotos;

                    Models.InspAvaria inspAvaria = BLL.Avarias.ListarPorId(inspAvaria_ID, Configuracao);
                    //Models.InspVeiculo inspVeiculo = BLL.InspecaoVeiculo.ListarPorId(inspAvaria.InspVeiculo_ID, configuracao);
                    Models.Inspecao inspecao = BLL.Inspecao.ListarPorId(inspAvaria.Inspecao_ID, Configuracao);

                    string _cliente_id = Convert.ToString(inspecao.Cliente_ID);
                    string _inspecao_id = Convert.ToString(inspecao.Inspecao_ID);
                    string _inspecao = Convert.ToString((int)DAL.InspVeiculo.ListarPorId(inspAvaria.InspVeiculo_ID, Configuracao).Inspecao_ID);
                    string _inspVeiculo = Convert.ToString(inspAvaria.InspVeiculo_ID);
                    string _inspAvaria = Convert.ToString(inspAvaria.InspAvaria_ID);
                    string _ano = inspecao.Data.ToString("yyyy");
                    string _mesdia = inspecao.Data.ToString("MMdd");


                    var path = Path.Combine(Configuracao.PastaFotos, "Imagens", "Avarias", _cliente_id, _ano, _mesdia, _inspecao, _inspVeiculo, _inspAvaria, imagem);

                    using (var j = new FileStream(path, FileMode.Open))
                    {
                        //Conversão para Byte
                        byte[] imgbyte = new byte[j.Length];
                        j.Read(imgbyte, 0, imgbyte.Length);
                        retorno = new FileContentResult(imgbyte, "image/jpeg");
                    }
                    return retorno;

                }
                catch (Exception ex)
                {
                    Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Erro, Mensagem = $"Não conseguiui receber dados da foto informada", Excecao = ex });
                    throw;
                }

            }
            else
            {
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"[SEM IMAGEM] - Recebe dados binarios:  inspAvaria_ID: {inspAvaria_ID}" });
                return retorno;
            }

        }


        /// <summary>
        /// Deleta a imagem escolhida referente a avaria informada. Este método é acionado via Ajax
        /// </summary>
        /// <param name="inspAvaria_ID"></param>
        /// <param name="imagem"></param>
        /// <returns>inteiro : 0 -ok,  -1 nok</returns>
        [HttpPost]
        public int DeletarFoto(string inspAvaria_ID, string imagem)
        {
            int int_inspAvaria_ID;
            Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"Executou DeletarFoto via Ajax - Assíncrono" });

            try
            {
                int.TryParse(inspAvaria_ID, out int_inspAvaria_ID);
                BLL.UploadImagens.DeletarImagem(int_inspAvaria_ID, imagem, Configuracao);
                return 0;
            }

            catch (Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem { Mensagem = "Erro ao deletar foto", Nivel = Diag.Nivel.Erro, Excecao = ex });
                return -1;
            }

        }

        /// <summary>
        /// Recebe locais checkpoint,  referente ao local selecionado
        /// </summary>
        /// <param name="localInspecao_ID">Local de inspeção ID</param>
        /// <returns>Retorna um JSON contendo a lista de checkpoints do referido local</returns>
        public JsonResult RecebeDadosLocalCheckPoint(int localInspecao_ID)
        {
            var listaLocalCheckpoint = new List<Models.LocalCheckPoint>();

            Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"Executou RecebeDadosLocalCheckPoint via Ajax - Assíncrono" });

            try
            {
                ViewModels.LoginViewModel dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);
                listaLocalCheckpoint = DAL.LocalCheckPoint.Listar(dadosUsuario.UsuarioId, Configuracao).Where(p => p.LocalInspecao_ID == localInspecao_ID).ToList();
            }
            catch (Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Erro, Mensagem = $"Erro ao receber dados do LocalInspecaoCheckPoint informada", Excecao = ex });
                throw;
            }

            return Json(listaLocalCheckpoint);
        }


        /// <summary>
        /// Recebe dados do transportador dependendo do checkpoint selecionado
        /// </summary>
        /// <param name="localCheckPoint_ID"></param>
        /// <returns>JSON contendo os dados </returns>
        public JsonResult RecebeDadosTransportador(int localCheckPoint_ID)
        {

            var listaTransportador = new List<Models.Transportador>();

            if (localCheckPoint_ID != 0)
            {

                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"Executou RecebeDadosTransportador via Ajax - Assíncrono - LocalCheckPointId {localCheckPoint_ID}" });

                try
                {
                    ViewModels.LoginViewModel dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);

                    Models.LocalCheckPoint LocalCheckPoint = DAL.LocalCheckPoint.Listar(dadosUsuario.UsuarioId, Configuracao)
                        .Where(p => p.LocalCheckPoint_ID == localCheckPoint_ID).FirstOrDefault();

                    listaTransportador = DAL.Transportador.Listar(dadosUsuario.UsuarioId, Configuracao)
                        .Where(p => p.Tipo == LocalCheckPoint.Tipo).OrderBy(p => p.IdTipo).ToList();

                }
                catch (Exception ex)
                {
                    Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Erro, Mensagem = $"Erro ao receber dados do Transportador conforme o LocalCheckPoint informado", Excecao = ex });
                }
            }

            return Json(listaTransportador);
        }
    }
}