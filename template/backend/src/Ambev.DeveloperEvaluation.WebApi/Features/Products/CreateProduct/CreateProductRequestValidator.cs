using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(product => product.Title)
            .NotEmpty()
            .Length(2, 100);

        RuleFor(product => product.Description)
            .NotEmpty()
            .Length(3, 500);

        RuleFor(product => product.Image)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(product => product.CategoryId)
            .NotEmpty();

        RuleFor(product => product.Price)
            .GreaterThan(0);
    }
}
