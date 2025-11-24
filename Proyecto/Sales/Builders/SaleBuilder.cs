using Proyecto.Core;
using Proyecto.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Sales.Builders
{
    public class SaleBuilder
    {
        private Sale _sale = new Sale();
        private DatabaseService _db = DatabaseService.GetInstance();

        public SaleBuilder(Employee cashier)
        {
            _sale.Id = _db.Sales.Count + 1;
            _sale.Cashier = cashier;
        }

        public bool AddProduct(string productId, int quantity)
        {
            var product = _db.Products.Find(p => p.Id == productId);
            if (product == null)
            {
                Console.WriteLine($"Error: Producto ID {productId} no encontrado.");
                return false;
            }
            if (product.Stock < quantity)
            {
                Console.WriteLine($"Error: Stock insuficiente para '{product.Name}'.");
                return false;
            }

            // Descuenta el stock (esto dispara el Observer)
            product.Stock -= quantity;

            for (int i = 0; i < quantity; i++)
            {
                _sale.Items.Add(product);
            }

            Console.WriteLine($"'{product.Name}' x{quantity} añadido al carrito.");
            return true;
        }

        public Sale Build()
        {
            _sale.Total = _sale.Items.Sum(item => item.Price);
            _db.Sales.Add(_sale);
            return _sale;
        }

    }
}
