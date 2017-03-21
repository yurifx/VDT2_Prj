using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VDT2.ViewModels
    {
    public class ListarConferenciaAvariaViewModel
        {

        public List<Models.InspAvaria_Conf> listaInspAvaria_Conf { get; set; }
        public Models.InspAvaria_Conf InspAvaria_Conf { get; set; }
        public DateTime DataAvaria { get; set; }
        public string LocalInspecao { get; set; }
        public string LocalCheckPoint { get; set; }

        }
    }
