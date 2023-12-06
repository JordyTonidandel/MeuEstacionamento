namespace MeuEstacionamento.Domain
{
    public class Estacionamento
    {
        public int Id { get; set; }
        public int VagasTotais { get; set; } = 20;
        public int VagasOcupadas { get; set; } = 0;
        public int VagasRestantes { get; set; } = 20;
        public int VagasDeMoto { get; set; } = 6;
        public int VagasDeMotoOcupadas { get; set; } = 0;
        public int VagasDeCarro { get; set; } = 10;
        public int VagasDeCarroOcupadas { get; set; } = 0;
        public int VagasDeVan { get; set; } = 4;
        public int VagasDeVanOcupadas { get; set; } = 0;
        public bool ExibirAlerta { get; set; } = false;
        public string MensagemAlerta { get; set; } = "";
        public bool EstacionamentoCheio { get; set; } = false;
        public bool EstacionamentoVazio { get; set; } = true;

        public List<Carro> Carros { get; set; } = [];
        public List<Moto> Motos { get; set; } = [];
        public List<Van> Vans { get; set; } = [];
    }
}
