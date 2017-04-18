using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDT2.Models
{
    public class InspAvaria_Cons
    {


        //Dados a serem enviados.
        public int Cliente_ID { get; set; }

        public string Chassi { get; set; }

        [NotMapped]
        public string LocalInspecao { get; set; }

        [NotMapped]
        public string LocalCheckPoint { get; set; }

        [NotMapped]
        public string Transportador { get; set; }

        [NotMapped]
        public string Lote { get; set; } 

        [NotMapped]
        public string Marca { get; set; }

        [NotMapped]
        public string Modelo { get; set; }

        [NotMapped]
        public string Area { get; set; }

        [NotMapped]
        public string Condicao { get; set; }

        [NotMapped]
        public string Dano { get; set; }

        [NotMapped]
        public string Quadrante { get; set; }

        [NotMapped]
        public string Gravidade { get; set; }

        [NotMapped]
        public string Severidade { get; set; }

        [NotMapped]
        public string TipoDefeito { get; set; }

        [NotMapped]
        public string DanoOrigem { get; set; }

        [NotMapped]
        public string TransportadorTipo { get; set; }

        [NotMapped]
        public string FrotaViagem { get; set; }

        [NotMapped]
        public string Navio { get; set; }

        [NotMapped]
        public DateTime DataInicio { get; set; }

        [NotMapped]
        public DateTime DataFinal { get; set; }


        //Dados a serem recebidos: Resultado da procedure
        [Key]
        public int RowID { get; set; }

        public DateTime Data { get; set; }

        public int Inspecao_ID { get; set; }

        public int? InspAvaria_ID { get; set; }

        public int LocalCodigo { get; set; }

        public string LocalNome { get; set; }

        public int CheckPointCodigo { get; set; }

        public string CheckPointNome { get; set; }

        public string TransportadorNome { get; set; }
        
        public int FrotaViagem_ID { get; set; }

        public string FrotaViagemNome { get; set; }

        public int? Navio_ID { get; set; }

        public string NavioNome { get; set; }

        public int InspVeiculo_ID { get; set; }

        public string FabricaTransporte { get; set; }

        public Decimal? Custo { get; set; }

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


        public string TextoLog()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($" | Cliente: {this.Cliente_ID}");
            sb.Append($" | Data Início: {this.DataInicio}");
            sb.Append($" | Data Final: {this.DataFinal}");
            sb.Append($" | Chassi: {this.Chassi}");
            sb.Append($" | Local: {this.LocalInspecao}");
            sb.Append($" | CheckPoint: {this.LocalCheckPoint}");
            sb.Append($" | Transportador: {this.Transportador}");
            sb.Append($" | Marca: {this.Marca}");
            sb.Append($" | Modelo: {this.Modelo}");
            sb.Append($" | Area: {this.Area}");
            sb.Append($" | Condição: {this.Condicao}");
            sb.Append($" | Dano: {this.Dano}");
            sb.Append($" | Gravidade: {this.Gravidade}");
            sb.Append($" | Quadrante: {this.Quadrante}");
            sb.Append($" | Severidade: {this.Severidade}");
            sb.Append($" | FabricaTransporte: {this.FabricaTransporte}");
            sb.Append($" | DanoOrigem: {this.FabricaTransporte}");
            sb.Append($" | Tipo Transportador: {this.TransportadorTipo}");
            sb.Append($" | Frota/Viagem: {this.FrotaViagem}");
            sb.Append($" | Navio: {this.FrotaViagem}");
            return sb.ToString();

        }
    }
}

