using MeuEstacionamento.Core;
using MeuEstacionamento.Core.Exceptions;
using MeuEstacionamento.Domain;
using MeuEstacionamento.Domain.Base;
using MeuEstacionamento.Repository.Interfaces;
using MeuEstacionamento.Service.Interfaces;

namespace MeuEstacionamento.Service
{
    public class EstacionamentoService : IEstacionamentoService
    {
        private readonly IEstacionamentoRepository _estacionamentoRepository;

        public EstacionamentoService(IEstacionamentoRepository estacionamentoRepository)
        {
            _estacionamentoRepository = estacionamentoRepository;
        }

        public async Task<Estacionamento> AdicionarVeiculo(Veiculo veiculo)
        {
            var estacionamento = await _estacionamentoRepository.BuscarEstacionamento();

            if (estacionamento == null)
                throw new DomainException("Não existe um estacionamento iniciado");

            ResetarAlerta(estacionamento);
            AdicionarVeiculoAoEstacionamento(veiculo, estacionamento);
            AnaliseVagas(estacionamento);

            return await _estacionamentoRepository.AtualizarEstacionamento(estacionamento);
        }

        private void AnaliseVagas(Estacionamento estacionamento)
        {
            if (estacionamento.VagasOcupadas == estacionamento.VagasTotais)
            {
                estacionamento.EstacionamentoCheio = true;
            }
            else if (estacionamento.VagasOcupadas > 0 && estacionamento.VagasOcupadas < estacionamento.VagasTotais)
            {
                estacionamento.EstacionamentoCheio = false;
                estacionamento.EstacionamentoVazio = false;
            }
            else if (estacionamento.VagasOcupadas == 0)
            {
                estacionamento.EstacionamentoVazio = true;
            }
            else
            {
                throw new DomainException("Erro ao analisar vagas do estacionamento");
            }
        }

        private static void ResetarAlerta(Estacionamento estacionamento)
        {
            estacionamento.ExibirAlerta = false;
            estacionamento.MensagemAlerta = string.Empty;
        }

        private static void AdicionarVeiculoAoEstacionamento(Veiculo veiculo, Estacionamento estacionamento)
        {
            switch (veiculo.Tipo)
            {
                case EnumTipoVeiculo.Carro:
                    AdicionarCarroAoEstacionamento((Carro)veiculo, estacionamento);
                    break;
                case EnumTipoVeiculo.Moto:
                    AdicionarMotoAoEstacionamento((Moto)veiculo, estacionamento);
                    break;
                case EnumTipoVeiculo.Van:
                    AdicionarVanAoEstacionamento((Van)veiculo, estacionamento);
                    break;
                default:
                    throw new Exception("Tipo de veículo não suportado");
            }
        }

        private static void AdicionarCarroAoEstacionamento(Carro carro, Estacionamento estacionamento)
        {
            if (estacionamento.VagasDeCarroOcupadas < estacionamento.VagasDeCarro)
            {
                estacionamento.VagasDeCarroOcupadas++;
                
                if (estacionamento.VagasDeCarroOcupadas == estacionamento.VagasDeCarro)
                {
                    estacionamento.ExibirAlerta = true;
                    estacionamento.MensagemAlerta = "Última vaga para carros ocupada";
                }
            }
            else if (estacionamento.VagasDeVanOcupadas < estacionamento.VagasDeVan)
            {
                estacionamento.VagasDeVanOcupadas++;

                if (estacionamento.VagasDeVanOcupadas == estacionamento.VagasDeVan)
                {
                    estacionamento.ExibirAlerta = true;
                    estacionamento.MensagemAlerta = "Última vaga para vans ocupada";
                }
            }
            else
            {
                throw new Exception("Não há mais vagas para carros");
            }

            estacionamento.VagasOcupadas++;
            estacionamento.VagasRestantes--;
            estacionamento.Carros.Add(carro);
        }

        private static void AdicionarMotoAoEstacionamento(Moto moto, Estacionamento estacionamento)
        {
            if (estacionamento.VagasDeMotoOcupadas < estacionamento.VagasDeMoto)
            {
                estacionamento.VagasDeMotoOcupadas++;

                if (estacionamento.VagasDeMotoOcupadas == estacionamento.VagasDeMoto)
                {
                    estacionamento.ExibirAlerta = true;
                    estacionamento.MensagemAlerta = "Última vaga para motos ocupada";
                }
            }
            else if (estacionamento.VagasDeCarroOcupadas < estacionamento.VagasDeCarro)
            {
                estacionamento.VagasDeCarroOcupadas++;

                if (estacionamento.VagasDeCarroOcupadas == estacionamento.VagasDeCarro)
                {
                    estacionamento.ExibirAlerta = true;
                    estacionamento.MensagemAlerta = "Última vaga para carros ocupada";
                }
            }
            else if (estacionamento.VagasDeVanOcupadas < estacionamento.VagasDeVan)
            {
                estacionamento.VagasDeVanOcupadas++;

                if (estacionamento.VagasDeVanOcupadas == estacionamento.VagasDeVan)
                {
                    estacionamento.ExibirAlerta = true;
                    estacionamento.MensagemAlerta = "Última vaga para vans ocupada";
                }
            }
            else
            {
                throw new Exception("Não há mais vagas para motos");
            }

            estacionamento.VagasOcupadas++;
            estacionamento.VagasRestantes--;
            estacionamento.Motos.Add(moto);
        }

        private static void AdicionarVanAoEstacionamento(Van van, Estacionamento estacionamento)
        {
            if (estacionamento.VagasDeVanOcupadas < estacionamento.VagasDeVan)
            {
                estacionamento.VagasDeVanOcupadas++;
                estacionamento.VagasOcupadas++;
                estacionamento.VagasRestantes--;

                if (estacionamento.VagasDeVanOcupadas == estacionamento.VagasDeVan)
                {
                    estacionamento.ExibirAlerta = true;
                    estacionamento.MensagemAlerta = "Última vaga para vans ocupada";
                }
            }
            else if (estacionamento.VagasDeCarroOcupadas <= estacionamento.VagasDeCarro - 3)
            {
                estacionamento.VagasDeCarroOcupadas += 3;
                estacionamento.VagasOcupadas += 3;
                estacionamento.VagasRestantes -= 3;

                if (estacionamento.VagasDeCarroOcupadas == estacionamento.VagasDeCarro)
                {
                    estacionamento.ExibirAlerta = true;
                    estacionamento.MensagemAlerta = "Última vaga para carros ocupada";
                }
            }
            else
            {
                throw new DomainException("Não há mais vagas para vans");
            }

            estacionamento.Vans.Add(van);
        }

        public async Task<int> VagasOcupadasPorVans()
        {
            var estacionamento = await _estacionamentoRepository.BuscarEstacionamento().ConfigureAwait(false);

            if (estacionamento == null)
                throw new Exception("Não existe um estacionamento iniciado");

            if (estacionamento.Vans.Count <= estacionamento.VagasDeVan)
            {
                return estacionamento.Vans.Count;
            }
            else if (estacionamento.Vans.Count > estacionamento.VagasDeVan)
            {
                return estacionamento.VagasDeVan + (3 * (estacionamento.Vans.Count - estacionamento.VagasDeVan));
            }
            else
            {
                throw new Exception("Não há mais vagas para vans");
            }
        }

        public async Task<int> VagasRestantes()
        {
            var estacionamento = await _estacionamentoRepository.BuscarEstacionamento().ConfigureAwait(false);

            if (estacionamento == null)
                throw new Exception("Não existe um estacionamento iniciado");

            return estacionamento.VagasRestantes;
        }

        public async Task<int> VagasTotais()
        {
            var estacionamento = await _estacionamentoRepository.BuscarEstacionamento().ConfigureAwait(false);

            if (estacionamento == null)
                throw new Exception("Não existe um estacionamento iniciado");

            return estacionamento.VagasTotais;
        }

        public async Task<Estacionamento> IniciarEstacionamento()
        {
            var estacionamento = await _estacionamentoRepository.BuscarEstacionamento().ConfigureAwait(false);

            if (estacionamento != null)
                throw new DomainException("Já existe um estacionamento iniciado");

            return await _estacionamentoRepository.CriarEstacionamento().ConfigureAwait(false);
        }

        public async Task<Estacionamento> ResetarEstacionamento()
        {
            var estacionamento = new Estacionamento();

            return await _estacionamentoRepository.AtualizarEstacionamento(estacionamento).ConfigureAwait(false);
        }
    }
}
