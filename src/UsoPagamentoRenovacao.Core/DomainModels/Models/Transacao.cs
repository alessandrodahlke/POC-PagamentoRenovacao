namespace UsoPagamentoRenovacao.Core.DomainModels.Models
{
    public class Transacao : Entity
    {
        public string IdTransacao { get; set; }
        public Guid RequestId { get; set; }

        public Transacao(string idTransacao)
        {
            IdTransacao = idTransacao;
            RequestId = Guid.NewGuid();
        }
    }
}
