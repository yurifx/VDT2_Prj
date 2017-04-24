// <copyright file="AvCondicao.cs" company="Bureau Veritas">
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
    public class AvCondicao
    {
        [Key]
        public int AvCondicao_ID { get; set; }

        public string Codigo { get; set; }

        public string Nome_Pt { get; set; }

        public string Nome_En { get; set; }

        public string Nome_Es { get; set; }

        [NotMapped]
        public bool Erro { get; set; }

        [NotMapped]
        public string MensagemErro { get; set; }

        public AvCondicao()
        {
            Codigo = "";
            Nome_Pt = "";
            Nome_En = "";
            Nome_Es = "";
            MensagemErro = "";
        }
    }
}
