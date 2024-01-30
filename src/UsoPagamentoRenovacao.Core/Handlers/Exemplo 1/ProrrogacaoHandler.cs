using UsoPagamentoRenovacao.Core.DomainModels.Models;
using UsoPagamentoRenovacao.Core.DomainServices.Interfaces.Gateways;
using UsoPagamentoRenovacao.Core.DomainServices.Interfaces.Handlers;
using UsoPagamentoRenovacao.Core.DomainServices.Interfaces.Repositories;
using UsoPagamentoRenovacao.Core.Handlers.Commands;
using UsoPagamentoRenovacao.Core.Handlers.Events;

namespace UsoPagamentoRenovacao.Core.Handlers
{
    public class ProrrogacaoHandler : IRequestHandler<SolicitarProrrogacaoCommand>,
                                      IRequestHandler<ProrrogarContratoCommand>,
                                      IRequestHandler<ProrrogacaoEfetuadaEvent>,
                                      IRequestHandler<ProrrogacaoNaoEfetuadaEvent>

    {

        private readonly IProrrogacaoRepository _prorrogacaoRepository;
        private readonly IContratosGateway _contratosGateway;
        private readonly IEventoRepository _eventoRepository;
        private readonly IRequestHandler<SolicitarPagamentoCommand> _solicitarPagamentoHandler;

        public ProrrogacaoHandler(IProrrogacaoRepository prorrogacaoRepository, 
            IContratosGateway contratosGateway, 
            IEventoRepository eventoRepository, 
            IRequestHandler<SolicitarPagamentoCommand> solicitarPagamentoHandler)
        {
            _prorrogacaoRepository = prorrogacaoRepository;
            _contratosGateway = contratosGateway;
            _eventoRepository = eventoRepository;
            _solicitarPagamentoHandler = solicitarPagamentoHandler;
        }

        public async Task Handle(SolicitarProrrogacaoCommand message)
        {
            //Gravar prorrogacao na tabela prorrogacoes
            await _prorrogacaoRepository.Adicionar(new Prorrogacao());

            var realizarPagamento = true;

            if (realizarPagamento)
            {
                //Disparar comando para solicitar pagamento
                await _solicitarPagamentoHandler.Handle(new SolicitarPagamentoCommand());
            }
            else
            {
                //Publicar comando na fila prorrogar-contrato (ProrrogarContratoCommand)
            }
        }

        public async Task Handle(ProrrogarContratoCommand message)
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
        }

        public async Task Handle(ProrrogacaoEfetuadaEvent message)
        {
            //Atualizar prorrogacao na tabela prorrogacoes para  ProrrogacaoEfetuada
            await _prorrogacaoRepository.Atualizar(new Prorrogacao());

            //Notificar cliente via Hermes
        }

        public async Task Handle(ProrrogacaoNaoEfetuadaEvent message)
        {
            //Atualizar prorrogacao na tabela prorrogacoes para  ProrrogacaoNaoEfetuada
            await _prorrogacaoRepository.Atualizar(new Prorrogacao());

            //Notificar cliente via Hermes

        }
    }
}
