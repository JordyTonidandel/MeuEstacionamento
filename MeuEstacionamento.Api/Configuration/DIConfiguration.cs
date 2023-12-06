using MeuEstacionamento.Repository.Interfaces;
using MeuEstacionamento.Repository.Repositories;
using MeuEstacionamento.Service.Interfaces;
using MeuEstacionamento.Service;

namespace MeuEstacionamento.Api.Configuration
{
    public static class DIConfiguration
    {
        public static void ConfigureDI(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IEstacionamentoRepository, EstacionamentoRepository>();
            builder.Services.AddScoped<IEstacionamentoService, EstacionamentoService>();
        }
    }
}
