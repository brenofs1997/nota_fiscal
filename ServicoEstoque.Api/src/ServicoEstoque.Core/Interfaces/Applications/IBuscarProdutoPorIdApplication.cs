using ServicoEstoque.Core.Models.ViewModel;

namespace ServicoEstoque.Core.Interfaces.Applications
{
    public interface IBuscarProdutoPorIdApplication
    {
        Task<ProdutoViewModel?> BuscarPorIdAsync(Guid id);
    }
}
