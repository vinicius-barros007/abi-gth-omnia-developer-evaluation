using Ambev.DeveloperEvaluation.TestData.Sales;
using Bogus;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.TestData.Products;

public static class CreateSaleCommandTestData
{
    public static IEnumerable<CreateSaleItemCommand> GenerateValidSaleItem()
    {
        var faker = new Faker<CreateSaleItemCommand>()
            .RuleFor(c => c.ProductId, Guid.NewGuid())
            .RuleFor(c => c.Quantity, f => f.Random.Int(1, 20));

        return faker.Generate(5);
    }

    public static CreateSaleCommand GenerateValidCommand()
    {
        var faker = new Faker<CreateSaleCommand>()
            .RuleFor(s => s.Items, GenerateValidSaleItem())
            .RuleFor(s => s.SaleDate, f => f.Date.Past())
            .RuleFor(s => s.Branch, BranchTestData.GenerateValidBranch())
            .RuleFor(s => s.Customer, CustomerTestData.GenerateValidCustomer());

        return faker.Generate(); 
    }
}
