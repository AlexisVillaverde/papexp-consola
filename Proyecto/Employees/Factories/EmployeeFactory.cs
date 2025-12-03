using Proyecto.Core.Models;

namespace Proyecto.Employees.Factories
{
    // Clase creadora abstracta
    public abstract class EmployeeFactory
    {
        public abstract Employee CreateEmployee(string name, string password);
    }
}