using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OptiStock.AppContext;
using OptiStock.Repositories;
using OptiStock.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Configuración de MongoDB
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDbSettings")
);

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddSingleton<MongoDbContext>();


// Repositorios
builder.Services.AddScoped<IAuditoriasRepository, AuditoriasRepository>();
builder.Services.AddScoped<IReportesRepository, ReportesRepository>();
builder.Services.AddScoped<INotificacionesRepository, NotificacionesRepository>();
builder.Services.AddScoped<IHistorialPreciosRepository, HistorialPreciosRepository>();
builder.Services.AddScoped<IHistorialCostosRepository, HistorialCostosRepository>();
builder.Services.AddScoped<IHistorialCajasRepository, HistorialCajasRepository>();
builder.Services.AddScoped<IHistorialMovimientosRepository, HistorialMovimientosRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
