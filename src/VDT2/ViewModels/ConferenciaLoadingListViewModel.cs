using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VDT2.ViewModels
{
    public class ConferenciaLoadingListViewModel
    {
        public List<Models.Cliente> ListaCliente { get; set; }
        public List<Models.LocalInspecao> ListaLocalInspecao { get; set; }
        public int Cliente_ID { get; set; }
        public int LocalInspecao_ID { get; set; }
    }
}
