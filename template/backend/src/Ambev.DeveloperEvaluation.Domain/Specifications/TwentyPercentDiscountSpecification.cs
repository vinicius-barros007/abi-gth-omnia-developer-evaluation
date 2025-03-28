using Ambev.DeveloperEvaluation.Domain.Entities.Sales;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public class TwentyPercentDiscountSpecification : ISpecification<SaleItem>
{
    public bool IsSatisfiedBy(SaleItem item)
    {
        return item.Quantity >= 10 && item.Quantity <= 20;
    }
}
