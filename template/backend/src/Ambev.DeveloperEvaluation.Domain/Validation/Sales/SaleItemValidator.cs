using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Sales;

public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public SaleItemValidator()
    {
        RuleFor(item => item.ProductId)
            .NotEmpty()
            .WithMessage("Product title cannot be empty.");

        RuleFor(item => item.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity should be greater than zero.")
            .LessThanOrEqualTo(20)
            .WithMessage("Quantity should be lower or equal to twenty.");

        RuleFor(item => item.UnitPrice)
            .GreaterThan(0)
            .WithMessage("Unit price should be greater than zero.");

        RuleFor(item => item.Discount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Discount should be greater or equal to zero.");
    }
}
