using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Employees
{
    internal interface IEmployeeCommand
    {
        // --- PATRÓN COMMAND ---
        // 1. La Interfaz de Comando
        public interface IEmployeeCommand
        {
            void Execute();
        }
    }
}
