using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.TestData.Sales;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Repositories
{
    public class SaleItemRepositoryTests : RepositoryBaseTests
    {
        private readonly SaleItemRepository _repository;

        public SaleItemRepositoryTests() : base()
        {
            _repository = new SaleItemRepository(_context);
        }

        [Fact(DisplayName = "Creation of sale item should succeed")]
        public async Task Create_ValidSaleItem_Then_ShouldSucceed()
        {
            // Arrange
            var item = SaleItemTestData.GenerateValidItem();

            // Act
            var action = async () =>
            {
                await _repository.CreateAsync(item);
                await _unitOfWork.SaveChangesAsync();
            };

            // Assert
            await action.Should().NotThrowAsync();
        }
    }
}
