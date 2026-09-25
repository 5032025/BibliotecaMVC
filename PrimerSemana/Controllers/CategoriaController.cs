using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimerSemana.Models;
using PrimerSemana.Services;

[Authorize]
public class CategoriaController : Controller
{
    private readonly IService_API _apiService;

    public CategoriaController(IService_API apiService)
    {
        _apiService = apiService;
    }

    public async Task<IActionResult> Index(int page = 1, string search = "")
    {
        var categorias = await _apiService.GetAsync<Categoria>($"Categorias?page={page}&search={search}");

        // Si el usuario autenticado es Administrador, lo enviamos a la vista unificada del panel admin
        if (User.IsInRole("Admin"))
        {
            return View("~/Views/Admin/Categoria.cshtml", categorias ?? new List<Categoria>());
        }

        // Si es un usuario normal, enviamos los datos a la vista estándar
        ViewBag.CurrentPage = page;
        ViewBag.Search = search;

        return View(categorias ?? new List<Categoria>());
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Create() => RedirectToAction(nameof(Index));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(Categoria model)
    {
        if (ModelState.IsValid)
        {
            var response = await _apiService.PostAsync("Categorias", model);
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
    public async Task<IActionResult> Edit(int id, Categoria model)
    {
        if (ModelState.IsValid)
        {
            var response = await _apiService.PutAsync($"Categorias/{id}", model);
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
        await _apiService.DeleteAsync($"Categorias/{id}");
        return RedirectToAction(nameof(Index));
    }
}