using ServicoFaturamento.Core.Models.ViewModel;

namespace ServicoFaturamento.Core.Interfaces.Applications
{
    public interface IBuscarNotaFiscalPorIdApplication
    {
        Task<NotaFiscalViewModel?> BuscarPorIdAsync(Guid id);
    }
}
