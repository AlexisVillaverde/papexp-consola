using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Core.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public Employee Cashier { get; set; }
        public List<Product> Items { get; set; } = new List<Product>();
        public decimal Total { get; set; } 
    }
}