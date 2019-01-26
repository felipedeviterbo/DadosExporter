using DadosExporter.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DadosExporter.Repositorios
{
    public class RelatoriosRepository
    {
        public string GetPathArquivoConfiguracaoRelatorios()
        {
            return ConfigurationManager.AppSettings.Get(Consts.ArquivoConfiguracaoRelatorio);
        }

        public List<Relatorio> GetTodosRelatorios()
        {
            string arquivo = GetPathArquivoConfiguracaoRelatorios();
            List<Relatorio> result = new List<Relatorio>();
            XElement relatoriosConfig = XElement.Load(arquivo);
            foreach (var node in relatoriosConfig.Elements())
            {
                var nome = node.Attribute("nome").Value;
                var data = DateTime.Parse(node.Attribute("data").Value, new CultureInfo("pt-BR"));
                var hora = DateTime.Parse(node.Attribute("hora").Value, new CultureInfo("pt-BR"));
                var arquivoOrigem = node.Attribute("filePath").Value;
                var destino = node.Attribute("pathDestino").Value;
                Relatorio r = new Relatorio(nome, data, hora, arquivoOrigem, destino);
                result.Add(r);
            }
            return result;
        }

        public Relatorio FindRelatorioByKey(string key)
        {
            var relatorios = GetTodosRelatorios();
            return relatorios.Where(r => r.KeyName().Equals(key)).FirstOrDefault();
        }
    }
}
