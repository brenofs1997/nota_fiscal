namespace ServicoEstoque.Core.Models.InputModels
{
    public abstract class BaseProdutoInputModel(string? codigo, string? descricao, int? saldo)
    {
        public string? Codigo { get; init; } = codigo;

        public string? Descricao { get; init; } = descricao;

        public int? Saldo { get; init; } = saldo;
    }
}
