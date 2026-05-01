using FluentValidation;
using ServicoEstoque.Core.Models.InputModels;

namespace ServicoEstoque.Application.Validators
{
    public class BaseProdutoValidator<T> : AbstractValidator<T> where T : BaseProdutoInputModel
    {
        public BaseProdutoValidator()
        {
            RuleFor(c => c.Codigo)
                .NotEmpty()
                .WithMessage("O codigo do produto deve ser informado.");

            RuleFor(c => c.Descricao)
                .NotEmpty()
                .WithMessage("A descricao do produto deve ser informado.");

            RuleFor(c => c.Saldo)
                .NotEmpty()
                .WithMessage("O saldo do produto deve ser informado.");
        }
    }
}
