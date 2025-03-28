using Ambev.DeveloperEvaluation.Domain.Entities.Sales;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public class TenPercentDiscountSpecification : ISpecification<SaleItem>
{
    public bool IsSatisfiedBy(SaleItem item)
    {
        return item.Quantity >= 4 && item.Quantity < 10;
    }
}
