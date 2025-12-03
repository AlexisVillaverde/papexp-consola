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

            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== PAPEXP - Papelería Arcoíris ===\n");
                Console.WriteLine("1. Módulo de Ventas");
                Console.WriteLine("2. Módulo de Almacén");
                Console.WriteLine("3. Módulo de Empleados");
                Console.WriteLine("4. Módulo de Reportes");
                Console.WriteLine("5. Salir");
                Console.Write("\nSeleccione una opción: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": RunSalesFlow(); break;
                    case "2": RunWarehouseModule(); break;
                    case "3": RunEmployeeModule(); break;
                    case "4": RunReportsModule(); break;
                    case "5": running = false; break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // ------------------- MODULO DE VENTAS -------------------
        static void RunSalesFlow()
        {
            Console.Clear();
            Console.WriteLine("=== MÓDULO DE VENTAS ===\n");

            var cashier = _db.Employees.Find(e => e.Id == 1);
            var saleBuilder = new SaleBuilder(cashier);

            bool adding = true;

            while (adding)
            {
                Console.Clear();
                Console.WriteLine("=== NUEVA VENTA ===\n");

                Console.WriteLine("ID   | Producto                       | Precio | Stock ");
                Console.WriteLine("--------------------------------------------------------");

                foreach (var p in _db.Products)
                    Console.WriteLine($"{p.Id.PadRight(4)} | {p.Name.PadRight(28)} | ${p.Price,-6:F2} | {p.Stock}");

                Console.Write("\nIngrese el ID del producto: ");
                string productId = Console.ReadLine();

                Console.Write("Cantidad: ");
                if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
                {
                    Console.WriteLine("Cantidad inválida.");
                    Console.ReadKey();
                    continue;
                }

                saleBuilder.AddProduct(productId, qty);

                Console.Write("\n¿Agregar otro producto? (S/N): ");
                adding = Console.ReadLine().Trim().ToUpper() == "S";
            }

            // Construir venta
            Sale sale = saleBuilder.Build();

            // Decorator (descuento)
            ISale finalSale = new BaseSale(sale);
            finalSale = new EmployeeDiscountDecorator(finalSale);

            // Método de pago
            Console.Clear();
            Console.WriteLine("=== MÉTODO DE PAGO ===\n");
            Console.WriteLine("1. Efectivo");
            Console.WriteLine("2. Tarjeta");
            Console.Write("\nSeleccione: ");
            string pay = Console.ReadLine();

            var pos = new PointOfSaleFacade();

            if (pay == "2")
                pos.SetPaymentStrategy(new CardPaymentStrategy());
            else
                pos.SetPaymentStrategy(new CashPaymentStrategy());

            Console.Clear();
            pos.CompleteSale(finalSale);

            Console.WriteLine("\nPresione una tecla para regresar al Menú principal...");
            Console.ReadKey();
        }

        // ------------------- MÓDULO DE ALMACÉN -------------------
        static void RunWarehouseModule()
        {
            Console.Clear();
            Console.WriteLine("=== MÓDULO DE ALMACÉN ===\n");

            Console.WriteLine("Catálogo actual:\n");
            _db.RootCategory.Display(0);

            Console.WriteLine("\nImportando productos del sistema antiguo...\n");
            var importer = new LegacyProductImporter();
            importer.ImportProducts();

            Console.WriteLine("\nCatálogo actualizado:\n");
            _db.RootCategory.Display(0);

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }

        // ------------------- MÓDULO DE EMPLEADOS -------------------
        static void RunEmployeeModule()
        {
            Console.Clear();
            Console.WriteLine("=== MÓDULO DE EMPLEADOS ===\n");

            var manager = new EmployeeManager();
            var newEmployee = new Employee { Name = "Cesar Martínez", Role = "Admin" };

            IEmployeeCommand command = new AddEmployeeCommand(newEmployee);
            manager.ProcessCommand(command);

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }

        // ------------------- MÓDULO DE REPORTES -------------------
        static void RunReportsModule()
        {
            Console.Clear();
            Console.WriteLine("=== MÓDULO DE REPORTES ===\n");

            new SalesReport().GenerateReport();
            new StockReport().GenerateReport();

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }

        // ------------------- SEEDING DE DATOS -------------------
        static void SeedData()
        {
            _db.Employees.Add(new Employee { Id = 1, Name = "Francisco Villaverde", Role = "Vendedor" });
            _db.Employees.Add(new Employee { Id = 2, Name = "Saúl Santiago", Role = "Almacenista" });

            var p1 = new Product { Id = "1", Name = "Cuaderno Profesional", Price = 45m, Stock = 20 };
            var p2 = new Product { Id = "2", Name = "Lápiz Mirado", Price = 8.5m, Stock = 50 };
            var p3 = new Product { Id = "3", Name = "Pluma BIC", Price = 7m, Stock = 100 };
            var p4 = new Product { Id = "4", Name = "Pluma Negra (Stock Bajo)", Price = 7m, Stock = 6 };

            var alert = new Proyecto.Inventory.Observers.StockAlert("admin@arcoiris.com");
            p4.RegisterObserver(alert);

            var catMain = new Proyecto.Inventory.Composite.CategoryComposite("Papelería");
            var catWriting = new Proyecto.Inventory.Composite.CategoryComposite("Escritura");

            catMain.Add(p1);
            catMain.Add(p2);
            catWriting.Add(p3);
            catWriting.Add(p4);
            catMain.Add(catWriting);

            _db.RootCategory.Add(catMain);

            _db.Products.AddRange(new List<Product> { p1, p2, p3, p4 });
        }
    }
}