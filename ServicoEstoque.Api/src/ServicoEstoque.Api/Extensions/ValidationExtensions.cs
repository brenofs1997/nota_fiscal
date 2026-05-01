using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ServicoEstoque.Application.Validators;
using ServicoEstoque.Core.Models.ViewModel;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;

namespace ServicoEstoque.Api.Extensions
{
    public static class ValidationExtensions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation(c =>
            {
                c.OverrideDefaultResultFactoryWith<CustomResultFactory>();
            });

            services.AddValidatorsFromAssemblyContaining<CadastrarProdutoValidator>();

            return services;
        }

        public class CustomResultFactory : IFluentValidationAutoValidationResultFactory
        {
            public Task<IActionResult?> CreateActionResult(ActionExecutingContext context, ValidationProblemDetails validationProblemDetails, IDictionary<IValidationContext, ValidationResult> validationResults)
            {
                var result = new BadRequestObjectResult(
                    new RespostaPadraoViewModel(
                        context
                            .ModelState
                            .SelectMany(ms => ms.Value?.Errors ?? Enumerable.Empty<ModelError>())
                            .Select(e => e.ErrorMessage)
                    )
                );

                return Task.FromResult<IActionResult?>(result);
            }
        }
    }
}
