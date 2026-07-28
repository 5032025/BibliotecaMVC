using Microsoft.AspNetCore.Mvc;
using PrimerSemana.Models;
using System.Diagnostics;
using System.Linq;

namespace PrimerSemana.Controllers
{
    // Controlador para gestionar las operaciones relacionadas con la página de inicio
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // 1. Validar sesión y rol actual
            string rolActual = UsuarioController.ObtenerRolActual();
            string usuarioActualEmail = UsuarioController.ObtenerEmailActual();

            if (string.IsNullOrEmpty(usuarioActualEmail))
            {
                // Si no ha iniciado sesión, mandarlo directo al Login
                return RedirectToAction("Login", "Usuario");
            }

            // 2. Si es Administrador, mostrar la vista exclusiva de Admin
            if (rolActual == "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }

            // 3. Si es Usuario normal, cargar los libros directamente desde la base de datos estática del Admin
            // Si quieres mostrar solo los disponibles, usa .Where(l => l.Disponible)
            // Si quieres mostrar todos (como los autores), simplemente usa AdminController._librosBD
            var librosDestacados = AdminController._librosBD;

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