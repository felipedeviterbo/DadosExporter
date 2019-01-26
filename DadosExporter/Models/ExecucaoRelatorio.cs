using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DadosExporter.Models
{
    public class ExecucaoRelatorio
    {
        ///tabela do banco de dados a
        //data hora, nome do relatório gerado e consulta executada.
        public int id { get; set; }
        public string nome { get; set; }
        public DateTime dataExecucao { get; set; }
        public string consulta { get; set; }
    }
}
