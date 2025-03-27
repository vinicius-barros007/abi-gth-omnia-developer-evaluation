using Ambev.DeveloperEvaluation.Application.ProductCategories.CreateProductCategory;
using Bogus;

namespace Ambev.DeveloperEvaluation.TestData.Products;

public static class CreateProductCategoryCommandTestData
{
    public static CreateProductCategoryCommand GenerateValidCommand()
    {
        Faker<CreateProductCategoryCommand> faker = new Faker<CreateProductCategoryCommand>()
            .RuleFor(u => u.Description, f => f.Commerce.ProductMaterial());

        return faker.Generate();
    }
}
