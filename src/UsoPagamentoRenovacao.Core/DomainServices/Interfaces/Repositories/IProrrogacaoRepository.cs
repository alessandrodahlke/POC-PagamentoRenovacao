using UsoPagamentoRenovacao.Core.DomainModels.Models;

namespace UsoPagamentoRenovacao.Core.DomainServices.Interfaces.Repositories
{
    public interface IProrrogacaoRepository
    {
        Task Adicionar(Prorrogacao prorrogacao);
        Task Atualizar(Prorrogacao prorrogacao);
        Task<Prorrogacao> ObterPorId(Guid id);
        Task<List<Prorrogacao>> ObterPorCodigoContrato(string codigoContrato);
        Task<Prorrogacao> ObterPorIdFormulario(Guid idFormulario);
    }
}
