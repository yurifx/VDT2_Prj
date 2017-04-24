using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;

namespace VDT2.ViewModels
{
    public class ConferenciaConsultaVeiculosViewModel
    {

        public List<Models.InspAvaria_Cons> ListaInspAvaria_Cons { get; set; }
        public List<Models.InspAvaria_Summary> ListaInspAvaria_Summary { get; set; }
        public int QuantidadeInspecionada { get; set; } //id = 1
        public int VeiculosSemAvaria { get; set; } // id = 2
        public int VeiculosComAvaria { get; set; } // id = 3
        public int QuantidadeAvarias { get; set; } // id = 4
        public int QuantidadeAvariasTransporte { get; set; } //id = 5
        public int QuantidadeAvariasFabrica { get; set; } //id = 6


        public decimal PercentualAvariado { get; set; }
        public decimal PercentualSemAvaria { get; set; }
    }
}

