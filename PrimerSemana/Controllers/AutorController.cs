using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimerSemana.Models;
using PrimerSemana.Services;

[Authorize]
public class AutorController : Controller
{
    private readonly IService_API _apiService;

    public AutorController(IService_API apiService)
    {
        _apiService = apiService;
    }

    public async Task<IActionResult> Index(int page = 1, string search = "")
    {
        var autores = await _apiService.GetAsync<Autor>($"Autores?page={page}&search={search}");

        // Si el usuario autenticado es Administrador, lo mandamos a la vista del panel admin
        if (User.IsInRole("Admin"))
        {
            return View("~/Views/Admin/Autores.cshtml", autores ?? new List<Autor>());
        }

        // Si es un usuario normal, le mostramos la vista estándar
        ViewBag.CurrentPage = page;
        ViewBag.Search = search;
        return View(autores ?? new List<Autor>());
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Create() => RedirectToAction(nameof(Index));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(Autor model)
    {
        if (ModelState.IsValid)
        {
            var response = await _apiService.PostAsync("Autores", model);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Edit(int id) => RedirectToAction(nameof(Index));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id, Autor model)
    {
        if (ModelState.IsValid)
        {
            var response = await _apiService.PutAsync($"Autores/{id}", model);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _apiService.DeleteAsync($"Autores/{id}");
        return RedirectToAction(nameof(Index));
    }
}