namespace UsoPagamentoRenovacao.Core.DomainModels.Models
{
    public abstract class Entity
    {
        private List<Evento> _eventos;

        public Guid Id { get; set; }
                protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public void AdicionarEvento(Evento evento)
        {
            _eventos ??= new List<Evento>();
            _eventos.Add(evento);
        }
    }
}
