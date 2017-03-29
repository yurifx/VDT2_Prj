using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VDT2.Models
    {
    public class LocalInspecao
        {

        [Key]
        public int LocalInspecao_ID { get; set; }
        
        public string Nome { get; set; }

        public bool Ativo { get; set; }

        [NotMapped]
        public bool Erro { get; set; }

        [NotMapped]
        public string MensagemErro { get; set; }


        public LocalInspecao()
        {
            Nome = "";
            MensagemErro = "";
        }
        }
    }