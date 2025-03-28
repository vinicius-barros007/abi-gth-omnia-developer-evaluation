using Ambev.DeveloperEvaluation.TestData.Extensions;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.TestData.Sales;
using Bogus;

namespace Ambev.DeveloperEvaluation.TestData.Products;

public static class SaleTestData
{
    public static Sale GenerateValidSale()
    {
        Faker<Sale> faker = new Faker<Sale>()
            .RuleFor(s => s.Items, SaleItemTestData.GenerateValidItem(5))
            .RuleFor(s => s.SaleNumber, f => f.Random.Long(1))
            .RuleFor(s => s.SaleDate, f => f.Date.Past())
            .RuleFor(s => s.Branch, BranchTestData.GenerateValidBranch())
            .RuleFor(s => s.Customer, CustomerTestData.GenerateValidCustomer())
            .RuleFor(s => s.Status, f => f.GenerateRandomSaleStatus());

        return faker.Generate(); 
    }

    public static Sale GenerateInvalidSale()
    {
        Sale product = GenerateValidSale();

        product.Branch.Name = product.Branch.Name.PadLeft(101, '*');
        product.Customer.Name = product.Customer.Name.PadLeft(501, '*');
        product.Status = Domain.Enums.Sales.SaleStatus.None;
        product.SaleNumber = int.MinValue;

        return product;
    }
}
