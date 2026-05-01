using ServicoEstoque.Core.Models.InputModels;

namespace ServicoEstoque.Core.Interfaces.Applications
{
    public interface IAtualizarSaldoProdutoApplication
    {
        Task AtualizarSaldoAsync(IEnumerable<BaixaEstoqueInputModel> inputModel); 
    }
}
