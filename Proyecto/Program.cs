// Importamos todos los módulos y patrones
using Proyecto.Core;
using Proyecto.Core.Models;
using Proyecto.Employees;
using Proyecto.Employees.Commands;
using Proyecto.Employees.Factories;
using Proyecto.Inventory.Adapters;
using Proyecto.Inventory.Commands;
using Proyecto.Inventory.Factories;
using Proyecto.Inventory.Visitors;
using Proyecto.Reports.Proxies;
using Proyecto.Sales;
using Proyecto.Sales.Builders;
using Proyecto.Sales.Chain;
using Proyecto.Sales.Decorators;
using Proyecto.Sales.Strategies;
using Proyecto.Security;
using Proyecto.Core;
using Proyecto.Core.Models;
using Proyecto.Employees;
using Proyecto.Employees.Commands;
using Proyecto.Employees.Factories;
using Proyecto.Inventory.Adapters;
using Proyecto.Inventory.Commands;
using Proyecto.Inventory.Visitors;
using Proyecto.Reports.Proxies;
using Proyecto.Sales;
using Proyecto.Sales.Builders;
using Proyecto.Sales.Chain;
using Proyecto.Sales.Decorators;
using Proyecto.Sales.Strategies;
using Proyecto.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto
{
    class Program
    {
        // SINGLETON: Acceso global a la BD
        private static DatabaseService _db = DatabaseService.GetInstance();

        static void Main(string[] args)
        {
            // PROXY: El login pasa por el proxy de seguridad
            IAuthService auth = new AuthProxy();
            bool appRunning = true;

            while (appRunning)
            {
                Console.Clear();
                Console.WriteLine("=========================================");
                Console.WriteLine("   PAPELERÍA ARCOÍRIS - SISTEMA CENTRAL   ");
                Console.WriteLine("=========================================");
                Console.WriteLine("1. Iniciar Sesión");
                Console.WriteLine("2. Salir del Sistema");
                Console.Write("\nSeleccione una opción: ");

                string mainOp = Console.ReadLine();

                if (mainOp == "2") break;

                if (mainOp == "1")
                {
                    Console.Write("\nUsuario: ");
                    string u = Console.ReadLine();
                    Console.Write("Contraseña: ");
                    string p = Console.ReadLine();

                    // Intentar login
                    if (auth.Login(u, p))
                    {
                        // Si es exitoso, entramos al menú principal
                        RunMainMenu(auth);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n[!] Credenciales incorrectas.");
                        Console.ResetColor();
                        Console.WriteLine("Presione una tecla para continuar...");
                        Console.ReadKey();
                    }
                }
            }
            Console.WriteLine("Sistema apagado.");
        }

        static void RunMainMenu(IAuthService auth)
        {
            bool sessionActive = true;
            while (sessionActive)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"USUARIO ACTIVO: {_db.CurrentUser.Name} | ROL: {_db.CurrentUser.Role}");
                Console.ResetColor();
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("1. Punto de Venta (Carrito, Cobro)");

                // Menú dinámico según ROL
                if (_db.CurrentUser.Role == "Admin")
                {
                    Console.WriteLine("2. Gestión de Inventario (CRUD, Importar, Exportar)");
                    Console.WriteLine("3. Gestión de Empleados (CRUD)");
                    Console.WriteLine("4. Reportes del Sistema");
                }

                Console.WriteLine("5. Cerrar Sesión");
                Console.Write("\n> ");
                string op = Console.ReadLine();

                switch (op)
                {
                    case "1": RunSalesModule(); break;
                    case "2":
                        if (CheckAdmin()) RunInventoryModule();
                        break;
                    case "3":
                        if (CheckAdmin()) RunEmployeesModule();
                        break;
                    case "4":
                        if (CheckAdmin()) RunReportsModule();
                        break;
                    case "5":
                        auth.Logout();
                        sessionActive = false;
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }

        // Helper para validar rol rápidamente en UI
        static bool CheckAdmin() => _db.CurrentUser.Role == "Admin";

        // ---------------------------------------------------------
        // MÓDULO DE VENTAS (Builder, Memento, Chain, Decorator, Strategy, Facade, State)
        // ---------------------------------------------------------
        static void RunSalesModule()
        {
            Console.Clear();
            Console.WriteLine("--- PUNTO DE VENTA ---");

            // BUILDER: Inicia la construcción de la venta
            var builder = new SaleBuilder(_db.CurrentUser);
            bool building = true;

            while (building)
            {
                Console.Clear();
                Console.WriteLine("--- CARRITO DE COMPRAS ---");
                var currentSale = builder.GetSale();

                if (currentSale.Items.Count == 0) Console.WriteLine("(Vacío)");
                else
                {
                    var groups = currentSale.Items.GroupBy(x => x.Name);
                    foreach (var g in groups)
                        Console.WriteLine($"- {g.Count()}x {g.Key} (${g.First().Price * g.Count()})");
                    Console.WriteLine($"\nSUBTOTAL: ${currentSale.Items.Sum(x => x.Price)}");
                }

                Console.WriteLine("\n[1] Ver Catálogo y Agregar");
                Console.WriteLine("[2] Deshacer Último Agregado (Memento)");
                Console.WriteLine("[3] Finalizar y Cobrar");
                Console.WriteLine("[4] Cancelar y Salir");
                Console.Write("> ");
                string op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        Console.WriteLine("\n--- CATÁLOGO ---");
                        foreach (var p in _db.Products)
                            Console.WriteLine($"ID: {p.Id} | {p.Name} | ${p.Price} | Stock: {p.Stock}");

                        Console.Write("\nIngrese ID del producto: ");
                        string id = Console.ReadLine();
                        Console.Write("Cantidad: ");
                        if (int.TryParse(Console.ReadLine(), out int qty))
                        {
                            builder.AddProduct(id, qty);
                            // OBSERVER: Al bajar stock en memoria (si se implementara en tiempo real), saltaría alerta
                        }
                        break;

                    case "2":
                        // MEMENTO: Undo
                        builder.RemoveLastAdded();
                        Console.WriteLine("Última acción deshecha.");
                        Console.ReadKey();
                        break;

                    case "3":
                        FinalizeCheckout(builder.GetSale());
                        building = false; // Salir tras cobrar
                        break;

                    case "4":
                        return;
                }
            }
        }

        static void FinalizeCheckout(Sale sale)
        {
            // CHAIN OF RESPONSIBILITY: Validaciones antes de cobrar
            var stockCheck = new StockHandler();
            var limitCheck = new LimitHandler();
            stockCheck.SetNext(limitCheck);

            if (!stockCheck.Handle(sale))
            {
                Console.WriteLine("\n[!] La venta no pasó las validaciones automáticas.");
                Console.ReadKey();
                return;
            }

            // DECORATOR: Descuentos
            ISale finalSale = new BaseSale(sale);
            Console.Write("\n¿Cliente tiene tarjeta de puntos/empleado? (s/n): ");
            if (Console.ReadLine().ToLower() == "s")
            {
                Console.Write("Su descuento de 10% se aplicará automáticamente.\n");
                finalSale = new EmployeeDiscountDecorator(finalSale);
                Console.WriteLine("-> Descuento aplicado.");
            }

            // FACADE & STRATEGY: Cobro
            var posFacade = new PointOfSaleFacade();
            Console.WriteLine($"\nTOTAL A PAGAR: ${finalSale.GetTotalCost():F2}");
            Console.WriteLine("Seleccione método de pago:");
            Console.WriteLine("1. Efectivo");
            Console.WriteLine("2. Tarjeta Bancaria");
            string payOp = Console.ReadLine();

            if (payOp == "2") posFacade.SetPaymentStrategy(new CardPaymentStrategy());
            else posFacade.SetPaymentStrategy(new CashPaymentStrategy());

            // Ejecuta el cobro a través de la fachada
            // STATE: Internamente cambia el estado de la venta a 'PaidState'
            posFacade.CompleteSale(finalSale);

            // Persistencia: Guardar cambios de stock y venta
            foreach (var item in sale.Items)
            {
                var p = _db.Products.FirstOrDefault(x => x.Id == item.Id);
                if (p != null) p.Stock--;
            }
            _db.Sales.AddSale(sale);
            _db.SaveData();

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }

        // ---------------------------------------------------------
        // MÓDULO DE INVENTARIO (Command, Abstract Factory, Adapter, Prototype, Visitor)
        // ---------------------------------------------------------
        static void RunInventoryModule()
        {
            bool inModule = true;
            while (inModule)
            {
                Console.Clear();
                Console.WriteLine("--- GESTIÓN DE INVENTARIO ---");
                Console.WriteLine("1. Listar Productos");
                Console.WriteLine("2. Agregar Producto Nuevo (Command)");
                Console.WriteLine("3. Editar Producto (Command)");
                Console.WriteLine("4. Eliminar Producto (Command)");
                Console.WriteLine("5. Crear desde Kit Predefinido (Abstract Factory)");
                Console.WriteLine("6. Clonar Producto Existente (Prototype)");
                Console.WriteLine("7. Importar desde CSV Legacy (Adapter)");
                Console.WriteLine("8. Exportar Inventario Completo (Visitor)");
                Console.WriteLine("9. Volver");
                Console.Write("> ");
                string op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        Console.WriteLine("\nListado:");
                        foreach (var p in _db.Products) p.Display(0);
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Write("ID: "); string id = Console.ReadLine();
                        Console.Write("Nombre: "); string n = Console.ReadLine();
                        Console.Write("Precio: "); decimal pr = decimal.Parse(Console.ReadLine());
                        Console.Write("Stock: "); int s = int.Parse(Console.ReadLine());
                        new AddProductCommand(new Product { Id = id, Name = n, Price = pr, Stock = s }).Execute();
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.Write("ID a editar: "); string eid = Console.ReadLine();
                        Console.Write("Nuevo Precio: "); decimal npr = decimal.Parse(Console.ReadLine());
                        Console.Write("Nuevo Stock: "); int ns = int.Parse(Console.ReadLine());
                        new UpdateProductCommand(eid, npr, ns).Execute();
                        Console.ReadKey();
                        break;
                    case "4":
                        Console.Write("ID a eliminar: "); string did = Console.ReadLine();
                        new DeleteProductCommand(did).Execute();
                        Console.ReadKey();
                        break;
                    case "5": // ABSTRACT FACTORY
                        Console.WriteLine("1. Kit Escolar | 2. Kit Oficina");
                        string kitOp = Console.ReadLine();
                        IProductKitFactory factory = (kitOp == "1") ? (IProductKitFactory)new SchoolKitFactory() : new OfficeKitFactory();

                        var p1 = factory.CreateWritingTool();
                        var p2 = factory.CreatePaperProduct();

                        // Añadir los productos creados por la fábrica a la BD
                        new AddProductCommand(p1).Execute();
                        new AddProductCommand(p2).Execute();
                        Console.WriteLine("Kit agregado al inventario.");
                        Console.ReadKey();
                        break;
                    case "6": // PROTOTYPE
                        Console.Write("ID del producto a clonar: ");
                        string cloneId = Console.ReadLine();
                        var proto = _db.Products.FirstOrDefault(x => x.Id == cloneId);
                        if (proto != null)
                        {
                            var clone = (Product)proto.Clone();
                            clone.Id = clone.Id + "_copy"; // Nuevo ID
                            clone.Name += " (Copia)";
                            new AddProductCommand(clone).Execute();
                            Console.WriteLine("Producto clonado.");
                        }
                        Console.ReadKey();
                        break;
                    case "7": // ADAPTER
                        var adapter = new LegacyProductImporter();
                        adapter.ImportProducts();
                        _db.SaveData();
                        Console.WriteLine("Importación legacy finalizada.");
                        Console.ReadKey();
                        break;
                    case "8": // VISITOR
                        var visitor = new InventoryExportVisitor();
                        Console.WriteLine("\n--- EXPORTACIÓN DE DATOS ---");
                        foreach (var p in _db.Products) p.Accept(visitor);
                        Console.WriteLine("--- FIN EXPORTACIÓN ---");
                        Console.ReadKey();
                        break;
                    case "9": inModule = false; break;
                }
            }
        }

        // ---------------------------------------------------------
        // MÓDULO DE EMPLEADOS (Command, Factory Method)
        // ---------------------------------------------------------
        static void RunEmployeesModule()
        {
            var manager = new EmployeeManager();
            bool inModule = true;
            while (inModule)
            {
                Console.Clear();
                Console.WriteLine("--- GESTIÓN DE EMPLEADOS ---");
                foreach (var e in _db.Employees) Console.WriteLine($"ID: {e.Id} | {e.Name} | {e.Role}");

                Console.WriteLine("\n1. Agregar Nuevo (Factory + Command)");
                Console.WriteLine("2. Editar (Command)");
                Console.WriteLine("3. Eliminar (Command)");
                Console.WriteLine("4. Volver");
                Console.Write("> ");
                string op = Console.ReadLine();

                if (op == "1")
                {
                    Console.Write("Nombre: "); string name = Console.ReadLine();
                    Console.Write("Password: "); string pass = Console.ReadLine();
                    Console.WriteLine("Tipo: 1. Vendedor | 2. Admin");
                    string type = Console.ReadLine();

                    // FACTORY METHOD: Decide qué tipo de empleado crear
                    EmployeeFactory factory = (type == "2") ? (EmployeeFactory)new AdminFactory() : new VendedorFactory();
                    var newEmp = factory.CreateEmployee(name, pass);

                    // COMMAND: Ejecuta la inserción
                    manager.ProcessCommand(new AddEmployeeCommand(newEmp));
                    Console.ReadKey();
                }
                else if (op == "2")
                {
                    Console.Write("ID: "); int id = int.Parse(Console.ReadLine());
                    Console.Write("Nuevo Nombre: "); string n = Console.ReadLine();
                    Console.Write("Nuevo Rol: "); string r = Console.ReadLine();
                    Console.Write("Nueva Pass: "); string p = Console.ReadLine();
                    manager.ProcessCommand(new UpdateEmployeeCommand(id, n, r, p));
                    Console.ReadKey();
                }
                else if (op == "3")
                {
                    Console.Write("ID: "); int id = int.Parse(Console.ReadLine());
                    manager.ProcessCommand(new DeleteEmployeeCommand(id));
                    Console.ReadKey();
                }
                else if (op == "4") inModule = false;
            }
        }

        // ---------------------------------------------------------
        // MÓDULO DE REPORTES (Proxy, Template Method, Iterator)
        // ---------------------------------------------------------
        static void RunReportsModule()
        {
            Console.Clear();
            Console.WriteLine("--- REPORTES (Acceso Protegido por Proxy) ---");

            // PROXY: Instanciamos el proxy, no el servicio real directamente
            IReportService reportService = new ReportServiceProxy();

            Console.WriteLine("1. Reporte de Ventas (Iterator + Template Method)");
            Console.WriteLine("2. Reporte de Stock (Template Method)");
            Console.WriteLine("3. Volver");
            Console.Write("> ");
            string op = Console.ReadLine();

            if (op == "1") reportService.ShowSalesReport();
            else if (op == "2") reportService.ShowStockReport();

            if (op != "3")
            {
                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}