// <copyright file="VisualizarAvariasViewModel.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Grupo Asserth</author>
// <email>yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>ViewModel VisualizarAvariasViewModel</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;

namespace VDT2.ViewModels
{
    public class VisualizarAvariasViewModel
    {
        public int Inspecao_ID { get; set; }
        public int InspVeiculo_ID { get; set; }

        public List<InspAvaria> Avarias { get; set; }

        public string VIN_6 { get; set; }

    }
}
