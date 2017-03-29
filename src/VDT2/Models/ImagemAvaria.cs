using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VDT2.Models
{
    public class ImagemAvaria
    {
        public string Imagem { get; set; }
        public string Path { get; set; }

        [NotMapped]
        public bool Erro { get; set; }

        [NotMapped]
        public string MensagemErro { get; set; }

        public ImagemAvaria()
        {
            MensagemErro = "";
            Imagem = "";
            Path = "";
        }
    }
}

