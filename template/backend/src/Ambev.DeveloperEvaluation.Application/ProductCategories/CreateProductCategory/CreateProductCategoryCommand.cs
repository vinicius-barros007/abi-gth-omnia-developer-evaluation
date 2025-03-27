using MediatR;

namespace Ambev.DeveloperEvaluation.Application.ProductCategories.CreateProductCategory;

public class CreateProductCategoryCommand : IRequest<CreateProductCategoryResult>
{
    public string Description { get; set; } = string.Empty;
}
