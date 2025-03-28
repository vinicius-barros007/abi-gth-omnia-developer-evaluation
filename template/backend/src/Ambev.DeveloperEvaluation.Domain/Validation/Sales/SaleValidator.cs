using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Enums.Sales;
using Ambev.DeveloperEvaluation.Domain.Validation.Common;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Sales;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(sale => sale.Items)
            .NotEmpty()
            .WithMessage("Items cannot be empty.")
            .ForEach(item =>
            {
                item.SetValidator(new SaleItemValidator());
            });

        RuleFor(sale => sale.Status)
            .NotEqual(SaleStatus.None)
            .WithMessage("Sale status cannot be None.");

        RuleFor(sale => sale.Branch)
            .NotEmpty()
            .WithMessage("Branch cannot be empty.")
            .SetValidator(new BranchValidator());

        RuleFor(sale => sale.Customer)
            .NotEmpty()
            .WithMessage("Customer cannot be empty.")
            .SetValidator(new CustomerValidator());
    }
}
