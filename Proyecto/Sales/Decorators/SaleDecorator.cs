using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Sales.Decorators
{
    public abstract class SaleDecorator : ISale
    {
        protected ISale _wrappedSale;
        public SaleDecorator(ISale sale) => _wrappedSale = sale;
        public virtual decimal GetTotalCost() => _wrappedSale.GetTotalCost();
        public virtual string GetDescription() => _wrappedSale.GetDescription();

    }
}
