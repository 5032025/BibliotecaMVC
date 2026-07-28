using Microsoft.AspNetCore.Mvc;
using PrimerSemana.Models;
using System.Linq;

namespace PrimerSemana.Controllers
{
    // Controlador para gestionar las operaciones relacionadas con los autores
    public class AutorController : Controller
    {
        public IActionResult Index()
        {
            // Enviamos toda la lista centralizada del Admin para que la vista maneje ambos estados
            var todosLosAutores = AdminController._autoresBD;

            return View(todosLosAutores);
        }
    }
}