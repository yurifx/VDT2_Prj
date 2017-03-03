using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VDT2.Models
{
    public class Transportador
    {
        [Key]
        public int Transportador_ID { get; set; }

        public string Nome { get; set; }

        public string Tipo { get; set; }

        public bool Ativo { get; set; }
        
        public string IdTipo
            {
            get { return $"{Transportador_ID.ToString()}_{Tipo}"; }
            }
        }
}
