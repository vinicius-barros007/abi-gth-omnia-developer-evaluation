using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Bogus;

namespace Ambev.DeveloperEvaluation.Functional.Features.Products.TestData
{
    internal static class CreateProductRequestTestData
    {
        public static CreateProductRequest GenerateValidRequest()
        {
            var faker = new Faker<CreateProductRequest>()
                .RuleFor(u => u.Title, f => f.Commerce.ProductName())
                .RuleFor(u => u.Description, f => f.Commerce.ProductDescription())
                .RuleFor(u => u.Price, f => Convert.ToDecimal(f.Commerce.Price()))
                .RuleFor(u => u.CategoryId, Guid.NewGuid())
                .RuleFor(p => p.Image, f => f.Image.PlaceImgUrl());

            return faker.Generate();
        }
    }
}
