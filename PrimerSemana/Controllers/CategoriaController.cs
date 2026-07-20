using Microsoft.AspNetCore.Mvc;
using PrimerSemana.Models; 
namespace PrimerSemana.Controllers

// Controlador para gestionar las operaciones relacionadas con los categorías

{
    public class CategoriaController : Controller
    {
        public IActionResult Index()
        {

            var lista = new List<Categoria>
{
    new Categoria { Id = 1, Nombre = "Ciencia ficción", Descripcion = "Exploración de realidades alternativas, avances tecnológicos y el impacto de la ciencia en la condición humana." },
    new Categoria { Id = 2, Nombre = "Historia", Descripcion = "Análisis cronológico de acontecimientos, figuras y procesos sociales que han definido la trayectoria de la humanidad." },
    new Categoria { Id = 3, Nombre = "Terror", Descripcion = "Relatos inmersivos que exploran los límites del miedo, lo sobrenatural y la psicología humana ante lo desconocido." },
    new Categoria { Id = 4, Nombre = "Romance", Descripcion = "Obras centradas en la complejidad de las relaciones interpersonales, la pasión y los vínculos afectivos." },
    new Categoria { Id = 5, Nombre = "Fantasía", Descripcion = "Narrativas épicas ambientadas en mundos imaginarios donde la magia y la mitología desafían las leyes de la realidad." },
    new Categoria { Id = 6, Nombre = "Comedia", Descripcion = "Obras que utilizan el ingenio, la sátira y el humor para observar la ironía de la vida cotidiana." }
};
            return View(lista);
        }
    }
}