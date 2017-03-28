﻿// <copyright file="Avarias.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Amauri Rodrigues & Yuri Vasconcelos</author>
// <email>amauri.rodrigues@grupoasserth.com.br | yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>Camada de negócios referentes a Avarias</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;

/// <summary>
/// Camada de negócios - Avarias
/// </summary>

namespace VDT2.BLL
{
    public class Avarias
    {
        /// <summary>
        /// Lista avarias do veículo informado
        /// </summary>
        /// <param name="cliente_ID"></param>
        /// <param name="VIN_6">Chassi com 6 dígitos</param>
        /// <param name="configuracao"></param>
        /// <returns>Lista de avarias</returns>
        public static List<Models.InspAvaria> Listar(int cliente_ID, string VIN_6, Configuracao configuracao)
        {
            List<Models.InspAvaria> listaAvarias = new List<Models.InspAvaria>();
            try
            {
                listaAvarias = DAL.InspAvaria.Listar(cliente_ID, VIN_6, configuracao);
                return listaAvarias;
            }
            catch
            {
                listaAvarias.Add(new Models.InspAvaria { Erro = true, MensagemErro = "Erro ao processar DAL - InspAvaria - Listar()" });
                return listaAvarias;
            }

        }

        /// <summary>
        /// Insere uma nova avaria referente ao veículo informado
        /// </summary>
        /// <param name="inspAvaria">Model de dados da avaria</param>
        /// <param name="configuracao"></param>
        /// <returns>Retorna a avaria informada</returns>
        public static Models.InspAvaria Inserir(Models.InspAvaria inspAvaria, Configuracao configuracao)
        {
            try
            {
                inspAvaria = DAL.InspAvaria.Inserir(inspAvaria, configuracao);
                return inspAvaria;
            }
            catch
            {
                inspAvaria.Erro = true;
                inspAvaria.MensagemErro = "Erro ao inserir avaria, tente novamente mais tarde ou entre em contato com o service desk";
                return inspAvaria;
            }
        }

        /// <summary>
        /// Atualiza os dados da avaria. 
        /// </summary>
        /// <param name="inspAvaria">Model com os dados da avaria a ser atualizada</param>
        /// <param name="configuracao"></param>
        /// <returns>Avaria informada</returns>
        public static Models.InspAvaria Update(Models.InspAvaria inspAvaria, Configuracao configuracao)
        {
            try
            {
                inspAvaria = DAL.InspAvaria.Update(inspAvaria, configuracao);
                return inspAvaria;
            }
            catch
            {
                inspAvaria.Erro = true;
                inspAvaria.MensagemErro = "Erro ao realizar atualização da avaria, tente novamente mais tarde ou entre em contato com o suporte técnico";
                return inspAvaria;
            }

        }

        /// <summary>
        /// Realiza listagem de áreas pertinentes ao cliente
        /// </summary>
        /// <param name="cliente_ID">Id do cliente</param>
        /// <param name="configuracao"></param>
        /// <returns>Lista de áreas</returns>
        public static List<Models.AvArea> ListarAreas(int cliente_ID, Configuracao configuracao)
        {
            List<Models.AvArea> listaAreas = new List<Models.AvArea>();
            try
            {
                listaAreas = DAL.AvArea.Listar(cliente_ID, configuracao);
                return listaAreas;
            }
            catch
            {
                listaAreas.Add(new AvArea { Erro = true, Nome_Pt = "ERRO", MensagemErro = "Erro ao listar Areas", AvArea_ID = 0 });
                return listaAreas;
            }
        }


        /// <summary>
        /// Realiza listagem de condicoes pertinentes ao cliente
        /// </summary>
        /// <param name="cliente_ID">Id do cliente</param>
        /// <param name="configuracao"></param>
        /// <returns>Lista de condicoes</returns>
        public static List<Models.AvCondicao> ListarCondicoes(int cliente_ID, Configuracao configuracao)
        {
            List<Models.AvCondicao> listaCondicoes = new List<Models.AvCondicao>();
            try
            {
                listaCondicoes = DAL.AvCondicao.Listar(cliente_ID, configuracao);
                return listaCondicoes;
            }
            catch
            {
                listaCondicoes.Add(new AvCondicao { Erro = true, Nome_Pt = "ERRO", MensagemErro = "Erro ao listar Condicoes", AvCondicao_ID = 0 });
                return listaCondicoes;
            }
        }


        /// <summary>
        /// Realiza listagem de danos pertinentes ao cliente
        /// </summary>
        /// <param name="cliente_ID">Id do cliente</param>
        /// <param name="configuracao"></param>
        /// <returns>Lista de danos</returns>
        public static List<Models.AvDano> ListarDanos(int cliente_ID, Configuracao configuracao)
        {
            List<Models.AvDano> listaDanos = new List<Models.AvDano>();
            try
            {
                listaDanos = DAL.AvDano.Listar(cliente_ID, configuracao);
                return listaDanos;
            }
            catch
            {
                listaDanos.Add(new AvDano { Erro = true, Nome_Pt = "ERRO", MensagemErro = "Erro ao listar Danos", AvDano_ID = 0 });
                return listaDanos;
            }
        }

        /// <summary>
        /// Realiza listagem de gravidades pertinentes ao cliente
        /// </summary>
        /// <param name="cliente_ID">Id do cliente</param>
        /// <param name="configuracao"></param>
        /// <returns>Lista de gravidades</returns>
        public static List<Models.AvGravidade> ListarGravidades(int cliente_ID, Configuracao configuracao)
        {
            List<Models.AvGravidade> listaGravidades = new List<Models.AvGravidade>();

            try
            {
                listaGravidades = DAL.AvGravidade.Listar(cliente_ID, configuracao);
                return listaGravidades;
            }
            catch
            {
                listaGravidades.Add(new AvGravidade { Erro = true, Nome_Pt = "ERRO", MensagemErro = "Erro ao listar Gravidades", AvGravidade_ID = 0 });
                return listaGravidades;
            }
        }

        /// <summary>
        /// Realiza listagem de quadrantes pertinentes ao cliente
        /// </summary>
        /// <param name="cliente_ID">Id do cliente</param>
        /// <param name="configuracao"></param>
        /// <returns>Lista de quadrantes</returns>
        public static List<Models.AvQuadrante> ListarQuadrantes(int cliente_ID, Configuracao configuracao)
        {
            List<Models.AvQuadrante> listaQuadrantes = new List<Models.AvQuadrante>();
            try
            {
                listaQuadrantes = DAL.AvQuadrante.Listar(cliente_ID, configuracao);
                return listaQuadrantes;
            }
            catch
            {
                listaQuadrantes.Add(new AvQuadrante { Erro = true, Nome_Pt = "ERRO", MensagemErro = "Erro ao listar Quadrantes", AvQuadrante_ID = 0 });
                return listaQuadrantes;
            }
        }

        /// <summary>
        /// Realiza listagem de severidades pertinentes ao cliente
        /// </summary>
        /// <param name="cliente_ID">Id do cliente</param>
        /// <param name="configuracao"></param>
        /// <returns>Lista de severidades</returns>
        public static List<Models.AvSeveridade> ListarSeveridades(int cliente_ID, Configuracao configuracao)
        {

            List<Models.AvSeveridade> listaSeveridades = new List<Models.AvSeveridade>();
            try
            {
                listaSeveridades = DAL.AvSeveridade.Listar(cliente_ID, configuracao);
                return listaSeveridades;
            }
            catch
            {
                listaSeveridades.Add(new AvSeveridade { Erro = true, Nome_Pt = "ERRO", MensagemErro = "Erro ao listar Severidades", AvSeveridade_ID = 0 });
                return listaSeveridades;
            }
        }

        /// <summary>
        /// Lista todos os dados de uma avaria
        /// </summary>
        /// <param name="inspAvaria_ID">ID da avaria</param>
        /// <param name="configuracao"></param>
        /// <returns>Model contendo os dados da avaria do ID informado</returns>
        public static Models.InspAvaria ListarPorId(int inspAvaria_ID, Configuracao configuracao)
        {
            Models.InspAvaria inspAvaria = new Models.InspAvaria();

            try
            {
                inspAvaria = DAL.InspAvaria.Listar(inspAvaria_ID, configuracao);
                return inspAvaria;
            }
            catch (Exception ex)
            {

                #region gravalogErro
                Diag.Log.Grava(
                    new Diag.LogItem()
                    {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar BLL - InsecaoAvaria - ListarPorId",
                        Excecao = ex
                    });
                #endregion

                inspAvaria.InspAvaria_ID = 0;
                inspAvaria.Erro = true;
                inspAvaria.MensagemErro = "Erro ao consultar dados da avaria, tente novamente mais tarde ou entre em contato com o suporte técnico";
            }
            return inspAvaria;
        }

    }
}
