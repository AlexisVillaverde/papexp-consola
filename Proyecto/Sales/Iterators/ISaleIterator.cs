using Proyecto.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Sales.Iterators
{
    public interface ISaleIterator
    {
        bool HasNext();
        Sale Next();
    }
}