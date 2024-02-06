using MediatR;

namespace UsoPagamentoRenovacao.Core.Handlers.Commands
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediatr;

        public MediatorHandler(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        public async Task EnviarComando<T>(T comando) where T : Command
        {
            await _mediatr.Send(comando);
        }

        public async Task PublicarEvento<T>(T evento) where T : Event
        {
            await _mediatr.Publish(evento);
        }
    }
}
