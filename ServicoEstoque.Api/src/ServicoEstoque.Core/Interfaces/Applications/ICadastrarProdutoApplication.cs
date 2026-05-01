using ServicoEstoque.Core.Models.InputModels;

namespace ServicoEstoque.Core.Interfaces.Applications
{
    public interface ICadastrarProdutoApplication
    {
        Task<Guid> CadastrarAsync(ProdutoInputModel inputModel);
    }
}
