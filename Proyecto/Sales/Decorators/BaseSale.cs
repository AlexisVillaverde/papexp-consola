using Proyecto.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Sales.Decorators
{
    public class BaseSale : ISale
    {
        private Sale _sale;
        public BaseSale(Sale sale) => _sale = sale;
        public decimal GetTotalCost() => _sale.Total;
        public string GetDescription() => $"Venta #{_sale.Id} ({_sale.Items.Count} ítems)";

    }
}
