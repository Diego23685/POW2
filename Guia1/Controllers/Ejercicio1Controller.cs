using Microsoft.AspNetCore.Mvc;

namespace GuiaMVC.Controllers
{
    // RUTA BASE: /Ejercicio1
    public class Ejercicio1Controller : Controller
    {
        // GET /Ejercicio1/convertir?texto=hola mundo
        // Devuelve el texto en MAYÚSCULAS y al revés.
        [HttpGet]
        public IActionResult Convertir(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return Content("Proporciona ?texto=...");

            string mayus = texto.ToUpperInvariant();
            string inverso = new string(mayus.Reverse().ToArray());
            return Content(inverso);
        }

        // GET /Ejercicio1/comparar?a=uno&b=UNO
        // Retorna si son iguales o diferentes (comparación ordinal ignorando mayúsculas)
        [HttpGet]
        public IActionResult Comparar(string a, string b)
        {
            if (a is null || b is null)
                return Content("Proporciona ?a=...&b=...");

            bool iguales = string.Equals(a, b, System.StringComparison.OrdinalIgnoreCase);
            return Content(iguales ? "Iguales" : "Diferentes");
        }

        // GET /Ejercicio1/perfil
        // Devuelve una vista simple con datos básicos.
        [HttpGet]
        public IActionResult Perfil()
        {
            // Podrías pasar un modelo, pero para lo simple basta con ViewData
            ViewData["Nombre"] = "Diego Rafael Mairena López";
            ViewData["Edad"] = 20;
            ViewData["Carrera"] = "Ingeniería en Sistemas de Información";
            ViewData["FotoUrl"] = Url.Content("~/images/4569906ab0a3e73daba334dc493dfe08.jpg");
            return View();
        }
    }
}
