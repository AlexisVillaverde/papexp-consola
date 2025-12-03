using Proyecto.Core.Models;
using System.Collections.Generic;

namespace Proyecto.Sales.Memento
{
    // PATRÓN MEMENTO
    public class SaleSnapshot
    {
        public List<Product> Items { get; }
        public SaleSnapshot(List<Product> items) => Items = new List<Product>(items);
    }

    public class SaleHistory
    {
        private Stack<SaleSnapshot> _history = new Stack<SaleSnapshot>();

        public void Save(Sale sale)
        {
            _history.Push(new SaleSnapshot(sale.Items));
        }

        public void Undo(Sale sale)
        {
            if (_history.Count > 0)
            {
                var snapshot = _history.Pop();
                sale.Items = new List<Product>(snapshot.Items);
            }
        }
    }
}