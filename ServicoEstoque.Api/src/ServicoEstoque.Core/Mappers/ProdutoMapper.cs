using ServicoEstoque.Core.Entities;
using ServicoEstoque.Core.Models.ViewModel;

namespace ServicoEstoque.Core.Mappers
{
    public static class ProdutoMapper
    {
        public static ProdutoViewModel ToViewModel(this Produto produto)
        {
            return new ProdutoViewModel(
                produto.Id,
                produto.Codigo,
                produto.Descricao,
                produto.Saldo
            );
        }
    }
}
