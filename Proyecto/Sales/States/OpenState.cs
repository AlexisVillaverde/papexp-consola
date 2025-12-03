using Proyecto.Core.Models;
using Proyecto.Sales.States;
using System;

namespace Proyecto.Sales.States
{
    public class OpenState : ISaleState
    {
        public void AddItem(Sale context, int quantity)
        {
            // COMPORTAMIENTO A: Permitir
            Console.WriteLine($"✅ Estado Abierto: Agregando {quantity} artículos a la venta...");
            // Aquí la lógica real agregaría el ítem a la lista context.Items
        }

        public void Pay(Sale context)
        {
            Console.WriteLine("Procesando pago...");
            // CAMBIO DE ESTADO: De Abierta -> Pagada
            context.State = new PaidState();
            Console.WriteLine("--> La venta ha pasado a estado: PAGADA");
        }

        public void Cancel(Sale context)
        {
            Console.WriteLine("Cancelando venta... Vaciando carrito.");
            context.Items.Clear();
        }
    }
}