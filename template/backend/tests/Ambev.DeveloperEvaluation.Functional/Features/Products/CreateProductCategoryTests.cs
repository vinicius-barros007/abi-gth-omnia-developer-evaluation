using Ambev.DeveloperEvaluation.Functional.Features.ProductCategories.TestData;
using Ambev.DeveloperEvaluation.Functional.Features.Products.TestData;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.ProductCategories.CreateProductCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Ambev.DeveloperEvaluation.Functional.Features.Products
{
    public class CreateProductTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public CreateProductTests(TestWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact(DisplayName = "Creation of valid product should return created status code")]
        public async Task Valid_Product_ShouldReturn_Created()
        {
            // Arrange
            var category = CreateProductCategoryRequestTestData.GenerateValidRequest();
            var response = await _client.PostAsJsonAsync("/api/product-category", category);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateProductCategoryResponse>>();
            var productCategory = result?.Data ?? throw new Exception("Cannot create product category");

            var product = CreateProductRequestTestData.GenerateValidRequest();
            product.CategoryId = productCategory.Id;

            // Act
            response = await _client.PostAsJsonAsync("/api/product", product);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var content = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateProductResponse>>();

            content.Should().NotBeNull();
            content.Success.Should().BeTrue();

            content.Message.Should().Be("Product created successfully");
            content.Data.Should().NotBeNull();

            var productResponse = content.Data;
            productResponse.Id.Should().NotBeEmpty();

            productResponse.Description.Should().Be(product.Description);
            productResponse.Title.Should().Be(product.Title);
            productResponse.Price.Should().Be(product.Price);
            productResponse.Image.Should().Be(product.Image);
        }

        [Fact(DisplayName = "Creation of invalid product should return bad request status code")]
        public async Task Invalid_Product_ShouldReturn_BadRequest()
        {
            // Arrange
            var product = CreateProductRequestTestData.GenerateValidRequest();
            product.Description = string.Empty; // Invalidating the request

            // Act
            var response = await _client.PostAsJsonAsync("/api/product", product);

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
