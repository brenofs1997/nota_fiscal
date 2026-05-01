using Microsoft.Extensions.Logging;
using ServicoEstoque.Application.Notifications;
using ServicoEstoque.Core.Interfaces.Applications;
using ServicoEstoque.Core.Interfaces.Notifications;
using ServicoEstoque.Core.Interfaces.Repositories;
using ServicoEstoque.Core.Models.InputModels;
using System.Net;

namespace ServicoEstoque.Application.Applications
{
    public class AtualizarProdutoApplication(
        IProdutoRepository produtoRepository,
        INotifier notifier,
        ILogger<AtualizarProdutoApplication> logger
    ) : IAtualizarProdutoApplication
    {
        public async Task AtualizarAsync(Guid id, AtualizacaoProdutoInputModel inputModel)
        {
            var produto = await produtoRepository.BuscarPorIdAsync(id);

            if (produto == null)
            {
                logger.LogWarning("Tentativa de cadastro de produto.");
                notifier.Handle("O produto informado não foi encontrado na base", HttpStatusCode.NotFound);
            }
                

            if (inputModel.Saldo.HasValue && produto.Saldo < inputModel.Saldo)
            {
                logger.LogWarning("Saldo insuficiente para realizar a baixa.");
                notifier.Handle("Saldo insuficiente para realizar a baixa.", HttpStatusCode.BadRequest);
            }

            produto.Codigo = inputModel.Codigo ?? produto.Codigo;
            produto.Descricao = inputModel.Descricao ?? produto.Descricao;
            produto.Saldo = inputModel.Saldo ?? produto.Saldo;

            await produtoRepository.SaveChangesAsync();
            logger.LogWarning("Produto Atualizado.");
        }
    }
}
