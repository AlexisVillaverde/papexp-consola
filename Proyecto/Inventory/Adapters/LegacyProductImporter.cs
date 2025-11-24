using Proyecto.Core;
using Proyecto.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Inventory.Adapters
{
    public class LegacyProductImporter
    {
        private readonly OldCsvService _oldService = new OldCsvService();
        private readonly DatabaseService _db = DatabaseService.GetInstance();

        public void ImportProducts()
        {
            Console.WriteLine("Importando productos del sistema CSV antiguo...");
            string csvData = _oldService.GetProductsAsCsv();
            string[] lines = csvData.Split('\n');

            foreach (var line in lines)
            {
                string[] parts = line.Split(',');
                var product = new Product
                {
                    Id = parts[0],
                    Name = parts[1],
                    Stock = int.Parse(parts[2]),
                    Price = decimal.Parse(parts[3])
                };

                _db.Products.Add(product);
                _db.RootCategory.Add(product);
                Console.WriteLine($"Producto '{product.Name}' importado.");
            }
        }
    }
}
