using Microsoft.AspNetCore.Mvc;
using PrimerSemana.Models;

namespace PrimerSemana.Controllers

// Controlador para gestionar las operaciones relacionadas con los préstamos
{
    public class PrestamoController : Controller
    {
        
        public IActionResult Index()
        {
            var lista = new List<Prestamo>
    {
        new Prestamo { Id = 1, Libro = "Fundación", Autor = "Isaac Asimov", Usuario = "Usuario Demo", FechaVencimiento = DateTime.Now.AddDays(7), Estado = EstadoPrestamo.Activo },
        new Prestamo { Id = 2, Libro = "Los pilares de la Tierra", Autor = "Ken Follett", Usuario = "Usuario Demo", FechaVencimiento = DateTime.Now.AddDays(-2), Estado = EstadoPrestamo.Vencido },
        
        
        new Prestamo { Id = 3, Libro = "El resplandor", Autor = "Stephen King", Usuario = "Usuario Demo", FechaVencimiento = DateTime.Now.AddDays(-10), Estado = EstadoPrestamo.Devuelto }
    };

            return View(lista);
        }
    }
}
