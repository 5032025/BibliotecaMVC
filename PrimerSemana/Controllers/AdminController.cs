using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PrimerSemana.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // Esto renderiza Views/Admin/Index.cshtml
        public IActionResult Index()
        {
            return View();
        }
    }
}