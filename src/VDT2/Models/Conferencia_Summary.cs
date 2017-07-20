using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VDT2.Models
{
    public class Conferencia_Summary
    {
        //[Key]
        //public int ID { get; set; }
        [Key]
        public string Tipo { get; set; }

        public int Total { get; set; }

        [NotMapped]
        public int Cliente_ID { get; set; }

        [NotMapped]
        public int LocalInspecao_ID { get; set; }

        [NotMapped]
        public int LocalCheckPoint_ID { get; set; }

        [NotMapped]
        public DateTime Data { get; set; }

    }
}
