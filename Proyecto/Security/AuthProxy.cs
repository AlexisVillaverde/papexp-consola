using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Security
{
    public class AuthProxy: IAuthService
    {
        private AuthService _realService = new AuthService();

        public bool Login(string user, string pass)
        {
            Console.WriteLine($"[Seguridad] Verificando credenciales para usuario '{user}'...");
            // Validaciones extra antes de llamar al servicio real
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                Console.WriteLine("[Seguridad] Error: Campos vacíos.");
                return false;
            }
            return _realService.Login(user, pass);
        }

        public void Logout() => _realService.Logout();
    }

}

