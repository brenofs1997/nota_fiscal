namespace ServicoEstoque.Core.Models.ViewModel
{
    public class ProdutoViewModel(Guid id, string codigo, string descricao, int saldo)
    {
        public Guid Id { get; } = id;
        public string Codigo { get; } = codigo;
        public string Descricao { get; } = descricao;
        public int Saldo { get; } = saldo;
    }
}
