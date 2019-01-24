﻿using DadosExporter.Exporter;
using DadosExporter.Services;
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
        private EventosWindowsLogger Logger;
        private Configurer Configurer;
        public ExportarDadosService()
        {
            InitializeComponent();
            Logger = new EventosWindowsLogger();
            Configurer = new Configurer();
        }

        protected override void OnStart(string[] args)
        {
            Logger.AddLog("DadosExporter iniciado");
            Configurer.ConfigurarRelatorios();
        }

        protected override void OnStop()
        {
            Logger.AddLog("DadosExporter finalizado");
        }

        protected override void OnContinue()
        {
            Logger.AddLog("DadosExporter ativo");
        }

    }
}
