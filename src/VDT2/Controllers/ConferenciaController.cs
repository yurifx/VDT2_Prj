using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VDT2.ViewModels;
using VDT2.Models;
using Microsoft.AspNetCore.Http;

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
            #region dadosUsuario
            var dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);
            if (dadosUsuario == null)
            {
                return RedirectToAction("Index", "Home");
            }
            #endregion

            ConferenciaIndexViewModel conferenciaVM = new ConferenciaIndexViewModel();

            conferenciaVM.ListaLocalInspecao = BLL.Inspecao.ListarLocaisInspecao(dadosUsuario.UsuarioId, configuracao);
            if (conferenciaVM.ListaLocalInspecao.Count() == 0)
            {
                ViewData["MensagemErro"] = "Não há locais a serem listados, por favor entre em contato com o suporte técnico";
            }

            conferenciaVM.ListaLocalCheckPoint = BLL.Inspecao.ListarLocalCheckPoint(dadosUsuario.UsuarioId, configuracao);
            if (conferenciaVM.ListaLocalCheckPoint.Count() == 0)
            {
                ViewData["MensagemErro"] = "Não há locais a serem listados, por favor entre em contato com o suporte técnico";
            }

            return View(conferenciaVM);
        }

        /// <summary>
        /// Listagem de veículos e suas respectivas avarias 2a tela.
        /// </summary>
        /// <param name="conferenciaVM"></param>
        /// <returns></returns>
        public IActionResult ConferenciaListarVeiculos(ConferenciaIndexViewModel conferenciaVM)
        {
            #region dadosUsuario
            var dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);
            if (dadosUsuario == null)
            {
                return RedirectToAction("Index", "Home");
            }
            #endregion

            List<InspAvaria_Conf> listaInspAvaria_Conf = new List<InspAvaria_Conf>();
            ListarConferenciaAvariaViewModel listarConferenciaAvariaVM = new ListarConferenciaAvariaViewModel();

            listarConferenciaAvariaVM.InspAvaria_Conf = new Models.InspAvaria_Conf();
            listarConferenciaAvariaVM.InspAvaria_Conf.Data = conferenciaVM.Data;
            listarConferenciaAvariaVM.InspAvaria_Conf.LocalNome = BLL.Inspecao.ListarLocaisInspecao(dadosUsuario.UsuarioId, configuracao).Where(p => p.LocalInspecao_ID == conferenciaVM.LocalInspecao_ID).FirstOrDefault().Nome;
            listarConferenciaAvariaVM.InspAvaria_Conf.CheckPointNome = BLL.Inspecao.ListarLocalCheckPoint(dadosUsuario.UsuarioId, configuracao).Where(p => p.LocalCheckPoint_ID == conferenciaVM.LocalCheckPoint_ID).FirstOrDefault().Nome_Pt;


            listarConferenciaAvariaVM.listaInspAvaria_Conf = DAL.InspAvaria.InspAvariaConf(conferenciaVM.LocalInspecao_ID, conferenciaVM.LocalCheckPoint_ID, conferenciaVM.Data, configuracao);

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
            conferenciaEditarAvariasVM.InspAvaria = BLL.InspecaoAvaria.ListarPorId(inspAvaria_ID, configuracao);
            conferenciaEditarAvariasVM.InspVeiculo = BLL.InspecaoVeiculo.ListarPorId(conferenciaEditarAvariasVM.InspAvaria.InspVeiculo_ID, configuracao);
            conferenciaEditarAvariasVM.Inspecao = BLL.Inspecao.ListarPorId(conferenciaEditarAvariasVM.InspVeiculo.Inspecao_ID, configuracao);

            //preenche dados próxima view
            conferenciaEditarAvariasVM.listaMarcas = BLL.InspecaoVeiculo.ListaMarca(conferenciaEditarAvariasVM.Inspecao.Cliente_ID, configuracao);
            conferenciaEditarAvariasVM.listaModelos = BLL.InspecaoVeiculo.ListaModelo(conferenciaEditarAvariasVM.Inspecao.Cliente_ID, configuracao);
            conferenciaEditarAvariasVM.listaAreas = BLL.RegistrarAvarias.ListarAreas(conferenciaEditarAvariasVM.Inspecao.Cliente_ID, configuracao);
            conferenciaEditarAvariasVM.listaCondicoes = BLL.RegistrarAvarias.ListarCondicoes(conferenciaEditarAvariasVM.Inspecao.Cliente_ID, configuracao);
            conferenciaEditarAvariasVM.listaDanos = BLL.RegistrarAvarias.ListarDanos(conferenciaEditarAvariasVM.Inspecao.Cliente_ID, configuracao);
            conferenciaEditarAvariasVM.listaGravidades = BLL.RegistrarAvarias.ListarGravidades(conferenciaEditarAvariasVM.Inspecao.Cliente_ID, configuracao);
            conferenciaEditarAvariasVM.listaQuadrantes = BLL.RegistrarAvarias.ListarQuadrantes(conferenciaEditarAvariasVM.Inspecao.Cliente_ID, configuracao);
            conferenciaEditarAvariasVM.listaSeveridades = BLL.RegistrarAvarias.ListarSeveridades(conferenciaEditarAvariasVM.Inspecao.Cliente_ID, configuracao);

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
            listarConferenciaAvariaVM.listaInspAvaria_Conf = DAL.InspAvaria.InspAvariaConf(conferenciaEditarAvariasVM.Inspecao.LocalInspecao_ID, conferenciaEditarAvariasVM.Inspecao.LocalCheckPoint_ID, conferenciaEditarAvariasVM.Inspecao.Data, configuracao);


            listarConferenciaAvariaVM.InspAvaria_Conf = new Models.InspAvaria_Conf();

            listarConferenciaAvariaVM.InspAvaria_Conf.Data = conferenciaEditarAvariasVM.Inspecao.Data;
            listarConferenciaAvariaVM.InspAvaria_Conf.LocalNome = listarConferenciaAvariaVM.listaInspAvaria_Conf.FirstOrDefault().LocalNome;
            listarConferenciaAvariaVM.InspAvaria_Conf.CheckPointNome = listarConferenciaAvariaVM.listaInspAvaria_Conf.FirstOrDefault().CheckPointNome;

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
            visualizarAvariasVM.InspAvaria = BLL.InspecaoAvaria.ListarPorId(inspAvaria_ID, configuracao);

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

            bool uploadImagem = BLL.UploadImagens.UploadImagensAvaria(inspAvaria_ID, files, configuracao);
            if (uploadImagem == false)
            {
                ViewData["MensagemErro"] = "Erro ao realizar upload de imagens da avaria";
            }
            else
            {
                ViewData["MensagemSucesso"] = "Fotos atualizadas com sucesso";
            }

            ConferenciaEditarAvariasViewModel conferenciaEditarAvariasVM = new ConferenciaEditarAvariasViewModel();

            conferenciaEditarAvariasVM.InspAvaria = new Models.InspAvaria();
            conferenciaEditarAvariasVM.InspVeiculo = new Models.InspVeiculo();
            conferenciaEditarAvariasVM.Inspecao = new Models.Inspecao();

            conferenciaEditarAvariasVM.InspAvaria = BLL.InspecaoAvaria.ListarPorId(inspAvaria_ID, configuracao);
            conferenciaEditarAvariasVM.InspVeiculo = BLL.InspecaoVeiculo.ListarPorId(conferenciaEditarAvariasVM.InspAvaria.InspVeiculo_ID, configuracao);
            conferenciaEditarAvariasVM.Inspecao = BLL.Inspecao.ListarPorId(conferenciaEditarAvariasVM.InspAvaria.Inspecao_ID, configuracao);


            ListarConferenciaAvariaViewModel listarConferenciaAvariaVM = new ListarConferenciaAvariaViewModel();


            //Mudar para pegar via BLL
            listarConferenciaAvariaVM.listaInspAvaria_Conf = DAL.InspAvaria.InspAvariaConf(conferenciaEditarAvariasVM.Inspecao.LocalInspecao_ID, conferenciaEditarAvariasVM.Inspecao.LocalCheckPoint_ID, conferenciaEditarAvariasVM.Inspecao.Data, configuracao);
            listarConferenciaAvariaVM.InspAvaria_Conf = new Models.InspAvaria_Conf();
            listarConferenciaAvariaVM.InspAvaria_Conf.Data = conferenciaEditarAvariasVM.Inspecao.Data;
            listarConferenciaAvariaVM.InspAvaria_Conf.LocalNome = listarConferenciaAvariaVM.listaInspAvaria_Conf.FirstOrDefault().LocalNome;
            listarConferenciaAvariaVM.InspAvaria_Conf.CheckPointNome = listarConferenciaAvariaVM.listaInspAvaria_Conf.FirstOrDefault().CheckPointNome;
            return View("ListarConferenciaAvarias", listarConferenciaAvariaVM);
        }

        public IActionResult LoadingListInicio()
        {
            ViewModels.LoginViewModel dadosUsuario = null;

            //Verifica dados do usuário
            #region dadosUsuario
            dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);
            if (dadosUsuario == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["UsuarioNome"] = dadosUsuario.Nome;
            }

            ViewData["UsuarioIdentificacao"] = dadosUsuario.Identificacao;
            #endregion

            ConferenciaLoadingListViewModel conferenciaLoadingListVM = new ConferenciaLoadingListViewModel();
            conferenciaLoadingListVM.ListaCliente = BLL.Inspecao.ListarClientes(dadosUsuario.UsuarioId, configuracao);
            if (conferenciaLoadingListVM.ListaCliente.Count() == 0)
            {
                ViewData["MensagemErro"] = "Erro ao listar Clientes, tente novamente mais tarde ou entre em contato com o suporte técnico.";
                conferenciaLoadingListVM.ListaCliente = new List<Models.Cliente>();
                conferenciaLoadingListVM.ListaCliente.Add(new Cliente { Erro = true, Nome = "Erro", Cliente_ID = 0 });
            }
            else if (conferenciaLoadingListVM.ListaCliente.FirstOrDefault().Erro == true)
            {
                ViewData["MensagemErro"] = "Erro ao listar Clientes, tente novamente mais tarde ou entre em contato com o suporte técnico.";
            }


            conferenciaLoadingListVM.ListaLocalInspecao = BLL.Inspecao.ListarLocaisInspecao(dadosUsuario.UsuarioId, configuracao);

            if (conferenciaLoadingListVM.ListaLocalInspecao.Count() == 0)
            {
                ViewData["MensagemErro"] = "Erro ao listar Locais Inspeção, tente novamente mais tarde ou entre em contato com o suporte técnico.";
                conferenciaLoadingListVM.ListaLocalInspecao = new List<Models.LocalInspecao>();
                conferenciaLoadingListVM.ListaLocalInspecao.Add(new Models.LocalInspecao { Erro = true, Nome = "Erro", LocalInspecao_ID = 0 });
            }

            else if (conferenciaLoadingListVM.ListaLocalInspecao.FirstOrDefault().Erro == true)
            {
                ViewData["MensagemErro"] = "Erro ao listar Locais Inspeção, tente novamente mais tarde ou entre em contato com o suporte técnico.";
            }


            return View("LoadingListInicio", conferenciaLoadingListVM);
        }


        public IActionResult PackingListInicio()
        {
            ViewModels.LoginViewModel dadosUsuario = null;

            //Verifica dados do usuário
            #region dadosUsuario
            dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);
            if (dadosUsuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            else
            {
                ViewData["UsuarioNome"] = dadosUsuario.Nome;
            }

            ViewData["UsuarioIdentificacao"] = dadosUsuario.Identificacao;
            #endregion

            ConferenciaPackingListViewModel conferenciaPackingListVM = new ConferenciaPackingListViewModel();

            conferenciaPackingListVM.ListaCliente = BLL.Inspecao.ListarClientes(dadosUsuario.UsuarioId, configuracao);
            if (conferenciaPackingListVM.ListaCliente.Count() == 0)
            {
                ViewData["MensagemErro"] = "Erro ao listar Clientes, tente novamente mais tarde ou entre em contato com o suporte técnico.";
                conferenciaPackingListVM.ListaCliente = new List<Models.Cliente>();
                conferenciaPackingListVM.ListaCliente.Add(new Cliente { Erro = true, Nome = "Erro", Cliente_ID = 0 });
            }
            else if (conferenciaPackingListVM.ListaCliente.FirstOrDefault().Erro == true)
            {
                ViewData["MensagemErro"] = "Erro ao listar Clientes, tente novamente mais tarde ou entre em contato com o suporte técnico.";
            }
        

            conferenciaPackingListVM.ListaLocalInspecao = BLL.Inspecao.ListarLocaisInspecao(dadosUsuario.UsuarioId, configuracao);

            if (conferenciaPackingListVM.ListaLocalInspecao.Count() == 0)
            {
                ViewData["MensagemErro"] = "Erro ao listar Locais Inspeção, tente novamente mais tarde ou entre em contato com o suporte técnico.";
                conferenciaPackingListVM.ListaLocalInspecao = new List<Models.LocalInspecao>();
                conferenciaPackingListVM.ListaLocalInspecao.Add(new Models.LocalInspecao { Erro = true, Nome = "Erro", LocalInspecao_ID = 0 });
            }

            else if (conferenciaPackingListVM.ListaLocalInspecao.FirstOrDefault().Erro == true)
            {
                ViewData["MensagemErro"] = "Erro ao listar Locais Inspeção, tente novamente mais tarde ou entre em contato com o suporte técnico.";
            }
          

            return View("PackingListInicio", conferenciaPackingListVM);

    }

    public IActionResult LoadingListSalvar(ConferenciaLoadingListViewModel conferenciaLoadingListVM, ICollection<IFormFile> files)
{
    bool salvou = false;
    bool inseriuArquivo = false;
    bool integrou = false;

    //Verifica dados do usuário
    ViewModels.LoginViewModel dadosUsuario = null;

    #region dadosUsuario
    dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);
    if (dadosUsuario == null)
    {
        return RedirectToAction("Index", "Home");
    }
    else
    {
        ViewData["UsuarioNome"] = dadosUsuario.Nome;
    }
    ViewData["UsuarioIdentificacao"] = dadosUsuario.Identificacao;
    #endregion

    conferenciaLoadingListVM.ListaCliente = BLL.Inspecao.ListarClientes(dadosUsuario.UsuarioId, configuracao);
    conferenciaLoadingListVM.ListaLocalInspecao = BLL.Inspecao.ListarLocaisInspecao(dadosUsuario.UsuarioId, configuracao);

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
                ViewData["MensagemErro"] = "Erro ao gravar arquivo. Tente novamente mais tarde ou entre em contato com service desk";
            }
        }
        else if (files.FirstOrDefault().ContentType == "text/plain")
        {
            ViewData["MensagemErro"] = "Erro ao gravar arquivo. Tente novamente mais tarde ou entre em contato com service desk";
        }
    }
    else if (files.Count() >= 0)
    {
        ViewData["MensagemErro"] = "Erro ao gravar arquivo. Tente novamente mais tarde ou entre em contato com service desk";
    }

    return View("LoadingListInicio", conferenciaLoadingListVM);
}

/// <summary>
/// Salvar PackingList Informado pelo usuároi
/// </summary>
/// <param name="conferenciaPackingListVM"></param>
/// <param name="files"></param>
/// <returns></returns>

public IActionResult PackingListSalvar(ConferenciaPackingListViewModel conferenciaPackingListVM, ICollection<IFormFile> files)
{
    bool salvou = false;
    bool inseriuArquivo = false;
    bool integrou = false;

    //Verifica dados do usuário
    ViewModels.LoginViewModel dadosUsuario = null;

    #region dadosUsuario
    dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);
    if (dadosUsuario == null)
    {
        return RedirectToAction("Index", "Home");
    }
    else
    {
        ViewData["UsuarioNome"] = dadosUsuario.Nome;
    }
    ViewData["UsuarioIdentificacao"] = dadosUsuario.Identificacao;
    #endregion

    conferenciaPackingListVM.ListaCliente = BLL.Inspecao.ListarClientes(dadosUsuario.UsuarioId, configuracao);
    conferenciaPackingListVM.ListaLocalInspecao = BLL.Inspecao.ListarLocaisInspecao(dadosUsuario.UsuarioId, configuracao);

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


}
}
