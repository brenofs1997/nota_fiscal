using Microsoft.Extensions.Logging;
using ServicoEstoque.Application.Notifications;
using ServicoEstoque.Core.Interfaces.Applications;
using ServicoEstoque.Core.Interfaces.Notifications;
using ServicoEstoque.Core.Interfaces.Repositories;
using ServicoEstoque.Core.Models.InputModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ServicoEstoque.Application.Applications
{
    public class DeletarProdutoApplication(
          IProdutoRepository produtoRepository,
          ILogger<DeletarProdutoApplication> logger,
          IUnitOfWork unitOfWork,
          INotifier notifier
    ) : IDeletarProdutoApplication
    {
        public async Task Deletar(Guid id)
        {
            var produto = await produtoRepository.BuscarPorIdAsync(id);

            if (produto == null)
            {
                logger.LogWarning("Tentativa de cadastro de produto.");
                notifier.Handle("O produto informado não foi encontrado na base", HttpStatusCode.NotFound);
            }

            await unitOfWork.Produtos.Delete(produto);
            await unitOfWork.Produtos.SaveChangesAsync();
            logger.LogWarning("Produto Deletado.");
        }
    }
}
