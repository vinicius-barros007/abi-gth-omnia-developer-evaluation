using Ambev.DeveloperEvaluation.Domain.Validation.Common;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
    {
        RuleFor(sale => sale.Branch)
            .NotNull()
            .SetValidator(new BranchValidator());

        RuleFor(sale => sale.Customer)
            .NotNull()
            .SetValidator(new CustomerValidator());

        RuleFor(sale => sale.Items)
            .NotEmpty()
            .ForEach(item =>
            {
                item.NotEmpty()
                    .SetValidator(new CreateSaleItemValidator());
            });
    }
}

public class CreateSaleItemValidator : AbstractValidator<CreateSaleItem>
{
    public CreateSaleItemValidator()
    {
        RuleFor(item => item.ProductId)
            .NotEmpty();

        RuleFor(item => item.Quantity)
            .GreaterThan(0)
            .LessThanOrEqualTo(20);
    }
}
