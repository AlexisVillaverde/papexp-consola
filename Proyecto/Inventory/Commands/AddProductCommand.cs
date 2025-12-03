using Proyecto.Core;
using Proyecto.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Inventory.Commands
{
    public class AddProductCommand : IProductCommand
    {
        private Product _product;
        private DatabaseService _db = DatabaseService.GetInstance();

        public AddProductCommand(Product product)
        {
            _product = product;
        }

        public void Execute()
        {
            // Verificamos si el ID ya existe
            if (_db.Products.Exists(p => p.Id == _product.Id))
            {
                Console.WriteLine($"Error: Ya existe un producto con el ID {_product.Id}.");
                return;
            }

            // Agregamos a la lista principal
            _db.Products.Add(_product);

            // Agregamos al árbol de categorías (Composite)
            _db.RootCategory.Add(_product);

            _db.SaveData(); // Persistencia en JSON
            Console.WriteLine($"Producto '{_product.Name}' agregado correctamente.");
        }
    }

}
