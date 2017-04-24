// <copyright file="InspecaoVeiculoViewModel.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Grupo Asserth</author>
// <email>yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>ViewModel InspecaoVeiculoViewModel</summary>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;

namespace VDT2.ViewModels
{
    public class InspecaoVeiculoViewModel
    {
        public Inspecao Inspecao;
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Marca { get; set; }
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Modelo { get; set; }

        public string Observacoes { get; set; }

        public int Inspecao_ID { get; set; }
        public int InspVeiculo_ID { get; set; }

        public InspVeiculo InspVeiculo { get; set; }

        public int LocalInspecao_ID { get; set; }

        public int Marca_ID { get; set; }
        public int Modelo_ID { get; set; }
        public int Edicao { get; set; } //0 e 1 - 0 = não, 1 = sim

        public string VIN_6 { get; set; }


        public InspecaoVeiculoViewModel()
        {
            this.Marca = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            this.Modelo = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            this.Inspecao_ID = 0;
            this.InspVeiculo_ID = 0;

        }
    }


}
