using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimerSemana.Models;
using PrimerSemana.Services;

[Authorize]
public class HomeController : Controller
{
    private readonly IService_API _apiService;

    public HomeController(IService_API apiService)
    {
        _apiService = apiService;
    }

    public async Task<IActionResult> Index()
    {
        var librosDestacados = await _apiService.GetAsync<Libro>("Libros?page=1&search=");

        if (librosDestacados == null)
        {
            librosDestacados = new List<Libro>();
        }

        return View(librosDestacados);
    }

    public IActionResult AcercaDe()
    {
        return View();
    }
}