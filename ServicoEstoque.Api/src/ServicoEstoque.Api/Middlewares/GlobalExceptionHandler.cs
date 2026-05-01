using ServicoEstoque.Core;
using ServicoEstoque.Core.Models.ViewModel;
using System.Net;

namespace ServicoEstoque.Api.Middlewares
{
    public class GlobalExceptionHandler : IMiddleware
    {
        private const string MENSAGEM_PADRAO = "Tivemos um problema interno no servidor. Tente novamente mais tarde!";

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var mensagemErro =
                Variaveis.Geral.ENV == "dev" ?
                    (ex.InnerException?.Message ?? ex.Message ?? MENSAGEM_PADRAO)
                    : MENSAGEM_PADRAO;

            if (context != null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsJsonAsync(
                    new RespostaPadraoViewModel([mensagemErro])
                );
            }
        }
    }
}
