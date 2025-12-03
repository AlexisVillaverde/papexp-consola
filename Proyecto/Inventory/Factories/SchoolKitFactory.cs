using System;
using Proyecto.Core.Models;

namespace Proyecto.Inventory.Factories
{
    // FÁBRICA CONCRETA 1: Crea productos para el Kit Escolar
    public class SchoolKitFactory : IProductKitFactory
    {
        public Product CreateWritingTool()
        {
            // Genera un ID temporal (el comando AddProduct luego lo validará o usará)
            return new Product
            {
                Id = "KIT-SCH-01",
                Name = "Caja de Colores 12pz (Escolar)",
                Price = 45.00m,
                Stock = 50
            };
        }

        public Product CreatePaperProduct()
        {
            return new Product
            {
                Id = "KIT-SCH-02",
                Name = "Cuaderno de Dibujo",
                Price = 35.50m,
                Stock = 30
            };
        }
    }
}