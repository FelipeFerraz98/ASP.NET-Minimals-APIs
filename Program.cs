using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.Dominio.DTOs;
using MinimalAPI.Dominio.Interfaces;
using MinimalAPI.Dominio.ModelViews;
using MinimalAPI.Dominio.Servico;
using MinimalAPI.Infraestrutura.Db;
using MinimalAPI.Dominio.Entidades;

#region Builder
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAdministradorServico, AdministradorServico>();
builder.Services.AddScoped<IVeiculoServico, VeiculoServico>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbContexto>(options => {
    options.UseMySql(
        builder.Configuration.GetConnectionString("mysql"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))
    );
});

var app = builder.Build();
#endregion

#region Home
app.MapGet("/", () => Results.Json(new Home()));
#endregion

#region Administradores
app.MapPost("/admnistradores/login", ([FromBody] LoginDTO loginDTO, IAdministradorServico administradorServico) => {
    if(administradorServico.Login(loginDTO) != null)
        return Results.Ok("Logado com sucesso");
    else
        return Results.Unauthorized();
});
#endregion

#region Veiculos
app.MapPost("/veiculos", ([FromBody] VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico) => {
    
    var veiculo = new Veiculo{
        Nome = veiculoDTO.Nome,
        Marca = veiculoDTO.Marca,
        Ano = veiculoDTO.Ano,  
    };
    
    veiculoServico.Incluir(veiculo);

    return Results.Created($"/veiculo/{veiculo.Id}", veiculo);
});

#endregion

#region App
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
#endregion