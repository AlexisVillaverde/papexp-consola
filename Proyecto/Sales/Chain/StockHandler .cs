using Proyecto.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Sales.Chain
{
    public class StockHandler : IHandler
    {
        private IHandler _next; 

        public IHandler SetNext(IHandler next)
        {
            _next = next;
            return next;
        }

        public bool Handle(Sale sale)
        {
            if (sale.Items.Count == 0)
            {
                Console.WriteLine("Error: El carrito está vacío.");
                return false;
            }

            return _next?.Handle(sale) ?? true;
        }
    }

}

