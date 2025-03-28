using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IProductRepository using Entity Framework Core
/// </summary>
public class ProductRepository : IProductRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of ProductRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public ProductRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new product in the database
    /// </summary>
    /// <param name="product">The product to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created product</returns>
    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        return product;
    }

    private IQueryable<Product> GetQueryable(Expression<Func<Product, bool>> predicate)
    {
        var products = _context.Products.AsQueryable();
        if (predicate is not null)
            products = products.Where(predicate);

        return from product in products.AsNoTracking()
               join category in _context.Set<ProductCategory>().AsNoTracking()
               on product.CategoryId equals category.Id
               select new Product()
               {
                   Id = product.Id,
                   Title = product.Title,
                   Description = product.Description,
                   Category = category,
                   CategoryId = product.CategoryId,
                   Image = product.Image,
                   Price = product.Price,
                   CreatedAt = product.CreatedAt,
                   UpdatedAt = product.UpdatedAt,
               };
    }

    /// <summary>
    /// Retrieves a product by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product if found, null otherwise</returns>
    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetQueryable(o => o.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
