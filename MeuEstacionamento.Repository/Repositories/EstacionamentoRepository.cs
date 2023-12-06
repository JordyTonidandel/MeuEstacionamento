using MeuEstacionamento.Domain;
using MeuEstacionamento.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeuEstacionamento.Repository.Repositories
{
    public class EstacionamentoRepository(ApplicationDbContext context) : IEstacionamentoRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Estacionamento> AtualizarEstacionamento(Estacionamento estacionamento, CancellationToken cancellationToken = default)
        {
            _context.Estacionamentos.Update(estacionamento);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return estacionamento;
        }

        public async Task<Estacionamento> BuscarEstacionamento(CancellationToken cancellationToken = default)
        {
            var estacionamento = await _context.Estacionamentos
                .AsNoTracking()
                .Include(i => i.Carros)
                .Include(i => i.Vans)
                .Include(i => i.Motos)
                .FirstOrDefaultAsync(cancellationToken);

            return estacionamento;
        }

        public async Task<Estacionamento> CriarEstacionamento(CancellationToken cancellationToken = default)
        {
            var estacionamento = new Estacionamento();

            await _context.Estacionamentos.AddAsync(estacionamento, cancellationToken).ConfigureAwait(false);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return estacionamento;
        }
    }
}
