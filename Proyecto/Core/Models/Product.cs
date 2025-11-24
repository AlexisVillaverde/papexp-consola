using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto.Inventory.Composite;
using Proyecto.Inventory.Observers;

namespace Proyecto.Core.Models
{ 
    
    public class Product : ICatalogItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        private int _stock;

        // --- PATRÓN OBSERVER (Subject) ---
        private List<IStockObserver> _observers = new List<IStockObserver>();

        public int Stock
        {
            get => _stock;
            set
            {
                _stock = value;
                if (_stock < 5) NotifyObservers();
            }
        }

        public void RegisterObserver(IStockObserver o) => _observers.Add(o);
        public void RemoveObserver(IStockObserver o) => _observers.Remove(o);
        public void NotifyObservers()
        {
            foreach (var o in _observers) o.Update(this);
        }

        // --- PATRÓN COMPOSITE (Leaf) ---
        public void Display(int indent)
        {
            Console.WriteLine(new string(' ', indent) + $"- Producto: {Name} (Stock: {Stock}) - ${Price}");
        }
    }
}