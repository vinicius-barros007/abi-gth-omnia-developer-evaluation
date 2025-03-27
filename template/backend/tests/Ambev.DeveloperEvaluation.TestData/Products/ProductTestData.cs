using Ambev.DeveloperEvaluation.Domain.Entities.Catalog;
using Bogus;

namespace Ambev.DeveloperEvaluation.TestData.Products;

public static class ProductTestData
{
    public static Product GenerateValidProduct()
    {
        Faker<Product> faker = new Faker<Product>()
            .RuleFor(p => p.Title, f => f.Commerce.Product())
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.Price, f => Convert.ToDecimal(f.Commerce.Price()))
            .RuleFor(p => p.CategoryId, Guid.NewGuid())
            .RuleFor(p => p.Category, ProductCategoryTestData.GenerateValidCategory())
            .RuleFor(p => p.Image, f => f.Image.PlaceImgUrl());

        return faker.Generate(); 
    }

    public static Product GenerateInvalidProduct()
    {
        Product product = GenerateValidProduct();

        product.Title = product.Title.PadLeft(101, '*');
        product.Description = product.Description.PadLeft(501, '*');
        product.Image = product.Image.PadLeft(1001, '*');
        product.Price = product.Price * -1;
        product.CategoryId = Guid.Empty;

        return product;
    }
}
