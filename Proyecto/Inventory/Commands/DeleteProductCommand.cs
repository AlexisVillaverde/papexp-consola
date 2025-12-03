using Proyecto.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Inventory.Commands
{
    public class DeleteProductCommand : IProductCommand
    {
        private string _id;
        private DatabaseService _db = DatabaseService.GetInstance();

        public DeleteProductCommand(string id)
        {
            _id = id;
        }

        public void Execute()
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == _id);

            if (product != null)
            {
                _db.Products.Remove(product);
                _db.SaveData();
                Console.WriteLine($"Producto '{product.Name}' eliminado del inventario.");
            }
            else
            {
                Console.WriteLine($"Error: Producto con ID {_id} no encontrado.");
            }
        }
    }


}

