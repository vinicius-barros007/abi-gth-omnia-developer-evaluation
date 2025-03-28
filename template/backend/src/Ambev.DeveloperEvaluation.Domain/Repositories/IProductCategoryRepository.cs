using Ambev.DeveloperEvaluation.Domain.Entities.Products;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for ProductCategory entity operations
/// </summary>
public interface IProductCategoryRepository
{
    /// <summary>
    /// Creates a new product category in the repository
    /// </summary>
    /// <param name="productCategory">The product category to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created product category</returns>
    Task<ProductCategory> CreateAsync(ProductCategory productCategory, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a product category by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the product category</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product category if found, null otherwise</returns>
    Task<ProductCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
