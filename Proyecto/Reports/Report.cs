using Proyecto.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Reports
{
        // PATRÓN TEMPLATE METHOD: Define el esqueleto del algoritmo en una operación.
    public abstract class Report
    {
        protected DatabaseService _db = DatabaseService.GetInstance();

        // El "Template Method"
        public void GenerateReport()
        {

            Console.WriteLine("\n[Generando Reporte...]");
            // Paso 1: Obtener datos (Abstracto)
            var data = FetchData();
            // Paso 2: Procesar/Formatear datos (Abstracto)
            string processedData = ProcessData(data);
            // Paso 3: Imprimir/Exportar (Concreto - Común para todos)
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
            Console.WriteLine($"Generado el: {DateTime.Now}");
        }
    }
}
