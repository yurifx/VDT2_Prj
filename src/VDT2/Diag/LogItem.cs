// <copyright file="LogItem.cs" company="Bureau Veritas">
// Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Amauri Rodrigues</author>
// <email>amauri.rodrigues@grupoasserth.com.br</email>
// <date>2017-02-19</date>
// <summary>Um item de LOG</summary>

using System;

namespace VDT2.Diag
{
    /// <summary>
    /// Um item de LOG
    /// </summary>
    public class LogItem
    {
        /// <summary>
        /// Nível do item
        /// </summary>
        public Nivel Nivel { get; set; }

        // public DateTime DataHora { get; set; } = DateTime.Now;

        /// <summary>
        /// Mensagem que será gravada no arquivo de LOG
        /// </summary>
        public string Mensagem { get; set; }

        /// <summary>
        /// Exception, se houver
        /// </summary>
        public System.Exception Excecao { get; set; } = null;

        /// <summary>
        /// Origem - Namespace, classe e método
        /// </summary>
        public string Origem { get; set; }

        /// <summary>
        /// Item de log formatado
        /// <para>
        /// Exemplo:
        /// [30/07/2016 15:55:32 - V 1.0.27 - ERRO] Erro tentando calcular [Areas.Controle.BLL.LiberacaoVisitas.MontaGrupos]
        /// </para>
        /// </summary>
        /// <returns>Item formatado</returns>
        public override string ToString() {

            string textoExcecao = string.Empty;

            if (this.Excecao != null) {
                textoExcecao = Excecao.ToString();
            }

            string natureza = string.Empty;

            switch (this.Nivel) {

                case Nivel.Informacao:
                    natureza = "info";
                    break;

                case Nivel.Aviso:
                    natureza = "aviso";
                    break;

                case Nivel.Erro:
                    natureza = "ERRO";
                    break;

                case Nivel.Critico:
                    natureza = "CRITICO";
                    break;

                default:
                    natureza = "????";
                    break;
            }

            string versao = BLL.Globais.Versao;

            string prefixo = string.Concat(
                "[",
                DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                " - V ",
                versao,
                " - ",
                natureza,
                "] ");

            string sufixo = string.Concat(
                " [",
                this.Origem,
                "]");

            int tamanhoEstimado =
                prefixo.Length +
                Mensagem.Length +
                textoExcecao.Length +
                sufixo.Length +
                4;

            System.Text.StringBuilder sb = new System.Text.StringBuilder(tamanhoEstimado);

            sb.Append(prefixo);
            sb.Append(Mensagem);
            sb.Append(sufixo);

            if (this.Excecao != null) {
                sb.AppendLine();
                sb.Append(textoExcecao);
            }

            sb.Append(System.Environment.NewLine);

            return sb.ToString();
        }
    }
}
