using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Sales.Strategies
{
    internal class CardPaymentStrategy : IPaymentStrategy
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine($"PAGO: Se cobraron ${amount:F2} a la tarjeta.");
        }
    }
}
