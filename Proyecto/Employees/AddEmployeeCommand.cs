using Proyecto.Core;
using Proyecto.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Employees
{
    // --- PATRÓN COMMAND ---
    // 2. El Comando Concreto (para HU2: Módulo de Empleados)
    public class AddEmployeeCommand : IEmployeeCommand
    {
        private readonly Employee _employee;
        private readonly DatabaseService _db;

        public AddEmployeeCommand(Employee employee)
        {
            _employee = employee;
            _db = DatabaseService.GetInstance(); // Usa el Singleton
        }

        public void Execute()
        {
            _employee.Id = _db.Employees.Count + 1;
            _db.Employees.Add(_employee);
            Console.WriteLine($"Empleado '{_employee.Name}' añadido con ID: {_employee.Id}");
        }
    }
}
