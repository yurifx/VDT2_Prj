using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VDT2.Models
    {
    public class InspVeiculo
        {
        [Key]
        public int InspVeiculo_ID { get; set; }

        public int Inspecao_ID { get; set; }

        public int Marca_ID { get; set; }

        public int Modelo_ID { get; set; }

        public string VIN_6 { get; set; }

        public string VIN { get; set; }

        public string Observacoes { get; set; }

        }
    }
