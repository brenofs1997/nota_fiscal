using Microsoft.EntityFrameworkCore.Storage;
using ServicoEstoque.Core.Interfaces.Repositories;

namespace ServicoEstoque.Infrastructure.Repositories
{
    public class UnitOfWork(
       ServicoEstoqueContext dbContext,
       IProdutoRepository produtos
   ) : IUnitOfWork
    {
        private IDbContextTransaction? _transaction;

        public IProdutoRepository Produtos => produtos;

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
