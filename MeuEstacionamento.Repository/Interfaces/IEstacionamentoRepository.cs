using MeuEstacionamento.Domain;

namespace MeuEstacionamento.Repository.Interfaces
{
    public interface IEstacionamentoRepository
    {
        Task<Estacionamento> CriarEstacionamento(CancellationToken cancellationToken = default);
        Task<Estacionamento> BuscarEstacionamento(CancellationToken cancellationToken = default);
        Task<Estacionamento> AtualizarEstacionamento(Estacionamento estacionamento, CancellationToken cancellationToken = default);
    }
}
