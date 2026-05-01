using Microsoft.EntityFrameworkCore;
using ServicoEstoque.Core.Entities;
using ServicoEstoque.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoEstoque.Infrastructure.Repositories
{
    public class ProdutoRepository(ServicoEstoqueContext dbContext) : IProdutoRepository
    {
        public async Task<Guid> CadastrarAsync(Produto produto)
        {
            var entidade = await dbContext.Produtos.AddAsync(produto);

            await dbContext.SaveChangesAsync();

            return entidade.Entity.Id;
        }

        public async Task<IEnumerable<Produto>> BuscarAsync()
        {
            var produtos = await dbContext.Produtos.ToListAsync();

            return produtos;
        }

        public async Task<Produto?> BuscarPorIdAsync(Guid id)
        {
            var produto = await dbContext.Produtos.FirstOrDefaultAsync(c => c.Id == id);

            return produto;
        }

        public Task SaveChangesAsync()
        {
            return dbContext.SaveChangesAsync();
        }
        public Task Delete(Produto produto)
        {
            dbContext.Produtos.Remove(produto);
            return Task.CompletedTask;
        }

    }
}
