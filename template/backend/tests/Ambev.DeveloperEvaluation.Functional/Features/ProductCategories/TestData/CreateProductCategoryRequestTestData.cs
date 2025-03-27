using Ambev.DeveloperEvaluation.WebApi.Features.ProductCategories.CreateProductCategory;
using Bogus;

namespace Ambev.DeveloperEvaluation.Functional.Features.ProductCategories.TestData
{
    internal static class CreateProductCategoryRequestTestData
    {
        private static readonly Faker<CreateProductCategoryRequest> ProductCategoryFaker = new Faker<CreateProductCategoryRequest>()
            .RuleFor(c => c.Description, f => f.Commerce.ProductMaterial());

        public static CreateProductCategoryRequest GenerateValidRequest() => ProductCategoryFaker.Generate();
    }
}
