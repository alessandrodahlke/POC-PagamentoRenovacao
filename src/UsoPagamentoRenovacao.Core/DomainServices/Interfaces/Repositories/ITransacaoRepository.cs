using UsoPagamentoRenovacao.Core.DomainModels.Models;

namespace UsoPagamentoRenovacao.Core.DomainServices.Interfaces.Repositories
{
    public interface ITransacaoRepository
    {
        Task Adicionar(Transacao transacao);
        Task Atualizar(Transacao transacao);
        Task<Transacao> ObterPorId(Guid id);
    }
}
