﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VDT2.Models
{
    public class AvArea
    {
        [Key]
        public int AvArea_ID { get; set; }

        public int Cliente_ID { get; set; }

        [StringLength(20)]
        public string Codigo { get; set; }

        [StringLength(152)]
        public string Nome_Pt { get; set; }

        [StringLength(152)]
        public string Nome_En { get; set; }

        [StringLength(152)]
        public string Nome_Es { get; set; }

        [NotMapped]
        public bool Erro { get; set; }

        [NotMapped]
        public string MensagemErro { get; set; }
        }
}
