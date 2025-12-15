using Microsoft.EntityFrameworkCore;
using MiniMarket.API.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. KONFIGURACJA BAZY DANYCH
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MiniMarketContext>(options =>
    options.UseNpgsql(connectionString));

// 2. DODANIE KONTROLERÓW (To jest kluczowe, ¿eby ProductController zadzia³a³!)
builder.Services.AddControllers(); // <-- Tego brakowa³o

// Konfiguracja Swaggera
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Konfiguracja HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// 3. MAPOWANIE KONTROLERÓW (Dziêki temu aplikacja "widzi" pliki w folderze Controllers)
app.MapControllers(); // <-- Tego te¿ brakowa³o

app.Run();