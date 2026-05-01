namespace ServicoFaturamento.Core.Models.InputModels
{
    public class NotaFiscalInputModel(
        Guid idempotencyKey,
        IEnumerable<NotaFiscalItemInputModel> itens)
    {
        public Guid IdempotencyKey { get; } = idempotencyKey;
        public IEnumerable<NotaFiscalItemInputModel> Itens { get; } = itens;
    }
}
