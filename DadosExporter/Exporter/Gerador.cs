using DadosExporter.Models;
using DadosExporter.Repositorios;
using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using DadosExporter.Context;

namespace DadosExporter.Exporter
{
    public class Gerador : IJob
    {
        private static readonly ILog Log =
              LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private RelatoriosRepository RelatoriosRepository { get; set; }
        private Relatorio relatorio { get; set; }

        public Gerador()
        {
            RelatoriosRepository = new RelatoriosRepository();
        }

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                relatorio = RelatoriosRepository.FindRelatorioByKey(context.Trigger.Key.Name);
                if (relatorio == null) throw new InvalidOperationException($"Relatório não definido chave {context.Trigger.Key.Name}");
                GerarRelatorio();
            }
            catch (Exception ex)
            {
                Log.Error($@"Erro gerar relatorio {relatorio.Name}. Mensagem: {ex.Message}");
            }
        }

        public void GerarRelatorio()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[Consts.GeradorRelatorioConnection].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(relatorio.GetSqlRelatorio(), connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                GerarArquivoCsv(reader);
                reader.Close();
            }
            SalvarGeracaoArquivo();
        }

        public void GerarArquivoCsv(SqlDataReader reader)
        {
            string caminho = relatorio.GetCSVPathDestino();
            using (StreamWriter file = new StreamWriter(caminho))
            {
                while (reader.Read())
                {
                    string linha = string.Empty;
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        linha += $@"{reader[i]};";
                    }
                    file.WriteLine(linha);
                }
            }
        }
        
        public void SalvarGeracaoArquivo()
        {
            Log.Info($"Salvar relatorio {relatorio.Name}");
            ExporterContexto contexto = new ExporterContexto();
            contexto.ExecucaoRelatorio.Add(new ExecucaoRelatorio {
                nome = relatorio.Name,
                consulta = relatorio.GetSqlRelatorio(),
                dataExecucao = DateTime.Now,
            });
            contexto.SaveChanges();
        }
    }
}
