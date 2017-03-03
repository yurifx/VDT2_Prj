using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        //[Required]
        //[StringLength(50)]
        //public string Area_Pt { get; set; }

        //[StringLength(50)]
        //public string Local_Pt { get; set; }

        //[StringLength(50)]
        //public string Lado_Pt { get; set; }

        ////[StringLength(50)]
        ////public string Area_En { get; set; }

        //[StringLength(50)]
        //public string Local_En { get; set; }

        //[StringLength(50)]
        //public string Lado_En { get; set; }

        //[StringLength(50)]
        //public string Area_Es { get; set; }

        //[StringLength(50)]
        //public string Local_Es { get; set; }

        //[StringLength(50)]
        //public string Lado_Es { get; set; }

        [StringLength(152)]
        public string Nome_Pt { get; set; }

        [StringLength(152)]
        public string Nome_En { get; set; }

        [StringLength(152)]
        public string Nome_Es { get; set; }

        //public bool Ativo { get; set; }
        }
}
