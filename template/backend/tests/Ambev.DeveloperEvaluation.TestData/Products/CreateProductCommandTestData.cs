using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Bogus;

namespace Ambev.DeveloperEvaluation.TestData.Products;

public static class CreateProductCommandTestData
{
    public static CreateProductCommand GenerateValidCommand()
    {
        Faker<CreateProductCommand> faker = new Faker<CreateProductCommand>()
            .RuleFor(u => u.Title, f => f.Commerce.ProductName())
            .RuleFor(u => u.Description, f => f.Commerce.ProductDescription())
            .RuleFor(u => u.Price, f => Convert.ToDecimal(f.Commerce.Price()))
            .RuleFor(u => u.CategoryId, Guid.NewGuid())
            .RuleFor(p => p.Image, f => f.Image.PlaceImgUrl());

        return faker.Generate();
    }
}
