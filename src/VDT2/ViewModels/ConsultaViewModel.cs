using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VDT2.ViewModels
{
    public class ConsultaViewModel
    {
        public List<Models.LocalInspecao> ListaLocalInspecao { get; set; }
        public List<Models.LocalCheckPoint> ListaLocalCheckPoint { get; set; }

        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> ListaMarca { get; set; }
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> ListaModelo { get; set; }

        public List<Models.AvArea> ListaArea { get; set; }
        public List<Models.AvCondicao> ListaCondicao { get; set; }
        public List<Models.AvDano> ListaDano { get; set; }
        public List<Models.AvQuadrante> ListaQuadrante { get; set; }
        public List<Models.AvGravidade> ListaGravidade { get; set; }
        public List<Models.AvSeveridade> ListaSeveridade { get; set; }

        public List<Models.Transportador> ListaTransportador { get; set; }

        public string VIN_6 { get; set; }
        public int Marca_ID { get; set; }
        public int Modelo_ID { get; set; }
        
    }
}
