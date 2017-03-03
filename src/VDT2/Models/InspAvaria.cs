using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VDT2.Models
{
    public class InspAvaria
    {
        [Key]
        public int InspAvaria_ID { get; set; }


        public int InspVeiculo_ID { get; set; }

        public int AvArea_ID { get; set; }

        public int AvDano_ID { get; set; }

        public int AvSeveridade_ID { get; set; }

        public int AvQuadrante_ID { get; set; }

        public int AvGravidade_ID { get; set; }

        public int AvCondicao_ID { get; set; }

        public string FabricaTransporte { get; set; }

        //[NotMapped]
        public int Inspecao_ID { get; set; }

        //[NotMapped]
        public int Cliente_ID { get; set; }

        //[NotMapped]
        public string Area_Pt { get; set; }
        
        //[NotMapped]
        public string Dano_Pt { get; set; }
        //[NotMapped]
        public string Severidade_Pt { get; set; }
        //[NotMapped]
        public string Gravidade_Pt { get; set; }
        //[NotMapped]
        public string Quadrante_Pt { get; set; }
        //[NotMapped]
        public string Condicao_Pt { get; set; }
        }
    }

