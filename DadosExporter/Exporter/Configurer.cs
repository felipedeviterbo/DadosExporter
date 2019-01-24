﻿using DadosExporter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Configuration;
using System.IO;
using DadosExporter.Types.Exceptions;

namespace DadosExporter.Exporter
{
    public class Configurer
    {
        public void ConfigurarRelatorios()
        {
            //ArquivoConfiguracaoRelatorio
            string arquivo = ConfigurationSettings.AppSettings.Get(Consts.ArquivoConfiguracaoRelatorio);
            if (string.IsNullOrEmpty(arquivo))
                throw new ConfiguracaoNaoEncontradaException(Consts.ArquivoConfiguracaoRelatorio);
            if (!File.Exists(arquivo))
                throw new FileNotFoundException($@"Arquivo de configuração de relatórios não encontrado({arquivo})");
            //Ler Relatorios
            List<Relatorio> relatorios = new List<Relatorio>();
            XElement relatoriosConfig = XElement.Load(arquivo);
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
            //CriarRelatorios

        }
    }
}
