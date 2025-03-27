using Ambev.DeveloperEvaluation.Domain.Validation.Products;
using Ambev.DeveloperEvaluation.TestData.Products;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation.Products;

public class ProductValidatorTests
{
    private readonly ProductValidator _validator;

    public ProductValidatorTests()
    {
        _validator = new ProductValidator();
    }

    [Fact(DisplayName = "Valid product should pass all validation rules")]
    public void Given_ValidProductCategory_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory(DisplayName = "Invalid product title formats should fail validation")]
    [InlineData("")] // Empty
    [InlineData("a")] // Less than 3 characters
    public void Given_InvalidProductTitle_When_Validated_Then_ShouldHaveError(string title)
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Title = title;

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Fact(DisplayName = "Product title longer than maximum length should fail validation")]
    public void Given_TitleLongerThanMaximum_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var product = ProductTestData.GenerateInvalidProduct();

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Title)
            .WithErrorMessage("Product title cannot be longer than 100 characters.");
    }

    [Theory(DisplayName = "Invalid product description formats should fail validation")]
    [InlineData("")] // Empty
    [InlineData("ab")] // Less than 3 characters
    public void Given_InvalidProductDescription_When_Validated_Then_ShouldHaveError(string description)
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Description = description;

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Fact(DisplayName = "Product description longer than maximum length should fail validation")]
    public void Given_DescriptionLongerThanMaximum_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var product = ProductTestData.GenerateInvalidProduct();

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description)
            .WithErrorMessage("Product description cannot be longer than 500 characters.");
    }

    [Fact(DisplayName = "Empty product image should fail validation")]
    public void Given_EmptyProductImage_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Image = default!;

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Image);
    }

    [Fact(DisplayName = "Product description longer than maximum length should fail validation")]
    public void Given_ImageLongerThanMaximum_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var product = ProductTestData.GenerateInvalidProduct();

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Image)
            .WithErrorMessage("Product image cannot be longer than 1000 characters.");
    }

    [Fact(DisplayName = "Empty product category should fail validation")]
    public void Given_EmptyProductCategory_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var product = ProductTestData.GenerateInvalidProduct();

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CategoryId);
    }


    [Fact(DisplayName = "Invalid product price should fail validation")]
    public void Given_InvalidProductPrice_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var product = ProductTestData.GenerateInvalidProduct();

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Price);
    }
}
