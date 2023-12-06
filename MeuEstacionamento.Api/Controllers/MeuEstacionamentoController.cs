using MeuEstacionamento.Api.ViewModel;
using MeuEstacionamento.Domain;
using MeuEstacionamento.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeuEstacionamento.Api.Controllers
{
    [ApiController]
    [Route("api/v1/estacionamento")]
    public class MeuEstacionamentoController : ControllerBase
    {
        private readonly IEstacionamentoService _estacionamentoService;

        public MeuEstacionamentoController(IEstacionamentoService estacionamentoService)
        {
            _estacionamentoService = estacionamentoService;
        }

        [HttpPost("iniciar")]
        public async Task<IActionResult> IniciarEstacionamento()
        {
            var estacionamento = await _estacionamentoService.IniciarEstacionamento();

            return Ok(estacionamento);
        }

        [HttpPost("estacionar-carro")]
        public async Task<IActionResult> AdicionarCarro()
        {
            var veiculo = new Carro();

            var estacionamento = await _estacionamentoService.AdicionarVeiculo(veiculo);

            return Ok(new ResultViewModel(true, "Carro estacionado com sucesso!" , estacionamento));
        }

        [HttpPost("estacionar-moto")]
        public async Task<IActionResult> AdicionarMoto()
        {
            var veiculo = new Moto();
            var estacionamento = await _estacionamentoService.AdicionarVeiculo(veiculo);

            return Ok(new ResultViewModel(true, "Moto estacionada com sucesso!" , estacionamento));
        }

        [HttpPost("estacionar-van")]
        public async Task<IActionResult> AdicionarVan()
        {
            var veiculo = new Van();
            var estacionamento = await _estacionamentoService.AdicionarVeiculo(veiculo);

            return Ok(new ResultViewModel(true, "Van estacionada com sucesso!" , estacionamento));
        }

        [HttpGet("vagas-restantes")]
        public async Task<IActionResult> VagasRestantes()
        {
            var vagasRestantes = await _estacionamentoService.VagasRestantes();

            return Ok(new ResultViewModel(true, "Sucesso", vagasRestantes));
        }

        [HttpGet("vagas-totais")]
        public async Task<IActionResult> VagasTotais()
        {
            var vagasTotais = await _estacionamentoService.VagasTotais();

            return Ok(new ResultViewModel(true, "Sucesso", vagasTotais));
        }

        [HttpGet("vagas-ocupadas-por-vans")]
        public async Task<IActionResult> VagasOcupadasPorVans()
        {
            var vagasOcupadasPorVans = await _estacionamentoService.VagasOcupadasPorVans();

            return Ok(new ResultViewModel(true, "Sucesso", $"Vagas ocupadas por vans: {vagasOcupadasPorVans}"));
        }

        [HttpPost("resetar-estacionamento")]
        public async Task<IActionResult> ResetarEstacionamento()
        {
            var estacionamento = await _estacionamentoService.ResetarEstacionamento();

            return Ok(new ResultViewModel(true, "Estacionamento resetado com sucesso", estacionamento));
        }
    }
}
