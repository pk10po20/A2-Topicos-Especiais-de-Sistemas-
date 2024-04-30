using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;

var builder = WebApplication.CreateBuilder(args);

//Configuração Swagger no builder
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuração banco MySQL
builder.Services.AddDbContext<BancoDeDados>();

var app = builder.Build();

//Configuração Swagger na aplicação
app.UseSwagger();
app.UseSwaggerUI();

// http://localhost:xxxx/swagger/index.html

app.MapGet("/", () => "Desafio CSharp!");

//APIs

app.MapPessoasApi();

app.MapProdutosApi();

app.MapVendaApi();

app.Run();
