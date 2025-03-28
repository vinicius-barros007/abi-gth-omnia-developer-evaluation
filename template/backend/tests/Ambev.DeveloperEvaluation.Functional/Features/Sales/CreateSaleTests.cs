using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
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
    public class CreateSaleTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public CreateSaleTests(TestWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        private async Task<CreateProductCategoryResponse> CreateProductCategoryAsync()
        {
            var category = CreateProductCategoryRequestTestData.GenerateValidRequest();
            var response = await _client.PostAsJsonAsync("/api/product-category", category);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateProductCategoryResponse>>();
            return result?.Data ?? throw new Exception("Cannot create product category");
        }

        private async Task<CreateProductResponse> CreateProductAsync(Guid categoryId)
        {
            var product = CreateProductRequestTestData.GenerateValidRequest();
            product.CategoryId = categoryId;
            var response = await _client.PostAsJsonAsync("/api/product", product);

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateProductResponse>>();

            return result?.Data ?? throw new Exception("Cannot create product category");
        }


        [Fact(DisplayName = "Creation of valid sale should return created status code")]
        public async Task Valid_Sale_ShouldReturn_Created()
        {
            // Arrange            
            var sale = CreateSaleRequestTestData.GenerateValidRequest();
            foreach (var item in sale.Items)
            {
                var category = await CreateProductCategoryAsync();
                var product = await CreateProductAsync(category.Id);   

                item.ProductId = product.Id;
            }

            // Act
            var response = await _client.PostAsJsonAsync("/api/sale", sale);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var content = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateSaleResult>>();

            content.Should().NotBeNull();
            content.Success.Should().BeTrue();

            content.Message.Should().Be("Sale created successfully");
            content.Data.Should().NotBeNull();

            var saleResponse = content.Data;
            saleResponse.Id.Should().NotBeEmpty();
        }

        [Fact(DisplayName = "Creation of invalid sale should return bad request status code")]
        public async Task Invalid_Sale_ShouldReturn_BadRequest()
        {
            // Arrange
            var sale = CreateSaleRequestTestData.GenerateValidRequest();

            // Act
            var response = await _client.PostAsJsonAsync("/api/sale", sale);

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
