using Microsoft.Extensions.Logging;
using ServicoEstoque.Core.Interfaces.Applications;
using ServicoEstoque.Core.Interfaces.Repositories;
using ServicoEstoque.Core.Mappers;
using ServicoEstoque.Core.Models.ViewModel;

namespace ServicoEstoque.Application.Applications
{
    public class BuscarProdutosApplication(
        IProdutoRepository produtoRepository,
        ILogger<BuscarProdutosApplication> logger
        ) : IBuscarProdutosApplication
    {
        public async Task<IEnumerable<ProdutoViewModel>> BuscarAsync()
        {
            logger.LogInformation("Iniciando busca dos Produtos.");

            var produtos = await produtoRepository.BuscarAsync();

            logger.LogInformation("Produtos encontrados.");

            return produtos.Select(p => p.ToViewModel());
        }
    }
}
