using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;

namespace VDT2.ViewModels
    {
    public class InspecaoDadosGeraisViewModel
        {
        public List<Cliente> ListaCliente;
        public List<Transportador> ListaTransportador;
        public List<LocalInspecao> ListaLocalInspecao;
        public List<LocalCheckPoint> ListaLocalCheckPoint;


        public int Cliente_ID { get; set; }
        public int LocalInspecao_ID { get; set; }
        public int LocalCheckPoint_ID { get; set; }
        public string IdTipo { get; set; }
        public string FrotaViagem { get; set; }
        public string NomeNavio { get; set; }
        }
    }

