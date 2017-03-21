using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VDT2.Models
{
    public class AvCondicao
    {
        [Key]
        public int AvCondicao_ID { get; set; }

        
        public string Codigo { get; set; }

        
        public string Nome_Pt { get; set; }

        public string Nome_En { get; set; }

        
        public string Nome_Es { get; set; }

        [NotMapped]
        public bool Erro { get; set; }

        [NotMapped]
        public string MensagemErro { get; set; }
        }
}
