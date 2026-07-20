using Microsoft.AspNetCore.Mvc;
using PrimerSemana.Models;

// Controlador para gestionar las operaciones relacionadas con los usuarios
public class UsuarioController : Controller
{
    // Simulamos un usuario cargado desde la base de datos
    private static Usuario _usuarioActual = new Usuario
    {
        Nombre = "Usuario Demo",
        Email = "usuario@ejemplo.com",
        Ubicacion = "San Salvador",
        Edad = 25
    };

    public IActionResult Index() => View(_usuarioActual);

    [HttpPost]
    public IActionResult Editar(Usuario usuario)
    {
        _usuarioActual = usuario; 
        return RedirectToAction("Index");
    }
}