using System.ComponentModel.DataAnnotations;

namespace VDT2.Models
    {
    public class LocalInspecao
        {

        [Key]
        public int LocalInspecao_ID { get; set; }

        
        public string Nome { get; set; }

        public bool Ativo { get; set; }

        }
    }