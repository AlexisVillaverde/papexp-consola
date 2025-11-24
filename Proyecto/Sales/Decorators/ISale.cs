using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Sales.Decorators
{
    public interface ISale
    {
        decimal GetTotalCost();
        string GetDescription();
    }
}
