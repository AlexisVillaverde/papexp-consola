using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;

namespace Proyecto.Core.Models
{
    // MEMENTO: Guarda una "foto" de la lista de productos
    public class SaleSnapshot
    {
        public List<Product> Items { get; }
        // Guardamos una COPIA de la lista, no la referencia
        public SaleSnapshot(List<Product> items) => Items = new List<Product>(items);
    }
}