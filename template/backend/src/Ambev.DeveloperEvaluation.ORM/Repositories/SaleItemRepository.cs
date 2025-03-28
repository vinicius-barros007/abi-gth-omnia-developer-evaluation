using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleItemRepository : ISaleItemRepository
{
    private readonly DefaultContext _context;

    public SaleItemRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<SaleItem> CreateAsync(SaleItem item, CancellationToken cancellationToken = default)
    {
        await _context.Set<SaleItem>().AddAsync(item, cancellationToken);
        return item;
    }
}
