using Microsoft.AspNetCore.Mvc;
using PrimerSemana.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimerSemana.Controllers
{
    public class AdminController : Controller
    {
        // 1. Lista centralizada y pública de Autores
        public static List<Autor> _autoresBD = new List<Autor>
        {
            new Autor { Id = 1, Nombre = "Isaac Asimov", Nacionalidad = "Estadounidense", FechaNacimiento = new DateTime(1920, 1, 2), Estado = EstadoAutor.Activo, FotoUrl = "IsaacAsimov.jpg" },
            new Autor { Id = 2, Nombre = "Ken Follett", Nacionalidad = "Británica", FechaNacimiento = new DateTime(1949, 6, 5), Estado = EstadoAutor.Activo, FotoUrl = "KenFollett.jpg" },
            new Autor { Id = 3, Nombre = "Stephen King", Nacionalidad = "Estadounidense", FechaNacimiento = new DateTime(1947, 9, 21), Estado = EstadoAutor.Activo, FotoUrl = "StephenKing.jpg" },
            new Autor { Id = 4, Nombre = "Nicholas Sparks", Nacionalidad = "Estadounidense", FechaNacimiento = new DateTime(1965, 12, 31), Estado = EstadoAutor.Activo, FotoUrl = "NicholasSparks.jpg" },
            new Autor { Id = 5, Nombre = "George R.R. Martin", Nacionalidad = "Estadounidense", FechaNacimiento = new DateTime(1948, 9, 20), Estado = EstadoAutor.Activo, FotoUrl = "GeorgeR.R.Martin.jpg" },
            new Autor { Id = 6, Nombre = "Eduardo Mendoza", Nacionalidad = "Española", FechaNacimiento = new DateTime(1943, 11, 11), Estado = EstadoAutor.Activo, FotoUrl = "EduardoMendoza.jpeg" }
        };

        public static List<Libro> _librosBD = new List<Libro>
{
    new Libro { Titulo = "Fundación", Autor = "Isaac Asimov", Categoria = "Ciencia Ficción", Precio = 20.5m, Disponible = false, ImagenUrl = "~/images/Fundacion.jpg" },
    new Libro { Titulo = "Los pilares de la Tierra", Autor = "Ken Follett", Categoria = "Historia", Precio = 30.0m, Disponible = true, ImagenUrl = "~/images/LospilaresdelaTierra.jpg" },
    new Libro { Titulo = "El resplandor", Autor = "Stephen King", Categoria = "Terror", Precio = 18.0m, Disponible = true, ImagenUrl = "~/images/Elresplandor.jpg" },
    new Libro { Titulo = "El cuaderno de Noah", Autor = "Nicholas Sparks", Categoria = "Romance", Precio = 15.0m, Disponible = true, ImagenUrl = "~/images/ElcuadernodeNoah.jpg" },
    new Libro { Titulo = "Juego de tronos", Autor = "George R.R. Martin", Categoria = "Fantasía", Precio = 22.0m, Disponible = true, ImagenUrl = "~/images/Juegodetronos.jpg" },
    new Libro { Titulo = "Sin noticias de Gurb", Autor = "Eduardo Mendoza", Categoria = "Comedia", Precio = 12.0m, Disponible = true, ImagenUrl = "~/images/SinnoticiasdeGurb.jpg" }
};
        // Validación interna de seguridad de sesión y rol
        private bool EsAdminValido()
        {
            string rolActual = UsuarioController.ObtenerRolActual();
            string emailActual = UsuarioController.ObtenerEmailActual();
            return !string.IsNullOrEmpty(emailActual) && rolActual == "Admin";
        }

        // Dashboard principal de administración
        public IActionResult Index()
        {
            if (!EsAdminValido()) return RedirectToAction("Login", "Usuario");
            return View();
        }

        // ================= GESTIÓN DE LIBROS =================

        public IActionResult Libros()
        {
            if (!EsAdminValido()) return RedirectToAction("Login", "Usuario");
            return View(_librosBD);
        }

        [HttpPost]
        public IActionResult CrearLibro(Libro nuevoLibro)
        {
            if (!EsAdminValido()) return RedirectToAction("Login", "Usuario");

            var autorEncontrado = _autoresBD.FirstOrDefault(a => a.Nombre.ToLower() == nuevoLibro.Autor.ToLower());
            if (autorEncontrado == null || autorEncontrado.Estado == EstadoAutor.Inactivo)
            {
                ModelState.AddModelError("", "El autor no existe o se encuentra deshabilitado.");
                return View("Libros", _librosBD);
            }

            nuevoLibro.Disponible = true;
            _librosBD.Add(nuevoLibro);
            return RedirectToAction("Libros");
        }

        [HttpPost]
        public IActionResult CambiarEstadoLibro(string titulo)
        {
            if (!EsAdminValido()) return RedirectToAction("Login", "Usuario");

            var libro = _librosBD.FirstOrDefault(l => l.Titulo.ToLower() == titulo.ToLower());
            if (libro != null)
            {
                libro.Disponible = !libro.Disponible;
            }
            return RedirectToAction("Libros");
        }

        // GET: Editar Libro
        [HttpGet]
        public IActionResult EditarLibro(string titulo)
        {
            if (!EsAdminValido()) return RedirectToAction("Login", "Usuario");

            var libro = _librosBD.FirstOrDefault(l => l.Titulo.ToLower() == titulo.ToLower());
            if (libro == null) return NotFound();
            return View(libro);
        }

        // POST: Editar Libro
        [HttpPost]
        public IActionResult EditarLibro(Libro libroModificado)
        {
            if (!EsAdminValido()) return RedirectToAction("Login", "Usuario");

            var libroExistente = _librosBD.FirstOrDefault(l => l.Titulo.ToLower() == libroModificado.Titulo.ToLower());
            if (libroExistente != null)
            {
                libroExistente.Autor = libroModificado.Autor;
                libroExistente.Categoria = libroModificado.Categoria;
                libroExistente.Precio = libroModificado.Precio;
            }
            return RedirectToAction("Libros");
        }

        // ================= GESTIÓN DE AUTORES =================

        public IActionResult Autores()
        {
            if (!EsAdminValido()) return RedirectToAction("Login", "Usuario");
            return View(_autoresBD);
        }

        [HttpPost]
        public IActionResult CrearAutor(Autor nuevoAutor)
        {
            if (!EsAdminValido()) return RedirectToAction("Login", "Usuario");

            nuevoAutor.Id = _autoresBD.Any() ? _autoresBD.Max(a => a.Id) + 1 : 1;
            nuevoAutor.Estado = EstadoAutor.Activo;
            _autoresBD.Add(nuevoAutor);
            return RedirectToAction("Autores");
        }

        [HttpPost]
        public IActionResult CambiarEstadoAutor(int id)
        {
            if (!EsAdminValido()) return RedirectToAction("Login", "Usuario");

            var autor = _autoresBD.FirstOrDefault(a => a.Id == id);
            if (autor != null)
            {
                autor.Estado = (autor.Estado == EstadoAutor.Activo) ? EstadoAutor.Inactivo : EstadoAutor.Activo;

                if (autor.Estado == EstadoAutor.Inactivo)
                {
                    var librosDelAutor = _librosBD.Where(l => l.Autor.ToLower() == autor.Nombre.ToLower());
                    foreach (var libro in librosDelAutor)
                    {
                        libro.Disponible = false;
                    }
                }
            }
            return RedirectToAction("Autores");
        }

        

        // POST: Editar Autor (Protegiendo la imagen para que no cambie)
        [HttpPost]
        public IActionResult EditarAutor(Autor autorModificado)
        {
            if (!EsAdminValido()) return RedirectToAction("Login", "Usuario");

            var autorExistente = _autoresBD.FirstOrDefault(a => a.Id == autorModificado.Id);
            if (autorExistente != null)
            {
                autorExistente.Nombre = autorModificado.Nombre;
                autorExistente.Nacionalidad = autorModificado.Nacionalidad;
                autorExistente.FechaNacimiento = autorModificado.FechaNacimiento;
                // Nota: FotoUrl se omite a propósito para que la imagen original permanezca inalterada.
            }
            return RedirectToAction("Autores");
        }

        public IActionResult Usuarios()
        {
            if (!EsAdminValido()) return RedirectToAction("Login", "Usuario");
            var listaUsuarios = UsuarioController._usuariosBD;
            return View(listaUsuarios);
        }
    }
}