using Microsoft.Extensions.Logging;
using ServicoEstoque.Core.Interfaces.Applications;
using ServicoEstoque.Core.Interfaces.Repositories;
using ServicoEstoque.Core.Mappers;
using ServicoEstoque.Core.Models.ViewModel;

namespace ServicoEstoque.Application.Applications
{
    public class BuscarProdutoPorIdApplication(
        IProdutoRepository produtoRepository,
        ILogger<BuscarProdutoPorIdApplication> logger
        ) : IBuscarProdutoPorIdApplication
    {
        public async Task<ProdutoViewModel?> BuscarPorIdAsync(Guid id)
        {
            logger.LogInformation("Iniciando busca do Produto.");

            var produto = await produtoRepository.BuscarPorIdAsync(id);

            logger.LogInformation("Produto encontrado.");

            return produto?.ToViewModel();
        }
    }
}
