using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Products;

public class ProductCategory : BaseEntity
{
    public ProductCategory()
    {
        CreatedAt = DateTime.UtcNow;
    }

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public ICollection<Product> Products { get; set; } = [];
}