using System;
using Proyecto.Sales.Decorators;
using Proyecto.Sales.Strategies;

namespace Proyecto.Sales
{
    // PATRÓN FACADE: Proporciona una interfaz simplificada para un sistema complejo.
    public class PointOfSaleFacade
    {
        private IPaymentStrategy _paymentStrategy;

        public void SetPaymentStrategy(IPaymentStrategy strategy)
        {
            _paymentStrategy = strategy;
        }

        public void CompleteSale(ISale saleDecorator)
        {
            Console.WriteLine("\n--- RESUMEN DE VENTA (Facade) ---");
             Console.WriteLine("\n--- Procesando Venta---");
            decimal finalCost = saleDecorator.GetTotalCost(); // Usa el Decorator
            Console.WriteLine($"Descripción: {saleDecorator.GetDescription()}");


            // 1. Obtener costo final (con o sin descuentos aplicados por Decorator)
            decimal finalAmount = saleDecorator.GetTotalCost();
            Console.WriteLine($"Descripción: {saleDecorator.GetDescription()}");

            Console.WriteLine($"Total a Pagar: ${finalAmount:F2}");


            // 2. Ejecutar estrategia de pago
            if (_paymentStrategy == null)
            {
                // Default a efectivo si no se seleccionó nada
                _paymentStrategy = new CashPaymentStrategy();
            }
            _paymentStrategy.Pay(finalAmount);

            // 3. Confirmación
            Console.WriteLine("Transacción completada exitosamente.");
            Console.WriteLine("-----------------------------------");
        }
    }
}


