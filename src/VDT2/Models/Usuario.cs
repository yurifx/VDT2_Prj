// <copyright file="Usuario.cs" company="Bureau Veritas">
// Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Amauri Rodrigues</author>
// <email>amauri.rodrigues@grupoasserth.com.br</email>
// <date>2017-02-19</date>
// <summary>Dados de um usuário</summary>

using System.ComponentModel.DataAnnotations; // [Key]
using System.ComponentModel.DataAnnotations.Schema;   // [NotMapped]

namespace VDT2.Models
{
    /// <summary>
    /// Dados de um usuário - retorno da procedure ChkLogin
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Identificação do usuário
        /// </summary>
        [Key]
        public int? Usuario_ID { get; set; }
        public short UsuarioPerfil_ID { get; set; }
        public string Login { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }

        public bool Inspetor { get; set; }
        public bool RequerTrocaSenha { get; set; }

        /// <summary>
        /// '*' ou lista dos clientes que os usuários têm acesso separados por pipe (|)
        /// </summary>
        public string Clientes { get; set; }

        /// <summary>
        /// '*' ou lista dos locais   que os usuários têm acesso separados por pipe (|) 
        /// </summary>
        public string Locais { get; set; }

        /// <summary>
        /// Nome do perfil
        /// </summary>
        public string UsuarioPerfilNome { get; set; }

        /// <summary>
        /// Usuários podem fazer a manutenção das tabelas de usuários e de perfis de acesso
        /// </summary>
        public bool Administrador { get; set; }

        /// <summary>
        /// Usuários podem acessar o módulo de inspeção
        /// </summary>
        public bool AcessaModuloInspecao { get; set; }

        /// <summary>
        /// Usuários podem acessar o módulo de back-office
        /// </summary>
        public bool AcessaModuloBackOffice { get; set; }

        /// <summary>
        /// Usuários podem acessar o módulo de consultas
        /// </summary>
        public bool AcessaModuloConsultas { get; set; }

        /// <summary>
        /// Usuários podem consultar dados das inspeções
        /// </summary>
        public bool ConsultaInspecao { get; set; }

        /// <summary>
        /// Usuários podem registrar(inserir) inspeções
        /// </summary>
        public bool RegistraInspecao { get; set; }

        /// <summary>
        /// Usuários podem modificar dados das inspeções
        /// </summary>
        public bool AlteraInspecao { get; set; }

        /// <summary>
        /// Usuários podem publicar inspeções
        /// </summary>
        public bool PublicaInspecao { get; set; }

        /// <summary>
        /// Usuários podem despublicar inspeções
        /// </summary>
        public bool DespublicaInspecao { get; set; }

        /// <summary>
        /// Usuários podem importar pack-lists e loading-lists
        /// </summary>
        public bool ImportaListas { get; set; }

        /// <summary>
        /// Usuários podem inserir novos escritórios
        /// </summary>
        public bool InsereEscritorios { get; set; }

        /// <summary>
        /// Usuários podem alterar escritórios
        /// </summary>
        public bool AlteraEscritorios { get; set; }

        /// <summary>
        /// Usuários podem inserir novos locais de inspeção
        /// </summary>
        public bool InsereLocais { get; set; }

        /// <summary>
        /// Usuários podem alterar locais
        /// </summary>
        public bool AlteraLocais { get; set; }

        /// <summary>
        /// Usuários podem inserir novos check-points
        /// </summary>
        public bool InsereCheckPoints { get; set; }

        /// <summary>
        /// Usuários podem alterar check-points
        /// </summary>
        public bool AlteraCheckPoints { get; set; }

        /// <summary>
        /// Usuários podem inserir novas marcas de veículos
        /// </summary>
        public bool InsereMarcas { get; set; }

        /// <summary>
        /// Usuários podem alterar marcas de veículos
        /// </summary>
        public bool AlteraMarcas { get; set; }

        /// <summary>
        /// Usuários podem inserir novos modelos de veículos
        /// </summary>
        public bool InsereModelos { get; set; }

        /// <summary>
        /// Usuários podem alterar modelos de veículos
        /// </summary>
        public bool AlteraModelos { get; set; }

        /// <summary>
        /// Usuários podem inserir linhas nas tabelas de áreas, danos, severidades, quadrantes, gravidades e condições de inspeção
        /// </summary>
        public bool InsereTabAvarias { get; set; }

        /// <summary>
        /// Usuários podem alterar áreas, danos, severidades, quadrantes, gravidades e condições de inspeção
        /// </summary>
        public bool InsereTransportadores { get; set; }

        /// <summary>
        /// Usuários podem alterar transportadores
        /// </summary>
        public bool AlteraTransportadores { get; set; }

        [NotMapped]
        public bool Autenticou { get; set; } = false;
    }
}
