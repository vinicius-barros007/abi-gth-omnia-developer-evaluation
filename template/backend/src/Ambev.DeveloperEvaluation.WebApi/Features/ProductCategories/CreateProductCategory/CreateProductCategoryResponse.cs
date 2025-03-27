namespace Ambev.DeveloperEvaluation.WebApi.Features.ProductCategories.CreateProductCategory;

public sealed class CreateProductCategoryResponse
{
    public Guid Id { get; set; } = Guid.Empty;

    public string Description { get; set; } = string.Empty;
}
