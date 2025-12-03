using System;
using System.Linq;
using Proyecto.Core;
using Proyecto.Core.Models;
using Proyecto.Sales.Memento;

namespace Proyecto.Sales.Builders
{
    public class SaleBuilder
    {
        private Sale _sale;
        private DatabaseService _db = DatabaseService.GetInstance();
        private SaleHistory _history = new SaleHistory(); // Memento Caretaker

        public SaleBuilder(Employee cashier)
        {
            _sale = new Sale { Id = _db.Sales.GetList().Count + 1, Cashier = cashier };
        }

        public bool AddProduct(string productId, int quantity)
        {
            _history.Save(_sale); // Guardar estado antes de modificar

            var product = _db.Products.FirstOrDefault(p => p.Id == productId);
            // Verificamos stock
            if (product != null && product.Stock >= quantity)
            {
                for (int i = 0; i < quantity; i++)
                {
                    _sale.Items.Add(product);
                }
                Console.WriteLine($"Agregado: {product.Name} x{quantity}");
                return true;
            }

            Console.WriteLine("Error: Stock insuficiente o producto no encontrado.");
            return false;
        }

        public void RemoveLastAdded()
        {
            Console.WriteLine("Deshaciendo última acción...");
            _history.Undo(_sale);
        }

        // --- CORRECCIÓN AQUÍ ---
        public Sale GetSale()
        {
            // Antes de devolver la venta, calculamos el total real basado en los ítems actuales
            if (_sale.Items != null)
            {
                _sale.Total = _sale.Items.Sum(item => item.Price);
            }
            return _sale;
        }
    }
}