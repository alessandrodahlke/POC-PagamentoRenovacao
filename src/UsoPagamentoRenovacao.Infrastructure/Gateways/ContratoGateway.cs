using UsoPagamentoRenovacao.Core.DomainServices.Interfaces.Gateways;

namespace UsoPagamentoRenovacao.Infrastructure.Gateways
{
    public class ContratoGateway : IContratosGateway
    {
        public Task ProrrogarContrato()
        {
            return Task.CompletedTask;
        }
    }
}
