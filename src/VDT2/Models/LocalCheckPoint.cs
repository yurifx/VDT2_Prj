// <copyright file="LocalCheckPoint.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Grupo Asserth</author>
// <email>yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>Models de Inspecao</summary>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VDT2.Models
{
    public class LocalCheckPoint
    {

        [Key]
        public int LocalCheckPoint_ID { get; set; }

        public int LocalInspecao_ID { get; set; }

        public string Codigo { get; set; }

        public string Nome_Pt { get; set; }

        public string Nome_En { get; set; }

        public string Nome_Es { get; set; }

        public string Operacao { get; set; }

        public string Tipo { get; set; }

        [NotMapped]
        public bool Erro { get; set; }

        [NotMapped]
        public string MensagemErro { get; set; }


        public LocalCheckPoint()
        {
            Codigo = "";
            Nome_Pt = "";
            Nome_En = "";
            Nome_Es = "";
            MensagemErro = "";
        }

    }
}