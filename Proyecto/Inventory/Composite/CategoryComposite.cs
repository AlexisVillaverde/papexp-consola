using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Inventory.Composite
{
    public class CategoryComposite : ICatalogItem
    {
        public string Name { get; set; }
        private List<ICatalogItem> _items = new List<ICatalogItem>();

        public CategoryComposite(string name) { Name = name; }
        public void Add(ICatalogItem item) => _items.Add(item);
        public void Remove(ICatalogItem item) => _items.Remove(item);

        public void Display(int indent)
        {
            Console.WriteLine(new string(' ', indent) + $"+ Categoría: {Name}");
            foreach (var item in _items)
            {
                item.Display(indent + 2);
            }
        }
    }
}
