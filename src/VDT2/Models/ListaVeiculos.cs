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

        public char Tipo { get; set; }

    }
}
