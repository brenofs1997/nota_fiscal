using Microsoft.Extensions.Logging;
using ServicoEstoque.Application.Notifications;
using ServicoEstoque.Core.Entities;
using ServicoEstoque.Core.Interfaces.Applications;
using ServicoEstoque.Core.Interfaces.Notifications;
using ServicoEstoque.Core.Interfaces.Repositories;
using ServicoEstoque.Core.Models.InputModels;
using System.Net;

namespace ServicoEstoque.Application.Applications
{
    public class CadastrarProdutoApplication(
        IUnitOfWork unitOfWork,
        INotifier notifier,
        ILogger<CadastrarProdutoApplication> logger
    ) : ICadastrarProdutoApplication
    {
        public async Task<Guid> CadastrarAsync(ProdutoInputModel inputModel)
        {
            try
            {
                logger.LogInformation("Iniciando cadastro do produto: {Codigo} - {Descricao}", inputModel.Codigo, inputModel.Descricao);
                
                var produto = new Produto(
                  inputModel.Codigo,
                  inputModel.Descricao,
                  inputModel.Saldo
                );

           
                await unitOfWork.BeginTransactionAsync();

                var produtoId = await unitOfWork.Produtos.CadastrarAsync(produto);

                await unitOfWork.CommitAsync();

                logger.LogInformation("Produto cadastrado com sucesso. ID: {ProdutoId}", produtoId);

                return produtoId;
            }
            catch (Exception)
            {
                notifier.Handle("Ocorreu um erro ao cadastrar o produto.", HttpStatusCode.InternalServerError);
                return Guid.Empty;
            }
        }
    }
}
