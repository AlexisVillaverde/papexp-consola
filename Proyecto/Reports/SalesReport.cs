using Proyecto.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Reports
{
    public class SalesReport: Report
    {
        protected override object FetchData()
        {
            return _db.Sales;
        }

        protected override string ProcessData(object data)
        {
            var sales = data as List<Sale>;
            decimal totalRevenue = sales.Sum(s => s.Total);
            return $"REPORTE DE VENTAS:\nTotal de Ventas: {sales.Count}\nIngresos Totales: ${totalRevenue:F2}";
        }
    }
}
