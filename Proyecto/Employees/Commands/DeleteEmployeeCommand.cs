using System;
using System.Linq;
using Proyecto.Core;

namespace Proyecto.Employees.Commands
{
    public class DeleteEmployeeCommand : IEmployeeCommand
    {
        private int _id;
        private DatabaseService _db = DatabaseService.GetInstance();

        public DeleteEmployeeCommand(int id)
        {
            _id = id;
        }

        public void Execute()
        {
            var emp = _db.Employees.FirstOrDefault(e => e.Id == _id);
            if (emp != null)
            {
                // Evitar que se borre a sí mismo si es el usuario actual
                if (_db.CurrentUser != null && _db.CurrentUser.Id == emp.Id)
                {
                    Console.WriteLine("⚠️ Error: No puedes eliminar tu propio usuario mientras estás logueado.");
                    return;
                }

                _db.Employees.Remove(emp);
                _db.SaveData();
                Console.WriteLine($"🗑️ Empleado ID {_id} ({emp.Name}) eliminado.");
            }
            else
            {
                Console.WriteLine($"❌ Error: No se encontró empleado con ID {_id}.");
            }
        }
    }
}