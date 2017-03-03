using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VDT2.Models
{
    public class AvGravidade
    {
        [Key]
        public int AvGravidade_ID { get; set; }

        public int Cliente_ID { get; set; }

        [StringLength(20)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome_Pt { get; set; }

        [StringLength(50)]
        public string Nome_En { get; set; }

        [StringLength(50)]
        public string Nome_Es { get; set; }

        //public bool Ativo { get; set; }

        }
    }
