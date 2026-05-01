using Microsoft.Extensions.DependencyInjection;
using ServicoFaturamento.Application.Applications;
using ServicoFaturamento.Application.Notifications;
using ServicoFaturamento.Core.Interfaces.Applications;
using ServicoFaturamento.Core.Interfaces.Notifications;

namespace ServicoFaturamento.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICadastrarNotaFiscalApplication, CadastrarNotaFiscalApplication>();
            services.AddScoped<IBuscarNotasFiscaisApplication, BuscarNotasFiscaisApplication>();
            services.AddScoped<IBuscarNotaFiscalPorIdApplication, BuscarNotaFiscalPorIdApplication>();
            services.AddScoped<IFecharNotaFiscalApplication, FecharNotaFiscalApplication>();
            services.AddScoped<INotifier, Notifier>();

            return services;
        }
    }
}
