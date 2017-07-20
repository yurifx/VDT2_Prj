// <copyright file="Log.cs" company="Bureau Veritas">
// Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Amauri Rodrigues</author>
// <email>amauri.rodrigues@grupoasserth.com.br</email>
// <date>2017-02-19</date>
// <summary>Registro em arquivos de LOG</summary>

using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;

namespace VDT2.Diag
{
    /// <summary>
    /// Registro em arquivos de LOG
    /// </summary>
    public class Log
    {
        private static Object syncRoot = new Object(); // Implementing Singleton in C# : http://msdn2.microsoft.com/en-us/library/ms998558.aspx

        private static Object syncGravando = new Object(); // Implementing Singleton in C# : http://msdn2.microsoft.com/en-us/library/ms998558.aspx

        /// <summary>
        /// Instância da classe
        /// </summary>
        private static volatile Log instance = null;

        /// <summary>
        /// Instância da classe
        /// </summary>
        private static Log Instance {
            get {
                if (instance == null) {

                    lock (syncRoot) {

                        if (instance == null) {

                            instance = new Log();

                            instance.timerGravacao = new System.Threading.Timer(new System.Threading.TimerCallback(OnTimedEvent), null, 1000, 1000);
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Fila de itens para gravar no LOG
        /// </summary>
        private ConcurrentQueue<LogItem> itensGravar = null;

        /// <summary>
        /// Fila de itens para gravar no LOG
        /// </summary>
        public ConcurrentQueue<LogItem> ItensGravar {
            get {
                if (this.itensGravar == null) {
                    this.itensGravar = new ConcurrentQueue<LogItem>();
                }

                return this.itensGravar;
            }
        }

        /// <summary>
        /// Timer para descarregar a fila de itens no arquivo de LOG
        /// </summary>
        private System.Threading.Timer timerGravacao = null;

        /// <summary>
        /// Semáforo para evitar colisões durante a gravação no LOG
        /// </summary>
        private bool gravando = false;

        /// <summary>
        /// Caminho da pasta para gravar os arquivos de LOG (appsettings.json)
        /// </summary>
        public static string PastaLog { get; set; }

        /// <summary>
        /// Nível mínimo de itens de log para gravar (appsettings.json)
        /// </summary>
        public static string NivelLog { get; set; }

        /// <summary>
        /// Nível mínimo de itens de log para gravar (appsettings.json)
        /// </summary>
        private int menorNivel = -1;

        /// <summary>
        /// Nível mínimo de itens de log para gravar (appsettings.json)
        /// </summary>
        private int MenorNivel {
            get {
                if (Instance.menorNivel == -1) {

                    if (NivelLog.ToUpperInvariant() == Nivel.Critico.ToString().ToUpperInvariant()) {
                        Instance.menorNivel = (int)Nivel.Critico;
                    }
                    else if (NivelLog.ToUpperInvariant() == Nivel.Erro.ToString().ToUpperInvariant()) {
                        Instance.menorNivel = (int)Nivel.Erro;
                    }
                    else if (NivelLog.ToUpperInvariant() == Nivel.Aviso.ToString().ToUpperInvariant()) {
                        Instance.menorNivel = (int)Nivel.Aviso;
                    }
                    else if (NivelLog.ToUpperInvariant() == Nivel.Informacao.ToString().ToUpperInvariant()) {
                        Instance.menorNivel = (int)Nivel.Informacao;
                    }
                    else {
                        Instance.menorNivel = 0;
                    }
                }
                return Instance.menorNivel;
            }
        }

        /// <summary>
        /// Construtor default
        /// </summary>
        private Log() {
        }

        /// <summary>
        /// Grava um item na fila interna de LOG
        /// </summary>
        /// <param name="item"></param>
        public static void Grava(LogItem item,
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",

            [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",

            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0) {

            if ((int)item.Nivel >= Instance.MenorNivel) {

                // Monta o nome do arquivo-fonte (sem todo o caminho "C:\Clientes\BVeritas\VDT2\VDT2\src\VDT2\.cs")
                const string nomeProjeto = @"\VDT2\";

                int p = sourceFilePath.LastIndexOf(nomeProjeto);

                string arqFonte = string.Empty;

                if (p >= 0) {
                    arqFonte = sourceFilePath.Substring(p + nomeProjeto.Length);
                }
                else {
                    arqFonte = sourceFilePath;
                }

                // Monta a "origem" da mensagem 
                string origem = $"{memberName} - {arqFonte}:{sourceLineNumber}";

                item.Origem = origem;

                Instance.ItensGravar.Enqueue(item);
            }
        }

        /// <summary>
        /// Método acionado pelo timer, para iniciar a gravação no arquivo de LOG
        /// </summary>
        /// <param name="o"></param>
        private static void OnTimedEvent(object o){

            if (Instance.gravando) {
                return;
            }

            if (Instance.ItensGravar.Count == 0) {
                return;
            }

            lock (syncRoot) {

                if (Instance.gravando) {

                    return;
                }
                else {
                    lock (syncGravando) { // Bloqueia para gravação

                        Instance.gravando = true;

                        string linha = string.Empty;

                        try {
                            using (FileStream fs = new FileStream(
                                       Instance.NomeArqLog,
                                       FileMode.Append,
                                       FileAccess.Write,
                                       FileShare.Read)) {

                                Encoding enc = Encoding.GetEncoding(0); // Para permitir acentos

                                while (Instance.ItensGravar.Count > 0) {

                                    LogItem item = null;

                                    if (Instance.ItensGravar.TryDequeue(out item)) {

                                        linha = item.ToString();

                                        byte[] bytes = enc.GetBytes(linha);

                                        int qtdBytes = bytes.Length;

                                        fs.Write(bytes, 0, qtdBytes);
                                    }
                                }

                                fs.Flush();
                            }
                        }
                        catch (System.Exception ex) {

                            try {
                                StringBuilder sb = new StringBuilder(1024);

                                sb.AppendLine($"ERRO TENTANDO GRAVAR NO ARQUIVO DE LOG:{Instance.NomeArqLog}");
                                sb.AppendLine("TENTAVA GRAVAR:");
                                sb.AppendLine(linha);
                                sb.AppendLine("Exception:");
                                sb.AppendLine(ex.ToString());

                                using (FileStream fs = new FileStream(
                                    Instance.NomeArqErro,
                                    FileMode.Append,
                                    FileAccess.Write,
                                    FileShare.Read)) {

                                    Encoding enc = Encoding.GetEncoding(0); // Para permitir acentos

                                    byte[] bytes = enc.GetBytes(sb.ToString());

                                    int qtdBytes = bytes.Length;

                                    fs.Write(bytes, 0, qtdBytes);
                                    fs.Flush(true);
                                }
                            }
                            catch {
                                throw;
                            }
                        }
                        finally {
                            Instance.gravando = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Nome completo do arquivo de LOG (com o caminho)
        /// </summary>
        private string nomeArqLog = null;

        /// <summary>
        /// Nome completo do arquivo de LOG (com o caminho)
        /// </summary>
        private string NomeArqLog {
            get {
                if (Instance.nomeArqLog == null) {
                    MontaNomeArquivoLog();
                }

                return Instance.nomeArqLog;
            }
        }

        /// <summary>
        /// Monta o nome do arquivo de log
        /// </summary>
        private static void MontaNomeArquivoLog() {

            lock (Log.syncRoot) {

                // string pasta = Globais.PastaLog();

                string pasta = PastaLog;

                DateTime dataLog = DateTime.Today;

                // string data = Instance.dataLog.ToString("ddMMMyyyy", Globais.FormatoDatas).ToUpperInvariant();

                string data = dataLog.ToString("ddMMMyyyy").ToUpperInvariant();

                string nome = String.Format("VDT_{0}.log", data);

                if (Instance.nomeArqLog != Path.Combine(pasta, nome)) {

                    Instance.nomeArqLog = Path.Combine(pasta, nome);
                }
            }
        }

        /// <summary>
        /// Nome completo do arquivo de erros (quando não consegue gravar no LOG)
        /// </summary>
        private string nomeArqErro = null;

        /// <summary>
        /// Nome completo do arquivo de erros (quando não consegue gravar no LOG)
        /// </summary>
        private string NomeArqErro {
            get {
                if (Instance.nomeArqErro == null) {
                    MontaNomeArquivoErro();
                }

                return Instance.nomeArqErro;
            }
        }

        /// <summary>
        /// Monta o nome completo do arquivo de erros (quando não consegue gravar no LOG)
        /// </summary>
        private void MontaNomeArquivoErro() {
            lock (Log.syncRoot) {
                Instance.nomeArqErro = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "VDT.ERR");
            }
        }
    }
}
