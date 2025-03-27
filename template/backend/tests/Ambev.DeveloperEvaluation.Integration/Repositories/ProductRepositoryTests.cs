using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.TestData.Products;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Repositories
{
    public class ProductRepositoryTests : RepositoryBaseTests
    {
        private readonly ProductRepository _repository;

        public ProductRepositoryTests() : base()
        {
            _repository = new ProductRepository(_context);
        }

        [Fact(DisplayName = "Creation of product should succeed")]
        public async Task Create_ValidProduct_Then_ShouldSucceed()
        {
            // Arrange
            var product = ProductTestData.GenerateValidProduct();

            // Act
            var action = async () =>
            {
                await _repository.CreateAsync(product);
                await _unitOfWork.SaveChangesAsync();
            };

            // Assert
            await action.Should().NotThrowAsync();
        }

        [Fact(DisplayName = "Creation of product should fail")]
        public async Task Create_InvalidProduct_Then_ShouldThrowError()
        {
            // Arrange
            var product = ProductTestData.GenerateValidProduct();
            product.Title = default!;

            // Act
            var action = async () =>
            {
                await _repository.CreateAsync(product);
                await _unitOfWork.SaveChangesAsync();
            };

            // Assert
            await action.Should().ThrowAsync<Exception>();
        }
    }
}
