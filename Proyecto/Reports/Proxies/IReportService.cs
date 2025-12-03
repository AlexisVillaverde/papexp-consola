using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Reports.Proxies

{
    // Interfaz común para el Proxy y el Servicio Real
    public interface IReportService
    {
        void ShowSalesReport();
        void ShowStockReport();
    }
}