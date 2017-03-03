using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;

namespace VDT2.ViewModels
{
    public class InspecaoVeiculoViewModel
    {
        public Inspecao Inspecao;
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Marca { get; set; }
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Modelo { get; set; }
        
        //[StringLength(3, ErrorMessage = "Nao pode ter mais que 3 caracteres.")]
        public string Observacoes { get; set; }

        public int Inspecao_ID { get; set; }
        public int LocalInspecao_ID { get; set; }

        //esses dois, pegar do model Veiculo
        public int Marca_ID { get; set; }
        public int Modelo_ID { get; set; }

        public string VIN_6 { get; set; }

        //após inserção

        public int UltimoVeiculo_InspVeiculo_ID { get; set; }

        }
}
