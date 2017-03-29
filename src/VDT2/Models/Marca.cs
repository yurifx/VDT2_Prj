﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VDT2.Models
{
    public class Marca
    {
        [Key]
        public int Marca_ID { get; set; }

        public string Nome { get; set; }

        public bool Ativo { get; set; }

        [NotMapped]
        public bool Erro { get; set; }

        [NotMapped]
        public string MensagemErro { get; set; }

        public Marca()
        {
            Nome = "";
            MensagemErro = "";
        }
    }
}
