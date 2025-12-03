using Proyecto.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Security
{
    public interface IAuthService
    {
        bool Login(string username, string password);
        void Logout();
    }

}

