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

        // Si el usuario autenticado es Administrador, lo dirigimos a la vista de administración en Admin/Libros.cshtml
        if (User.IsInRole("Admin"))
        {
            return View("~/Views/Admin/Libros.cshtml", books ?? new List<Libro>());
        }

        // Si es un usuario normal, enviamos los datos a su respectiva vista estándar
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
    public IActionResult Create() => RedirectToAction(nameof(Index));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(Libro model, int autorIdForm, int categoriaIdForm)
    {
        // Aseguramos que se asigne a AutorIds (con 'r') tal como lo exige la API
        model.AutorIds = new List<int> { autorIdForm };
        model.CategoriaIds = new List<int> { categoriaIdForm };

        if (ModelState.IsValid)
        {
            var response = await _apiService.PostAsync("Libros", model);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, "Error de API: " + errorContent);
            }
        }

        var books = await _apiService.GetAsync<Libro>("Libros");
        return View("~/Views/Admin/Libros.cshtml", books ?? new List<Libro>());
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Edit(int id) => RedirectToAction(nameof(Index));

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
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _apiService.DeleteAsync($"Libros/{id}");
        return RedirectToAction(nameof(Index));
    }
}