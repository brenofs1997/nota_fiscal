using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServicoEstoque.Core.Interfaces.Repositories;
using ServicoEstoque.Infrastructure.Repositories;

namespace ServicoEstoque.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<ServicoEstoqueContext>(p => p.UseNpgsql("Server=localhost;Port=5595;Database=servicoestoque;User Id=admin;Password=admin;"));

            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
