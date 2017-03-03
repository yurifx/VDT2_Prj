using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VDT2.Models
{
    public class Inspecao
    {
        [Key]
        public int Inspecao_ID { get; set; }

        public int Cliente_ID { get; set; }

        public int LocalInspecao_ID { get; set; }

        public int LocalCheckPoint_ID { get; set; }

        public int Transportador_ID { get; set; }

        public int FrotaViagem_ID { get; set; }

        public int? Navio_ID { get; set; }

        public int Usuario_ID { get; set; }

        }
    }
