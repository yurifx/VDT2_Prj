﻿// <copyright file="LocalInspecao.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Yuri Vasconcelos</author>
// <email>yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>Models de Inspecao</summary>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VDT2.Models
{
    public class LocalInspecao
    {

        [Key]
        public int LocalInspecao_ID { get; set; }

        public string Nome { get; set; }

        public bool Ativo { get; set; }

        [NotMapped]
        public bool Erro { get; set; }

        [NotMapped]
        public string MensagemErro { get; set; }


        public LocalInspecao()
        {
            Nome = "";
            MensagemErro = "";
        }
    }
}