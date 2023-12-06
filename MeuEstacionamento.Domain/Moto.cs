using MeuEstacionamento.Core;
using MeuEstacionamento.Domain.Base;

namespace MeuEstacionamento.Domain
{
    public class Moto : Veiculo
    {
        public Moto() : base(EnumTipoVeiculo.Moto) { }
    }
}
