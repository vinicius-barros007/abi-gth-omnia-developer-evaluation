using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IProductCategoryRepository using Entity Framework Core
/// </summary>
public class ProductCategoryRepository : IProductCategoryRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of ProductCategoryRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public ProductCategoryRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new product category in the database
    /// </summary>
    /// <param name="productCategory">The product category to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created product category</returns>
    public async Task<ProductCategory> CreateAsync(ProductCategory productCategory, CancellationToken cancellationToken = default)
    {
        await _context.Set<ProductCategory>().AddAsync(productCategory, cancellationToken);
        return productCategory;
    }

    /// <summary>
    /// Retrieves a product category by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the product category</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product category if found, null otherwise</returns>
    public async Task<ProductCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<ProductCategory>()
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }
}
