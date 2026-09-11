

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PrimerSemana.Models;
using PrimerSemana.Services;
using System.Security.Claims;

public class AccountController : Controller
{
    private readonly IService_API _apiService; 

    public AccountController(IService_API apiService)
    {
        _apiService = apiService;
    }

    [HttpGet]
    public IActionResult Register() => View();

    
    [HttpPost]
    public async Task<IActionResult> Register(Usuario model)
    {
        var response = await _apiService.PostAsync("Account/Register", model);

       

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError("", "No se pudo completar el registro.");
            return View(model);
        }

        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var response = await _apiService.PostAsync("Account/Login", model);
        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError("", "Credenciales inválidas.");
            return View(model);
        }

        // Leemos la respuesta de la API que trae el token: { token = "..." }
        var result = await response.Content.ReadFromJsonAsync<TokenResponseModel>();

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, model.Email),
            new Claim("Token", result!.Token) // Guardamos el JWT para que el ApiService lo reenvíe
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
}

// Modelo auxiliar para capturar el token que devuelve el Login de la API
public class TokenResponseModel
{
    public string Token { get; set; }
}