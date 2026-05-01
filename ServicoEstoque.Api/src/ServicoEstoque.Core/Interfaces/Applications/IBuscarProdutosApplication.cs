using ServicoEstoque.Core.Models.ViewModel;

namespace ServicoEstoque.Core.Interfaces.Applications
{
    public interface IBuscarProdutosApplication
    {
        Task<IEnumerable<ProdutoViewModel>> BuscarAsync();
    }
}
