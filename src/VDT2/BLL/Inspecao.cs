// <copyright file="Inspecao.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Grupo Asserth</author>
// <email>yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>Classe referente a camada de negócios - Inspecao</summary>


//Dependências
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDT2.DAL;
using VDT2.Models;
using VDT2.ViewModels;


/// <summary>
/// Camada de negócios Inspecao
/// </summary>
namespace VDT2.BLL
{
    public class Inspecao
    {

        /// <summary>
        /// Insere um novo modelo de veículo no banco de dados.
        /// </summary>
        /// <param name="cliente_ID">ID do cliente</param>
        /// <param name="novoModeloNome">Nome do modelo</param>
        /// <param name="configuracao"></param>
        /// <returns></returns>
        public static int InserirNovoModelo(int cliente_ID, string novoModeloNome, Configuracao configuracao)
        {
            try
            {
                var modelo_ID = 0;
                bool existe;
                //verificar se existe
                var modelolist = DAL.Modelo.Listar(cliente_ID, configuracao);

                existe = modelolist.Exists(p => p.Nome == novoModeloNome);

                if (existe)
                {
                    modelo_ID = modelolist.Where(p => p.Nome == novoModeloNome).FirstOrDefault().Modelo_ID;
                }
                else
                {
                    modelo_ID = DAL.Modelo.Inserir(cliente_ID, novoModeloNome, configuracao);
                }
                return modelo_ID;
            }
            catch
            {
                #region gravalogErro
                Diag.Log.Grava(
               new Diag.LogItem()
               {
                   Nivel = Diag.Nivel.Erro,
                   Mensagem = $"Erro ao executar Inspecao.InserirNovoModelo() | Cliente_ID {cliente_ID}, modelo_nome {novoModeloNome}"
               });
                #endregion
                return -1;

            }
        }

        /// <summary>
        /// Insere uma nova marca no banco de dados
        /// </summary>
        /// <param name="cliente_ID"></param>
        /// <param name="novaMarcaNome"></param>
        /// <param name="configuracao"></param>
        /// <returns>int com a numeração da marca</returns>
        public static int InserirNovaMarca(int cliente_ID, string novaMarcaNome, Configuracao configuracao)
        {
            try
            {
                var marca_ID = 0;
                bool existe;
                //verificar se existe
                var marcalist = DAL.Marca.Listar(cliente_ID, configuracao);

                existe = marcalist.Exists(p => p.Nome == novaMarcaNome);
                if (existe)
                {
                    marca_ID = marcalist.Where(p => p.Nome == novaMarcaNome).FirstOrDefault().Marca_ID;
                }
                else
                {
                    marca_ID = DAL.Marca.Inserir(cliente_ID, novaMarcaNome, configuracao);
                }

                return marca_ID;
            }

            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Realiza a listagem de todos os dados da inspeção informando o ID
        /// </summary>
        /// <param name="inspecao_ID"></param>
        /// <param name="configuracao"></param>
        /// <returns>Todas informações do cabeçalho da inspeção</returns>
        public static Models.Inspecao ListarPorId(int inspecao_ID, Configuracao configuracao)
        {
            Models.Inspecao inspecao = new Models.Inspecao();
            try
            {
                inspecao = DAL.Inspecao.ListarPorId(inspecao_ID, configuracao);
                return inspecao;
            }
            catch
            {
                inspecao.Inspecao_ID = 0;
                inspecao.Erro = true;
                inspecao.MensagemErro = "Erro ao receber dados da inspeção. Tente novamente mais tarde ou entre em contato com o suporte técnico";
                return inspecao;
            }
        }

        /// <summary>
        /// Realiza a listagem de todos os dados do transportador informando seu ID
        /// </summary>
        /// <param name="transportador_ID"></param>
        /// <param name="configuracao"></param>
        /// <returns>Dados contendo informações do transportador</returns>
        public static Models.Transportador ListarTransportadorPorId(int transportador_ID, Configuracao configuracao)
        {
            Models.Transportador transportador = new Models.Transportador();
            try
            {
                transportador = DAL.Transportador.ListarPorId(transportador_ID, configuracao);
                return transportador;
            }
            catch
            {
                transportador.Tipo = "M";
                transportador.Erro = true;
                transportador.MensagemErro = "Erro ao listar Transportador, tente novamente mais tarde ou entre em contato com o suporte técnico";
                return transportador;
            }

        }


        /// <summary>
        /// Realiza a listagem de todos os dados do navio informando seu ID
        /// </summary>
        /// <param name="navio_ID"></param>
        /// <param name="configuracao"></param>
        /// <returns>Dados contendo informações sobre o transportador</returns>
        public static Models.Navio ConsultaNavioPorId(int? navio_ID, Configuracao configuracao)
        {
            Models.Navio navio = new Models.Navio();
            navio.Erro = false;
            try
            {
                navio = DAL.Navio.ConsultaNavioPorId(navio_ID, configuracao);
                return navio;
            }
            catch
            {
                navio.Erro = true;
                navio.MensagemErro = "Erro ao consultar dados do navio, tente novamente mais tarde ou entre em contato com o suporte técnico";
                navio.Nome = "ERRO";
                navio.Navio_ID = 0;
                return navio;
            }

        }


        /// <summary>
        /// Lista a frota viagem por ID
        /// </summary>
        /// <param name="frotaviagem_ID"></param>
        /// <param name="configuracao"></param>
        /// <returns>Models.FrotaViagem</returns>
        public static Models.FrotaViagem ConsultaFrotaViagemPorId(int frotaviagem_ID, Configuracao configuracao)
        {
            Models.FrotaViagem frotaViagem = new Models.FrotaViagem();

            try
            {
                frotaViagem = DAL.FrotaViagem.Selecionar(frotaviagem_ID, configuracao);
                return frotaViagem;
            }

            catch
            {
                frotaViagem.Nome = "ERRO";
                frotaViagem.FrotaViagem_ID = 0;
                frotaViagem.Erro = true;
                frotaViagem.MensagemErro = "Erro oa selecionar frotaviagem, por favor tente novamente mais tarde ou entre em contato com o suporte técnico";
                return frotaViagem;
            }
        }

        /// <summary>
        /// Monta dados da Inspecao
        /// </summary>
        /// <param name="inspecaoDadosGeraisVM"></param>
        /// <param name="configuracao"></param>
        /// <returns>Retorna Models.Inspecao com os dados preenchidos</returns>
        /// 
        public static Models.Inspecao MontaDadosInspecao(InspecaoDadosGeraisViewModel inspecaoDadosGeraisVM, Configuracao configuracao)
        {
            Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"Inicializando MontaDadosInspecao -> Cliente_ID:  {inspecaoDadosGeraisVM.Cliente_ID} | Transportador_ID: {Convert.ToInt32(inspecaoDadosGeraisVM.IdTipo.Split('_')[0])} | LocalCheckPoint_ID: {inspecaoDadosGeraisVM.LocalCheckPoint_ID} | Local {inspecaoDadosGeraisVM.LocalInspecao_ID} Edicao: {inspecaoDadosGeraisVM.Edicao} | FrotaViagem: {inspecaoDadosGeraisVM.FrotaViagemNome}" });

            inspecaoDadosGeraisVM.Inspecao = new Models.Inspecao
            {
                Inspecao_ID = inspecaoDadosGeraisVM.Inspecao_ID,
                Transportador_ID = Convert.ToInt32(inspecaoDadosGeraisVM.IdTipo.Split('_')[0]),
                Cliente_ID = inspecaoDadosGeraisVM.Cliente_ID,
                LocalInspecao_ID = inspecaoDadosGeraisVM.LocalInspecao_ID,
                LocalCheckPoint_ID = inspecaoDadosGeraisVM.LocalCheckPoint_ID,
            };

            inspecaoDadosGeraisVM.FrotaViagemNome = inspecaoDadosGeraisVM.FrotaViagemNome.RemoveEspacosUpperKey();

            if (inspecaoDadosGeraisVM.NomeNavio != null)
            {
                inspecaoDadosGeraisVM.NomeNavio = inspecaoDadosGeraisVM.NomeNavio.RemoveEspacosUpperKey();
            }

            try
            {
                //Mapeando Frota/Viagem - Neste caso, para buscar frota/viagem, temos que passar o tipo de transportador e o Id do transportador
                //recebe todos dados do transportador informado
                Models.Transportador transportador = null;
                try
                {
                    transportador = DAL.Transportador.ListarPorId(inspecaoDadosGeraisVM.Inspecao.Transportador_ID, configuracao);
                }

                catch (System.Exception ex)
                {
                    Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Erro, Mensagem = $"Não conseguiu listar Transportador - MontaDadosInspecao -  TransportadorRepositorio.Listar", Excecao = ex });
                    throw;
                }

                //lista todas Frotas/Viagens  onde sejam do tipo do transportador informado e do ID do transportador informado
                //No caso atual, estamos pegando todos as frotas, e só estamos passando como filtro transportador ID e tipo;
                try
                {
                    inspecaoDadosGeraisVM.Inspecao.FrotaViagem_ID = DAL.FrotaViagem.Inserir(transportador.Transportador_ID, inspecaoDadosGeraisVM.FrotaViagemNome, configuracao);
                }
                catch (System.Exception ex)
                {
                    Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Erro, Mensagem = $"Não conseguiu executar MontaDadosInspecao ->  FrotaViagemRepositorio.Inserir", Excecao = ex });
                }


                //Recebe navio caso seja transportador Marítimo
                if (!string.IsNullOrEmpty(inspecaoDadosGeraisVM.NomeNavio))
                {
                    //Primeiro, verifica se existe este navio no bdd
                    try
                    {
                        inspecaoDadosGeraisVM.Inspecao.Navio_ID = DAL.Navio.InserirNavio(inspecaoDadosGeraisVM.NomeNavio, configuracao);
                    }

                    catch (System.Exception ex)
                    {
                        Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Erro, Mensagem = $"Não conseguiu Consultar Navio - MontaDadosInspecao -  NavioRepositorio.ConsultaIdNavio", Excecao = ex });
                        throw;
                    }
                }
            }

            catch (System.Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Erro, Mensagem = $"Não conseguiu executar InpsecaoDadosCabecalho | MontaDadosInspecao |  INSPECAO: CLIENTE {inspecaoDadosGeraisVM.Inspecao.Cliente_ID}, Transportador {inspecaoDadosGeraisVM.Inspecao}, LOCAL: {inspecaoDadosGeraisVM.Inspecao} LOCALCHECKPOINT: {inspecaoDadosGeraisVM.Inspecao}, FROTAVIAGEM {inspecaoDadosGeraisVM.Inspecao}", Excecao = ex });
                throw;
            }

            return inspecaoDadosGeraisVM.Inspecao;
        }


        /// <summary>
        /// Realiza a atualização das informações da Inspeção
        /// </summary>
        /// <param name="inspecao"></param>
        /// <param name="configuracao"></param>
        /// <returns>Um objeto contendo todos os valores informados</returns>
        public static Models.Inspecao Update(Models.Inspecao inspecao, Configuracao configuracao)
        {
            const string _mensagemErro = "Erro ao atualizar inspeção";

            try
            {
                inspecao = DAL.Inspecao.Update(inspecao, configuracao);
                return inspecao;
            }

            catch (System.Exception ex)
            {
                inspecao.Erro = true;
                inspecao.MensagemErro = _mensagemErro;

                Diag.Log.Grava( new Diag.LogItem{ Nivel = Diag.Nivel.Erro, Mensagem = $"Não conseguiu executar InpsecaoDadosCabecalho - Update", Excecao = ex });

                return inspecao;
            }
        }


        /// <summary>
        /// Realiza a inserção de um novo registro de inspeção.
        /// </summary>
        /// <param name="inspecao"></param>
        /// <param name="configuracao"></param>
        /// <returns></returns>
        public static Models.Inspecao Inserir(Models.Inspecao inspecao, Configuracao configuracao)
        {
            const string _mensagemErro = "Erro ao inserir dados inspeção, por favor entre em contato com o suporte";

            try
            {
                inspecao = DAL.Inspecao.Inserir(inspecao, configuracao);
                return inspecao;
            }
            catch (Exception ex)
            {
                inspecao.Erro = true;
                inspecao.MensagemErro = _mensagemErro;

                #region gravalogerro
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar InpsecaoDadosCabecalho | Inserir | Erro: {ex}",
                        Excecao = ex
                    });
                #endregion

                return inspecao;
            }
        }


        /// <summary>
        /// Realiza a listagem de todas as informações do cliente informando seu ID
        /// </summary>
        /// <param name="usuario_ID"></param>
        /// <param name="configuracao"></param>
        /// <returns>Objeto contendo todos os dados do cliente;</returns>
        public static List<Models.Cliente> ListarClientes(int usuario_ID, Configuracao configuracao)
        {

            const string _mensagemErro = "Não foi possível listar Clientes, tente novamente mais tarde ou entre em contato com o suporte técnico.";
            List<Models.Cliente> listaClientes = new List<Models.Cliente>();
            try
            {
                listaClientes = DAL.Cliente.Listar(usuario_ID, configuracao);
                if (listaClientes.Count() == 0)
                {
                    listaClientes.Add(new Models.Cliente { Erro = true, MensagemErro = _mensagemErro, Cliente_ID = 0, Nome = "Erro" });
                }

                return listaClientes;
            }
            catch (Exception ex)
            {
                listaClientes.Add(new Models.Cliente { Erro = true, MensagemErro = _mensagemErro, Cliente_ID = 0, Nome = "Erro" });

                #region gravalogerro
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar Inspecao | ListarCliente | Erro: {ex}",
                        Excecao = ex
                    });
                #endregion

                return listaClientes;
            }
        }


        /// <summary>
        /// Realiza a listagem de todos os locais de inpseção, informando seu usuario_id e locaisusuario. Este segundo, é referente ao seu perfil. Tabela UsuarioPerfil 
        /// </summary>
        /// <param name="usuario_ID"></param>
        /// <param name="configuracao"></param>
        /// <param name="locaisUsuario"></param>
        /// <returns></returns>
        public static List<Models.LocalInspecao> ListarLocaisInspecao(int usuario_ID, Configuracao configuracao, string locaisUsuario)
        {

            const string _mensagemErro = "Não foi possível listar LocalInspecao, tente novamente mais tarde ou entre em contato com o suporte técnico.";
            List<Models.LocalInspecao> listaLocaisInspecao = new List<Models.LocalInspecao>();

            try
            {
                if (locaisUsuario != "*")
                {
                    var _lst = locaisUsuario.Split('|');

                    List<int> _listaLocaisUsuario = new List<int>();

                    foreach (var local in _lst)
                    {
                        if (local != "")
                        {
                            _listaLocaisUsuario.Add(Convert.ToInt32(local));
                        }
                    }

                    listaLocaisInspecao = DAL.LocalInspecao.Listar(usuario_ID, configuracao).Where(x => _listaLocaisUsuario.Contains(x.LocalInspecao_ID)).ToList();
                }
                else
                {
                    listaLocaisInspecao = DAL.LocalInspecao.Listar(usuario_ID, configuracao);
                }


                if (listaLocaisInspecao.Count() == 0)
                {
                    listaLocaisInspecao.Add(new Models.LocalInspecao { Erro = true, MensagemErro = _mensagemErro, LocalInspecao_ID = 0, Nome = "Erro" });
                }
                return listaLocaisInspecao;
            }

            catch (Exception ex)
            {
                #region gravalogErro
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar Inspecao | ListarLocaisInspecao() | Parâmetros Informados: UsuarioID {usuario_ID}, locaisUsuario: {locaisUsuario} ",
                        Excecao = ex
                    });
                #endregion
                listaLocaisInspecao.Add(new Models.LocalInspecao { Erro = true, MensagemErro = _mensagemErro, LocalInspecao_ID = 0, Nome = "Erro" });
                return listaLocaisInspecao;
            }
        }


        /// <summary>
        /// Realiza a listagem de todos os locais de CheckPoint informando  seu usuario_id
        /// </summary>
        /// <param name="usuario_ID"></param>
        /// <param name="configuracao"></param>
        /// <returns>Objeto contendo todos checkpoints do usuario_id informado</returns>
        public static List<Models.LocalCheckPoint> ListarLocalCheckPoint(int usuario_ID, Configuracao configuracao)
        {

            const string _mensagemErro = "Não foi possível listar LocalCheckPoint, tente novamente mais tarde ou entre em contato com o suporte técnico.";

            List<Models.LocalCheckPoint> listaLocalCheckPoint = new List<Models.LocalCheckPoint>();
            try
            {

                listaLocalCheckPoint = DAL.LocalCheckPoint.Listar(usuario_ID, configuracao);

                if (listaLocalCheckPoint.Count() == 0)
                {
                    listaLocalCheckPoint.Add(new Models.LocalCheckPoint { Erro = true, MensagemErro = _mensagemErro, LocalCheckPoint_ID = 0, Nome_Pt = "Erro" });
                }
                return listaLocalCheckPoint;
            }
            catch (Exception ex)
            {
                #region gravalogErro
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar Inspecao | ListarLocalCheckPoint() | Parâmetros Informados: UsuarioID {usuario_ID} ",
                        Excecao = ex
                    });
                #endregion
                listaLocalCheckPoint.Add(new Models.LocalCheckPoint { Erro = true, MensagemErro = _mensagemErro, LocalCheckPoint_ID = 0, Nome_Pt = "Erro" });
                return listaLocalCheckPoint;
            }
        }


        /// <summary>
        /// Realiza a listagem de todos os transportadores informando seu usuario_id
        /// </summary>
        /// <param name="usuario_ID"></param>
        /// <param name="configuracao"></param>
        /// <returns>Objeto contendo todos os dados do transportador</returns>
        public static List<Models.Transportador> ListarTransportadores(int usuario_ID, Configuracao configuracao)
        {

            const string _mensagemErro = "Não foi possível listar LocalCheckPoint, tente novamente mais tarde ou entre em contato com o suporte técnico.";
            List<Models.Transportador> listaTransportadores = new List<Models.Transportador>();

            try
            {
                listaTransportadores = DAL.Transportador.Listar(usuario_ID, configuracao);
                if (listaTransportadores.Count() == 0)
                {
                    listaTransportadores.Add(new Models.Transportador { Erro = true, MensagemErro = _mensagemErro, Transportador_ID = 0, Nome = "Erro" });
                }

                return listaTransportadores;
            }

            catch (Exception ex)
            {
                #region gravalogErro
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar Inspecao | ListarTransportadores() | Parâmetros Informados: UsuarioID {usuario_ID} ",
                        Excecao = ex
                    });
                #endregion
                listaTransportadores.Add(new Models.Transportador { Erro = true, MensagemErro = _mensagemErro, Transportador_ID = 0, Nome = "Erro" });
                return listaTransportadores;
            }
        }

        public static bool Publicar(int UsuarioId, string Inspecoes, Configuracao configuracao)
        {
            return DAL.Inspecao.Publicar(UsuarioId, Inspecoes, configuracao);
        }
    }
}
