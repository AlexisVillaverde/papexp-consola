using System;
using System.Linq;
using Proyecto.Core;
using Proyecto.Core.Models;

namespace Proyecto.Employees.Commands
{
    public class AddEmployeeCommand : IEmployeeCommand
    {
        private Employee _employee;
        private DatabaseService _db = DatabaseService.GetInstance();

        public AddEmployeeCommand(Employee employee)
        {
            _employee = employee;
        }

        public void Execute()
        {
            // Generar ID autoincremental
            int newId = _db.Employees.Any() ? _db.Employees.Max(e => e.Id) + 1 : 1;
            _employee.Id = newId;

            _db.Employees.Add(_employee);
            _db.SaveData(); // Guardar cambios en JSON

            Console.WriteLine($"Empleado agregado: {_employee.Name} (ID: {_employee.Id})");
        }
    }
}