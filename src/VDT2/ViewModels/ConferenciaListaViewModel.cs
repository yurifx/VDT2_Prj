// <copyright file="ConferenciaPackingListViewModel.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Grupo Asserth</author>
// <email>yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>ViewModel PackingList</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDT2.ViewModels
{
    public class ConferenciaListaViewModel
    {
        public List<Models.Cliente> ListaCliente { get; set; }
        public List<Models.LocalInspecao> ListaLocalInspecao { get; set; }
        public List<Models.LocalCheckPoint> ListaLocalCheckPoint { get; set; }
        public int Cliente_ID { get; set; }
        public int LocalInspecao_ID { get; set; }
        public int LocalCheckPoint_ID { get; set; }
        public string TipoLista { get; set; }
        public string Lote { get; set; }
        public DateTime DataLista { get; set; }

        public string TextoLog()
        {
            StringBuilder sbLog = new StringBuilder("Action acionada: LoadingListSalvar | Parametros ");
            sbLog.Append($"  | Cliente_ID {this.Cliente_ID}");
            sbLog.Append($"  | LocalInspecao_ID {this.LocalInspecao_ID}");
            sbLog.Append($"  | LocalCheckPoint_ID {this.LocalCheckPoint_ID}");
            sbLog.Append($"  | Lote {this.Lote}");
            sbLog.Append($"  | Tipo Lista {this.TipoLista}");

            return sbLog.ToString();
        }

    }
}