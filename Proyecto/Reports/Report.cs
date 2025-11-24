using Proyecto.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Reports
{
    public abstract class Report
    {
        protected DatabaseService _db = DatabaseService.GetInstance();

        // El "Template Method"
        public void GenerateReport()
        {
            var data = FetchData();
            string processedData = ProcessData(data);
            PrintReport(processedData);
        }

        // Pasos abstractos
        protected abstract object FetchData();
        protected abstract string ProcessData(object data);

        // Paso concreto (compartido)
        private void PrintReport(string data)
        {
            Console.WriteLine("\n--- INICIO REPORTE ---");
            Console.WriteLine(data);
            Console.WriteLine("--- FIN REPORTE ---");
        }
    }
}
