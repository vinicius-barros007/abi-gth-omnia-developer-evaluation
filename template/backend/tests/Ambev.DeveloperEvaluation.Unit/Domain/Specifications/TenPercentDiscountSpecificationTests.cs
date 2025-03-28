using Ambev.DeveloperEvaluation.Domain.Specifications;
using Ambev.DeveloperEvaluation.TestData.Sales;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications;

public class TenPercentDiscountSpecificationTests
{
    [Theory]
    [InlineData(3, false)]
    [InlineData(4, true)]
    [InlineData(9, true)]
    [InlineData(10, false)]
    public void IsSatisfiedBy_ShouldValidateQuantity(int quantity, bool expectedResult)
    {
        // Arrange
        var user = SaleItemTestData.GenerateValidItem();
        user.Quantity = quantity;
        var specification = new TenPercentDiscountSpecification();

        // Act
        var result = specification.IsSatisfiedBy(user);

        // Assert
        result.Should().Be(expectedResult);
    }
}
