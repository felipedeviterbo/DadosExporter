using System;
using System.Collections.Generic;
using System.IO;
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

        public Relatorio(string name, DateTime dataExecucao, DateTime horaExecucao, 
            string filePath, string path)
        {
            this.Name = name;
            this.DataExecucao = dataExecucao;
            this.HoraExecucao = horaExecucao;
            this.FilePath = filePath;
            this.PathDestino = path;
        }

        public string GetCronSchedule()
        {
            return $@"{HoraExecucao.Second} {HoraExecucao.Minute} {HoraExecucao.Hour} "+
                   $@"{DataExecucao.Day} {DataExecucao.Month} ? {DataExecucao.Year}";
        }

        public string GetSqlRelatorio()
        {
            return File.ReadAllText(this.FilePath);
        }
        
        public string GetCSVPathDestino()
        {
            return string.Format(@"{0}\{1}{2}.csv", PathDestino, Name, DateTime.Now.ToString("ddMMyyyyHHmmss"));
        }

        public string KeyName()
        {
            return $@"{Name}{DataExecucao.ToString("ddMMyyyy")}{HoraExecucao.ToString("HHmmss")}";
        }
    }
}
