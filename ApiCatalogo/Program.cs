using ApiCatalogo.ApiEndpoints;
using ApiCatalogo.AppServicesExtensions;
using ApiCatalogo.Context;
using ApiCatalogo.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



builder.AddApiSwagger();
builder.AddPersistence();
builder.Services.AddCors();
builder.AddAutenticationJwt();

//builder.Services.AddSingleton<ITokenService>(new TokenService());

//builder.Services.AddScoped<ITokenService, TokenService>();


//builder.Services.AddAuthorization();


var app = builder.Build();

app.MapAutenticacaoEndpoints();
app.MapCategoriasEndpoints();
app.MapProdutosEndpoints();

var enviorement = app.Environment;
app.UsexceptionHandler(enviorement)
    .UseSwaggerMiddleware()
    .UseAppCors();
    
app.UseAuthentication();
app.UseAuthorization();

app.Run();
