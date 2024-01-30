using UsoPagamentoRenovacao.Core.DomainModels.Models;

namespace UsoPagamentoRenovacao.Core.DomainServices.Interfaces.Repositories
{
    public interface IEventoRepository
    {
        Task Adicionar(Evento evento);
    }
}
