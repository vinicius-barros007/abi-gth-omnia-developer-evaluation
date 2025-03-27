namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

public sealed class CreateProductResponse
{
    public Guid Id { get; set; } = Guid.Empty;

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Image { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public decimal Price { get; set; }
}
