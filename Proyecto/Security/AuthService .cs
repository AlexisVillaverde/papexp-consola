using Proyecto.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Security
{
    public class AuthService
    {
        public bool Login(string username, string password)
        {
            var db = DatabaseService.GetInstance();
            // Busca empleado que coincida en nombre y password
            var emp = db.Employees.FirstOrDefault(e => e.Name == username && e.Password == password);
            if (emp != null)
            {
                db.CurrentUser = emp; //Establece la sesión en el Sigletón
                return true;
            }
            return false;
        }
        public void Logout()
        {
            DatabaseService.GetInstance().CurrentUser = null;
        }
    }
}
