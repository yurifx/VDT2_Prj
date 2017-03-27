using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VDT2.ViewModels
    {
    public class ConferenciaEditarAvariasViewModel
        {
        public Models.InspAvaria_Conf InspAvaria_Conf { get; set; } //não usado
        public Models.InspAvaria InspAvaria { get; set; }
        public Models.InspVeiculo InspVeiculo { get; set; }
        public Models.Inspecao Inspecao { get; set; }
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> ListaMarcas { get; set; }
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> ListaModelos { get; set; }
        public List<Models.AvArea> ListaAreas { get; set; }
        public List<Models.AvDano> ListaDanos{ get; set; }
        public List<Models.AvCondicao> ListaCondicoes { get; set; }
        public List<Models.AvGravidade> ListaGravidades { get; set; }
        public List<Models.AvQuadrante> ListaQuadrantes { get; set; }
        public List<Models.AvSeveridade> ListaSeveridades { get; set; }

        public Models.Usuario Usuario { get; set; }
        }
}

