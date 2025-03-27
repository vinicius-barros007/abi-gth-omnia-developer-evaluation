using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.ProductCategories.CreateProductCategory;

public class CreateProductCategoryCommandValidator : AbstractValidator<CreateProductCategoryCommand>
{
    public CreateProductCategoryCommandValidator()
    {
        RuleFor(category => category.Description)
            .NotEmpty()
            .Length(3, 70);
    }
}