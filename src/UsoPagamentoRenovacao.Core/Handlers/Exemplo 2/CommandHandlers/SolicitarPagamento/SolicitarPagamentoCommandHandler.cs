using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsoPagamentoRenovacao.Core.DomainServices.Interfaces.Handlers;

namespace UsoPagamentoRenovacao.Core.Handlers.Exemplo_2.CommandHandlers.SolicitarPagamento
{
    internal class SolicitarPagamentoCommandHandler : IRequestHandler<SolicitarPagamentoCommand>
    {
        public Task Handle(SolicitarPagamentoCommand request)
        {
            throw new NotImplementedException();
        }
    }
}
