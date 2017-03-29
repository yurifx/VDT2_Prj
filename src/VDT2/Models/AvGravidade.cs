using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VDT2.Models
{
    public class AvGravidade
    {
        [Key]
        public int AvGravidade_ID { get; set; }
        public int Cliente_ID { get; set; }
        public string Codigo { get; set; }
        public string Nome_Pt { get; set; }
        public string Nome_En { get; set; }
        public string Nome_Es { get; set; }
        public bool Erro { get; set; }

        [NotMapped]
        public string MensagemErro { get; set; }

        public AvGravidade()
        {
            Codigo = "";
            Nome_Pt = "";
            Nome_En = "";
            Nome_Es = "";
            MensagemErro = "";

        }

    }
}
