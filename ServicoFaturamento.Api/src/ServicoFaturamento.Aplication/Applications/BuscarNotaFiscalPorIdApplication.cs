using Microsoft.Extensions.Logging;
using ServicoFaturamento.Core.Interfaces.Applications;
using ServicoFaturamento.Core.Interfaces.Repositories;
using ServicoFaturamento.Core.Mappers;
using ServicoFaturamento.Core.Models.ViewModel;

namespace ServicoFaturamento.Application.Applications
{
    public class BuscarNotaFiscalPorIdApplication(
        INotaFiscalRepository notaFiscalRepository,
        ILogger<BuscarNotaFiscalPorIdApplication> logger
        ) : IBuscarNotaFiscalPorIdApplication
    {
        public async Task<NotaFiscalViewModel?> BuscarPorIdAsync(Guid id)
        {
            logger.LogInformation("Iniciando busca da Nota Fiscal.");
            var notaFiscal = await notaFiscalRepository.BuscarPorIdAsync(id);
            logger.LogInformation("Nota Fiscal encontrada.");
            return notaFiscal?.ToViewModel();
        }
    }
}
