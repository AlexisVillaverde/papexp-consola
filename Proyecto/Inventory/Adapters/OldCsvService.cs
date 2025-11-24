using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Inventory.Adapters
{
    public class OldCsvService
    {
        public string GetProductsAsCsv()
        {
            return "10,Cuaderno Raya,50,25.50\n11,Pluma Azul,100,5.00";
        }

    }
}
