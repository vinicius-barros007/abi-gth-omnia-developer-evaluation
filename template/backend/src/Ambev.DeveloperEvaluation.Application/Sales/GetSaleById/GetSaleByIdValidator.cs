using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

public class GetSaleByIdCommandValidator : AbstractValidator<GetSaleByIdCommand>
{
    public GetSaleByIdCommandValidator()
    {
        RuleFor(sale => sale.Id)
            .NotNull();
    }
}