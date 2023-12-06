using MeuEstacionamento.Core;
using MeuEstacionamento.Domain.Base;

namespace MeuEstacionamento.Domain
{
    public class Carro : Veiculo
    {
        public Carro() : base(EnumTipoVeiculo.Carro) { }
    }
}
