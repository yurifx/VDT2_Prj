// <copyright file="Inspecao.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Grupo Asserth</author>
// <email>yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>Classe referente a camada de negócios - Inspecao</summary>


//Dependencias
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;
using VDT2.ViewModels;


/// <summary>
/// Camada de negócios - InspecaoVeículo
/// </summary>
namespace VDT2.BLL
{
    public class InspecaoVeiculo
    {

        /// <summary>
        /// Realiza a atualização do veículo conforme os dados do veículo informado
        /// </summary>
        /// <param name="inspVeiculo"></param>
        /// <param name="configuracao"></param>
        /// <returns>Modelo de dados contendo as informações do veículo</returns>
        public static Models.InspVeiculo Update(InspVeiculo inspVeiculo, Configuracao configuracao)
        {
            Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"BLL.InspecaoVeiculo.Update: Parametros: inspVeiculo {inspVeiculo}" });

            try
            {
                inspVeiculo = DAL.InspVeiculo.Update(inspVeiculo, configuracao);
                return inspVeiculo;
            }

            catch (Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Erro, Mensagem = $"Erro ao realizar Operação - BLL.InspecaoVeiculo.Update: Parametros: inspVeiculo {inspVeiculo}", Excecao = ex });

                inspVeiculo.Erro = true;
                inspVeiculo.MensagemErro = "Erro ao atualizar dados do veículo, tente novamente mais tarde ou entre em contato com o suporte técnico";

                return inspVeiculo;
            }
        }

        /// <summary>
        /// Realiza a inserção do veículo no banco de dados conforme os dados apresentados
        /// </summary>
        /// <param name="inspVeiculo">Objeto contendo todos os dados do veículo</param>
        /// <param name="configuracao"></param>
        /// <returns>Retorna o próprio objeto porém com o ID preenchido </returns>
        public static Models.InspVeiculo Inserir(InspVeiculo inspVeiculo, Configuracao configuracao)
        {
            Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"BLL.InspecaoVeiculo.Inserir: Parametros: inspVeiculo {inspVeiculo}" });

            try
            {
                inspVeiculo = DAL.InspVeiculo.Inserir(inspVeiculo, configuracao);
                return inspVeiculo;
            }

            catch (Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Erro, Mensagem = $"Erro ao realizar Operação - BLL.InspecaoVeiculo.Inserir: Parametros: inspVeiculo {inspVeiculo}", Excecao = ex });

                inspVeiculo.Erro = true;
                inspVeiculo.MensagemErro = "Erro ao inserir dados do veículo, tente novamente mais tarde ou entre em contato com o suporte técnico";

                return inspVeiculo;
            }
        }

        /// <summary>
        /// Verifica se existe o veículo no banco de dados
        /// </summary>
        /// <param name="Inspecao_ID"></param>
        /// <param name="VIN_6"></param>
        /// <param name="configuracao"></param>
        /// <returns>Não existe = 0 | int>0 = Veiculo_ID | ERRO = -1 </returns>
        public static int Existe(int Inspecao_ID, string VIN_6, Configuracao configuracao)
        {
            Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"BLL.InspecaoVeiculo.Existe: Parametros: Inspecao_ID {Inspecao_ID},  VIN_6 {VIN_6}" });

            try
            {
                int inspVeiculo_ID = DAL.InspVeiculo.Existe(Inspecao_ID, VIN_6, configuracao);

                return inspVeiculo_ID;
            }

            catch
            {
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Erro, Mensagem = $"Erro ao realizar consulta: BLL.InspecaoVeiculo.Existe: Parametros: Inspecao_ID {Inspecao_ID},  VIN_6 {VIN_6}" });

                return -1;
            }
        }

        /// <summary>
        /// Realiza a listagem do inpsVeículo informando o seu ID
        /// </summary>
        /// <param name="inspVeiculo_ID">ID do veículo</param>
        /// <param name="configuracao"></param>
        /// <returns>Retorna um objeto contendo os valores do</returns>
        public static Models.InspVeiculo ListarPorId(int inspVeiculo_ID, Configuracao configuracao)
        {

            Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"BLL.InspecaoVeiculo.ListarPorId: Parametros: inspVeiculo_ID {inspVeiculo_ID}" });

            Models.InspVeiculo inspVeiculo = new Models.InspVeiculo();

            try
            {
                inspVeiculo = DAL.InspVeiculo.ListarPorId(inspVeiculo_ID, configuracao);
                return inspVeiculo;
            }

            catch (Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Erro, Mensagem = $"InspecaoVeículo - Erro ao ListarPorId", Excecao = ex });

                inspVeiculo.Erro = true;
                inspVeiculo.MensagemErro = "Erro ao consultar dados do veículo, tente novamente mais tarde ou entre em contato com o suporte técnico";

                return inspVeiculo;
            }
        }

        /// <summary>
        /// Realiza a listagem de todas as marcas do cliente_id informado
        /// </summary>
        /// <param name="Cliente_ID"></param>
        /// <param name="configuracao"></param>
        /// <returns>Objeto contendo todas as marcas</returns>
        public static List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> ListaMarca(int Cliente_ID, Configuracao configuracao)
        {

            Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"BLL.InspecaoVeiculo.ListaMarca: Parametros: Cliente_ID {Cliente_ID}" });

            List<Models.Marca> marcaList = DAL.Marca.Listar(Cliente_ID, configuracao);

            if (marcaList.FirstOrDefault().Erro == true)
            {
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"Erro ao listar Marcas - Cliente_ID{Cliente_ID}" });

                var selectListMarcaErro = new List<SelectListItem>();
                selectListMarcaErro.Add(new SelectListItem
                {
                    Text = "ERRO",
                    Value = 0.ToString()
                });

                return selectListMarcaErro;
            }

            var selectListMarca = new List<SelectListItem>(marcaList.Count + 1);

            foreach (var item in marcaList)
            {
                selectListMarca.Add(
                    new SelectListItem
                    {
                        Text = item.Nome,
                        Value = item.Marca_ID.ToString()
                    });
            }

            return selectListMarca;

        }

        /// <summary>
        /// Realiza a listagem de todas os modelos do cliente_id informado
        /// </summary>
        /// <param name="Cliente_ID"></param>
        /// <param name="configuracao"></param>
        /// <returns>Objeto contendo todas os modelos</returns>
        public static List<SelectListItem> ListaModelo(int Cliente_ID, Configuracao configuracao)
        {
            Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"BLL.InspecaoVeiculo.ListaModelo: Parametros: Cliente_ID {Cliente_ID}" });

            var modeloList = DAL.Modelo.Listar(Cliente_ID, configuracao);

            if (modeloList.Count() > 0)
            {
                if (modeloList.FirstOrDefault().Erro == true)
                {

                    Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"Erro ao listar Modelos: Cliente_ID {Cliente_ID}" });

                    var selectListModeloErro = new List<SelectListItem>();
                    selectListModeloErro.Add(new SelectListItem
                    {
                        Text = "ERRO",
                        Value = 0.ToString()
                    });

                    return selectListModeloErro;
                }
            }

            //Inicializa a lista de  Modelos, já inserindo o primeiro registro default
            var modeloSelectList = new List<SelectListItem>(modeloList.Count + 1);
            foreach (var item in modeloList)
            {
                modeloSelectList.Add(
                    new SelectListItem
                    {
                        Text = item.Nome,
                        Value = item.Modelo_ID.ToString()
                    });
            }

            return modeloSelectList;
        }


        /// <summary>
        /// Realiza a integração dos veículos no banco de dados;
        /// </summary>
        /// <param name="Cliente_ID"></param>
        /// <param name="LocalInspecao_ID"></param>
        /// <param name="configuracao"></param>
        /// <returns>Verdadeiro ou falso, caso tenha integrado corretamente</returns>
        public static List<Pendencia> IntegrarVIN(int Cliente_ID, int LocalInspecao_ID, int LocalCheckPoint_ID, DateTime DataInspecao, Configuracao configuracao)
        {

            Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"BLL.IntegrarVIN: Parametros: Cliente_ID {Cliente_ID}, LocalInspecao_ID {LocalInspecao_ID}, DataInspecao {DataInspecao}, Config {configuracao.ConnectionStringVDT}" });

            List<Pendencia> pendencias = new List<Pendencia>();

            try
            {
                pendencias = DAL.InspVeiculo.IntegrarVIN(Cliente_ID, LocalInspecao_ID, LocalCheckPoint_ID, DataInspecao, configuracao);
                return pendencias;

            }

            catch (Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Erro, Mensagem = $"Erro ao IntegrarVin", Excecao = ex });
                return pendencias;
            }
        }

        /// <summary>
        /// Remove o veículo do banco de dados.
        /// Este método só pode ser chamado caso não tenha avarias para o veículo
        /// </summary>
        /// <param name="veiculo_id"></param>
        /// <returns></returns>
        public static bool DeletarVeiculo(int veiculo_id, Configuracao configuracao)
        {
            Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"BLL.DeletarVeículo: Parametros: {veiculo_id}" });

            try
            {
                Models.InspVeiculo veiculo = DAL.InspVeiculo.ListarPorId(veiculo_id, configuracao);
                if (veiculo != null)
                {
                    //verificar se não existe avarias para este veículo
                    //var avarias = DAL.InspAvaria.Listar(1, veiculo.VIN_6, configuracao);

                    ////Caso não tenha avaria, faz o delete
                    //if (avarias.Count() == 0)
                    //{
                    //    Diag.Log.Grava(new Diag.LogItem
                    //    {
                    //        Nivel = Diag.Nivel.Informacao,
                    //        Mensagem = $"Não há avarias para o veículo, realizará então o delete : VIN_6 {veiculo.VIN_6}"
                    //    });
                    //}
                    //else
                    //{
                    //    Diag.Log.Grava(new Diag.LogItem
                    //    {
                    //        Nivel = Diag.Nivel.Informacao,
                    //        Mensagem = $"Não será possível realizar delete, constam {avarias.Count()} avarias para o veículo informado: {veiculo.VIN_6}, id {veiculo.InspVeiculo_ID}"
                    //    });
                    //    return false;
                    //}

                    return DAL.InspVeiculo.DeletarVeiculo(veiculo.InspVeiculo_ID, configuracao);

                }
                else
                {
                    Diag.Log.Grava(new Diag.LogItem { Nivel = Diag.Nivel.Informacao, Mensagem = $"Não foi possível consultar veículo informado - veiculo = null ID: {veiculo_id}" });

                }
                return true;

            }

            catch (Exception ex)
            {
                Diag.Log.Grava(new Diag.LogItem() { Nivel = Diag.Nivel.Erro, Mensagem = $"Erro ao deletar veículo", Excecao = ex });

                return false;
            }
        }
    }
}




