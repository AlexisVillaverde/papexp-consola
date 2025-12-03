using Proyecto.Core.Models;
using Proyecto.Sales.Iterators;
using System.Collections.Generic;

namespace Proyecto.Sales
{
    // Esta clase envuelve la lista de ventas para el patrón Iterator
    public class SalesCollection
    {
        private List<Sale> _sales = new List<Sale>();

        public void AddSale(Sale sale) => _sales.Add(sale);

        // Método helper para serialización JSON
        public List<Sale> GetList() => _sales;

        // Método Factory para crear el iterador
        public ISaleIterator CreateIterator()
        {
            return new SalesIterator(_sales);
        }
    }
}