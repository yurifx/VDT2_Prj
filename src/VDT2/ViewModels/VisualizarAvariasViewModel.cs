using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;

namespace VDT2.ViewModels
{
    public class VisualizarAvariasViewModel
    {
        public int Inspecao_ID { get; set; }
        public int InspVeiculo_ID { get; set; }

        public List<InspAvaria> Avarias { get; set; }

        public string VIN_6 { get; set; }
        
        }
}
