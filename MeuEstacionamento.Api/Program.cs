using MeuEstacionamento.Api.ViewModel;
using MeuEstacionamento.Core.Exceptions;
using MeuEstacionamento.Repository;
using MeuEstacionamento.Repository.Interfaces;
using MeuEstacionamento.Repository.Repositories;
using MeuEstacionamento.Service;
using MeuEstacionamento.Service.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("MeuBdEmMemoria"));

builder.Services.AddScoped<IEstacionamentoRepository, EstacionamentoRepository>();
builder.Services.AddScoped<IEstacionamentoService, EstacionamentoService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseExceptionHandler("/error");
app.Map("/error", (HttpContext http) =>
{
    var error = http.Features?.Get<IExceptionHandlerFeature>()?.Error;

    if (error != null)
    {
        var response = new ResultViewModel(false, error.Message, null);

        if (error is DomainException)
        {
            http.Response.StatusCode = 400;
        }
        else
            http.Response.StatusCode = 500;

        http.Response.WriteAsJsonAsync(response);
    }
});

app.Run();
