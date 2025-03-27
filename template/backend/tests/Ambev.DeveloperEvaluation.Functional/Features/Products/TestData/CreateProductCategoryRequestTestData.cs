using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Bogus;

namespace Ambev.DeveloperEvaluation.Functional.Features.Products.TestData
{
    internal static class CreateProductRequestTestData
    {
        private static readonly Faker<CreateProductRequest> ProductFaker = new Faker<CreateProductRequest>()
        .RuleFor(u => u.Title, f => f.Commerce.ProductName())
        .RuleFor(u => u.Description, f => f.Commerce.ProductDescription())
        .RuleFor(u => u.Price, f => Convert.ToDecimal(f.Commerce.Price()))
        .RuleFor(u => u.CategoryId, Guid.NewGuid())
        .RuleFor(p => p.Image, f => f.Image.PlaceImgUrl());

        public static CreateProductRequest GenerateValidRequest() => ProductFaker.Generate();
    }
}
