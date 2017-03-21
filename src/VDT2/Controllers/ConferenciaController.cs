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
        public IActionResult VisualizarFotos(int inspAvaria_ID)
        {
            ConferenciaVisualizarAvariasViewModel visualizarAvariasVM = new ConferenciaVisualizarAvariasViewModel();

            visualizarAvariasVM.InspAvaria = new Models.InspAvaria();
            visualizarAvariasVM.InspAvaria = BLL.InspecaoAvaria.ListarPorId(inspAvaria_ID, configuracao);

            visualizarAvariasVM.ListaImagemAvarias = BLL.UploadImagens.Listar(inspAvaria_ID, configuracao);


            return View("VisualizarFotos", visualizarAvariasVM);

        }

        public IActionResult SalvarFotos(int inspAvaria_ID, ICollection<IFormFile> files)
        {

            //ALTERAÇÕES TESTE GIT

            bool uploadImagem = BLL.UploadImagens.UploadImagensAvaria(inspAvaria_ID, files, configuracao);
            if (uploadImagem == false)
            {
                ViewData["MensagemErro"] = "Erro ao relaizar upload de imagens da avaria";
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

    }
}
