using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        if (_context.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory")
            sale.SaleNumber = 1;

        await _context.Sales.AddAsync(sale, cancellationToken);
        return sale;
    }

    private IQueryable<Sale> GetQueryable(Expression<Func<Sale, bool>> predicate)
    {
        var sales = _context.Sales.AsNoTracking().AsQueryable();
        if(predicate is not null)
            sales = sales.Where(predicate);

        return sales
            .Include(s => s.Items).ThenInclude(i => i.Product);
    }

    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetQueryable(s => s.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
