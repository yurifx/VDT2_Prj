using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;

namespace VDT2.BLL
    {
    public class InspecaoAvaria
        {
        public static Models.InspAvaria ListarPorId(int inspAvaria_ID, Configuracao configuracao)
            {
            Models.InspAvaria inspAvaria = new Models.InspAvaria();

            try
                {
                inspAvaria = DAL.InspAvaria.Listar(inspAvaria_ID, configuracao);
                return inspAvaria;
                }
            catch (Exception ex)
                {
                #region gravalogErro
                Diag.Log.Grava(
                    new Diag.LogItem()
                        {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar BLL - InsecaoAvaria - ListarPorId",
                        Excecao = ex
                        });
                #endregion
                //retorna erro
                inspAvaria.InspAvaria_ID = 0;
                inspAvaria.Erro = true;
                inspAvaria.MensagemErro = "Erro ao consultar dados da avaria, tente novamente mais tarde ou entre em contato com o suporte técnico";
                }
            return inspAvaria;
            }
        }
    }
