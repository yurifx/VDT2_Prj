// <copyright file="ListaVeiculosVin.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Grupo Asserth</author>
// <email>yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>Models de Inspecao</summary>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VDT2.Models
{
    public class ListaVeiculosVin
    {
        [Key]
        public int ListaVeiculosVin_ID { get; set; }
        public int ListaVeiculos_ID { get; set; }
        public string VIN_6 { get; set; }
        public string VIN { get; set; }

        public ListaVeiculosVin()
        {
            VIN_6 = "";
            VIN = "";
        }
    }
}
