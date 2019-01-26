using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DadosExporter.Types.Exceptions
{
    class ConfiguracaoInvalidaRelatorioException : Exception
    {
        public const string MESSAGE = "Configuracao {0} não encontrada para o relatório {1}";
        public ConfiguracaoInvalidaRelatorioException(string nomeConfiguracao, string nomeRelatorio)
            : base(string.Format(MESSAGE, nomeConfiguracao, nomeRelatorio))
        {
        }
    }
}
