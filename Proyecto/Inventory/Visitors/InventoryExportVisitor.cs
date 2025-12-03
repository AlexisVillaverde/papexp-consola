using Proyecto.Core.Models;
using Proyecto.Inventory.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Inventory.Visitors
{
    public class InventoryExportVisitor : IVisitor
    {
        public void VisitProduct(Product product)
        {
            // Simula exportar a un formato específico
            Console.WriteLine($"[CSV] P,{product.Id},{product.Name},{product.Price},{product.Stock}");
        }

        public void VisitCategory(CategoryComposite category)
        {
            Console.WriteLine($"[CSV] C,{category.Name}");
        }
    }
}
