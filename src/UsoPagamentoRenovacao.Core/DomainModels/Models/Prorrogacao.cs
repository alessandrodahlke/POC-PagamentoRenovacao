using UsoPagamentoRenovacao.Core.DomainModels.Enums;

namespace UsoPagamentoRenovacao.Core.DomainModels.Models
{
    public class Prorrogacao : Entity
    {
        public string CodigoContrato { get; set; }
        public string CodigoCliente { get; set; }
        public string NomeCliente { get; set; }
        public DateTime DataRetornoPrevista { get; set; }
        public DateTime DataRetornoRenovacao { get; set; }
        public string CodigoAgenciaRetorno { get; set; }

        public Guid IdFormulario { get; set; }
        public ETipoFormularioRenovacao TipoFormulario { get; set; }
    }
}
