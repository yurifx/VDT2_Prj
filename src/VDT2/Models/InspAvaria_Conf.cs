// <copyright file="InspAvaria_Conf.cs" company="Bureau Veritas">
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
    public class InspAvaria_Conf
    {

        

        [Key]
        public int RowID { get; set; }

        public DateTime Data { get; set; }

        public int? InspAvaria_ID { get; set; }

        public int Inspecao_ID { get; set; }

        public int InspVeiculo_ID { get; set; }

        public string VIN_6 { get; set; }

        public string VIN { get; set; }

        public int LocalCodigo { get; set; }

        public string LocalNome { get; set; }

        public int CheckPointCodigo { get; set; }

        public string CheckPointNome { get; set; }

        public int FrotaViagem_ID { get; set; }

        public string FrotaViagemNome { get; set; }

        public int? Navio_ID { get; set; }

        public string NavioNome { get; set; }

        public string Operacao { get; set; }

        public int MarcaCodigo { get; set; }

        public string MarcaNome { get; set; }

        public int ModeloCodigo { get; set; }

        public string ModeloNome { get; set; }

        public int? AreaCodigo { get; set; }

        public string Area_Pt { get; set; }

        public int? CondicaoCodigo { get; set; }

        public string Condicao_Pt { get; set; }

        public int? DanoCodigo { get; set; }

        public string Dano_Pt { get; set; }

        public int? GravidadeCodigo { get; set; }

        public string Gravidade_Pt { get; set; }

        public int? QuadranteCodigo { get; set; }

        public string Quadrante_Pt { get; set; }

        public int? SeveridadeCodigo { get; set; }

        public string Severidade_Pt { get; set; }

        public string FabricaTransporte { get; set; }

        public bool? DanoOrigem { get; set; }

        public int? HorasReparo { get; set; }

        public decimal? CustoReparo { get; set; }

        public bool? SubstituicaoPeca { get; set; }

        public decimal? ValorPeca { get; set; }

        public decimal? CustoTotal { get; set; }
        
        public int  TransportadorCodigo{ get; set; }
        
        public string TransportadorNome{ get; set; }
        
        public string TransportadorTipo{ get; set; }

        public int? LoteCodigo { get; set; }
        
        public string LoteNome { get; set; }



    }
}
