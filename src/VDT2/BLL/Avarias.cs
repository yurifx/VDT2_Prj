using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;

namespace VDT2.BLL
    {
    public class Avarias
        {
        public static List<Models.InspAvaria> Listar(int cliente_ID, string VIN_6, Configuracao configuracao)
            {
            List<Models.InspAvaria> listaAvarias = new List<Models.InspAvaria>();
            try
                {
                listaAvarias = DAL.InspAvaria.Listar(cliente_ID, VIN_6, configuracao);
                return listaAvarias;
                }
            catch (Exception ex)
                {
                listaAvarias.Add(new Models.InspAvaria { Erro = true, MensagemErro = "Erro ao processar DAL - InspAvaria - Listar()" });
                return listaAvarias;
                }

            }

        public static Models.InspAvaria Inserir(Models.InspAvaria inspAvaria, Configuracao configuracao)
            {
            try
                {
                inspAvaria = DAL.InspAvaria.Inserir(inspAvaria, configuracao);
                return inspAvaria;
                }
            catch
                {
                inspAvaria.Erro = true;
                inspAvaria.MensagemErro = "Erro ao inserir avaria, tente novamente mais tarde ou entre em contato com o service desk";
                return inspAvaria;
                }
            }


        public static Models.InspAvaria Update(Models.InspAvaria inspAvaria, Configuracao configuracao)
            {
            try
                {
                inspAvaria = DAL.InspAvaria.Update(inspAvaria, configuracao);
                return inspAvaria;
                }
            catch
                {
                inspAvaria.Erro = true;
                inspAvaria.MensagemErro = "Erro ao realizar atualização da avaria, tente novamente mais tarde ou entre em contato com o suporte técnico";
                return inspAvaria;
                }

            }

        }
    }
