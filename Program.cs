using ApiTesta.Data;
using ApiTesta.Repository;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();

builder.Services.AddEntityFrameworkSqlServer()
    .AddDbContext<BancoContext>(o =>
        o.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

// Adicionando CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Permite qualquer origem (modifique conforme necessário)
              .AllowAnyMethod()  // Permite qualquer método HTTP (GET, POST, etc.)
              .AllowAnyHeader(); // Permite qualquer cabeçalho
    });
});

var app = builder.Build();

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Adicionando o uso da política de CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
