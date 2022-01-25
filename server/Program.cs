using server.Models;
using server.Services;
using server.Services.Interfaces;
using server.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(
      builder =>
      {
        builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowCredentials().AllowAnyMethod();
      });
});

builder.Services.AddControllers().AddJsonOptions( options => options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();