using System;
using System.Collections.Generic;
using System.Text.Json.Serialization; // Necesario para evitar ciclos en JSON si los hubiera
using Proyecto.Inventory.Composite;
using Proyecto.Inventory.Observers;
using Proyecto.Inventory.Visitors;

namespace Proyecto.Core.Models
{
    public class Product : ICatalogItem, ICloneable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        private int _stock;

        // No serializamos observadores al JSON
        [JsonIgnore]
        private List<IStockObserver> _observers = new List<IStockObserver>();

        public int Stock
        {
            get => _stock;
            set { _stock = value; if (_stock < 5) NotifyObservers(); }
        }

        public void RegisterObserver(IStockObserver o) => _observers.Add(o);
        public void NotifyObservers() { if (_observers != null) foreach (var o in _observers) o.Update(this); }

        public void Display(int indent)
        {
            Console.WriteLine(new string(' ', indent) + $"- {Name} (${Price}) [Stock: {Stock}] (ID: {Id})");
        }

        // --- PATRÓN VISITOR ---
        public void Accept(IVisitor visitor)
        {
            visitor.VisitProduct(this);
        }

        public object Clone() => this.MemberwiseClone();
    }
}