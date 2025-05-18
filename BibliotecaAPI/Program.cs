using System.Text.Json.Serialization;
using BibliotecaAPI.Datos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Area de servicios
builder.Services.AddAutoMapper(typeof(Program));

// Para cuando no se tiene el uso de DTO's
//builder.Services.AddControllers().AddJsonOptions(opciones => 
//    opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Para cuando se tiene el uso de DTO's y se usara el patch
builder.Services.AddControllers().AddNewtonsoftJson();



builder.Services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlServer("name=DefaultConnection"));

// Fin de area de servicios

var app = builder.Build();

// Area de midleware

app.MapControllers();

app.Run();
