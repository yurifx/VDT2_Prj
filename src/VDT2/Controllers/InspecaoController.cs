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

namespace VDT2.Controllers
    {
    public class InspecaoController : Controller
        {

        private VDT2.Models.Configuracao configuracao { get; set; }

        /// <summary>
        /// Construtor da classe
        /// <para>Recebe a configuração do aplicativo, usando Dependency Injection</para>
        /// </summary>
        /// <param name="settings">Configuração geral do aplicativo, carregada de appsettings.json</param>
        public InspecaoController(IOptions<VDT2.Models.Configuracao> settings)
            {

            this.configuracao = settings.Value;
            }

        public IActionResult Index()
            {

            ViewModels.LoginViewModel dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);
            if (dadosUsuario == null)
                {
                return RedirectToAction("Index", "Home");
                }

            InspecaoDadosGeraisViewModel _inspecaoDadosGeraisVM = new InspecaoDadosGeraisViewModel();

            //recebe todos os dados do banco de dados para preencher posteriormente os referidos dropdownlist
            _inspecaoDadosGeraisVM.ListaCliente = ClienteRepositorio.Listar(dadosUsuario.UsuarioId, configuracao);
            _inspecaoDadosGeraisVM.ListaLocalInspecao = LocalInspecaoRepositorio.Listar(dadosUsuario.UsuarioId, configuracao);
            _inspecaoDadosGeraisVM.ListaLocalCheckPoint = LocalCheckPointRepositorio.Listar(dadosUsuario.UsuarioId, configuracao);
            _inspecaoDadosGeraisVM.ListaTransportador = TransportadorRepositorio.Listar(dadosUsuario.UsuarioId, configuracao);

            if (dadosUsuario != null)
                {
                ViewData["UsuarioNome"] = dadosUsuario.Nome;
                }

            ViewData["UsuarioIdentificacao"] = dadosUsuario.Identificacao;

            return View(_inspecaoDadosGeraisVM);
            }


        [HttpPost]
        public IActionResult InserirDadosCabecalhoInspecao(InspecaoDadosGeraisViewModel idgVM, string botaoEnviar)
            {

            //Verifica se é um reload do formulário anterior
            string formsubmit = idgVM.Cliente_ID + idgVM.FrotaViagem + idgVM.IdTipo + idgVM.LocalCheckPoint_ID + idgVM.LocalInspecao_ID + idgVM.NomeNavio;
            var ultimosubmit = this.HttpContext.Session.GetString("formsubmit");
            if (formsubmit != ultimosubmit)
                {
                this.HttpContext.Session.SetString("formsubmit", formsubmit);
                }
            else
                {
                return RedirectToAction("Index");
                }


            List<Inspecao> _listaInspecao = new List<Inspecao>();
            Inspecao inspecao = new Inspecao();

            //Recebe as informações do usuário logado
            ViewModels.LoginViewModel dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);

            //Recebe dados do formulário 
            inspecao.Cliente_ID = idgVM.Cliente_ID;
            inspecao.LocalInspecao_ID = idgVM.LocalInspecao_ID;
            inspecao.LocalCheckPoint_ID = idgVM.LocalCheckPoint_ID;
            //transportador
            int _transportadorId = Convert.ToInt32(idgVM.IdTipo.Split('_')[0]);
            char _tipoTransportador = Convert.ToChar(idgVM.IdTipo.Split('_')[1]);
            inspecao.Transportador_ID = _transportadorId;


            //Mapeando Frota/Viagem - Neste caso, para buscar frota/viagem, temos que passar o tipo de transportador e o Id do transportador
            //recebe todos dados do transportador informado
            var transportador = TransportadorRepositorio.Listar(0, configuracao).Where(p => p.Transportador_ID == _transportadorId).FirstOrDefault();

            //lista todas Frotas/Viagens  onde sejam do tipo do transportador informado e do ID do transportador informado
            var listaFrotaViagem = FrotaViagemRepositorio.Listar(transportador.Tipo, transportador.Transportador_ID, configuracao);

            //a partir da lista anterior [listaFrotaViagem], gostariamos de pegar apenas o que está com o nome igual o nome do transportador informado
            var frotaViagemInformada = listaFrotaViagem.Where(p => p.Nome == idgVM.FrotaViagem).FirstOrDefault();


            if (frotaViagemInformada == null)
                {   //caso não exista nenhum registro desta frota, inserir no banco
                inspecao.FrotaViagem_ID = FrotaViagemRepositorio.Inserir(transportador.Transportador_ID, idgVM.FrotaViagem, configuracao);
                }

            //caso já exista este transportador, apenas pegar seu frotaviagem_id
            else
                {
                inspecao.FrotaViagem_ID = frotaViagemInformada.FrotaViagem_ID;
                }
            //_inspecao.FrotaViagem_ID = listaFrotaViagem.Where(p=>p.Nome== transportador.Nome).FirstOrDefault().FrotaViagem_ID;


            //Só entra caso seja maritimo
            if (!string.IsNullOrEmpty(idgVM.NomeNavio))
                {
                //Primeiro, verifica se existe este navio no bdd
                inspecao.Navio_ID = NavioRepositorio.ConsultaIdNavio(idgVM.NomeNavio, configuracao);
                }

            inspecao.Usuario_ID = dadosUsuario.UsuarioId;

            //Insere os dados  da inspecao
            int _inspecaoId = InspecaoRepositorio.Inserir(inspecao, configuracao);

            //Se tudo der certo, retorna o registro inserido
            inspecao.Inspecao_ID = _inspecaoId;

            //Preenche viewmodel da próxima view - Veiculo1
            InspecaoVeiculoViewModel InspecaoVeiculoVM = new InspecaoVeiculoViewModel();
            InspecaoVeiculoVM.Inspecao = inspecao;

            //AQUI VALORES MARCA e MODELO SETADOS E ESTAO INDO P/ VIEW CORRETAMENTE 
            InspecaoVeiculoVM.Marca_ID = 0;
            InspecaoVeiculoVM.Modelo_ID = 0;

            var marcaList = MarcaRepositorio.Listar(1, configuracao);
            //Inicializa a lista de  Modelos, já inserindo o primeiro registro default
            InspecaoVeiculoVM.Marca = new List<SelectListItem>(marcaList.Count + 1);
            InspecaoVeiculoVM.Marca.Add(new SelectListItem
                {
                Text = "Selecione a Marca",
                Value = 0.ToString()
                });

            foreach (var item in marcaList)
                {
                InspecaoVeiculoVM.Marca.Add(
                    new SelectListItem
                        {
                        Text = item.Nome,
                        Value = item.Marca_ID.ToString()
                        });
                }


            var modeloList = ModeloRepositorio.Listar(dadosUsuario.UsuarioId, configuracao);
            //Inicializa a lista de  Modelos, já inserindo o primeiro registro default
            InspecaoVeiculoVM.Modelo = new List<SelectListItem>(modeloList.Count + 1);
            InspecaoVeiculoVM.Modelo.Add(new SelectListItem
                {
                Text = "Selecione o Modelo",
                Value = 0.ToString()
                });

            foreach (var item in modeloList)
                {
                InspecaoVeiculoVM.Modelo.Add(
                    new SelectListItem
                        {
                        Text = item.Nome,
                        Value = item.Modelo_ID.ToString()
                        });
                }

            return View("Veiculo", InspecaoVeiculoVM);

            }


        [HttpPost]
        public IActionResult InserirVeiculo(ViewModels.InspecaoVeiculoViewModel ivVM, int tipoBotao)
            {
            //caso não esteja logado, redirecionar para index.
            ViewModels.LoginViewModel dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);
            if (dadosUsuario == null)
                {
                return RedirectToAction("Index", "Home");
                }

            InspVeiculo inspVeiculo = new InspVeiculo
                {
                Inspecao_ID = ivVM.Inspecao_ID,
                Marca_ID = ivVM.Marca_ID,
                Modelo_ID = ivVM.Modelo_ID,
                VIN_6 = ivVM.VIN_6,
                VIN = null, //necessário pegar info correta no bdd
                InspVeiculo_ID = 1, //recebe no postback
                Observacoes = ivVM.Observacoes
                };

            //Verifica se existe o veículo no banco de dados - neste caso é passado a inspecao. Poderá haver o mesmo veículo em inspecoes diferentes
            inspVeiculo.InspVeiculo_ID = InspVeiculoRepositorio.Existe(inspVeiculo.Inspecao_ID, inspVeiculo.VIN_6, configuracao);

            if (inspVeiculo.InspVeiculo_ID == 0)
                {
                //Insert de veículo no BDD
                inspVeiculo.InspVeiculo_ID = InspVeiculoRepositorio.Inserir(inspVeiculo, configuracao);
                }

            //preenche valores -> irá enviar para a próxima view
            InspecaoVeiculoRegistrarAvariasVM ivraVM = new InspecaoVeiculoRegistrarAvariasVM();
            ivraVM.insVeiculo = inspVeiculo;
            ivraVM.dadosUsuario = dadosUsuario;


            //Verifica se o usuário clicou em "Registrar ou Registrar Avarias" / Neste ponto já registrou veículo no bdd
            if (tipoBotao == 1) //apenas registrar veiculo
                {
                InspecaoVeiculoViewModel InspecaoVeiculoVM = new InspecaoVeiculoViewModel();


                InspecaoVeiculoVM.Inspecao_ID = ivVM.Inspecao_ID;
                InspecaoVeiculoVM.UltimoVeiculo_InspVeiculo_ID = inspVeiculo.InspVeiculo_ID;

                InspecaoVeiculoVM.Inspecao = InspecaoRepositorio.ListarPorId(ivVM.Inspecao_ID);


                //Ver como inserir 0 e voltar para o 0
                InspecaoVeiculoVM.Marca_ID = 0;
                InspecaoVeiculoVM.Modelo_ID = 0;

                var marcaList = MarcaRepositorio.Listar(1, configuracao);
                InspecaoVeiculoVM.Marca = new List<SelectListItem>(marcaList.Count + 1);
                InspecaoVeiculoVM.Marca.Add(new SelectListItem
                    {
                    Text = "Selecione a  Marca",
                    Value = 0.ToString()
                    });
                foreach (var item in marcaList)
                    {
                    InspecaoVeiculoVM.Marca.Add(
                        new SelectListItem
                            {
                            Text = item.Nome,
                            Value = item.Marca_ID.ToString()
                            });
                    }


                var modeloList = ModeloRepositorio.Listar(dadosUsuario.UsuarioId, configuracao);
                InspecaoVeiculoVM.Modelo = new List<SelectListItem>(modeloList.Count + 1);
                InspecaoVeiculoVM.Modelo.Add(new SelectListItem
                    {
                    Text = "Selecione o Modelo",
                    Value = 0.ToString()
                    });

                foreach (var item in modeloList)
                    {
                    InspecaoVeiculoVM.Modelo.Add(
                        new SelectListItem
                            {
                            Text = item.Nome,
                            Value = item.Modelo_ID.ToString()
                            });
                    }



                return View("Veiculo", InspecaoVeiculoVM);
                }

            else if (tipoBotao == 2) //registrar avarias
                {
                ivraVM.avAreaLista = AvAreaRepositorio.Listar(1, configuracao);
                ivraVM.avCondicaoLista = AvCondicaoRepositorio.Listar(1, configuracao);
                ivraVM.avDanoRepositorioLista = AvDanoRepositorio.Listar(1, configuracao);
                ivraVM.avGravidadeLista = AvGravidadeRepositorio.Listar(1, configuracao);
                ivraVM.avQuadranteLista = AvQuadranteRepositorio.Listar(1, configuracao);
                ivraVM.avSeveridadeLista = AvSeveridadeRepositorio.Listar(1, configuracao);

                ivraVM.listaAvarias = InspAvariaRepositorio.Listar(1, ivVM.VIN_6, configuracao);
                return View("RegistrarAvarias", ivraVM);
                };
            return RedirectToAction("Index", "Home");
            }

        [HttpPost]
        public IActionResult InserirAvaria(InspecaoVeiculoRegistrarAvariasVM ivVM, int tipoBotao) //melhorar para pegar do viewmodel
            {
            ViewModels.LoginViewModel dadosUsuario = BLL.Login.ExtraiDadosUsuario(this.HttpContext.User.Claims);
            InspAvaria insAvaria = new InspAvaria();
            insAvaria.InspVeiculo_ID = ivVM.InspVeiculo_ID;
            insAvaria.AvArea_ID = ivVM.Area_ID;
            insAvaria.AvCondicao_ID = ivVM.Condicao_ID;
            insAvaria.AvDano_ID = ivVM.Dano_ID;
            insAvaria.AvGravidade_ID = ivVM.Gravidade_ID;
            insAvaria.AvQuadrante_ID = ivVM.Quadrante_ID;
            insAvaria.AvSeveridade_ID = ivVM.Severidade_ID;
            insAvaria.FabricaTransporte = ivVM.fabricatransporte;

            InspAvariaRepositorio.Inserir(insAvaria, configuracao);

            InspecaoVeiculoRegistrarAvariasVM ivraVM = new InspecaoVeiculoRegistrarAvariasVM();

            InspVeiculo insVeiculo = new InspVeiculo
                {
                Inspecao_ID = ivVM.Inspecao_ID,
                InspVeiculo_ID = ivVM.InspVeiculo_ID
                };

            ivraVM.insVeiculo = insVeiculo;
            ivraVM.dadosUsuario = dadosUsuario;
            ivraVM.avAreaLista = AvAreaRepositorio.Listar(1, configuracao);
            ivraVM.avCondicaoLista = AvCondicaoRepositorio.Listar(1, configuracao);
            ivraVM.avDanoRepositorioLista = AvDanoRepositorio.Listar(1, configuracao);
            ivraVM.avGravidadeLista = AvGravidadeRepositorio.Listar(1, configuracao);
            ivraVM.avQuadranteLista = AvQuadranteRepositorio.Listar(1, configuracao);
            ivraVM.avSeveridadeLista = AvSeveridadeRepositorio.Listar(1, configuracao);


            //RECEBER UM VIN_6, dependendo da minha inspecao e meu InspecaoVeiculo_ID
            var VIN_6 = InspVeiculoRepositorio.Listar(insVeiculo.Inspecao_ID, configuracao)
                .Where(p => p.InspVeiculo_ID == insVeiculo.InspVeiculo_ID).
                FirstOrDefault()
                .VIN_6;

            ivraVM.VIN_6 = VIN_6;
            //string VIN_6 = InspVeiculoRepositorio.Listar(insVeiculo.Inspecao_ID,configuracao).VIN_6;
            ivraVM.listaAvarias = InspAvariaRepositorio.Listar(1, VIN_6, configuracao);

            return View("RegistrarAvarias", ivraVM);
            }

        [HttpPost]
        public IActionResult VisualizarAvarias(VisualizarAvariasViewModel VisualizarAvariasVM, int tipoBotao) //melhorar para pegar do viewmodel
            {

            //Não preciso pegar novamento o VIN_6 pois está vindo da viewmodel [Inserir_Veículo]
            var VIN_6 = InspVeiculoRepositorio.Listar(VisualizarAvariasVM.Inspecao_ID, configuracao)
                .Where(p => p.InspVeiculo_ID == VisualizarAvariasVM.InspVeiculo_ID)
                .FirstOrDefault()
                .VIN_6;

            VisualizarAvariasVM.Avarias = InspAvariaRepositorio.Listar(1, VIN_6, configuracao);
            return View("Avarias", VisualizarAvariasVM);
            }

        }
    }

