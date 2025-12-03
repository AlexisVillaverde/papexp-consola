using Proyecto.Core;
using System;

namespace Proyecto.Reports.Proxies
{
    public class ReportServiceProxy : IReportService
    {
        private ReportService _realService;
        private DatabaseService _db = DatabaseService.GetInstance();

        public ReportServiceProxy()
        {
            _realService = new ReportService();
        }

        public void ShowSalesReport()
        {
            if (CheckAdminAccess())
            {
                _realService.ShowSalesReport();
            }
        }

        public void ShowStockReport()
        {
            if (CheckAdminAccess())
            {
                _realService.ShowStockReport();
            }
        }

        private bool CheckAdminAccess()
        {
            // Validamos que el usuario actual tenga rol de Admin
            if (_db.CurrentUser != null && _db.CurrentUser.Role == "Admin")
            {
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ACCESO DENEGADO: Se requieren permisos de Administrador para ver reportes.");
                Console.ResetColor();
                return false;
            }
        }
    }
}