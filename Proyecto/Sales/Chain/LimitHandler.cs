using Proyecto.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Sales.Chain
{
    public class LimitHandler : IHandler
    {
        private IHandler _next; // Variable local necesaria

        public IHandler SetNext(IHandler next)
        {
            _next = next;
            return next;
        }

        public bool Handle(Sale sale)
        {
            decimal total = sale.Items.Sum(x => x.Price);
            if (total > 10000)
            {
                Console.WriteLine($"Alerta: Venta de ${total} excede el límite permitido sin autorización.");
                return false;
            }

            return _next?.Handle(sale) ?? true;
        }
    }
}
