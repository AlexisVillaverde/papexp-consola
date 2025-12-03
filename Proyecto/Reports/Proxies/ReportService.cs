using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Reports.Proxies

{
    public class ReportService : IReportService
    {
        public void ShowSalesReport()
        {
            // Simplemente crea y ejecuta el reporte usando Template Method
            var report = new SalesReport();
            report.GenerateReport();
        }

        public void ShowStockReport()
        {
            var report = new StockReport();
            report.GenerateReport();
        }
    }
}