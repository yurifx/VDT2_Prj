﻿// <copyright file="Inspecao.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Grupo Asserth</author>
// <email>yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>Models de Inspecao</summary>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VDT2.Models
{
    public class InspVeiculo
    {
        [Key]
        public int InspVeiculo_ID { get; set; }

        public int Inspecao_ID { get; set; }

        public int Marca_ID { get; set; }

        public int Modelo_ID { get; set; }

        public string VIN_6 { get; set; }

        public string VIN { get; set; }

        public string Observacoes { get; set; }

        [NotMapped]
        public bool Erro { get; set; }

        [NotMapped]
        public string MensagemErro { get; set; }


        public InspVeiculo()
        {
            VIN_6 = "";
            VIN = "";
            Observacoes = "";
            MensagemErro = "";
        }
    }
}
