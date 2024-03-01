using MassTransit;
using MediatR;
using UsoPagamentoRenovacao.Core.DomainModels.Models;
using UsoPagamentoRenovacao.Core.DomainServices.Interfaces.Gateways;
using UsoPagamentoRenovacao.Core.DomainServices.Interfaces.Repositories;
using UsoPagamentoRenovacao.Core.Handlers.Commands;
using UsoPagamentoRenovacao.Core.Handlers.Events;

namespace UsoPagamentoRenovacao.Core.Handlers
{
    public class ProrrogacaoHandler : IRequestHandler<SolicitarProrrogacaoCommand, bool>,
                                      IRequestHandler<ProrrogarContratoCommand, bool>,
                                      INotificationHandler<ProrrogacaoEfetuadaEvent>,
                                      INotificationHandler<ProrrogacaoNaoEfetuadaEvent>

    {

        private readonly IProrrogacaoRepository _prorrogacaoRepository;
        private readonly IContratosGateway _contratosGateway;
        private readonly IEventoRepository _eventoRepository;
        private readonly IMediatorHandler _mediatorHandler;

        private readonly IPublishEndpoint _publisher;
        private readonly ISendEndpoint _sendEndpoint;
        private readonly IBus _bus;

        public ProrrogacaoHandler(IProrrogacaoRepository prorrogacaoRepository,
            IContratosGateway contratosGateway,
            IEventoRepository eventoRepository,
            IMediatorHandler mediatorHandler,
            IPublishEndpoint publisher)
        {
            _prorrogacaoRepository = prorrogacaoRepository;
            _contratosGateway = contratosGateway;
            _eventoRepository = eventoRepository;
            _mediatorHandler = mediatorHandler;
            _publisher = publisher;
        }

        public async Task<bool> Handle(SolicitarProrrogacaoCommand message, CancellationToken cancellationToken)
        {
            //Gravar prorrogacao na tabela prorrogacoes
            await _prorrogacaoRepository.Adicionar(new Prorrogacao());

            var realizarPagamento = false;

            if (realizarPagamento)
            {
                //Disparar comando para solicitar pagamento
                await _mediatorHandler.EnviarComando(new SolicitarPagamentoCommand());
            }
            else
            {
                //Publicar comando na fila prorrogar-contrato (ProrrogarContratoCommand)

                await _publisher.Publish(message);
            }
            return true;
        }

        public async Task<bool> Handle(ProrrogarContratoCommand message, CancellationToken cancellationToken)
        {
            //Solicitar prorrogacao ao AL.Contratos
            await _contratosGateway.ProrrogarContrato();

            var prorrogacao = new Prorrogacao();

            //Gravar evento ProrrogacaoSolicitada
            await _eventoRepository.Adicionar(new Evento());

            var prorrogou = true;

            if (prorrogou)
            {
                
                //Gravar evento ProrrogacaoEfetuada
                await _eventoRepository.Adicionar(new Evento());

                //Publicar evento na fila contrato-prorrogado
            }
            else
            {
                //Gravar evento ProrrogacaoNaoEfetuada
                await _eventoRepository.Adicionar(new Evento());

                //Publicar evento na fila contrato-nao-prorrogado
            }
            return true;
        }

        public async Task Handle(ProrrogacaoEfetuadaEvent message, CancellationToken cancellationToken)
        {
            //Atualizar prorrogacao na tabela prorrogacoes para  ProrrogacaoEfetuada
            await _prorrogacaoRepository.Atualizar(new Prorrogacao());

            //Notificar cliente via Hermes
        }

        public async Task Handle(ProrrogacaoNaoEfetuadaEvent message, CancellationToken cancellationToken)
        {
            //Atualizar prorrogacao na tabela prorrogacoes para  ProrrogacaoNaoEfetuada
            await _prorrogacaoRepository.Atualizar(new Prorrogacao());

            //Notificar cliente via Hermes

        }
    }
}
