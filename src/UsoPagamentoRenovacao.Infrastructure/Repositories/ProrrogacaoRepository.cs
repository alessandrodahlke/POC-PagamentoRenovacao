using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsoPagamentoRenovacao.Core.DomainModels.Models;
using UsoPagamentoRenovacao.Core.DomainServices.Interfaces.Repositories;

namespace UsoPagamentoRenovacao.Infrastructure.Repositories
{
    public class ProrrogacaoRepository : IProrrogacaoRepository
    {
        public Task Adicionar(Prorrogacao prorrogacao)
        {
            return Task.CompletedTask;
        }

        public Task Atualizar(Prorrogacao prorrogacao)
        {
            return Task.CompletedTask;
        }

        public Task<List<Prorrogacao>> ObterPorCodigoContrato(string codigoContrato)
        {
            return Task.FromResult(new List<Prorrogacao>());
        }

        public Task<Prorrogacao> ObterPorId(Guid id)
        {
            return Task.FromResult(new Prorrogacao());
        }

        public Task<Prorrogacao> ObterPorIdFormulario(Guid idFormulario)
        {
            return Task.FromResult(new Prorrogacao());
        }
    }
}
