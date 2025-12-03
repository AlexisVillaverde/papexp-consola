using Proyecto.Core.Models;

namespace Proyecto.Employees.Factories
{
    // Creador Concreto para Administradores
    public class AdminFactory : EmployeeFactory
    {
        public override Employee CreateEmployee(string name, string password)
        {
            return new Employee
            {
                Name = name,
                Password = password,
                Role = "Admin" // Rol predefinido
            };
        }
    }
}