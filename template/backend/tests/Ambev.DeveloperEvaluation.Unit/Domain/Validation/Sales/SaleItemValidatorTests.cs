using Ambev.DeveloperEvaluation.Domain.Validation.Sales;
using Ambev.DeveloperEvaluation.TestData.Sales;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation.Products;

public class SaleItemValidatorTests
{
    private readonly SaleItemValidator _validator;

    public SaleItemValidatorTests()
    {
        _validator = new SaleItemValidator();
    }

    [Fact(DisplayName = "Valid sale item should pass all validation rules")]
    public void Given_ValidSaleItem_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var item = SaleItemTestData.GenerateValidItem();

        // Act
        var result = _validator.TestValidate(item);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "Invalid product id should fail validation")]
    public void Given_InvalidProductId_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var item = SaleItemTestData.GenerateInvalidItem();

        // Act
        var result = _validator.TestValidate(item);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ProductId)
            .WithErrorMessage("Product title cannot be empty.");
    }

    [InlineData(0)]
    [InlineData(-1)]
    [Theory(DisplayName = "Quantity lower than one should fail validation")]
    public void Given_InvalidQuantityLowerThanOne_When_Validated_Then_ShouldHaveError(int quantity)
    {
        // Arrange
        var item = SaleItemTestData.GenerateInvalidItem();
        item.Quantity = quantity;

        // Act
        var result = _validator.TestValidate(item);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Quantity)
            .WithErrorMessage("Quantity should be greater than zero.");
    }

    [Fact(DisplayName = "Quantity lower than one should fail validation")]
    public void Given_InvalidQuantityGreaterThanTwenty_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var item = SaleItemTestData.GenerateInvalidItem();

        // Act
        var result = _validator.TestValidate(item);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Quantity)
            .WithErrorMessage("Quantity should be lower or equal to twenty.");
    }

    [InlineData(0)]
    [InlineData(-1)]
    [Theory(DisplayName = "Price lower than one should fail validation")]
    public void Given_InvalidPriceLowerThanOne_When_Validated_Then_ShouldHaveError(decimal price)
    {
        // Arrange
        var item = SaleItemTestData.GenerateInvalidItem();
        item.UnitPrice = price;

        // Act
        var result = _validator.TestValidate(item);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UnitPrice)
            .WithErrorMessage("Unit price should be greater than zero.");
    }

    [Fact(DisplayName = "Discount lower than one should fail validation")]
    public void Given_InvalidDiscountLowerThanOne_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var item = SaleItemTestData.GenerateInvalidItem();

        // Act
        var result = _validator.TestValidate(item);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Discount)
            .WithErrorMessage("Discount should be greater or equal to zero.");
    }
}
