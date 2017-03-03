using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VDT2.Models;

namespace VDT2.DAL
    {
    public class NavioRepositorio
        {
        public static List<Navio> ListarTodos(VDT2.Models.Configuracao configuracao)
            {
            List<Navio> _listaNavio;
            

            string nomeStoredProcedure = "Navio_Lst";

            SqlParameter parmAtivos = new SqlParameter("@p_Ativos", SqlDbType.Bit)
                {
                Value = 0
                };

            SqlParameter[] parametros = new SqlParameter[]
            {
                parmAtivos

            };

            string chamada = $"{nomeStoredProcedure}  {parmAtivos.ParameterName}";
            try
                {
                using (var contexto = new GeralDbContext(configuracao))
                    {

                    //var _teste = contexto.Cliente.FromSql("Select * from Cliente").ToList();
                    _listaNavio = contexto.Navio.FromSql(chamada, parametros).ToList();


                    return _listaNavio;
                    }
                }
            catch (System.Exception ex)
                {

                Diag.Log.Grava(
                    new Diag.LogItem()
                        {
                        Nivel = Diag.Nivel.Erro,
                        Mensagem = $"Não conseguiu executar a procedure {nomeStoredProcedure}",
                        Excecao = ex
                        });
                throw;
                }

            }

        public static int Inserir(string _nomeNavio)
            {
            Configuracao configuracao = new Configuracao();
            configuracao.ConnectionStringVDT = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=VDT2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            configuracao.PastaUploadListas = "C\\TMP";

            string nomeStoredProcedure = "Navio_Ins";

            SqlParameter parmNome = new SqlParameter("@p_Nome", SqlDbType.VarChar)
                {
                Value = _nomeNavio
                };
            SqlParameter parmNavioId = new SqlParameter("@p_Navio_Id", SqlDbType.Int)
                {
                Value = 1,
                Direction = ParameterDirection.Output
                };


            SqlParameter[] parametros = new SqlParameter[]
            {
                parmNome,
                parmNavioId
            };

            string chamada = $"{nomeStoredProcedure} {parmNome.ParameterName}, {parmNavioId.ParameterName} out";

            using (var contexto = new GeralDbContext(configuracao))
                {

                //var _teste = contexto.Cliente.FromSql("Select * from Cliente").ToList();
                contexto.Database.ExecuteSqlCommand(chamada, parametros);
                //recebendo o retorno do parametro_id igual exemplo Amauri
                var _navioId = (int)parmNavioId.Value;

                return _navioId;
                }

            }

        public static int ConsultaIdNavio(string _navio, VDT2.Models.Configuracao configuracao)
            {
            int navioId = 0;
            if (_navio != null)
                {
                //Primeiro, verifica se existe este navio no bdd
                var navio_from_db = NavioRepositorio.ListarTodos(configuracao).Where(p => p.Nome == _navio).FirstOrDefault();

                if (navio_from_db == null) //caso nao tenha achado, insere este como novo.
                    {
                    //insere e recebe o ID do novo registro
                    navioId = NavioRepositorio.Inserir(_navio);
                    }
                else { navioId = navio_from_db.Navio_ID; } //caso exista, só pega o seu id
                }
            return navioId;
            }

        }



    }
