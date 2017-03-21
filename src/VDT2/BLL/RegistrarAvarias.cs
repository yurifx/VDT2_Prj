using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;

namespace VDT2.BLL
    {
    public class RegistrarAvarias
        {

        public static List<Models.AvArea> ListarAreas(int cliente_ID, Configuracao configuracao)
            {
            List<Models.AvArea> listaAreas = new List<Models.AvArea>();
            try
                {
                listaAreas = DAL.AvArea.Listar(cliente_ID, configuracao);
                return listaAreas;
                }
            catch
                {
                listaAreas.Add(new AvArea { Erro = true, AvArea_ID = 0, Nome_Pt = "ERRO", MensagemErro = "Erro ao listar Areas" });
                return listaAreas;
                }
            }

        public static List<Models.AvCondicao> ListarCondicoes(int cliente_ID, Configuracao configuracao)
            {
            List<Models.AvCondicao> listaCondicoes = new List<Models.AvCondicao>();
            try
                {
                listaCondicoes = DAL.AvCondicao.Listar(cliente_ID, configuracao);
                return listaCondicoes;
                }
            catch
                {
                listaCondicoes.Add(new AvCondicao { Erro = true, Nome_Pt = "ERRO", MensagemErro = "Erro ao listar Condicoes", AvCondicao_ID = 0 });
                return listaCondicoes;
                }
            }

        public static List<Models.AvDano> ListarDanos(int cliente_ID, Configuracao configuracao)
            {
            List<Models.AvDano> listaDanos = new List<Models.AvDano>();
            try
                {
                listaDanos = DAL.AvDano.Listar(cliente_ID, configuracao);
                return listaDanos;
                }
            catch
                {
                listaDanos.Add(new AvDano { Erro = true, Nome_Pt = "ERRO", MensagemErro = "Erro ao listar Danos", AvDano_ID = 0 });
                return listaDanos;
                }
            }

        public static List<Models.AvGravidade> ListarGravidades(int cliente_ID, Configuracao configuracao)
            {
            List<Models.AvGravidade> listaGravidades = new List<Models.AvGravidade>();

            try
                {
                listaGravidades = DAL.AvGravidade.Listar(cliente_ID, configuracao);
                return listaGravidades;
                }
            catch
                {
                listaGravidades.Add(new AvGravidade { Erro = true, Nome_Pt = "ERRO", MensagemErro = "Erro ao listar Gravidades", AvGravidade_ID = 0 });
                return listaGravidades;
                }
            }

        public static List<Models.AvQuadrante> ListarQuadrantes(int cliente_ID, Configuracao configuracao)
            {
            List<Models.AvQuadrante> listaQuadrantes = new List<Models.AvQuadrante>();
            try
                {
                listaQuadrantes = DAL.AvQuadrante.Listar(cliente_ID, configuracao);
                return listaQuadrantes;
                }
            catch
                {
                listaQuadrantes.Add(new AvQuadrante { Erro = true, Nome_Pt = "ERRO", MensagemErro = "Erro ao listar Quadrantes", AvQuadrante_ID = 0 });
                return listaQuadrantes;
                }
            }

        public static List<Models.AvSeveridade> ListarSeveridades(int cliente_ID, Configuracao configuracao)
            {

            List<Models.AvSeveridade> listaSeveridades = new List<Models.AvSeveridade>();
            try
                {
                listaSeveridades = DAL.AvSeveridade.Listar(cliente_ID, configuracao);
                return listaSeveridades;
                }
            catch
                {
                listaSeveridades.Add(new AvSeveridade { Erro = true, Nome_Pt = "ERRO", MensagemErro = "Erro ao listar Severidades", AvSeveridade_ID = 0 });
                return listaSeveridades;
                }
            }

        }
    }
