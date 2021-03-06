﻿// <copyright file="ConferenciaLoadingViewModel.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Grupo Asserth</author>
// <email>yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>ViewModel de Conferencia Loading list  Index</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDT2.ViewModels
{
    public class ConferenciaLoadingListViewModel
    {
        public List<Models.Cliente> ListaCliente { get; set; }
        public List<Models.LocalInspecao> ListaLocalInspecao { get; set; }
        public List<Models.LocalCheckPoint> ListaLocalCheckPoint { get; set; }
        public int Cliente_ID { get; set; }
        public int LocalInspecao_ID { get; set; }
        public int LocalCheckPoint_ID { get; set; }
        public string Lote { get; set; }


        public string TextoLog()
        {
            StringBuilder sbLog = new StringBuilder("Action acionada: LoadingListSalvar | Parametros ");
            sbLog.Append($"  | Cliente_ID {this.Cliente_ID}");
            sbLog.Append($"  | LocalInspecao_ID {this.LocalInspecao_ID}");

            return sbLog.ToString();
        }
    }
}
