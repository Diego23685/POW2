using Guia2.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// HttpClient con User-Agent obligatorio para e926
builder.Services.AddHttpClient("e926", c =>
{
    c.BaseAddress = new Uri("https://e926.net/");
    c.DefaultRequestHeaders.Add("User-Agent", "Guia2/1.0 (by tu_usuario_e926)"); // ← CAMBIA esto por tu user
    c.DefaultRequestHeaders.Add("Accept", "application/json");
});

// Servicio de acceso a la API
builder.Services.AddScoped<Ie926Api, E926Api>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Ruta por defecto a Posts/Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Posts}/{action=Index}/{id?}");

app.Run();
