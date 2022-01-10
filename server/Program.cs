using server.Models;
using server.Services;

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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Connection string for all services
builder.Services.Configure<DbConnection>(builder.Configuration.GetSection("DbConnection"));

// Add Services
builder.Services.AddScoped<IAuteurService, AuteurService>();

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