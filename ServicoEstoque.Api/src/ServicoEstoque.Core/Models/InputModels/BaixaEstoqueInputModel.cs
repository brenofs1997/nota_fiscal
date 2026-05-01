namespace ServicoEstoque.Core.Models.InputModels
{
    public class BaixaEstoqueInputModel(
        Guid ProdutoId, 
        int Saldo
    )
    {
        public Guid ProdutoId { get; } = ProdutoId;
        public int Saldo { get; } = Saldo;
    }
}
