

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PrimerSemana.Models;
using PrimerSemana.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

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
        var response = await _apiService.PostAsync("Account/login", model);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(string.Empty, "Credenciales incorrectas.");
            return View(model);
        }

        using var jsonDoc = await response.Content.ReadFromJsonAsync<JsonDocument>();
        var token = jsonDoc?.RootElement.GetProperty("token").GetString();

        if (string.IsNullOrEmpty(token))
        {
            ModelState.AddModelError(string.Empty, "No se pudo obtener el token de acceso.");
            return View(model);
        }

        // 1. Decodificamos el JWT para extraer automáticamente todas las Claims (incluyendo los Roles)
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var claims = jwtToken.Claims.ToList();

        // 2. Agregamos el token plano para que tus repositorios HTTP puedan usarlo después
        claims.Add(new Claim("Token", token));

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        // 3. Iniciamos sesión en el MVC con la identidad enriquecida
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        // 4. Verificamos inteligentemente si el usuario posee el rol de Administrador
        bool isAdmin = claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");

        if (isAdmin)
        {
            return RedirectToAction("Index", "Admin"); // 👈 Redirige al panel de administración si es admin
        }

        // 5. Si es un usuario normal, va al Home
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