using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DadosExporter.Models
{
    public class Relatorio
    {
        public string Name { get; set; }
        public DateTime DataExecucao { get; set; }
        public DateTime HoraExecucao { get; set; }
        public string FilePath { get; set; }
        public string PathDestino { get; set; }
        
    }
}
