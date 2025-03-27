using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Catalog;

public class Product : BaseEntity
{
    public Product()
    {
        CreatedAt = DateTime.UtcNow;
        Category = default!;
        Rating = default!;
    }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Image { get; set; } = string.Empty;

    public Guid CategoryId { get; set; } = Guid.Empty;

    public ProductCategory Category { get; set; }

    public ProductRating Rating { get; set; }

    public decimal Price { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}