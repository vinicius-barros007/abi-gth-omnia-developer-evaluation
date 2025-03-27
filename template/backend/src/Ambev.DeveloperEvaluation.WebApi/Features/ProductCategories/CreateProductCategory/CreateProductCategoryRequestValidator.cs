using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.ProductCategories.CreateProductCategory;

public class CreateProductCategoryRequestValidator : AbstractValidator<CreateProductCategoryRequest>
{
    public CreateProductCategoryRequestValidator()
    {
        RuleFor(category => category.Description)
            .NotEmpty()
            .Length(3, 70);
    }
}
