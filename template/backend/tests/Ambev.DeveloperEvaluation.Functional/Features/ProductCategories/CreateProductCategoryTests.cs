using Ambev.DeveloperEvaluation.Functional.Features.ProductCategories.TestData;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.ProductCategories.CreateProductCategory;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Ambev.DeveloperEvaluation.Functional.Features.ProductCategories
{
    public class CreateProductCategoryTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public CreateProductCategoryTests(TestWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact(DisplayName = "Creation of valid product category should return created status code")]
        public async Task Valid_ProductCategory_ShouldReturn_Created()
        {
            // Arrange
            var category = CreateProductCategoryRequestTestData.GenerateValidRequest();

            // Act
            var response = await _client.PostAsJsonAsync("/api/product-category", category);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var content = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateProductCategoryResponse>>();

            content.Should().NotBeNull();
            content.Success.Should().BeTrue();

            content.Message.Should().Be("Product category created successfully");
            content.Data.Should().NotBeNull();

            var categoryResponse = content.Data;
            categoryResponse.Id.Should().NotBeEmpty();

            categoryResponse.Description.Should().Be(category.Description);
        }

        [Fact(DisplayName = "Creation of invalid product category should return bad request status code")]
        public async Task Invalid_ProductCategory_ShouldReturn_BadRequest()
        {
            // Arrange
            var category = CreateProductCategoryRequestTestData.GenerateValidRequest();
            category.Description = string.Empty; // Invalidating the request

            // Act
            var response = await _client.PostAsJsonAsync("/api/product-category", category);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var content = await response.Content.ReadFromJsonAsync<ApiResponse>();

            content.Should().NotBeNull();
            content.Success.Should().BeFalse();

            content.Message.Should().Be("Validation Failed");
            content.Errors.Should().HaveCountGreaterThan(0);
        }
    }
}
