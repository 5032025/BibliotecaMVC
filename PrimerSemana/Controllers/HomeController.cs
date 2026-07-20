using Microsoft.AspNetCore.Mvc;
using PrimerSemana.Models;
using System.Diagnostics;

namespace PrimerSemana.Controllers
// Controlador para gestionar las operaciones relacionadas con la página de inicio
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var librosDestacados = new List<Libro>
          {
             new Libro { Titulo = "Fundación", Autor = "Isaac Asimov", Categoria = "Ciencia Ficción", Precio = 20.5m, Disponible = true },
            new Libro { Titulo = "Los pilares de la Tierra", Autor = "Ken Follett", Categoria = "Historia", Precio = 25.0m, Disponible = true },
            new Libro { Titulo = "El resplandor", Autor = "Stephen King", Categoria = "Terror", Precio = 18.0m, Disponible = true },
            new Libro { Titulo = "El cuaderno de Noah", Autor = "Nicholas Sparks", Categoria = "Romance", Precio = 15.0m, Disponible = true },
            new Libro { Titulo = "Juego de tronos", Autor = "George R.R. Martin", Categoria = "Fantasía", Precio = 22.0m, Disponible = true },
           new Libro { Titulo = "Sin noticias de Gurb", Autor = "Eduardo Mendoza", Categoria = "Comedia", Precio = 12.0m, Disponible = true }
            };

            return View(librosDestacados);
        }

        public IActionResult AcercaDe()
        {
            return View();
        }


       



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
