using Proyecto.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Inventory.Observers
{
    // --- PATRÓN OBSERVER ---
    // 1. La Interfaz del Observador (para alertas de stock bajo)
    public interface IStockObserver
    {
        void Update(Product product);
    }
}
