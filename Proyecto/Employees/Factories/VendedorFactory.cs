using Proyecto.Core.Models;

namespace Proyecto.Employees.Factories
{
    // Creador Concreto para Vendedores
    public class VendedorFactory : EmployeeFactory
    {
        public override Employee CreateEmployee(string name, string password)
        {
            return new Employee
            {
                Name = name,
                Password = password,
                Role = "Vendedor" // Rol predefinido
            };
        }
    }
}