using MeuEstacionamento.Domain;
using Microsoft.EntityFrameworkCore;

namespace MeuEstacionamento.Repository
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseInMemoryDatabase("MeuBdEmMemoria");
            }
        }

        public DbSet<Carro> Carros { get; set; }
        public DbSet<Moto> Motos { get; set; }
        public DbSet<Van> Vans { get; set; }
        public DbSet<Estacionamento> Estacionamentos { get; set; }
    }
}
