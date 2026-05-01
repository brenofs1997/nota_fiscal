namespace ServicoFaturamento.Core.Models.ViewModel
{
    public class NotaFiscalItemViewModel(Guid id, Guid produtoId, int quantidade)
    {
        public Guid Id { get; } = id;

        public Guid ProdutoId { get; } = produtoId;
        public int Quantidade { get; } = quantidade;
    }
}
