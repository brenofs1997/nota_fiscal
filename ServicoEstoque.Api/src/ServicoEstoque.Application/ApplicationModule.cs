using Microsoft.Extensions.DependencyInjection;
using ServicoEstoque.Application.Applications;
using ServicoEstoque.Application.Notifications;
using ServicoEstoque.Core.Interfaces.Applications;
using ServicoEstoque.Core.Interfaces.Notifications;

namespace ServicoEstoque.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICadastrarProdutoApplication, CadastrarProdutoApplication>();
            services.AddScoped<IBuscarProdutosApplication, BuscarProdutosApplication>();
            services.AddScoped<IBuscarProdutoPorIdApplication, BuscarProdutoPorIdApplication>();
            services.AddScoped<IAtualizarProdutoApplication, AtualizarProdutoApplication>();
            services.AddScoped<IAtualizarSaldoProdutoApplication, AtualizarSaldoProdutoApplication>();
            services.AddScoped<IDeletarProdutoApplication, DeletarProdutoApplication>();
            services.AddScoped<INotifier, Notifier>();

            return services;
        }
    }
}
