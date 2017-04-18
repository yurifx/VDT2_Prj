using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public Models.Cliente Cliente { get; set; }

        public List<Models.Transportador> ListaTransportador { get; set; }

        public string VIN_6 { get; set; }
        public List<int> Marca_ID { get; set; }
        public List<int> Modelo_ID { get; set; }
        public List<int> LocalInspecao_ID { get; set; }
        public List<int> LocalCheckPoint_ID { get; set; }
        public List<string> IdTipo { get; set; }
        public List<int> Area_ID { get; set; }
        public List<int> Condicao_ID { get; set; }
        public List<int> Dano_ID { get; set; }
        public List<int> Quadrante_ID { get; set; }
        public List<int> Gravidade_ID { get; set; }
        public List<int> Severidade_ID { get; set; }
        public string Fabrica { get; set; } = "";
        public string Transporte { get; set; } = "";
        public string DanoOrigem { get; set; } = "";
        public string TransportadorMaritimo { get; set; } = ""; 
        public string TransportadorTerrestre { get; set; } = "";
        public string FrotaViagem { get; set; } = "";
        public string NavioNome { get; set; } = "";
        public string Lote { get; set; } = "";

        public DateTime DataInicial {get;set;}
        public DateTime DataFinal { get; set; }
    }
}
