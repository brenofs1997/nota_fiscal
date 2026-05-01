using ServicoFaturamento.Core.Models.ViewModel;

namespace ServicoFaturamento.Core.Interfaces.Applications
{
    public interface IBuscarNotasFiscaisApplication
    {
        Task<IEnumerable<NotaFiscalViewModel>> BuscarAsync();
    }
}
