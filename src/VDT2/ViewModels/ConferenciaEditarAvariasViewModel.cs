// <copyright file="ConferenciaEditarAvariasViewModel.cs" company="Bureau Veritas">
// Copyright (c) 2017 All Right Reserved
// </copyright>
// <author>Amauri Rodrigues & Yuri Vasconcelos</author>
// <email>amauri.rodrigues@grupoasserth.com.br | yuri.vasconcelos@grupoasserth.com.br</email>
// <date>2017-03-28</date>
// <summary>ViewModel Conferencia editar avarias</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public List<Models.AvDano> ListaDanos { get; set; }
        public List<Models.AvCondicao> ListaCondicoes { get; set; }
        public List<Models.AvGravidade> ListaGravidades { get; set; }
        public List<Models.AvQuadrante> ListaQuadrantes { get; set; }
        public List<Models.AvSeveridade> ListaSeveridades { get; set; }

        public Models.Usuario Usuario { get; set; }



        /// <summary>
        /// Metodo usado para extrair informações par ao log
        /// </summary>
        /// <returns>String contendo todas informações da classe</returns>
        public string TextoLog()
        {
            StringBuilder sbLog = new StringBuilder("Action acionada: SalvarAvaria - Parametros", 150);
            sbLog.Append($"  | InspAvaria_ID: {this.InspAvaria.InspAvaria_ID}");
            sbLog.Append($"  | Area_ID: {this.InspAvaria.AvArea_ID}");
            sbLog.Append($"  | Condicao_ID: {this.InspAvaria.AvCondicao_ID}");
            sbLog.Append($"  | Dano_ID: {this.InspAvaria.AvDano_ID}");
            sbLog.Append($"  | Gravidade_ID: {this.InspAvaria.AvGravidade_ID}");
            sbLog.Append($"  | Quadrante_ID: {this.InspAvaria.AvQuadrante_ID}");
            sbLog.Append($"  | Severidade_ID: {this.InspAvaria.AvSeveridade_ID}");
            sbLog.Append($"  | FabricaTransporte: {this.InspAvaria.FabricaTransporte}");

            return sbLog.ToString();
        }
    }
}

