using Proyecto.Core.Models;
using Proyecto.Reports;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Reports
{
    public class StockReport : Report
    {
        protected override object FetchData()
        {
            return _db.Products;
        }

        protected override string ProcessData(object data)
        {
            var products = data as List<Product>;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("REPORTE DE INVENTARIO (Stock Actual)");
            sb.AppendLine("---------------------------------");

            foreach (var p in products)
            {
                string status = p.Stock < 5 ? "[BAJO]" : "[OK]";
                sb.AppendLine($"{p.Id}: {p.Name} \t| Stock: {p.Stock} {status}");
            }

            return sb.ToString();
        }
    }
}