using DadosExporter.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleTeste
{
    class Program
    {
        static void Main(string[] args)
        {
            string ttt = ConfigurationSettings.AppSettings.Get("ArquivoConfiguracaoRelatorio");
            List <Relatorio> relatorios = new List<Relatorio>();
            XElement relatoriosConfig = XElement.Load("RelatoriosConfig.xml");
            foreach (var node in relatoriosConfig.Elements())
            {
                var nome = node.Attribute("nome").Value;
                var data = node.Attribute("data").Value;
                var hora = node.Attribute("hora").Value;
                var arquivoOrigem = node.Attribute("filePath").Value;
                var destino = node.Attribute("pathDestino").Value;
                Relatorio r = new Relatorio
                {
                    Name = nome,
                    DataExecucao = DateTime.Parse(data),
                    HoraExecucao = DateTime.Parse(hora),
                    FilePath = arquivoOrigem,
                    PathDestino = destino
                };
                relatorios.Add(r);
            }
        }
    }
}
