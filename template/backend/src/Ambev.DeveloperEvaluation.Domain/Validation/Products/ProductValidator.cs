using Ambev.DeveloperEvaluation.Domain.Entities.Catalog;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Products;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(product => product.Title)
            .NotEmpty()
            .WithMessage("Product title cannot be empty.")
            .MinimumLength(2)
            .WithMessage("Product title must be at least 2 characters long.")
            .MaximumLength(100)
            .WithMessage("Product title cannot be longer than 100 characters.");

        RuleFor(product => product.Description)
            .NotEmpty()
            .WithMessage("Product description cannot be empty.")
            .MinimumLength(3)
            .WithMessage("Product description must be at least 2 characters long.")
            .MaximumLength(500)
            .WithMessage("Product description cannot be longer than 500 characters.");

        RuleFor(product => product.Image)
            .NotEmpty()
            .WithMessage("Product image cannot be empty.")
            .MaximumLength(1000)
            .WithMessage("Product image cannot be longer than 1000 characters.");

        RuleFor(product => product.CategoryId)
            .NotEmpty()
            .WithMessage("Product category cannot be empty.");

        RuleFor(product => product.Price)
            .GreaterThan(0)
            .WithMessage("Product price should be greater than zero.");
    }
}
