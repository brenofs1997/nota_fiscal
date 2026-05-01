using ServicoEstoque.Api.Middlewares;

namespace ServicoEstoque.Api.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IServiceCollection AddMiddlewares(this IServiceCollection services)
        {
            services.AddTransient<GlobalExceptionHandler>();

            return services;
        }
    }
}
