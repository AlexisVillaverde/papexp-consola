using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Proyecto.Core.Models;
using Proyecto.Inventory.Composite;
using Proyecto.Sales;

namespace Proyecto.Core
{
    // PATRÓN SINGLETON + PERSISTENCIA JSON
    public class DatabaseService
    {
        private static DatabaseService _instance;
        private const string DbPath = "database.json";

        public List<Product> Products { get; set; }
        public List<Employee> Employees { get; set; }
        public SalesCollection Sales { get; set; }
        public CategoryComposite RootCategory { get; private set; }
        public Employee CurrentUser { get; set; }

        private DatabaseService()
        {
            Products = new List<Product>();
            Employees = new List<Employee>();
            Sales = new SalesCollection();
            RootCategory = new CategoryComposite("Catálogo General");
            LoadData();
        }

        public static DatabaseService GetInstance()
        {
            if (_instance == null) _instance = new DatabaseService();
            return _instance;
        }

        public void SaveData()
        {
            var data = new
            {
                Products,
                Employees,
                SalesList = Sales.GetList()
            };
            // Guardamos con indentación para que sea legible
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(DbPath, json);
        }

        private void LoadData()
        {
            if (!File.Exists(DbPath)) { SeedData(); return; }
            try
            {
                string json = File.ReadAllText(DbPath);
                var data = JsonSerializer.Deserialize<JsonElement>(json);

                if (data.TryGetProperty("Products", out var p))
                    Products = JsonSerializer.Deserialize<List<Product>>(p.GetRawText());

                if (data.TryGetProperty("Employees", out var e))
                    Employees = JsonSerializer.Deserialize<List<Employee>>(e.GetRawText());

                if (data.TryGetProperty("SalesList", out var s))
                {
                    var list = JsonSerializer.Deserialize<List<Sale>>(s.GetRawText());
                    Sales = new SalesCollection();
                    foreach (var item in list) Sales.AddSale(item);
                }

                // Reconstruir árbol composite (simplificado para el ejemplo)
                RootCategory = new CategoryComposite("Catálogo General");
                var catAll = new CategoryComposite("Todo");
                foreach (var prod in Products) catAll.Add(prod);
                RootCategory.Add(catAll);
            }
            catch { SeedData(); }
        }

        private void SeedData()
        {
            // Datos iniciales si no existe el JSON
            Employees.Add(new Employee { Id = 1, Name = "Admin", Role = "Admin", Password = "123" });
            Employees.Add(new Employee { Id = 2, Name = "Cajero", Role = "Vendedor", Password = "123" });

            Products.Add(new Product { Id = "1", Name = "Lápiz", Price = 5, Stock = 100 });
            Products.Add(new Product { Id = "2", Name = "Cuaderno", Price = 25, Stock = 50 });

            SaveData();
        }
    }
}