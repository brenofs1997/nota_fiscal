using ServicoFaturamento.Core.Entities;

namespace ServicoFaturamento.Core.Interfaces.Repositories
{
    public interface INotaFiscalRepository
    {
        Task<Guid> CadastrarAsync(NotaFiscal notaFiscal);
        Task<NotaFiscal?> ObterPorChaveIdempotenciaAsync(Guid idempotencyKey);

        Task<IEnumerable<NotaFiscal>> BuscarAsync();

        Task<NotaFiscal?>BuscarPorIdAsync(Guid id);

        Task SaveChangesAsync();


    }
}
