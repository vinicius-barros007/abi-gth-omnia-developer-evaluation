using Ambev.DeveloperEvaluation.Domain.Entities.Catalog;
using Bogus;

namespace Ambev.DeveloperEvaluation.TestData.Products;

public static class ProductCategoryTestData
{
    public static ProductCategory GenerateValidCategory()
    {
        Faker<ProductCategory> faker = new Faker<ProductCategory>()
            .RuleFor(c => c.Description, f => f.Commerce.ProductMaterial());

        return faker.Generate();
    }

    public static ProductCategory GenerateInvalidCategory()
    {
        ProductCategory category = GenerateValidCategory();
        category.Description = category.Description.PadLeft(71, '*');
        return category;
    }
}
