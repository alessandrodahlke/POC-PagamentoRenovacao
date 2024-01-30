using UsoPagamentoRenovacao.Core.DomainModels.Models;
using UsoPagamentoRenovacao.Core.DomainServices.Interfaces.Gateways;
using UsoPagamentoRenovacao.Core.DomainServices.Interfaces.Handlers;
using UsoPagamentoRenovacao.Core.DomainServices.Interfaces.Repositories;
using UsoPagamentoRenovacao.Core.Handlers.Commands;
using UsoPagamentoRenovacao.Core.Handlers.Events;

namespace UsoPagamentoRenovacao.Core.Handlers
{
    public class PagamentoHandler : IRequestHandler<SolicitarPagamentoCommand>,
                                    IRequestHandler<PagamentoEfetuadoEvent>,
                                    IRequestHandler<PagamentoNaoEfetuadoEvent>
    {
        private readonly IThorPagamentosGateway _thorPagamentosGateway;
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IEventoRepository _eventoRepository;

        public PagamentoHandler(IThorPagamentosGateway thorPagamentosGateway,
            ITransacaoRepository transacaoRepository,
            IEventoRepository eventoRepository)
        {
            _thorPagamentosGateway = thorPagamentosGateway;
            _transacaoRepository = transacaoRepository;
            _eventoRepository = eventoRepository;
        }

        public async Task Handle(SolicitarPagamentoCommand message)
        {
            //Solilicitar pagamento ao Thor Pagamentos
            var idTransacao = await _thorPagamentosGateway.SolicitarPagamento();

            var sucesso = true;
            if (sucesso)
            {
                //Salvar transacao na tabela transacoes
                await _transacaoRepository.Adicionar(new Transacao(idTransacao));

                //Gravar evento PagamentoSolicitado
                await _eventoRepository.Adicionar(new Evento());
            }
            else
            {
                //Publicar evento na fila contrato-nao-prorrogado (falha no pagamento)
            }
        }

        public async Task Handle(PagamentoEfetuadoEvent message)
        {

            //Atualizar transacao Capturada
            var transacao = await _transacaoRepository.ObterPorId(Guid.NewGuid());
            await _transacaoRepository.Atualizar(transacao);

            //Gravar evento PagamentoEfetuado
            await _eventoRepository.Adicionar(new Evento());

            //Publicar comando na fila prorrogar-contrato

        }

        public async Task Handle(PagamentoNaoEfetuadoEvent message)
        {
            //Atualizar transacao Negada
            var transacao = await _transacaoRepository.ObterPorId(Guid.NewGuid());
            await _transacaoRepository.Atualizar(transacao);

            //Gravar evento PagamentoNaoEfetuado
            await _eventoRepository.Adicionar(new Evento());

            //Publicar evento na fila contrato-nao-prorrogado


            await Task.CompletedTask;
        }
    }
}
