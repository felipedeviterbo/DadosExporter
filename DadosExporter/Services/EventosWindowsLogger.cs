using DadosExporter.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DadosExporter.Services
{
    public class EventosWindowsLogger: ILogger
    {
        private static ILog Log;

        public EventosWindowsLogger()
        {
            Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public void AddLog(string mensagem)
        {
            Log.Info(mensagem);
        }

        public void AddError(string error, Exception exception)
        {
            Log.Error(error, exception);
        }
    }
}
