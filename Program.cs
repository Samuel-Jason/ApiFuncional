using ApiTesta.Data;
using ApiTesta.Infra;
using ApiTesta.Infra.Auth;
using ApiTesta.Repository;
using ApiTesta.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Text;
using AutoMapper;
using ApiTesta.DTOs.Mappings;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers()
            .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

        builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
        builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

        builder.Services.AddScoped<IPessoaService, PessoaService>();
        builder.Services.AddScoped<ICategoriaService, CategoriaService>();
        builder.Services.AddScoped<IProdutoService, ProdutoService>();

        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddAutoMapper(typeof(MappingProfile));



        builder.Services.AddEntityFrameworkSqlServer()
            .AddDbContext<BancoContext>(o =>
                o.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

        // Adicionando CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()  // Permite qualquer origem (modifique conforme necess�rio)
                      .AllowAnyMethod()  // Permite qualquer m�todo HTTP (GET, POST, etc.)
                      .AllowAnyHeader(); // Permite qualquer cabe�alho
            });
        });

        // Configura��o da autentica��o JWT
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"], // Adicione estas configura��es no appsettings.json
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero // Remove a toler�ncia padr�o de 5 minutos
                };
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

        // Adicionando o uso da pol�tica de CORS
        app.UseCors("AllowAll");

        // Usando a autentica��o e autoriza��o
        app.UseAuthentication();  // Coloque antes de UseAuthorization
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}