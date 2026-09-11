using Microsoft.AspNetCore.Authentication.Cookies;
using PrimerSemana.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Soporte para MVC (Controladores y Vistas)
builder.Services.AddControllersWithViews();

// 2. Configurar HttpClient con la BaseUrl del appsettings.json
builder.Services.AddHttpClient("BookApi", client =>
{
    var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"];
    client.BaseAddress = new Uri(apiBaseUrl!);
});

// 3. Necesario para que el ApiService pueda leer el Token de las Cookies del usuario
builder.Services.AddHttpContextAccessor();

// 4. Registrar tu servicio personalizado
builder.Services.AddScoped<IService_API, Service_API_Repositorio>();

// 5. Configurar autenticación por Cookies (para guardar el Token JWT)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Ruta si no está autenticado
    });

var builderApp = builder.Build();

// Pipeline HTTP
if (!builderApp.Environment.IsDevelopment())
{
    builderApp.UseExceptionHandler("/Home/Error");
    builderApp.UseHsts();
}

builderApp.UseHttpsRedirection();
builderApp.UseStaticFiles();
builderApp.UseRouting();

builderApp.UseAuthentication(); // 👈 Obligatorio para leer el token de las cookies
builderApp.UseAuthorization();

builderApp.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

builderApp.Run();