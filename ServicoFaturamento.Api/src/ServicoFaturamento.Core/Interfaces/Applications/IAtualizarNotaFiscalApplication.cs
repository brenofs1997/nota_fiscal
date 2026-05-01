using ServicoFaturamento.Core.Models.InputModels;

namespace ServicoFaturamento.Core.Interfaces.Applications
{
    public interface IAtualizarNotaFiscalApplication
    {
        Task AtualizarAsync(Guid id, AtualizacaoNotaFiscalInputModel inputModel);
    }
}
