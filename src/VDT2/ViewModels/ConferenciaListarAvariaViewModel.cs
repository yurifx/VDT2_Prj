// <copyright file="ConferenciaListarAvariaViewModel.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Grupo Asserth</author>
// <email>yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>ViewModel de Conferencia Listar Avaria  Index</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;

namespace VDT2.ViewModels
{
    public class ListarConferenciaAvariaViewModel
    {

        public List<Models.InspAvaria_Conf> ListaInspAvaria_Conf { get; set; }
        public Models.InspAvaria_Conf InspAvaria_Conf { get; set; }
        public DateTime DataAvaria { get; set; }
        public string LocalInspecao { get; set; }
        public string LocalCheckPoint { get; set; }
        public Models.Usuario Usuario { get; set; }
        public List<Pendencia> Pendencias { get; set; }
        public string ConcatInspecoes { get; set; }
    }
}
