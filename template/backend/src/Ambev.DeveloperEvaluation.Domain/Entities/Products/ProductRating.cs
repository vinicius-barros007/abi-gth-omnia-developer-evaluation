using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Catalog;

public class ProductRating : BaseEntity
{
    public ProductRating()
    {
        CreatedAt = DateTime.UtcNow;
        Product = default!;
    }

    public Guid ProductId { get; set; } = Guid.Empty;

    public Product Product { get; set; }

    public decimal Rate { get; set; }

    public int Count { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}