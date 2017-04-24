// <copyright file="InspAvaria.cs" company="Bureau Veritas">
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
    public class InspAvaria
    {
        [Key]
        public int InspAvaria_ID { get; set; }

        public int InspVeiculo_ID { get; set; }

        public int AvArea_ID { get; set; }

        public int AvDano_ID { get; set; }

        public int AvSeveridade_ID { get; set; }

        public int AvQuadrante_ID { get; set; }

        public int AvGravidade_ID { get; set; }

        public int AvCondicao_ID { get; set; }

        public string FabricaTransporte { get; set; }

        public Boolean DanoOrigem { get; set; }
        //Novo Campo - versão 0.0.3

        public decimal? Custo { get; set; }
        //Novo Campo - versão 0.0.4

        public int Inspecao_ID { get; set; }

        [NotMapped]
        public int Cliente_ID { get; set; }

        public string Area_Pt { get; set; }

        public string Dano_Pt { get; set; }

        public string Severidade_Pt { get; set; }

        public string Gravidade_Pt { get; set; }

        public string Quadrante_Pt { get; set; }

        public string Condicao_Pt { get; set; }

        [NotMapped]
        public bool Erro { get; set; }

        [NotMapped]
        public string MensagemErro { get; set; }

        public InspAvaria()
        {
            FabricaTransporte = "";
            Area_Pt = "";
            Dano_Pt = "";
            Severidade_Pt = "";
            Quadrante_Pt = "";
            Condicao_Pt = "";
            MensagemErro = "";
        }


    }
}

