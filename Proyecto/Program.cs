using System;
using Proyecto.Core;
using Proyecto.Core.Models;
using Proyecto.Employees;
using Proyecto.Employees.Commands;
using Proyecto.Inventory.Adapters;
using Proyecto.Inventory.Composite;
using Proyecto.Inventory.Observers;
using Proyecto.Reports;
using Proyecto.Sales;
using Proyecto.Sales.Builders;
using Proyecto.Sales.Decorators;
using Proyecto.Sales.Strategies;

namespace Proyecto
{
    class Program
    {
        private static DatabaseService _db = DatabaseService.GetInstance();

        static void Main(string[] args)
        {
            SeedData();

            Console.WriteLine($"Bienvenido a PAPEXP - Papelería Arcoíris");
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- MENÚ PRINCIPAL ---");
                Console.WriteLine("1. Módulo de Ventas (Facade, Builder, Strategy, Decorator)");
                Console.WriteLine("2. Módulo de Almacén (Composite, Observer, Adapter)");
                Console.WriteLine("3. Módulo de Empleados (Command)");
                Console.WriteLine("4. Módulo de Reportes (Template Method, Singleton)");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": RunSalesModule(); break;
                    case "2": RunWarehouseModule(); break;
                    case "3": RunEmployeeModule(); break;
                    case "4": RunReportsModule(); break;
                    case "5": running = false; break;
                    default: Console.WriteLine("Opción no válida."); break;
                }
            }
        }

        static void RunSalesModule()
        {
            Console.WriteLine("\n--- Módulo de Ventas ---");
            var cashier = _db.Employees.Find(e => e.Id == 1);

            var saleBuilder = new SaleBuilder(cashier);
            saleBuilder.AddProduct("1", 2);  // 2 cuadernos
            saleBuilder.AddProduct("4", 3);  // 3 plumas (esto disparará el Observer)
            Sale newSale = saleBuilder.Build();

            ISale saleFinal = new BaseSale(newSale);
            saleFinal = new EmployeeDiscountDecorator(saleFinal);

            var pos = new PointOfSaleFacade();
            pos.SetPaymentStrategy(new CardPaymentStrategy());
            pos.CompleteSale(saleFinal);
        }

        static void RunWarehouseModule()
        {
            Console.WriteLine("\n--- Módulo de Almacén ---");
            Console.WriteLine("Catálogo de Productos (Composite):");
            _db.RootCategory.Display(0);

            Console.WriteLine("\nImportando productos de sistema antiguo (Adapter)...");
            var importer = new LegacyProductImporter();
            importer.ImportProducts();

            Console.WriteLine("\nCatálogo actualizado:");
            _db.RootCategory.Display(0);

            Console.WriteLine("\n(Patrón Observer está activo para 'Pluma Negra')");
        }

        static void RunEmployeeModule()
        {
            Console.WriteLine("\n--- Módulo de Empleados ---");
            var manager = new EmployeeManager();
            var newEmployee = new Employee { Name = "Cesar Martínez", Role = "Admin" }; // [cite: 1006]

            IEmployeeCommand command = new AddEmployeeCommand(newEmployee);
            manager.ProcessCommand(command);
        }

        static void RunReportsModule()
        {
            Console.WriteLine("\n--- Módulo de Reportes ---");

            Report salesReport = new SalesReport();
            salesReport.GenerateReport();

            Report stockReport = new StockReport();
            stockReport.GenerateReport();
        }

        static void SeedData()
        {
            // Empleados (HU2)
            _db.Employees.Add(new Employee { Id = 1, Name = "Francisco Villaverde", Role = "Vendedor" }); // [cite: 1006]
            _db.Employees.Add(new Employee { Id = 2, Name = "Saúl Santiago", Role = "Almacenista" }); // [cite: 1006]

            // Productos (HU4)
            var p1 = new Product { Id = "1", Name = "Cuaderno Profesional", Price = 45.00m, Stock = 20 };
            var p2 = new Product { Id = "2", Name = "Lápiz Mirado", Price = 8.50m, Stock = 50 };
            var p3 = new Product { Id = "3", Name = "Pluma BIC", Price = 7.00m, Stock = 100 };
            var p4 = new Product { Id = "4", Name = "Pluma Negra (Stock Bajo)", Price = 7.00m, Stock = 6 };

            // Configurar Observador (HU4 - Alertas)
            var stockAlert = new StockAlert("admin@arcoiris.com");
            p4.RegisterObserver(stockAlert);

            // Configurar Composite
            var papeleriaCat = new CategoryComposite("Papelería");
            papeleriaCat.Add(p1);
            papeleriaCat.Add(p2);
            var escrituraCat = new CategoryComposite("Escritura");
            escrituraCat.Add(p3);
            escrituraCat.Add(p4);
            papeleriaCat.Add(escrituraCat);
            _db.RootCategory.Add(papeleriaCat);

            _db.Products.AddRange(new List<Product> { p1, p2, p3, p4 });
        }
    }
}