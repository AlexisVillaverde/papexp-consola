using Proyecto.Core.Models;
using System.Collections.Generic;

namespace Proyecto.Sales.Iterators
{
    public class SalesIterator : ISaleIterator
    {
        private List<Sale> _sales;
        private int _position = 0;

        public SalesIterator(List<Sale> sales)
        {
            _sales = sales;
        }

        public bool HasNext()
        {
            return _position < _sales.Count;
        }

        public Sale Next()
        {
            if (!HasNext()) return null;
            Sale sale = _sales[_position];
            _position++;
            return sale;
        }
    }
}