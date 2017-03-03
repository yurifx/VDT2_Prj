using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VDT2.Models
{
    public class Navio
        {
        [Key]
        public int Navio_ID { get; set; }

        public string Nome { get; set; }

        public bool Ativo { get; set; }
        }
}
