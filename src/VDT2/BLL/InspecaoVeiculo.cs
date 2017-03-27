using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;
using VDT2.ViewModels;

namespace VDT2.BLL
{
    public class InspecaoVeiculo
    {
        public static Models.InspVeiculo Update(InspVeiculo inspVeiculo, Configuracao configuracao)
        {
            try
            {
                inspVeiculo = DAL.InspVeiculo.Update(inspVeiculo, configuracao);
                return inspVeiculo;
            }
            catch
            {
                inspVeiculo.Erro = true;
                inspVeiculo.MensagemErro = "Erro ao atualizar dados do veículo, tente novamente mais tarde ou entre em contato com o suporte técnico";
                return inspVeiculo;
            }
        }

        public static Models.InspVeiculo Inserir(InspVeiculo inspVeiculo, Configuracao configuracao)
        {
            try
            {
                inspVeiculo = DAL.InspVeiculo.Inserir(inspVeiculo, configuracao);
                return inspVeiculo;
            }
            catch
            {
                inspVeiculo.Erro = true;
                inspVeiculo.MensagemErro = "Erro ao inserir dados do veículo, tente novamente mais tarde ou entre em contato com o suporte técnico";
                return inspVeiculo;
            }
        }

        /// <summary>
        /// Verifica se existe o veículo no banco de dados
        /// </summary>
        /// <param name="Inspecao_ID"></param>
        /// <param name="VIN_6"></param>
        /// <param name="configuracao"></param>
        /// <returns>Não existe = 0 | int>0 = Veiculo_ID | ERRO = -1 </returns>
        public static int Existe(int Inspecao_ID, string VIN_6, Configuracao configuracao)
        {
            try
            {
                int inspVeiculo_ID = DAL.InspVeiculo.Existe(Inspecao_ID, VIN_6, configuracao);
                return inspVeiculo_ID;
            }
            catch
            {
                return -1;
            }

        }

        public static Models.InspVeiculo ListarPorId(int inspVeiculo_ID, Configuracao configuracao)
        {
            Models.InspVeiculo inspVeiculo = DAL.InspVeiculo.ListarPorId(inspVeiculo_ID, configuracao);
            try
            {
                inspVeiculo = DAL.InspVeiculo.ListarPorId(inspVeiculo_ID, configuracao);
                return inspVeiculo;
            }
            catch
            {
                inspVeiculo.Erro = true;
                inspVeiculo.MensagemErro = "Erro ao consultar dados do veículo, tente novamente mais tarde ou entre em contato com o suporte técnico";
                return inspVeiculo;
            }
        }

        public static List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> ListaMarca(int Cliente_ID, Configuracao configuracao)
        {
            List<Models.Marca> marcaList = DAL.Marca.Listar(Cliente_ID, configuracao);

            if (marcaList.FirstOrDefault().Erro == true)
            {
                var selectListMarcaErro = new List<SelectListItem>();
                selectListMarcaErro.Add(new SelectListItem
                {
                    Text = "ERRO",
                    Value = 0.ToString()
                });

                return selectListMarcaErro;
            }


            var selectListMarca = new List<SelectListItem>(marcaList.Count + 1);
            selectListMarca.Add(new SelectListItem
            {
                Text = "Selecione a Marca",
                Value = 0.ToString()
            });

            foreach (var item in marcaList)
            {
                selectListMarca.Add(
                    new SelectListItem
                    {
                        Text = item.Nome,
                        Value = item.Marca_ID.ToString()
                    });
            }

            return selectListMarca;
        }

        public static List<SelectListItem> ListaModelo(int Cliente_ID, Configuracao configuracao)
        {
            var modeloList = DAL.Modelo.Listar(Cliente_ID, configuracao);
            if (modeloList.Count() > 0)
            {
                if (modeloList.FirstOrDefault().Erro == true)
                {
                    var selectListModeloErro = new List<SelectListItem>();
                    selectListModeloErro.Add(new SelectListItem
                    {
                        Text = "ERRO",
                        Value = 0.ToString()
                    });

                    return selectListModeloErro;
                }
            }

            //Inicializa a lista de  Modelos, já inserindo o primeiro registro default
            var modeloSelectList = new List<SelectListItem>(modeloList.Count + 1);
            modeloSelectList.Add(new SelectListItem
            {
                Text = "Selecione o Modelo",
                Value = 0.ToString()
            });

            foreach (var item in modeloList)
            {
                modeloSelectList.Add(
                    new SelectListItem
                    {
                        Text = item.Nome,
                        Value = item.Modelo_ID.ToString()
                    });
            }

            return modeloSelectList;
        }


        public static bool IntegrarVIN(int Cliente_ID, int LocalInspecao_ID, Configuracao configuracao)
        {
            try
            {
                DAL.InspVeiculo.IntegrarVIN(Cliente_ID, LocalInspecao_ID, configuracao);
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }

    }
}




