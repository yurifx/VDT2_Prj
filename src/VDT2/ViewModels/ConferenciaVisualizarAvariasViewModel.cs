// <copyright file="ConferenciaVisualizarAvariasViewModel.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Grupo Asserth</author>
// <email>yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>ViewModel Conferencia Visualizar Avarias</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VDT2.ViewModels
{
    public class ConferenciaVisualizarAvariasViewModel
    {
        public Models.Inspecao Inspecao { get; set; }
        public Models.InspVeiculo InspVeiculo { get; set; }
        public Models.InspAvaria InspAvaria { get; set; }
        public List<Models.ImagemAvaria> ListaImagemAvarias { get; set; }


        public Models.Usuario Usuario { get; set; }
    }
}

