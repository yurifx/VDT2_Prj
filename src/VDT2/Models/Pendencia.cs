using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VDT2.Models
{
    public class Pendencia
    {
        [Key]
        public string VIN_6 { get; set; }
        public string Tipo { get; set; }
    }
}
