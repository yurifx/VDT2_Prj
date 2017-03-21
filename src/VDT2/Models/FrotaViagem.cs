using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VDT2.Models
{
    public class FrotaViagem
    {
        [Key]
        public int FrotaViagem_ID { get; set; }

        public int Transportador_ID { get; set; }

        public string Nome { get; set; }

        public bool Ativo { get; set; }


        [NotMapped]
        public bool Erro { get; set; }

        [NotMapped]
        public string MensagemErro { get; set; }
        }
    }
