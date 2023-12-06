using MeuEstacionamento.Core;

namespace MeuEstacionamento.Domain.Base
{
    public abstract class Veiculo(EnumTipoVeiculo tipo)
    {
        public int Id { get; set; } = 0;
        public EnumTipoVeiculo Tipo { get; } = tipo;
    }
}
