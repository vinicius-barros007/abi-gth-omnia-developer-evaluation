using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Services;
using Bogus;

namespace Ambev.DeveloperEvaluation.TestData.Sales;

public static class SaleItemTestData
{
    public static IEnumerable<SaleItem> GenerateValidItem(int count = 5)
    {
        Faker<SaleItem> faker = new Faker<SaleItem>()
            .RuleFor(c => c.SaleId, Guid.NewGuid())
            .RuleFor(c => c.ProductId, Guid.NewGuid())
            .RuleFor(c => c.Quantity, f => f.Random.Int(1, 20))
            .RuleFor(c => c.UnitPrice, f => Convert.ToDecimal(f.Commerce.Price()));

        DiscountService discountService = new();
        IEnumerable<SaleItem> items = faker.Generate(count);

        foreach (SaleItem item in items)
        {
            item.Discount = discountService.CalculateDiscount(item);
        }

        return items;
    }

    public static SaleItem GenerateValidItem()
    {
        Faker<SaleItem> faker = new Faker<SaleItem>()
            .RuleFor(c => c.SaleId, Guid.NewGuid())
            .RuleFor(c => c.ProductId, Guid.NewGuid())
            .RuleFor(c => c.Quantity, f => f.Random.Int(1, 20))
            .RuleFor(c => c.UnitPrice, f => Convert.ToDecimal(f.Commerce.Price()));

        SaleItem item = faker.Generate();
        DiscountService discountService = new();

        item.Discount = discountService.CalculateDiscount(item);
        return item;
    }

    public static SaleItem GenerateInvalidItem()
    {
        SaleItem item = GenerateValidItem();
        item.ProductId = Guid.Empty;
        item.Quantity = int.MaxValue;
        item.UnitPrice = short.MinValue;
        item.Discount = short.MinValue;
        return item;
    }
}
