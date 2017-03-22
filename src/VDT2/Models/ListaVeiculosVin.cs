using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VDT2.Models
{
    public class ListaVeiculosVin
    {
        [Key]
        public int ListaVeiculosVin_ID { get; set; }
        public int ListaVeiculos_ID { get; set; }
        public string VIN_6 { get; set; }
        public string VIN { get; set; }

    }
}
