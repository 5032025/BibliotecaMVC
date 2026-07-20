using Microsoft.AspNetCore.Mvc;
using PrimerSemana.Models;

// Controlador para gestionar las operaciones relacionadas con los autores
namespace PrimerSemana.Controllers
{
    public class AutorController : Controller
    {
        public IActionResult Index()
        {
            var autores = new List<Autor>
    {
        new Autor {
            Id = 1,
            Nombre = "Isaac Asimov",
            Nacionalidad = "Estadounidense",
            FechaNacimiento = new DateTime(1920, 1, 2),
            FotoUrl = "IsaacAsimov.jpg"
        },
        new Autor {
            Id = 2,
            Nombre = "Ken Follett",
            Nacionalidad = "Británica",
            FechaNacimiento = new DateTime(1949, 6, 5),
            FotoUrl = "KenFollett.jpg"
        },
        new Autor {
            Id = 3,
            Nombre = "Stephen King",
            Nacionalidad = "Estadounidense",
            FechaNacimiento = new DateTime(1947, 9, 21),
            FotoUrl = "StephenKing.jpg"
        },
        new Autor {
           Id = 4,
           Nombre = "Nicholas Sparks",
           Nacionalidad = "Estadounidense",
           FechaNacimiento = new DateTime(1965, 12, 31),
           FotoUrl = "NicholasSparks.jpg"
        },
        new Autor {
           Id = 5,
           Nombre = "George R.R. Martin",
           Nacionalidad = "Estadounidense",
           FechaNacimiento = new DateTime(1948, 9, 20),
           FotoUrl = "GeorgeR.R.Martin.jpg"
        },
        new Autor {
            Id = 6,
            Nombre = "Eduardo Mendoza",
            Nacionalidad = "Española",
            FechaNacimiento = new DateTime(1943, 11, 11),
            FotoUrl = "EduardoMendoza.jpeg"
        }
    };

            return View(autores);
        }
    }
}
