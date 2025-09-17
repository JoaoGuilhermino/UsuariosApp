using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;
using UsuariosApp.Domain.Interfaces.Repositories;
using UsuariosApp.Domain.Interfaces.Security;
using UsuariosApp.Domain.Interfaces.Services;
using UsuariosApp.Domain.Services;
using UsuariosApp.Infra.Data.Contexts;
using UsuariosApp.Infra.Data.Repositories;
using UsuariosApp.Infra.Security.Services;
using UsuariosApp.Infra.Security.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

//Configuração para que os endpoints da API sejam exibidos em letras minúsculas.
builder.Services.AddRouting(map => map.LowercaseUrls = true);

//Configuração para habilitar a documentação do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Injeção de dependência da classe DataContext do EntityFramework
//capturando a connectionstring do arquivo /appsettings.json
builder.Services.AddDbContext<DataContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(DataContext)));
});

//Injeção de dependência para capturar as configurações do JWT mapeadas no arquivo /appsettings.json
builder.Services.AddSingleton(builder.Configuration.GetSection(nameof(JwtBearerSettings))
       .Get<JwtBearerSettings>());

//Injeção de dependência para as demais interfaces e classes do projeto
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IPerfilRepository, PerfilRepository>();
builder.Services.AddScoped<IJwtBearerSecurity, JwtBearerSecurity>();

//Configurar a politica de autenticação do projeto
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    //configurações para validar o token
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true, //validando a expiração do token
        ValidateIssuerSigningKey = true, //valida a chave de assinatura do token
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
            (builder.Configuration.GetValue<string>("JwtBearerSettings:SecretKey")))
    };
});

//Configuração do CORS (Politica de acesso da API)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.MapOpenApi();

//Executar a documentação do Swagger
app.UseSwagger();
app.UseSwaggerUI();

//Executar a documentação do Scalar
app.MapScalarApiReference(options =>
{
    options.WithTheme(ScalarTheme.BluePlanet);
});

//Ativar a configuração do CORS
app.UseCors("AllowAll");

app.UseAuthentication(); //Aplicar as politicas de autenticação
app.UseAuthorization(); //Verificar as permissões de acesso

app.MapControllers();
app.Run();

//Confgurando a classe Program.cs como pública
public partial class Program { }


