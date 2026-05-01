using Microsoft.AspNetCore.Mvc;
using ServicoEstoque.Core.Interfaces.Applications;
using ServicoEstoque.Core.Interfaces.Notifications;
using ServicoEstoque.Core.Models.InputModels;

namespace ServicoEstoque.Api.Controllers
{
    public class ProdutoController(
        ICadastrarProdutoApplication cadastrarProdutoApplication,
        IBuscarProdutosApplication buscarProdutosApplication,
        IBuscarProdutoPorIdApplication buscarProdutoPorIdApplication,
        IAtualizarProdutoApplication atualizarProdutoApplication,
        IAtualizarSaldoProdutoApplication atualizarSaldoProdutoApplication,
        IDeletarProdutoApplication deletarProdutoApplication,
        INotifier notifier
    ) : MainController(notifier)
    {
        [HttpPost]
        public async Task<IActionResult> CadastrarAsync([FromBody] ProdutoInputModel inputModel)
        {
            var id = await cadastrarProdutoApplication.CadastrarAsync(inputModel);

            return RespostaPersonalizada(Ok(id));
        }

        [HttpGet]
        public async Task<IActionResult> BuscarAsync()
        {
            var produtos = await buscarProdutosApplication.BuscarAsync();

            return RespostaPersonalizada(Ok(produtos));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorIdAsync([FromRoute] Guid id)
        {
            var produto = await buscarProdutoPorIdApplication.BuscarPorIdAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            return RespostaPersonalizada(Ok(produto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarAsync([FromRoute] Guid id, [FromBody] AtualizacaoProdutoInputModel inputModel)
        {
            await atualizarProdutoApplication.AtualizarAsync(id, inputModel);

            return RespostaPersonalizada(NoContent());
        }

        [HttpPost("atualizar-saldo")]
        public async Task<IActionResult> AtualizarSaldoAsync([FromBody] IEnumerable<BaixaEstoqueInputModel> inputModel)
        {
            await atualizarSaldoProdutoApplication.AtualizarSaldoAsync(inputModel);

            return RespostaPersonalizada(NoContent());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar([FromRoute] Guid id)
        {
            await deletarProdutoApplication.Deletar(id);

            return RespostaPersonalizada(NoContent());
        }
    }
}
