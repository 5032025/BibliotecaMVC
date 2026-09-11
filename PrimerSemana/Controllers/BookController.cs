using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimerSemana.Models;
using PrimerSemana.Services;

[Authorize]
public class BookController : Controller
{
    private readonly IService_API _apiService;

    public BookController(IService_API apiService)
    {
        _apiService = apiService;
    }

    public async Task<IActionResult> Index(int page = 1, string search = "")
    {
        var books = await _apiService.GetAsync<Libro>($"Libros?page={page}&search={search}");

        ViewBag.CurrentPage = page;
        ViewBag.Search = search;

        return View(books ?? new List<Libro>());
    }

    public async Task<IActionResult> Details(int id)
    {
        var book = await _apiService.GetSingleAsync<Libro>($"Libros/{id}");
        if (book == null) return NotFound();
        return View(book);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Create() => View();
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(Libro model)
    {
        if (ModelState.IsValid)
        {
            var response = await _apiService.PostAsync("Libros", model);
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
        var book = await _apiService.GetSingleAsync<Libro>($"Libros/{id}");
        if (book == null) return NotFound();
        return View(book);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id, Libro model)
    {
        if (ModelState.IsValid)
        {
            var response = await _apiService.PutAsync($"Libros/{id}", model);
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
        await _apiService.DeleteAsync($"Libros/{id}");
        return RedirectToAction(nameof(Index));
    }
}