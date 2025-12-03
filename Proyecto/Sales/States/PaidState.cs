using Proyecto.Core.Models;
using Proyecto.Sales.States;
using System;

namespace Proyecto.Sales.States
{
    public class PaidState : ISaleState
    {
        public void AddItem(Sale context, int quantity)
        {
            // COMPORTAMIENTO B: Bloquear
            // En este estado, la MISMA acción (AddItem) se comporta diferente.
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ Error: No puedes agregar ítems a una venta que ya fue PAGADA.");
            Console.ResetColor();
        }

        public void Pay(Sale context)
        {
            Console.WriteLine("⚠️ Error: Esta venta ya está pagada. No se puede cobrar doble.");
        }

        public void Cancel(Sale context)
        {
            Console.WriteLine("❌ Error: No se puede cancelar una venta pagada (se requeriría una devolución).");
        }
    }
}