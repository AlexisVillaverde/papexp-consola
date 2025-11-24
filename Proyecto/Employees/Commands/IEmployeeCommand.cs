using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Employees.Commands
{
    public interface IEmployeeCommand
    {
        // --- PATRÓN COMMAND ---
        // 1. La Interfaz de Comando
            void Execute();
        
    }
}
