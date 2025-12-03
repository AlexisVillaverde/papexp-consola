
using Microsoft.VisualBasic;
using Proyecto.Sales.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization; // Para ignorar el Estado al guardar en JSON

namespace Proyecto.Core.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public Employee Cashier { get; set; }
        public List<Product> Items { get; set; } = new List<Product>();
        public decimal Total { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        // --- PATRÓN STATE ---
        // El estado no se guarda en la BD JSON, se reinicia al cargar o se maneja lógica aparte
        [JsonIgnore]
        public ISaleState State { get; set; }

        public Sale()
        {
            // Estado inicial por defecto
            State = new OpenState();
        }

        // Métodos que delegan al Estado
        public void Pay() => State.Pay(this);
        public void Cancel() => State.Cancel(this);
    }
}