﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDT2.ViewModels
{
    public class ConferenciaPackingListViewModel
    {
        public List<Models.Cliente> ListaCliente { get; set; }
        public List<Models.LocalInspecao> ListaLocalInspecao { get; set; }
        public int Cliente_ID { get; set; }
        public int LocalInspecao_ID { get; set; }


        public string TextoLog()
        {
            StringBuilder sbLog = new StringBuilder("Action acionada: LoadingListSalvar | Parametros ");
            sbLog.Append($"  | Cliente_ID {this.Cliente_ID}");
            sbLog.Append($"  | LocalInspecao_ID {this.LocalInspecao_ID}");

            return sbLog.ToString();
        }

    }
}
