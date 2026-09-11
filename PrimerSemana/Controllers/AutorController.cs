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

        ViewBag.CurrentPage = page;
        ViewBag.Search = search;

        return View(autores ?? new List<Autor>());
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Create() => View();

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
        return View(model);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id)
    {
        var autor = await _apiService.GetSingleAsync<Autor>($"Autores/{id}");
        if (autor == null) return NotFound();
        return View(autor);
    }

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
        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _apiService.DeleteAsync($"Autores/{id}");
        return RedirectToAction(nameof(Index));
    }
}