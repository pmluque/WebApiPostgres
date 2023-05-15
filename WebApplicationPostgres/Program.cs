using NetCoreWebApiPostgres.Data;
using Microsoft.Extensions.Configuration;
using WebApplicationPostgres.Startup;
using NetCoreWebApiPostgres.Data.Repositories;

// Cargar todas las variables de configuración, tanto del fichero como del sistemas a partir de las variables de entorno
// Metodo #1
    /*
    var config = new ConfigurationBuilder()
                     .AddJsonFile("appsettings.json")
                     .AddEnvironmentVariables()
                     .Build();
    var settingsDatabase = config.GetRequiredSection("Database:PostgresSQL").Get<PostgresSQLConfiguration>();
    var connectionString = settingsDatabase?.ConnectionString;
    */

// Método #2
var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppSettings();
var settings = builder.Configuration.GetRequiredSection("Database:PostgresSQL").Get<PostgresSQLConfiguration>();
var connectionString = settings?.ConnectionString;


// Método #3 : Coger variables directamente del builder y 
var connectionStringPostgres = builder.Configuration["Database:PostgresSQL:ConnectionString"];
var postgresSQLConfiguration = new PostgresSQLConfiguration( connectionStringPostgres );
builder.Services.AddSingleton(postgresSQLConfiguration);

// Inyección de dependencia
builder.Services.AddScoped<ICarRepository, CarRepository>();
// ------------------------------------------------------------------------------------------------------------------------


// Add services to the container.

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
