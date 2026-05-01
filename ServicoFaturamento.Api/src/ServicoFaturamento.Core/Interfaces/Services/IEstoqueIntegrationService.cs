using ServicoFaturamento.Core.Models.InputModels;

namespace ServicoFaturamento.Core.Interfaces.Services
{
    public interface IEstoqueIntegrationService
    {
        Task<bool> AtualizarSaldoProdutosAsync(List<BaixaEstoqueInputModel> itens);
    }
}
