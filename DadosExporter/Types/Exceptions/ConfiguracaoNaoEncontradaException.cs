using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DadosExporter.Types.Exceptions
{
    public class ConfiguracaoNaoEncontradaException: Exception
    {
        public const string MESSAGE = "Configuracao {0} não encontrada";
        public ConfiguracaoNaoEncontradaException(string nomeConfiguracao)
            : base(string.Format(MESSAGE, nomeConfiguracao))
        {
        }
    }
}
