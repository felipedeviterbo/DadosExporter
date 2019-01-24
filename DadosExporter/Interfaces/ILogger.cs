using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DadosExporter.Interfaces
{
    public interface ILogger
    {
        void AddLog(string mensagem);
        void AddError(string error, Exception exception);
    }
}
