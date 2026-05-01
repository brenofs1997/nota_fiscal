using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoEstoque.Core.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IProdutoRepository Produtos { get; }
        Task BeginTransactionAsync();
        Task CommitAsync();
    }
}
