using System;
using Proyecto.Core.Models;

namespace Proyecto.Inventory.Factories
{
    // FÁBRICA CONCRETA 2: Crea productos para el Kit de Oficina
    public class OfficeKitFactory : IProductKitFactory
    {
        public Product CreateWritingTool()
        {
            return new Product
            {
                Id = "KIT-OFF-01",
                Name = "Bolígrafo de Lujo (Oficina)",
                Price = 120.00m,
                Stock = 20
            };
        }

        public Product CreatePaperProduct()
        {
            return new Product
            {
                Id = "KIT-OFF-02",
                Name = "Paquete Carpetas Archivadoras",
                Price = 85.00m,
                Stock = 25
            };
        }
    }
}