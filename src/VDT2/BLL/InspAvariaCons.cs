using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDT2.Models;
using VDT2.ViewModels;

namespace VDT2.BLL
{
    public class InspAvariaCons
    {
        public static List<InspAvaria_Cons> ConsultarVeiculos(ConsultaViewModel consultaVM, Configuracao configuracao)
        {
            List<InspAvaria_Cons> listacons = new List<InspAvaria_Cons>();
            Models.InspAvaria_Cons inspAvaria_Cons = new Models.InspAvaria_Cons();
            inspAvaria_Cons = RecebeDadosUsuario(consultaVM);
            listacons = DAL.InspAvaria.Consultar(inspAvaria_Cons, configuracao);
            return listacons;

        }

        public static List<InspAvaria_Summary> ConsultarSumario(ConsultaViewModel consultaVM, Configuracao configuracao)
        {
            List<Models.InspAvaria_Summary> ListaSummary = new List<Models.InspAvaria_Summary>();
            try
            {
                Models.InspAvaria_Cons inspAvaria_Cons = new Models.InspAvaria_Cons();
                inspAvaria_Cons = RecebeDadosUsuario(consultaVM);
                ListaSummary = DAL.InspAvaria.ConsultarSummary(inspAvaria_Cons, configuracao);
                return ListaSummary;
            }
            catch
            {
                return ListaSummary;
            }

        }


        public static InspAvaria_Cons RecebeDadosUsuario(ConsultaViewModel consultaVM)
        {

            List<InspAvaria_Cons> listacons = new List<InspAvaria_Cons>();
            Models.InspAvaria_Cons inspAvaria_Cons = new Models.InspAvaria_Cons();

            //Cliente
            inspAvaria_Cons.Cliente_ID = consultaVM.Cliente.Cliente_ID;

            //Chassi
            inspAvaria_Cons.Chassi = consultaVM.VIN_6;


            //Local Inspecao
            if (consultaVM.LocalInspecao_ID != null)
            {
                if (consultaVM.LocalInspecao_ID.Count() > 0)
                {
                    inspAvaria_Cons.LocalInspecao = "|";
                    foreach (var item in consultaVM.LocalInspecao_ID)
                    {
                        inspAvaria_Cons.LocalInspecao += Convert.ToString(item) + "|";
                    }
                }
                else
                {
                    inspAvaria_Cons.LocalInspecao = "*";
                }

            }
            else
            {
                inspAvaria_Cons.LocalInspecao = "*";
            }

            //Local Checkpoint
            if (consultaVM.LocalCheckPoint_ID != null)
            {
                if (consultaVM.LocalCheckPoint_ID.Count() > 0)
                {
                    inspAvaria_Cons.LocalCheckPoint = "|";
                    foreach (var item in consultaVM.LocalCheckPoint_ID)
                    {
                        inspAvaria_Cons.LocalCheckPoint += Convert.ToString(item) + "|";
                    }
                }
                else
                {
                    inspAvaria_Cons.LocalCheckPoint = "*";
                }
            }
            else
            {
                inspAvaria_Cons.LocalCheckPoint = "*";
            }

            //Transportadores
            if (consultaVM.IdTipo != null)
            {
                if (consultaVM.IdTipo.Count() > 0)
                {
                    inspAvaria_Cons.Transportador = "|";

                    foreach (var item in consultaVM.IdTipo)
                    {
                        var transportador = item.Split('_')[0];
                        inspAvaria_Cons.Transportador += transportador + "|";
                    }
                }
                else
                {
                    inspAvaria_Cons.Transportador = "*";
                }
            }
            else
            {
                inspAvaria_Cons.Transportador = "*";
            }


            //Lote
            inspAvaria_Cons.Lote = consultaVM.Lote;


            //Marcas
            if (consultaVM.Marca_ID != null)
            {
                if (consultaVM.Marca_ID.Count() > 0)
                {
                    inspAvaria_Cons.Marca = "|";
                    foreach (var item in consultaVM.Marca_ID)
                    {
                        inspAvaria_Cons.Marca += Convert.ToString(item) + "|";
                    }
                }
                else
                {
                    inspAvaria_Cons.Marca = "*";
                }
            }
            else
            {
                inspAvaria_Cons.Marca = "*";
            }

            //Modelos
            if (consultaVM.Modelo_ID != null)
            {
                if (consultaVM.Modelo_ID.Count() > 0)
                {
                    inspAvaria_Cons.Modelo = "|";
                    foreach (var item in consultaVM.Modelo_ID)
                    {
                        inspAvaria_Cons.Modelo += Convert.ToString(item) + "|";
                    }
                }
                else
                {
                    inspAvaria_Cons.Modelo = "*";
                }
            }
            else
            {
                inspAvaria_Cons.Modelo = "*";
            }


            //Área
            if (consultaVM.Area_ID != null)
            {
                if (consultaVM.Area_ID.Count() > 0)
                {
                    inspAvaria_Cons.Area = "|";
                    foreach (var item in consultaVM.Area_ID)
                    {
                        inspAvaria_Cons.Area += Convert.ToString(item) + "|";
                    }
                }
                else
                {
                    inspAvaria_Cons.Area = "*";
                }
            }
            else
            {
                inspAvaria_Cons.Area = "*";
            }


            //Condicao
            if (consultaVM.Condicao_ID != null)
            {
                if (consultaVM.Condicao_ID.Count() > 0)
                {
                    inspAvaria_Cons.Condicao = "|";
                    foreach (var item in consultaVM.Condicao_ID)
                    {
                        inspAvaria_Cons.Condicao += Convert.ToString(item) + "|";
                    }
                }
                else
                {
                    inspAvaria_Cons.Condicao = "*";
                }
            }
            else
            {
                inspAvaria_Cons.Condicao = "*";
            }


            //Dano
            if (consultaVM.Dano_ID != null)
            {
                if (consultaVM.Dano_ID.Count() > 0)
                {
                    inspAvaria_Cons.Dano = "|";
                    foreach (var item in consultaVM.Dano_ID)
                    {
                        inspAvaria_Cons.Dano += Convert.ToString(item) + "|";
                    }
                }
                else
                {
                    inspAvaria_Cons.Dano = "*";
                }
            }
            else
            {
                inspAvaria_Cons.Dano = "*";
            }


            //Gravidade
            if (consultaVM.Gravidade_ID != null)
            {
                if (consultaVM.Gravidade_ID.Count() > 0)
                {
                    inspAvaria_Cons.Gravidade = "|";
                    foreach (var item in consultaVM.Gravidade_ID)
                    {
                        inspAvaria_Cons.Gravidade += Convert.ToString(item) + "|";
                    }
                }
                else
                {
                    inspAvaria_Cons.Gravidade = "*";
                }
            }
            else
            {
                inspAvaria_Cons.Gravidade = "*";
            }


            //Quadrante
            if (consultaVM.Quadrante_ID != null)
            {
                if (consultaVM.Quadrante_ID.Count() > 0)
                {
                    inspAvaria_Cons.Quadrante = "|";
                    foreach (var item in consultaVM.Quadrante_ID)
                    {
                        inspAvaria_Cons.Quadrante += Convert.ToString(item) + "|";
                    }
                }
                else
                {
                    inspAvaria_Cons.Quadrante = "*";
                }
            }
            else
            {
                inspAvaria_Cons.Quadrante = "*";
            }

            //Severidade
            if (consultaVM.Severidade_ID != null)
            {
                if (consultaVM.Severidade_ID.Count() > 0)
                {
                    inspAvaria_Cons.Severidade = "|";
                    foreach (var item in consultaVM.Severidade_ID)
                    {
                        inspAvaria_Cons.Severidade += Convert.ToString(item) + "|";
                    }
                }
                else
                {
                    inspAvaria_Cons.Severidade = "*";
                }
            }
            else
            {
                inspAvaria_Cons.Severidade = "*";
            }

            //TipoDano (Fábrica ou Transporte?) //Se enviou nulo, pega todos
            if (consultaVM.Fabrica == "" && consultaVM.Transporte == "")
            {
                inspAvaria_Cons.FabricaTransporte = "*";
            }

            else if (consultaVM.Fabrica == "on" && consultaVM.Transporte == "on")
            {
                inspAvaria_Cons.FabricaTransporte = "*";
            }
            else if (consultaVM.Transporte == "on")
            {
                inspAvaria_Cons.FabricaTransporte += "|T|";
            }
            else if (consultaVM.Fabrica == "on")
            {
                inspAvaria_Cons.FabricaTransporte += "|F|";
            }


            //Dano Origem?
            if (consultaVM.DanoOrigem == "on")
            {
                inspAvaria_Cons.DanoOrigem = "|1|";
            }
            else
            {
                inspAvaria_Cons.DanoOrigem = "*";
            }

            //Tipo Transportador | M | ou | T |  -> Marítimo ou Terrestre
            if (consultaVM.TransportadorMaritimo == "" && consultaVM.TransportadorTerrestre == "")
            {
                inspAvaria_Cons.TransportadorTipo = "*";
            }
            else if (consultaVM.TransportadorMaritimo == "on" && consultaVM.TransportadorTerrestre == "on")
            {
                inspAvaria_Cons.TransportadorTipo = "*";
            }
            else if (consultaVM.TransportadorMaritimo == "")
            {
                inspAvaria_Cons.TransportadorTipo = "|T|";
            }
            else
            {
                inspAvaria_Cons.TransportadorTipo = "|M|";
            }


            //FrotaViagem
            inspAvaria_Cons.FrotaViagem = consultaVM.FrotaViagem;


            //DATA INICIO
            inspAvaria_Cons.DataInicio = consultaVM.DataInicial;

            //DATA FINAL
            inspAvaria_Cons.DataFinal = consultaVM.DataFinal;


            //NAVIO
            inspAvaria_Cons.Navio = consultaVM.NavioNome;

            return inspAvaria_Cons;
        }
    }
}
