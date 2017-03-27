// <copyright file="LoginViewModel.cs" company="Bureau Veritas">
// Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Amauri Rodrigues</author>
// <email>amauri.rodrigues@grupoasserth.com.br</email>
// <date>2017-02-19</date>
// <summary>Dados para login</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VDT2.ViewModels
{
    /// <summary>
    /// Dados para login
    /// </summary>
    public class LoginViewModel
    {
        public bool Autenticado { get; set; }
        public string Identificacao { get; set; }
        public string Senha { get; set; }

        public string Nome { get; set; }

        public int UsuarioId { get; set; }

        public bool Inspetor { get; set; }

        /// <summary>
        /// Construtor da classe
        /// </summary>
        public LoginViewModel() {

            this.Autenticado = false;
        }

        public Models.Usuario Usuario { get; set; }
    }
}
