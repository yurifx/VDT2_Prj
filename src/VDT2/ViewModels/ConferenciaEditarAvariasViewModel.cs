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
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> listaMarcas { get; set; }
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> listaModelos { get; set; }
        public List<Models.AvArea> listaAreas { get; set; }
        public List<Models.AvDano> listaDanos{ get; set; }
        public List<Models.AvCondicao> listaCondicoes { get; set; }
        public List<Models.AvGravidade> listaGravidades { get; set; }
        public List<Models.AvQuadrante> listaQuadrantes { get; set; }
        public List<Models.AvSeveridade> listaSeveridades { get; set; }
        }
}

