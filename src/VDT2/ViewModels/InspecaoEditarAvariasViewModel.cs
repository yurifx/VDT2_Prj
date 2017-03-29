using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDT2.BLL;
using VDT2.Models;

namespace VDT2.ViewModels
{
    public class InspecaoEditarAvariasViewModel
        {
        public Models.Inspecao Inspecao { get; set; }
        public Models.InspAvaria InspAvaria { get; set; }
        public Models.InspVeiculo InspVeiculo { get; set; }

        //Lista de dados da avaria
        public List<AvArea> avAreaLista;
        public List<AvCondicao> avCondicaoLista;
        public List<AvDano> avDanoLista;
        public List<AvGravidade> avGravidadeLista;
        public List<AvQuadrante> avQuadranteLista;
        public List<AvSeveridade> avSeveridadeLista;

        public int Inspecao_ID { get; set; }
        public int InspVeiculo_ID { get; set; }
        public int InspAvaria_ID { get; set; }
        public int Area_ID { get; set; }
        public int Condicao_ID { get; set; }
        public int Dano_ID { get; set; }
        public int Gravidade_ID { get; set; }
        public int Quadrante_ID { get; set; }
        public int Severidade_ID { get; set; }


        public List<ImagemAvaria> ImagemAvarias;

        public string Fabricatransporte { get; set; }

        public InspecaoEditarAvariasViewModel()
        {
            Fabricatransporte = "";
        }

        }
}
