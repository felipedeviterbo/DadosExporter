using DadosExporter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Configuration;
using System.IO;
using DadosExporter.Types.Exceptions;
using System.Globalization;
using Quartz;
using DadosExporter.Repositorios;

namespace DadosExporter.Exporter
{
    public class Configurer
    {
        private RelatoriosRepository _RelatoriosRepository { get; set; }
        public Configurer()
        {
            _RelatoriosRepository = new RelatoriosRepository();
        }
        public void ConfigurarRelatorios(IScheduler scheduler)
        {
            //ArquivoConfiguracaoRelatorio
            ValidarArquivoConfiguracaoRelatorios();
            //Ler Relatorios
            List<Relatorio> relatorios = _RelatoriosRepository.GetTodosRelatorios();
            if (relatorios.Count() == 0) return;
            //CriarRelatorios
            
            foreach (var relatorio in relatorios)
            {
                if (ValidarRelatorio(relatorio))
                {
                    IJobDetail job = JobBuilder.Create<Gerador>()
                        .WithIdentity($@"{relatorio.KeyName()}Job", "reportGroup")
                        .Build();
                    ITrigger trigger = TriggerBuilder.Create()
                                .WithIdentity($@"{relatorio.KeyName()}", "reportGroup")
                                .StartNow()
                                .WithCronSchedule(relatorio.GetCronSchedule())
                                .Build();
                    scheduler.ScheduleJob(job, trigger);
                }
            }

        }

        private static void ValidarArquivoConfiguracaoRelatorios()
        {
            string arquivo = ConfigurationManager.AppSettings.Get(Consts.ArquivoConfiguracaoRelatorio);
            if (string.IsNullOrEmpty(arquivo))
                throw new ConfiguracaoNaoEncontradaException(Consts.ArquivoConfiguracaoRelatorio);
            if (!File.Exists(arquivo))
                throw new FileNotFoundException($@"Arquivo de configuração de relatórios não encontrado({arquivo})");
        }

        public bool ValidarRelatorio(Relatorio relatorio)
        {
            if (string.IsNullOrEmpty(relatorio.FilePath))
                throw new ConfiguracaoInvalidaRelatorioException("FilePath", relatorio.Name);
            if (string.IsNullOrEmpty(relatorio.PathDestino))
                throw new ConfiguracaoInvalidaRelatorioException("PathDestino", relatorio.Name);
            if (!File.Exists(relatorio.FilePath))
                throw new FileNotFoundException($@"Arquivo de consulta não encontrado para relatório {relatorio.Name}");
            if (!Directory.Exists(relatorio.PathDestino))
                throw new DirectoryNotFoundException($@"Não encontrado diretório de destion para relatório {relatorio.PathDestino}");
            return true;
        }
    }
}
