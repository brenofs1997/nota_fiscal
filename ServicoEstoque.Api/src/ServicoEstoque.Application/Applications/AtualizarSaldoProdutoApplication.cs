using ServicoEstoque.Core.Interfaces.Applications;
using ServicoEstoque.Core.Interfaces.Notifications;
using ServicoEstoque.Core.Interfaces.Repositories;
using ServicoEstoque.Core.Models.InputModels;
using System.Net;

namespace ServicoEstoque.Application.Applications
{
    public class AtualizarSaldoProdutoApplication(
          IProdutoRepository produtoRepository,
          INotifier notifier
      ) : IAtualizarSaldoProdutoApplication
    {
        public async Task AtualizarSaldoAsync(IEnumerable<BaixaEstoqueInputModel> itens)
        {
            foreach (var item in itens)
            {
                try
                {
                    var produto = await produtoRepository.BuscarPorIdAsync(item.ProdutoId);

                    if (produto == null)
                    {
                        throw new NullReferenceException("O produto informado não foi encontrado na base");
                    }

                    produto.SubtrairSaldo(item.Saldo);
                }
                catch (Exception ex)
                {
                    notifier.Handle(ex.Message, HttpStatusCode.BadRequest);
                    return;
                }
            }

            await produtoRepository.SaveChangesAsync();
        }
    }
}
