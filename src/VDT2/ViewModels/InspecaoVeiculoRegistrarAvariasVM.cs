﻿// <copyright file="InspecaoVeiculoRegistrarAvarias.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Grupo Asserth</author>
// <email>yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary> Inspecao Veiculo Registrar Avarias View Model</summary>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;

namespace VDT2.ViewModels
{
    public class InspecaoVeiculoRegistrarAvariasVM
    {
        public LoginViewModel dadosUsuario;
        public Inspecao Inspecao;
        public InspVeiculo InspVeiculo;


        //Lista de dados da avaria
        public List<AvArea> avAreaLista;
        public List<AvCondicao> avCondicaoLista;
        public List<AvDano> avDanoRepositorioLista;
        public List<AvGravidade> avGravidadeLista;
        public List<AvQuadrante> avQuadranteLista;
        public List<AvSeveridade> avSeveridadeLista;

        public InspAvaria InspAvaria;

        //Listar avarias
        public List<InspAvaria> listaAvarias;

        public int Inspecao_ID { get; set; }
        public int InspVeiculo_ID { get; set; }
        public int Area_ID { get; set; }
        public int Condicao_ID { get; set; }
        public int Dano_ID { get; set; }
        public int Gravidade_ID { get; set; }
        public int Quadrante_ID { get; set; }
        public int Severidade_ID { get; set; }


        //[Required]
        public string Fabricatransporte { get; set; }
        public string VIN_6 { get; set; }
        public int UltimoVeiculo_InspVeiculo_ID { get; set; }


        public InspecaoVeiculoRegistrarAvariasVM()
        {
            Fabricatransporte = "";
            VIN_6 = "";
        }
    }
}
