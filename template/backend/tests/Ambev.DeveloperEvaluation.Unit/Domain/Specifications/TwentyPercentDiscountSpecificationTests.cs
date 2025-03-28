using Ambev.DeveloperEvaluation.Domain.Specifications;
using Ambev.DeveloperEvaluation.TestData.Sales;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications;

public class TwentyPercentDiscountSpecificationTests
{
    [Theory]
    [InlineData(9, false)]
    [InlineData(10, true)]
    [InlineData(15, true)]
    [InlineData(20, true)]
    [InlineData(21, false)]
    public void IsSatisfiedBy_ShouldValidateQuantity(int quantity, bool expectedResult)
    {
        // Arrange
        var user = SaleItemTestData.GenerateValidItem();
        user.Quantity = quantity;
        var specification = new TwentyPercentDiscountSpecification();

        // Act
        var result = specification.IsSatisfiedBy(user);

        // Assert
        result.Should().Be(expectedResult);
    }
}
