using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Specifications;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public class DiscountService : IDiscountService
{
    private readonly ISpecification<SaleItem> _tenPercentDiscount = new TenPercentDiscountSpecification();
    private readonly ISpecification<SaleItem> _twentyPercentDiscount = new TwentyPercentDiscountSpecification();

    public decimal CalculateDiscount(SaleItem item)
    {
        decimal discountRate = 0;

        //Purchases between 10 and 20 identical items have a 20% discount
        if (_twentyPercentDiscount.IsSatisfiedBy(item))
            discountRate =  0.20m;

        // Purchases above 4 identical items have a 10% discount
        else if (_tenPercentDiscount.IsSatisfiedBy(item))
            discountRate =  0.10m;

        return item.UnitPrice * (decimal)item.Quantity * discountRate;
    }
}
