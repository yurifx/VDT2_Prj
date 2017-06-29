// <copyright file="InspecaoDadosGeraisViewModel.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Grupo Asserth</author>
// <email>yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>ViewModel de dados gerais da Inspecao</summary>


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;

namespace VDT2.ViewModels
{
    public class InspecaoDadosGeraisViewModel
    {
        public List<Cliente> ListaCliente;
        public List<Transportador> ListaTransportador;
        public List<LocalInspecao> ListaLocalInspecao;
        public List<LocalCheckPoint> ListaLocalCheckPoint;

        public DateTime Data { get; set; }
        public Inspecao Inspecao { get; set; }
        public InspVeiculo InspVeiculo { get; set; }
        public int Inspecao_ID { get; set; }
        public int Cliente_ID { get; set; }
        public int LocalInspecao_ID { get; set; }
        public int LocalCheckPoint_ID { get; set; }
        public string IdTipo { get; set; }
        public string FrotaViagemNome { get; set; }
        public string NomeNavio { get; set; }
        public int Transportador_ID { get; set; }
        public string TipoTransportador { get; set; }

        public FrotaViagem FrotaViagem { get; set; }
        public Transportador Transportador { get; set; }

        public int Edicao { get; set; } //0-não está editando  1- editando

        public bool Erro { get; set; }
        public string MensagemErro { get; set; }


        public InspecaoDadosGeraisViewModel()
        {
            IdTipo = "";
            FrotaViagemNome = "";
            NomeNavio = "";
            MensagemErro = "";
            Inspecao = new Models.Inspecao();
            InspVeiculo = new Models.InspVeiculo();
            FrotaViagem = new Models.FrotaViagem();
            Transportador = new Models.Transportador();
        }

    }
}

