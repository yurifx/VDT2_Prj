using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VDT2.Models
{
    public class InspAvaria_Cons_Summary
    {

        public string Tipo { get; set; }
        public int Total { get; set; }

        [NotMapped]
        public int Cliente_ID { get; set; }

        [NotMapped]
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

    }

}
