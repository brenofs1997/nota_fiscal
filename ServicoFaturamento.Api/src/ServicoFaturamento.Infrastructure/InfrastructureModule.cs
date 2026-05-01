
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServicoFaturamento.Core.Interfaces.Repositories;
using ServicoFaturamento.Infrastructure.Repositories;

namespace ServicoFaturamento.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<ServicoFaturamentoContext>(options =>
                options.UseNpgsql("Server=localhost;Port=5490;Database=servicofaturamento;User Id=admin;Password=admin;"));

            services.AddScoped<INotaFiscalRepository, NotaFiscalRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
