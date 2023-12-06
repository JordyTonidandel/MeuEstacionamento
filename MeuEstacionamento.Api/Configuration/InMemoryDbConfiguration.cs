using MeuEstacionamento.Repository;
using Microsoft.EntityFrameworkCore;

namespace MeuEstacionamento.Api.Configuration
{
    public static class InMemoryDbConfiguration
    {
        public static void ConfigureInMemoryDb(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("MeuBdEmMemoria"));
        }
    }
}
