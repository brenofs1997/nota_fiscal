using ServicoFaturamento.Core.Models.InputModels;

namespace ServicoFaturamento.Core.Interfaces.Applications
{
    public interface ICadastrarNotaFiscalApplication
    {
        Task<Guid> CadastrarAsync(NotaFiscalInputModel inputModel);
    }
}
