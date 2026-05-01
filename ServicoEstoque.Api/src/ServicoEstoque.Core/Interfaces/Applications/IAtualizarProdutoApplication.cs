using ServicoEstoque.Core.Models.InputModels;

namespace ServicoEstoque.Core.Interfaces.Applications
{
    public interface IAtualizarProdutoApplication
    {
        Task AtualizarAsync(Guid id, AtualizacaoProdutoInputModel inputModel);
    }
}
