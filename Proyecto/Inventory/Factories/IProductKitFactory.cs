using Proyecto.Core.Models;

namespace Proyecto.Inventory.Factories
{
    // ABSTRACT FACTORY: Define la interfaz para crear familias de objetos relacionados
    public interface IProductKitFactory
    {
        // Método para crear una herramienta de escritura (Lápiz, Pluma, etc.)
        Product CreateWritingTool();

        // Método para crear un producto de papel (Cuaderno, Hojas, Carpeta)
        Product CreatePaperProduct();
    }
}