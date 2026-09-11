using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimerSemana.Models;
using PrimerSemana.Services;

[Authorize]
public class PrestamoController : Controller
{
    private readonly IService_API _apiService;

    public PrestamoController(IService_API apiService)
    {
        _apiService = apiService;
    }

    public async Task<IActionResult> Index(int page = 1, string search = "")
    {
        var prestamos = await _apiService.GetAsync<Prestamo>($"Reservas?page={page}&search={search}");

        ViewBag.CurrentPage = page;
        ViewBag.Search = search;

        return View(prestamos ?? new List<Prestamo>());
    }

    public async Task<IActionResult> Details(int id)
    {
        var prestamo = await _apiService.GetSingleAsync<Prestamo>($"Reservas/{id}");
        if (prestamo == null) return NotFound();
        return View(prestamo);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Prestamo model)
    {
        if (ModelState.IsValid)
        {
            var response = await _apiService.PostAsync("Reservas", model);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _apiService.DeleteAsync($"Reservas/{id}");
        return RedirectToAction(nameof(Index));
    }
}