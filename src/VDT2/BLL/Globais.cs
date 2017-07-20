// <copyright file="Globais.cs" company="Bureau Veritas">
// Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Amauri Rodrigues</author>
// <email>amauri.rodrigues@grupoasserth.com.br</email>
// <date>2017-02-19</date>
// <summary>Objetos globais do aplicativo</summary>

using System;
using System.Globalization;

namespace VDT2.BLL
{
    /// <summary>
    /// Classe que mantém os objetos globais do aplicativo (settings, versão do programa, etc.)
    /// <para>Uma única instância dessa classe será compartilhada entre todas as sessões</para>
    /// </summary>
    public sealed class Globais
    {
        /// <summary>
        /// Instância da classe
        /// </summary>
        private static volatile Globais instance = null;

        private static Object syncRoot = new Object(); // Implementing Singleton in C# : http://msdn2.microsoft.com/en-us/library/ms998558.aspx

        public const string NomeCookieAutenticacao = "VDT_AuthCookie";

        public const int ExpiracaoCookieAutenticacao = 1000;  // Minutos

        /// <summary>
        /// Formato padrão para datas (pt-BR)
        /// </summary>
        private DateTimeFormatInfo formatoDatas;

        /// <summary>
        /// Formato padrão para valores (pt-BR)
        /// </summary>
        private NumberFormatInfo formatoValores;

        public static DateTime DataMinimaSqlServer = new DateTime(1753, 1, 1);

        /// <summary>
        /// Construtor default
        /// </summary>
        private Globais() {
        }

        /// <summary>
        /// Formato padrão para datas (pt-BR)
        /// </summary>
        public static DateTimeFormatInfo FormatoDatas {
            get {
                if (instance.formatoDatas == null) {
                    instance.formatoDatas = new CultureInfo("pt-BR").DateTimeFormat;
                    instance.formatoDatas.ShortDatePattern = "dd/MM/yyyy";
                    instance.formatoDatas.ShortTimePattern = "HH:mm";
                    instance.formatoDatas.LongTimePattern = "HH:mm:ss";
                }

                return instance.formatoDatas;
            }
        }

        /// <summary>
        /// Formato padrão para valores (pt-BR)
        /// </summary>
        public static NumberFormatInfo FormatoValores {
            get {
                if (instance.formatoValores == null) {
                    instance.formatoValores = new CultureInfo("pt-BR").NumberFormat;
                }

                return instance.formatoValores;
            }
        }

        /// <summary>
        /// Instância da classe
        /// </summary>
        private static Globais Instance {
            get {
                if (instance == null) {
                    lock (syncRoot) {
                        if (instance == null)
                            instance = new Globais();
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Versão do programa, no formato v.r.m :
        /// <para>v = número da versão</para>
        /// <para>r = número do release</para>
        /// <para>m = modificação</para>
        /// </summary>
        private string versao;

        /// <summary>
        /// Versão do programa, no formato v.r.m :
        /// <para>v = número da versão</para>
        /// <para>r = número do release</para>
        /// <para>m = modificação</para>
        /// </summary>
        public static string Versao {
            get {
                if (Instance.versao == null || Instance.versao.Length == 0) {

                    // Obtém versão, release e modificação
                    string productVersion = Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationVersion;

                    Instance.versao = productVersion;
                }

                return Instance.versao;
            }
        }
    }
}
