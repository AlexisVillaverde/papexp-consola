using Proyecto.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Inventory.Observers
{
    public class StockAlert : IStockObserver
    {
        private string AdminEmail { get; set; }
        public StockAlert(string adminEmail) => AdminEmail = adminEmail;

        public void Update(Product product)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ALERTA: Producto '{product.Name}' tiene stock bajo ({product.Stock}). Notificando a {AdminEmail}.");
            Console.ResetColor();
        }
    }
}
