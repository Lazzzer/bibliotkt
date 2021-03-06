using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using server.Services;
using server.Services.Interfaces;
using server.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowCredentials().AllowAnyMethod();
        });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "BiblioTkt API",
        Description = "Une API pour l'application web BiblioTkt dans le cadre du cours de BDR"
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Add Connection string for all services
builder.Services.Configure<DbConnection>(builder.Configuration.GetSection("DbConnection"));

// Add Services
builder.Services.AddScoped<IAuteurService, AuteurService>();
builder.Services.AddScoped<ILivreService, LivreService>();
builder.Services.AddScoped<ICategorieService, CategorieService>();
builder.Services.AddScoped<IEtatUsureService, EtatUsureService>();
builder.Services.AddScoped<IMaisonEditionService, MaisonEditionService>();
builder.Services.AddScoped<IEditionService, EditionService>();
builder.Services.AddScoped<IEmployeService, EmployeService>();
builder.Services.AddScoped<IMembreService, MembreService>();
builder.Services.AddScoped<IEmpruntService, EmpruntService>();
builder.Services.AddScoped<IExemplaireService, ExemplaireService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();