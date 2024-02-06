using Microsoft.AspNetCore.Mvc;
using UsoPagamentoRenovacao.Core.Handlers.Commands;

namespace UsoPagamentoRenovacao.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProrrogacaoController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Prorrogar([FromServices]IMediatorHandler mediatorHandler)
        {

            await mediatorHandler.EnviarComando(new SolicitarProrrogacaoCommand());

            return Ok();
        }

        [HttpGet("{id:Guid}")]
        public IActionResult ObterPorId()
        {
            return Ok();
        }

        [HttpGet("{idFormulario:Guid}")]
        public IActionResult ObterPorIdFormulario()
        {
            return Ok();
        }

        [HttpGet("{codigoContrato}")]
        public IActionResult ObterPorContrato()
        {
            return Ok();

        }
    }
}