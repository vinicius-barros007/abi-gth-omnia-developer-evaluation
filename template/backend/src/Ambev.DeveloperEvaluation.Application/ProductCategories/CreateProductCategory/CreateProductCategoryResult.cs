namespace Ambev.DeveloperEvaluation.Application.ProductCategories.CreateProductCategory;

public class CreateProductCategoryResult
{
    public Guid Id { get; set; } = Guid.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } 
}
