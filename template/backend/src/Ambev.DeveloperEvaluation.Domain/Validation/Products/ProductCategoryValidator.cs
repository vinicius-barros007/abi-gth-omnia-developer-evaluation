using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Products;

public class ProductCategoryValidator : AbstractValidator<ProductCategory>
{
    public ProductCategoryValidator()
    {
        RuleFor(category => category.Description)
            .NotEmpty()
            .WithMessage("Product category description cannot be empty.")
            .MinimumLength(3)
            .WithMessage("Product category description must be at least 2 characters long.")
            .MaximumLength(70)
            .WithMessage("Product category description cannot be longer than 70 characters.");
    }
}
