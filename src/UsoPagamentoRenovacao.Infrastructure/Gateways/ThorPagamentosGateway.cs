using UsoPagamentoRenovacao.Core.DomainServices.Interfaces.Gateways;

namespace UsoPagamentoRenovacao.Infrastructure.Gateways
{
    public class ThorPagamentosGateway : IThorPagamentosGateway
    {
        public Task<string> SolicitarPagamento()
        {
            return Task.FromResult(string.Empty);
        }
    }
}
