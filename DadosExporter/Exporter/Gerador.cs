using DadosExporter.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DadosExporter.Exporter
{
    public class Gerador
    {
        private static readonly ILog Log =
              LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void GerarRelatorio(Report report)
        {
            if (report == null) { Log.Error("Tentativa de gerar relatório inválido."); return; }
            if (string.IsNullOrEmpty(report.FilePath))
            {
                Log.Error($"Relatório {report.Name} sem arquivo de sql de origem informado");
                return;
            }
            if (!File.Exists(report.FilePath))
            {
                Log.Error($"Arquivo {report.FilePath} do relatório {report.Name} não encontrado");
                return;
            }
            if (string.IsNullOrEmpty(report.PathDestino))
            {
                Log.Error($"Relatório {report.Name} não possui path de destino informada.");
            }
            if (Directory.Exists(report.PathDestino))
            {
                Log.Error($"Relatório {report.Name} possui caminho de destino inválido({report.PathDestino}).");
                return;
            }

            string sql = ObterSqlArquivo(report.FilePath);
            GerarArquivoCsv(report, new List<string>());
            SalvarGeracaoArquivo(report.Name, sql);
        }

        public string ObterSqlArquivo(string path)
        {
            return File.ReadAllText(path);
        }

        public void GerarArquivoCsv(Report report, IEnumerable<string> dados)
        {
            string caminho = string.Format(@"{0}\{1}{2}.csv", report.PathDestino, report.Name, DateTime.Now.ToString("DDMMYYYYHHMMSS"));
            using (StreamWriter file = new StreamWriter(caminho))
            {
                foreach (string line in dados)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        file.WriteLine(line);
                    }
                }
            }
        }
        
        public void SalvarGeracaoArquivo(string nome, string consulta)
        {

        }
    }
}
