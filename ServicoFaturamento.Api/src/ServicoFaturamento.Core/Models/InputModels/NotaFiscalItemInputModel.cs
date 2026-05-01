namespace ServicoFaturamento.Core.Models.InputModels
{
    public class NotaFiscalItemInputModel(Guid produtoId, int quantidade)
    {
        public Guid ProdutoId { get; } = produtoId;
        public int Quantidade { get; } = quantidade;
    }
}
