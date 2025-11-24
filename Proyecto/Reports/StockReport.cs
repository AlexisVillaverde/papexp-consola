using Proyecto.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string reportData = "REPORTE DE STOCK (ALMACÉN):\n";
            foreach (var p in products)
            {
                reportData += $"- {p.Name}: {p.Stock} unidades\n";
            }
            return reportData;
        }
    }
}
