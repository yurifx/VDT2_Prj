using System.ComponentModel.DataAnnotations;

namespace VDT2.Models
    {
    public class LocalCheckPoint
        {
        [Key]
        public int LocalCheckPoint_ID { get; set; }

        public int LocalInspecao_ID { get; set; }

        
        public string Codigo { get; set; }

        
        public string Nome_Pt { get; set; }

        
        public string Nome_En { get; set; }

        
        public string Nome_Es { get; set; }

        }
    }