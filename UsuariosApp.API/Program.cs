using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

//Configuração para que os endpoints da API sejam exibidos em letras minúsculas.
builder.Services.AddRouting(map => map.LowercaseUrls = true);

//Configuração para habilitar a documentação do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//Executar a documentação do Swagger
app.UseSwagger();
app.UseSwaggerUI();

//Executar a documentação do Scalar
app.MapScalarApiReference(options =>
{
    options.WithTheme(ScalarTheme.BluePlanet);
});

app.UseAuthorization();
app.MapControllers();
app.Run();



