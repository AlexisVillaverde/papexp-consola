using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Employees
{
    // --- PATRÓN COMMAND ---
    // 3. El Invocador (Invoker)
    public class EmployeeManager
    {
        public void ProcessCommand(IEmployeeCommand command)
        {
            Console.WriteLine("Procesando comando de gestión de empleado...");
            command.Execute();
        }
    }
}
