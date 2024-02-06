using UsoPagamentoRenovacao.Core.DomainModels.Models;
using UsoPagamentoRenovacao.Core.DomainServices.Interfaces.Repositories;

namespace UsoPagamentoRenovacao.Infrastructure.Repositories
{
    public class TransacaoRepository : ITransacaoRepository
    {
        public Task Adicionar(Transacao transacao)
        {
            return Task.CompletedTask;
        }

        public Task Atualizar(Transacao transacao)
        {
            return Task.CompletedTask;
        }

        public Task<Transacao> ObterPorId(Guid id)
        {
            return Task.FromResult(new Transacao(string.Empty));
        }
    }
}
