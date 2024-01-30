namespace UsoPagamentoRenovacao.Core.DomainServices.Interfaces.Gateways
{
    public interface IThorPagamentosGateway
    {
        Task<string> SolicitarPagamento();
    }
}
