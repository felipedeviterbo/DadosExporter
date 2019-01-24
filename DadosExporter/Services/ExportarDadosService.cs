using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DadosExporter
{
    public partial class ExportarDadosService : ServiceBase
    {
        private static readonly ILog Log =
              LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ExportarDadosService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Log.Info("DadosExporter iniciado");
        }

        protected override void OnStop()
        {
            Log.Info("DadosExporter finalizado");
        }

        protected override void OnContinue()
        {
            Log.Info("DadosExporter ativo");
        }

    }
}
