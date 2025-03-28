using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.TestData.Products;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Repositories
{
    public class SaleRepositoryTests : RepositoryBaseTests
    {
        private readonly SaleRepository _repository;

        public SaleRepositoryTests() : base()
        {
            _repository = new SaleRepository(_context);
        }

        [Fact(DisplayName = "Creation of sale should succeed")]
        public async Task Create_ValidSale_Then_ShouldSucceed()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            // Act
            var action = async () =>
            {
                await _repository.CreateAsync(sale);
                await _unitOfWork.SaveChangesAsync();
            };

            // Assert
            await action.Should().NotThrowAsync();
        }
    }
}
