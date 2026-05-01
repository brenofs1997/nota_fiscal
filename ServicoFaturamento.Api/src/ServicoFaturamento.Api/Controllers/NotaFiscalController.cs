using Microsoft.AspNetCore.Mvc;
using ServicoFaturamento.Core.Interfaces.Applications;
using ServicoFaturamento.Core.Interfaces.Notifications;
using ServicoFaturamento.Core.Models.InputModels;

namespace ServicoFaturamento.Api.Controllers
{
    public class NotaFiscalController(
        ICadastrarNotaFiscalApplication cadastrarNotaFiscalApplication,
        IBuscarNotasFiscaisApplication buscarNotasFiscaisApplication,
        IBuscarNotaFiscalPorIdApplication buscarNotaFiscalPorIdApplication,
        IFecharNotaFiscalApplication fecharNotaFiscalApplication,
        INotifier notifier
    ) : MainController(notifier)
    {
        [HttpPost]
        public async Task<IActionResult> CadastrarAsync([FromBody] NotaFiscalInputModel inputModel)
        {
            var id = await cadastrarNotaFiscalApplication.CadastrarAsync(inputModel);

            return RespostaPersonalizada(Ok(id));
        }

        [HttpGet]
        public async Task<IActionResult> BuscarAsync()
        {
            var notasFiscais = await buscarNotasFiscaisApplication.BuscarAsync();

            return RespostaPersonalizada(Ok(notasFiscais));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorIdAsync([FromRoute] Guid id)
        {
            var notaFiscal = await buscarNotaFiscalPorIdApplication.BuscarPorIdAsync(id);

            if (notaFiscal == null)
            {
                return NotFound();
            }

            return RespostaPersonalizada(Ok(notaFiscal));
        }


        [HttpPost("{id}/imprimir")]
        public async Task<IActionResult> FecharAsync([FromRoute] Guid id)
        {
            await fecharNotaFiscalApplication.FecharAsync(id);

            return RespostaPersonalizada(NoContent());
        }

    }
}
