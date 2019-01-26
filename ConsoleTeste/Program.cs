using DadosExporter.Exporter;
using DadosExporter.Models;
using log4net;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleTeste
{
    class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
              //LogManager.GetLogger("EventLogAppender");
        static void Main(string[] args)
        {
            /*ISchedulerFactory schedFact = new StdSchedulerFactory();
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();
            new Configurer().ConfigurarRelatorios(sched);*/
            Log.Info("Started");
            Console.ReadKey();
        }

        public void OldMain()
        {
            Console.WriteLine(DateTime.Now.ToString("ddMMyyyyHHmmss"));
            string cron = $@"{DateTime.Now.Second} {DateTime.Now.Minute} {DateTime.Now.Hour} " +
                          $@"{DateTime.Now.Day} {DateTime.Now.Month} ? {DateTime.Now.Year}";
            Console.WriteLine(cron);
            Console.ReadKey();

            string ttt = ConfigurationManager.AppSettings.Get("ArquivoConfiguracaoRelatorio");
            List<Relatorio> relatorios = new List<Relatorio>();
            XElement relatoriosConfig = XElement.Load("RelatoriosConfig.xml");
            foreach (var node in relatoriosConfig.Elements())
            {
                var nome = node.Attribute("nome").Value;
                var data = node.Attribute("data").Value;
                var hora = node.Attribute("hora").Value;
                var arquivoOrigem = node.Attribute("filePath").Value;
                var destino = node.Attribute("pathDestino").Value;
                Relatorio r = new Relatorio(nome, DateTime.Parse(data, new CultureInfo("pt-BR")),
                    DateTime.Parse(hora, new CultureInfo("pt-BR")), arquivoOrigem, destino);
                relatorios.Add(r);
            }
        }
    }
}
