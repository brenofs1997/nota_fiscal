namespace ServicoFaturamento.Core.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        INotaFiscalRepository NotasFiscais { get; }
        Task BeginTransactionAsync();
        Task CommitAsync();
    }
}
