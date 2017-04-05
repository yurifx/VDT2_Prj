using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDT2.DAL;

namespace VDT2.ViewModels
    {
    public class ConferenciaIndexViewModel
        {
        public List<InspAvaria> InspAvarias { get; set; }
        public List<InspAvaria> Inspecao { get; set; }
        
        public List<Models.LocalInspecao> ListaLocalInspecao { get; set; }
        public List<Models.LocalCheckPoint> ListaLocalCheckPoint { get; set; }
        public List<Models.Cliente> ListaCliente { get; set; }

        public DateTime Data { get; set; }
        public int LocalInspecao_ID { get; set; } 
        public int LocalCheckPoint_ID { get; set; }
        public int Cliente_ID { get; set; }


        public string TextoLog()
        {
            StringBuilder sbLog = new StringBuilder("", 100);
            sbLog.Append($"  | Cliente_ID: {this.Cliente_ID}");
            sbLog.Append($"  | LocalInspecao_ID: {this.LocalInspecao_ID}");
            sbLog.Append($"  | LocalCheckPoint: {this.LocalCheckPoint_ID}");
            sbLog.Append($"  | Data: {this.Data}");

            return sbLog.ToString();
        }

    }
    }
