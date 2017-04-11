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
        public int QuantidadeInspecionada { get; set; }
        public int VeiculosSemAvaria { get; set; }
        public int VeiculosComAvaria { get; set; }

        public decimal PercentualAvariado { get; set; }
        public decimal PercentualSemAvaria { get; set; }
    }
}

