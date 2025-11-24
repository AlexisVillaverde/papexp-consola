using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Sales.Decorators
{
    public class EmployeeDiscountDecorator : SaleDecorator
    {
        public EmployeeDiscountDecorator(ISale sale) : base(sale) { }
        public override decimal GetTotalCost()
        {
            decimal originalCost = base.GetTotalCost();
            decimal discount = originalCost * 0.10m; // 10% DTO.
            return originalCost - discount;
        }
        public override string GetDescription()
        {
            return base.GetDescription() + " [DTO. Empleado 10%]";
        }
    }
}
