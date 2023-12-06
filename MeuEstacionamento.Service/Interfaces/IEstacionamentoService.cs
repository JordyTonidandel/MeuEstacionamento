using MeuEstacionamento.Domain;
using MeuEstacionamento.Domain.Base;

namespace MeuEstacionamento.Service.Interfaces
{
    public interface IEstacionamentoService
    {
        Task<Estacionamento> IniciarEstacionamento();
        Task<Estacionamento> AdicionarVeiculo(Veiculo veiculo);
        Task<int> VagasRestantes();
        Task<int> VagasTotais();
        Task<int> VagasOcupadasPorVans();
        Task<Estacionamento> ResetarEstacionamento();
    }
}
