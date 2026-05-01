using ServicoFaturamento.Core.Interfaces.Services;
using ServicoFaturamento.Core.Models.InputModels;
using System.Net.Http.Json;

public class EstoqueIntegrationService(HttpClient httpClient) : IEstoqueIntegrationService
{
    public async Task<bool> AtualizarSaldoProdutosAsync(List<BaixaEstoqueInputModel> itens)
    {
        var response = await httpClient.PostAsJsonAsync("/api/Produto/atualizar-saldo", itens);

        return response.IsSuccessStatusCode;
    }
}
