using Ambev.DeveloperEvaluation.Domain.Entities.Sales;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleItemRepository
{
    Task<SaleItem> CreateAsync(SaleItem item, CancellationToken cancellationToken = default);
}
