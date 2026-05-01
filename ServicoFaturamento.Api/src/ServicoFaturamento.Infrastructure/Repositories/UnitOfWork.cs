using Microsoft.EntityFrameworkCore.Storage;
using ServicoFaturamento.Core.Interfaces.Repositories;

namespace ServicoFaturamento.Infrastructure.Repositories
{
    public class UnitOfWork(
       ServicoFaturamentoContext dbContext,
       INotaFiscalRepository notasFiscais
   ) : IUnitOfWork
    {
        private IDbContextTransaction? _transaction;

        public INotaFiscalRepository NotasFiscais => notasFiscais;

        public async Task BeginTransactionAsync()
        {
            _transaction = await dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (_transaction == null)
                throw new Exception("Nenhuma transação iniciada");

            try
            {
                await _transaction.CommitAsync();
            }
            catch (Exception)
            {
                await _transaction.RollbackAsync();

                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                dbContext.Dispose();
        }
    }
}
