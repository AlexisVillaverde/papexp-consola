using Proyecto.Sales.Decorators;
using Proyecto.Sales.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Sales
{
    public class PointOfSaleFacade
    {
        private IPaymentStrategy _paymentStrategy;

        public void SetPaymentStrategy(IPaymentStrategy strategy)
        {
            _paymentStrategy = strategy;
        }

        public void CompleteSale(ISale sale)
        {
            Console.WriteLine("\n--- Procesando Venta (Facade) ---");
            decimal finalCost = sale.GetTotalCost(); // Usa el Decorator
            Console.WriteLine($"Descripción: {sale.GetDescription()}");

            if (_paymentStrategy == null)
            {
                _paymentStrategy = new CashPaymentStrategy();
            }

            _paymentStrategy.Pay(finalCost); // Usa la Strategy

            Console.WriteLine("Ticket generado. Venta completada.");
            Console.WriteLine("---------------------------------\n");
        }
    }
}
