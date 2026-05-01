using Microsoft.EntityFrameworkCore;
using ServicoEstoque.Core.Entities;
using System.Reflection;

namespace ServicoEstoque.Infrastructure
{
    public class ServicoEstoqueContext(DbContextOptions options) : DbContext(options)
    {
        public virtual DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
       
        }
    }
}
