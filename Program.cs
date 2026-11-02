using Final_Edi3.Components;
using Final_Edi3.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<Final_Edi3.Services.IProductoService, Final_Edi3.Services.ProductoService>();
builder.Services.AddDbContextFactory<TiendaDbContext>(opts =>  opts.UseSqlite(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<Final_Edi3.Services.CarritoState>();
//builder.Services.AddScoped<Final_Edi3.Services.ProductoState>();
builder.Services.AddHealthChecks()
    .AddCheck<Final_Edi3.Data.BaseDeDatosHealthCheck>("base-de-datos");

var app = builder.Build();

app.MapHealthChecks("/health");
var cadena = builder.Configuration.GetConnectionString("Default");
//var rutaBase = new SqliteConnectionStringBuilder(cadena).DataSource;
// Carpeta de la base de datos: ./datos junto a la aplicación
var carpetaDatos = Path.Combine(builder.Environment.ContentRootPath, "datos");
Directory.CreateDirectory(carpetaDatos);
AppDomain.CurrentDomain.SetData("DataDirectory", carpetaDatos);

var carpetaBase = Path.GetDirectoryName(Path.GetFullPath(rutaBase));
if (!string.IsNullOrEmpty(carpetaBase))
    Directory.CreateDirectory(carpetaBase);

using (var scope = app.Services.CreateScope())
{
    var factory = scope.ServiceProvider
        .GetRequiredService<IDbContextFactory<TiendaDbContext>>();
    await using var db = await factory.CreateDbContextAsync();
    await db.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
