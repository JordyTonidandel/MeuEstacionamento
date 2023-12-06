using MeuEstacionamento.Core;
using MeuEstacionamento.Domain.Base;

namespace MeuEstacionamento.Domain
{
    public class Van : Veiculo
    {
        public Van() : base(EnumTipoVeiculo.Van) { }
    }
}
