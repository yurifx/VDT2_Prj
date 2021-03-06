﻿// <copyright file="ListaVeiculos.cs" company="Bureau Veritas">
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
    public class ListaVeiculos
    {
        [Key]
        public int ListaVeiculo_ID { get; set; }

        public int Cliente_ID { get; set; }

        public int Usuario_ID { get; set; }

        public string NomeArquivo { get; set; }

        public DateTime DataHoraInclusao { get; set; }

        public int LocalInspecao_ID { get; set; }

        public int LocalCheckPoint_ID { get; set; }

        public string TipoLista { get; set; } //P - Packing, L- Loading, D- Discharging

        public string Lote { get; set; } = "";

        public int? Lote_ID { get; set; }

        public ListaVeiculos()
        {
            NomeArquivo = "";
        }

    }
}
