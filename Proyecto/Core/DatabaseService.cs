using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto.Core.Models;
using Proyecto.Inventory.Composite;

namespace Proyecto.Core
{
    // --- PATRÓN SINGLETON ---
    public class DatabaseService
    {
        private static DatabaseService _instance;
        public List<Product> Products { get; private set; }
        public List<Employee> Employees { get; private set; }
        public List<Sale> Sales { get; private set; }
        public CategoryComposite RootCategory { get; private set; }

        private DatabaseService()
        {
            Products = new List<Product>();
            Employees = new List<Employee>();
            Sales = new List<Sale>();
            RootCategory = new CategoryComposite("Catálogo General");
        }

        public static DatabaseService GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DatabaseService();
            }
            return _instance;
        }
    }
}