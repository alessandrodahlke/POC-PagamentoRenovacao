using UsoPagamentoRenovacao.Core.DomainModels.Models;
using UsoPagamentoRenovacao.Core.DomainServices.Interfaces.Repositories;

namespace UsoPagamentoRenovacao.Infrastructure.Repositories
{
    public class EventoRepository : IEventoRepository
    {
        public Task Adicionar(Evento evento)
        {
            return Task.CompletedTask;
        }
    }
}
