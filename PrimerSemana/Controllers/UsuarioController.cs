using Microsoft.AspNetCore.Mvc;
using PrimerSemana.Models;
using System.Collections.Generic;
using System.Linq;

namespace PrimerSemana.Controllers
{
    public class UsuarioController : Controller
    {
        // Cambiar private static por public static para que otros controladores puedan verla
        public static List<UsuarioModelSimulado> _usuariosBD = new List<UsuarioModelSimulado>
    {
        new UsuarioModelSimulado { Email = "admin@bibliocore.com", Password = "123", Rol = "Admin", Nombre = "Administrador", Ubicacion = "San Salvador", Edad = 30 },
         new UsuarioModelSimulado { Email = "usuario@ejemplo.com", Password = "123", Rol = "Usuario", Nombre = "Usuario Demo", Ubicacion = "San Salvador", Edad = 25 }
    };
        // Declaración única de la sesión (inicia en null para obligar a pasar por el Login)
        private static string _emailUsuarioSesion = null;

        // Métodos estáticos auxiliares para consultar la sesión desde otros controladores
        public static string ObtenerEmailActual() => _emailUsuarioSesion;

        public static string ObtenerRolActual()
        {
            var user = _usuariosBD.FirstOrDefault(u => u.Email == _emailUsuarioSesion);
            return user?.Rol ?? "Usuario";
        }

        // 1. Vista de Perfil (Solo accesible si está logueado)
        public IActionResult Index()
        {
            var usuarioActual = _usuariosBD.FirstOrDefault(u => u.Email == _emailUsuarioSesion);
            if (usuarioActual == null) return RedirectToAction("Login");

            ViewBag.Rol = usuarioActual.Rol;
            ViewBag.EsAdmin = usuarioActual.Rol == "Admin";

            return View(usuarioActual);
        }

        [HttpPost]
        public IActionResult Editar(UsuarioModelSimulado usuarioModificado)
        {
            var usuario = _usuariosBD.FirstOrDefault(u => u.Email == _emailUsuarioSesion);
            if (usuario != null)
            {
                usuario.Nombre = usuarioModificado.Nombre;
                usuario.Ubicacion = usuarioModificado.Ubicacion;
                usuario.Edad = usuarioModificado.Edad;
            }
            return RedirectToAction("Index");
        }

        // --- ACCESOS DE LOGIN Y REGISTRO ---

        public IActionResult Login() => View("~/Views/Auth/Login.cshtml");

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _usuariosBD.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                _emailUsuarioSesion = user.Email; // Establecemos la sesión simulada

                // Redirigir según el rol
                if (user.Rol == "Admin")
                {
                    return RedirectToAction("Index", "Home"); // Esto activará el AdminDashboard en el HomeController
                }
                else
                {
                    return RedirectToAction("Index", "Home"); // Esto activará el catálogo de usuario
                }
            }

            ModelState.AddModelError("", "Credenciales inválidas.");
            return View("~/Views/Auth/Login.cshtml");
        }

        public IActionResult Register() => View("~/Views/Auth/Register.cshtml");

        [HttpPost]
        public IActionResult Register(string nombre, string email, string password, string ubicacion, int edad)
        {
            if (_usuariosBD.Any(u => u.Email == email))
            {
                ModelState.AddModelError("", "El correo ya está registrado.");
                return View("~/Views/Auth/Register.cshtml");
            }

            var nuevoUsuario = new UsuarioModelSimulado
            {
                Nombre = nombre,
                Email = email,
                Password = password,
                Ubicacion = ubicacion,
                Edad = edad,
                Rol = "Usuario" // Por defecto se registran como usuarios normales
            };

            _usuariosBD.Add(nuevoUsuario);
            _emailUsuarioSesion = email; // Autenticar automáticamente al registrarse

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            _emailUsuarioSesion = null;
            return RedirectToAction("Login");
        }
    }

    // Modelo auxiliar en memoria para la simulación
    public class UsuarioModelSimulado
    {
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; } // "Admin" o "Usuario"
        public string Ubicacion { get; set; }
        public int Edad { get; set; }
    }
}