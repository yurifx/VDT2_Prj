// <copyright file="GeralDbContext.cs" company="Bureau Veritas">
// Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Amauri Rodrigues</author>
// <email>amauri.rodrigues@grupoasserth.com.br</email>
// <date>2017-02-19</date>
// <summary>Classe DbContext de uso geral</summary>

using Microsoft.EntityFrameworkCore;

namespace VDT2.DAL
{
    /// <summary>
    /// Classe DbContext de uso geral
    /// </summary>
    public class GeralDbContext : DbContext
    {
        /// <summary>
        /// String de conexão, carregada de appsettings.json
        /// </summary>
        private string connectionString = string.Empty;

        /// <summary>
        /// Dados de um usário - retorno da procedure ChkLogin
        /// </summary>
        public DbSet<Models.Usuario> Usuario { get; set; }
        public DbSet<Models.LocalInspecao> LocalInspecao { get; set; }
        public DbSet<Models.LocalCheckPoint> LocalCheckPoint { get; set; }
        public DbSet<Models.Transportador> Transportador { get; set; }
        public DbSet<Models.Cliente> Cliente { get; set; }
        public DbSet<Models.Inspecao> Inspecao { get; set; }
        public DbSet<Models.Navio> Navio { get; set; }
        public DbSet<Models.FrotaViagem> FrotaViagem { get; set; }
        public DbSet<Models.Marca> Marca { get; set; }
        public DbSet<Models.Modelo> Modelo { get; set; }
        public DbSet<Models.InspVeiculo> InspVeiculo { get; set; }
        public DbSet<Models.AvArea> AvArea { get; set; }
        public DbSet<Models.AvCondicao> AvCondicao { get; set; }
        public DbSet<Models.AvDano> AvDano { get; set; }
        public DbSet<Models.AvGravidade> AvGravidade { get; set; }
        public DbSet<Models.AvQuadrante> AvQuadrante { get; set; }
        public DbSet<Models.AvSeveridade> AvSeveridade { get; set; }
        public DbSet<Models.InspAvaria> InspAvaria { get; set; }
        public DbSet<Models.InspAvaria_Conf> InspAvaria_Conf { get; set; }
        public DbSet<Models.ListaVeiculos> ListaVeiculos { get; set; }
        public DbSet<Models.ListaVeiculosVin> ListaVeiculosVin { get; set; }
        public DbSet<Models.InspAvaria_Cons> InspAvaria_Cons { get; set; }
        
        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="configuracao">Configuração geral do aplicativo (para extrair a string de conexão)</param>
        public GeralDbContext(VDT2.Models.Configuracao configuracao)
            : base() {

            this.connectionString = configuracao.ConnectionStringVDT;
        }

        /// <summary>
        /// Associa a string de conexão ao objeto
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

            // Para evitar este erro quando tenta executar "contexto.Database.ExecuteSqlCommand(chamada, parametros);":
            // Unable to resolve service for type 'Microsoft.EntityFrameworkCore.Storage.IRawSqlCommandBuilder'. 
            // This is often because no database provider has been configured for this DbContext. 
            // A provider can be configured by overriding the DbContext.OnConfiguring method or 
            // by using AddDbContext on the application service provider. 
            // If AddDbContext is used, then also ensure that your DbContext type accepts
            // a DbContextOptions<TContext> object in its constructor and passes it to the 
            // base constructor for DbContext.
            optionsBuilder.UseSqlServer(this.connectionString);
        }
    }
}
