using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.TestData.Products;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Repositories
{
    public class ProductCategoryRepositoryTests : RepositoryBaseTests
    {
        private readonly ProductCategoryRepository _repository;

        public ProductCategoryRepositoryTests() : base()
        {
            _repository = new ProductCategoryRepository(_context);
        }

        [Fact(DisplayName = "Creation of product category should succeed")]
        public async Task Create_ValidProductCategory_Then_ShouldSucceed()
        {
            // Arrange
            var productCategory = ProductCategoryTestData.GenerateValidCategory();

            // Act
            var action = async () =>
            {
                await _repository.CreateAsync(productCategory);
                await _unitOfWork.SaveChangesAsync();
            };

            // Assert
            await action.Should().NotThrowAsync();
        }

        [Fact(DisplayName = "Creation of product category should fail")]
        public async Task Create_InvalidProductCategory_Then_ShouldThrowError()
        {
            // Arrange
            var productCategory = ProductCategoryTestData.GenerateValidCategory();
            productCategory.Description = default!;

            // Act
            var action = async () =>
            {
                await _repository.CreateAsync(productCategory);
                await _unitOfWork.SaveChangesAsync();
            };

            // Assert
            await action.Should().ThrowAsync<Exception>();
        }
    }
}
