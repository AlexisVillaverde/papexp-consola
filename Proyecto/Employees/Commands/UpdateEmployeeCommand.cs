using System;
using System.Linq;
using Proyecto.Core;

namespace Proyecto.Employees.Commands
{
    public class UpdateEmployeeCommand : IEmployeeCommand
    {
        private int _id;
        private string _newName;
        private string _newRole;
        private string _newPassword;
        private DatabaseService _db = DatabaseService.GetInstance();

        public UpdateEmployeeCommand(int id, string newName, string newRole, string newPassword)
        {
            _id = id;
            _newName = newName;
            _newRole = newRole;
            _newPassword = newPassword;
        }

        public void Execute()
        {
            var emp = _db.Employees.FirstOrDefault(e => e.Id == _id);
            if (emp != null)
            {
                // Solo actualizamos si el valor no está vacío
                if (!string.IsNullOrEmpty(_newName)) emp.Name = _newName;
                if (!string.IsNullOrEmpty(_newRole)) emp.Role = _newRole;
                if (!string.IsNullOrEmpty(_newPassword)) emp.Password = _newPassword;

                _db.SaveData();
                Console.WriteLine($"✅ Empleado ID {_id} actualizado correctamente.");
            }
            else
            {
                Console.WriteLine($"❌ Error: No se encontró empleado con ID {_id}.");
            }
        }
    }
}