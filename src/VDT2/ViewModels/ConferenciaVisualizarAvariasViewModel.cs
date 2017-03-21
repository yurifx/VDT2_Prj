using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VDT2.ViewModels
{
    public class ConferenciaVisualizarAvariasViewModel
        {
        public Models.Inspecao Inspecao {get;set;}
        public Models.InspVeiculo InspVeiculo { get; set; }
        public Models.InspAvaria InspAvaria { get; set; }
        public List<Models.ImagemAvaria> ListaImagemAvarias { get; set; }

        }
}

