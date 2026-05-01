using Microsoft.Extensions.Logging;
using ServicoFaturamento.Core.Interfaces.Applications;
using ServicoFaturamento.Core.Interfaces.Repositories;
using ServicoFaturamento.Core.Mappers;
using ServicoFaturamento.Core.Models.ViewModel;

namespace ServicoFaturamento.Application.Applications
{
    public class BuscarNotasFiscaisApplication(
        INotaFiscalRepository notaFiscalRepository,
        ILogger<BuscarNotasFiscaisApplication> logger
        ) : IBuscarNotasFiscaisApplication
    {
        public async Task<IEnumerable<NotaFiscalViewModel>> BuscarAsync()
        {
            logger.LogInformation("Iniciando busca das Notas Fiscais.");
            var notasFiscais = await notaFiscalRepository.BuscarAsync();
            logger.LogInformation("Notas encontradas.");
            return notasFiscais.Select(n => n.ToViewModel());
        }
    }
}
