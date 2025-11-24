using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Sales.Strategies
{
    public class CashPaymentStrategy
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine($"PAGO: Se pagaron ${amount:F2} en efectivo.");
        }
    }
}
