using DadosExporter.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DadosExporter.Context
{
    public class ExporterContexto: DbContext
    {
        public ExporterContexto()
            : base("Reply")
        {
        }

        public DbSet<ExecucaoRelatorio> ExecucaoRelatorio { get; set; }
    }
}
