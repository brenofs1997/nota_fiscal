using ServicoEstoque.Core.Entities;

namespace ServicoEstoque.Core.Interfaces.Repositories
{
    public interface IProdutoRepository
    {
        Task<Guid> CadastrarAsync(Produto cliente);

        Task<IEnumerable<Produto>> BuscarAsync();

        Task<Produto?> BuscarPorIdAsync(Guid id);

        Task SaveChangesAsync();

        Task Delete(Produto? produto);
    }
}
