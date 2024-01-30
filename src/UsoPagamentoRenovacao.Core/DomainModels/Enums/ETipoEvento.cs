namespace UsoPagamentoRenovacao.Core.DomainModels.Enums
{
    public enum ETipoEvento
    {
        ProrrogacaoContratoSolicitada = 1,
        ProrrogacaoContratoEfetuada,
        ProrrogacaoContratoNaoEfetuada,
        PagamentoPixSolicitado,
        PagamentoPixRegistrado,
        PagamentoCartaoSolicitado,
        PagmentoEfetuado,
        PagamentoNaoEfetuado,
        ClienteNotificado,
    }
}
