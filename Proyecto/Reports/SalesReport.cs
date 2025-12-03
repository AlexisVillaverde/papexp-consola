using System.Text;
using Proyecto.Core.Models;
using Proyecto.Sales.Iterators; // Asegúrate de tener este namespace

namespace Proyecto.Reports
{
    public class SalesReport : Report
    {
        protected override object FetchData()
        {
            // Obtenemos el ITERADOR de la colección de ventas
            return _db.Sales.CreateIterator();
        }

        protected override string ProcessData(object data)
        {
            var iterator = data as ISaleIterator;
            if (iterator == null) return "Error: No se pudo obtener el iterador de ventas.";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("REPORTE DE VENTAS (Historial Completo)");
            sb.AppendLine("---------------------------------");

            decimal grandTotal = 0;
            int count = 0;

            // PATRÓN ITERATOR: Recorremos sin saber cómo es la lista interna
            while (iterator.HasNext())
            {
                Sale sale = iterator.Next();
                sb.AppendLine($"Venta #{sale.Id} | Cajero: {sale.Cashier.Name} | Total: ${sale.Total}");
                grandTotal += sale.Total;
                count++;
            }

            sb.AppendLine("---------------------------------");
            sb.AppendLine($"Total Ventas: {count}");
            sb.AppendLine($"Ingresos Totales: ${grandTotal}");

            return sb.ToString();
        }
    }
}