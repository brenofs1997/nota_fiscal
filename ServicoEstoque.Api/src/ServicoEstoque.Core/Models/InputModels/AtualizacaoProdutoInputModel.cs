namespace ServicoEstoque.Core.Models.InputModels
{
    public class AtualizacaoProdutoInputModel(
        string? codigo = null,
        string? descricao = null,
        int? saldo = null
    ) : BaseProdutoInputModel(codigo, descricao, saldo)
    {
    }
}
