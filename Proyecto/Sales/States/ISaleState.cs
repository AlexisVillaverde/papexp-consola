using Proyecto.Core.Models;

namespace Proyecto.Sales.States
{
    // La interfaz define qué acciones se pueden INTENTAR hacer en una venta.
    public interface ISaleState
    {
        // El método recibe el contexto (Sale) para poder modificarlo o cambiar su estado
        void AddItem(Sale context, int quantity);
        void Pay(Sale context);
        void Cancel(Sale context);
    }
}