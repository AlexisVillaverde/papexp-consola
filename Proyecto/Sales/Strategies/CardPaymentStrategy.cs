using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Sales.Strategies
{
    internal class CardPaymentStrategy : IPaymentStrategy
    {
        private readonly string _cardNumber;
        private readonly string _pin;

        // Recibimos ambos datos al crear la estrategia
        public CardPaymentStrategy(string cardNumber, string pin)
        {
            _cardNumber = cardNumber;
            _pin = pin;
        }

        public void Pay(decimal amount)
        {
            // Obtener solo los últimos 4 dígitos de la tarjeta
            string lastFourDigits = _cardNumber.Length > 4
                ? _cardNumber.Substring(_cardNumber.Length - 4)
                : _cardNumber;

            // Simulación de validación (el NIP existe en memoria pero NO se imprime)

            // Ticket de salida
            Console.WriteLine($"PAGO TARJETA: Se cobraron ${amount:F2} a la tarjeta *{lastFourDigits}");
        }
    }
}
