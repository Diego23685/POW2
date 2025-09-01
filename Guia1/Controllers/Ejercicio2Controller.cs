using Microsoft.AspNetCore.Mvc;

namespace GuiaMVC.Controllers
{
    // RUTA BASE: /Ejercicio2
    public class Ejercicio2Controller : Controller
    {
        // Arreglo privado con al menos 5 elementos
        private readonly string[] _items = new[]
        {
            "manzana", "banana", "cereza", "durazno", "uva", "mango", "pera"
        };

        // GET /Ejercicio2/buscar/indice/2
        // Busca por posición y devuelve la cadena en esa posición.
        [HttpGet("Ejercicio2/buscar/indice/{pos:int}")]
        public IActionResult BuscarPorIndice(int pos)
        {
            if (pos < 0 || pos >= _items.Length)
                return Content($"Índice fuera de rango. Usa 0..{_items.Length - 1}");

            return Content(_items[pos]);
        }

        // GET /Ejercicio2/buscar/texto?q=uva
        // Busca si existe una cadena dentro del arreglo (contiene, ignore case).
        [HttpGet("Ejercicio2/buscar/texto")]
        public IActionResult BuscarPorTexto(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return Content("Proporciona ?q=...");

            var existe = _items.Any(s =>
                s.Contains(q, StringComparison.OrdinalIgnoreCase));

            if (!existe) return Content("No encontrada");

            var coincidencias = _items
                .Select((s, i) => new { s, i })
                .Where(x => x.s.Contains(q, StringComparison.OrdinalIgnoreCase))
                .Select(x => $"{x.i}:{x.s}");

            return Content("Encontrada(s): " + string.Join(", ", coincidencias));
        }
    }
}
