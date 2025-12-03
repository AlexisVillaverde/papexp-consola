using Proyecto.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Inventory.Commands
{
    public class UpdateProductCommand : IProductCommand
    {
        private string _id;
        private decimal _newPrice;
        private int _newStock;
        private DatabaseService _db = DatabaseService.GetInstance();

        public UpdateProductCommand(string id, decimal newPrice, int newStock)
        {
            _id = id;
            _newPrice = newPrice;
            _newStock = newStock;
        }

        public void Execute()
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == _id);

            if (product != null)
            {
                // Actualizamos valores
                product.Price = _newPrice;
                product.Stock = _newStock;

                _db.SaveData(); // Guardamos cambios
                Console.WriteLine($"Producto '{product.Name}' actualizado (Precio: ${product.Price}, Stock: {product.Stock}).");
            }
            else
            {
                Console.WriteLine($"Error: Producto con ID {_id} no encontrado.");
            }
        }
    }

}

