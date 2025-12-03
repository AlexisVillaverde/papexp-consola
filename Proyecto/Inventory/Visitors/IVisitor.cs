using Proyecto.Core.Models;
using Proyecto.Inventory.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Inventory.Visitors
{
    public interface IVisitor
    {
        void VisitProduct(Product product);
        void VisitCategory(CategoryComposite category);

    }
}
