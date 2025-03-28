using Ambev.DeveloperEvaluation.WebApi.Features.ProductCategories.CreateProductCategory;
using Bogus;

namespace Ambev.DeveloperEvaluation.Functional.Features.ProductCategories.TestData
{
    internal static class CreateProductCategoryRequestTestData
    {
        public static CreateProductCategoryRequest GenerateValidRequest()
        {
            var faker = new Faker<CreateProductCategoryRequest>()
                .RuleFor(c => c.Description, f => f.Commerce.ProductMaterial());

            return faker.Generate();
        }
    }
}
