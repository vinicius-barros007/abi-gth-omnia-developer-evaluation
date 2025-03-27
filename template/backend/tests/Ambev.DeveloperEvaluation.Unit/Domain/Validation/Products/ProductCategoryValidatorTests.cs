using Ambev.DeveloperEvaluation.Domain.Validation.Products;
using Ambev.DeveloperEvaluation.TestData.Products;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation.Products;

public class ProductCategoryValidatorTests
{
    private readonly ProductCategoryValidator _validator;

    public ProductCategoryValidatorTests()
    {
        _validator = new ProductCategoryValidator();
    }

    [Fact(DisplayName = "Valid product category should pass all validation rules")]
    public void Given_ValidProductCategory_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var productCategory = ProductCategoryTestData.GenerateValidCategory();

        // Act
        var result = _validator.TestValidate(productCategory);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory(DisplayName = "Invalid product category formats should fail validation")]
    [InlineData("")] // Empty
    [InlineData("ab")] // Less than 3 characters
    public void Given_InvalidProductCategory_When_Validated_Then_ShouldHaveError(string description)
    {
        // Arrange
        var productCategory = ProductCategoryTestData.GenerateValidCategory();
        productCategory.Description = description;

        // Act
        var result = _validator.TestValidate(productCategory);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Fact(DisplayName = "Description longer than maximum length should fail validation")]
    public void Given_DescriptionLongerThanMaximum_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var productCategory = ProductCategoryTestData.GenerateInvalidCategory();

        // Act
        var result = _validator.TestValidate(productCategory);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description)
            .WithErrorMessage("Product category description cannot be longer than 70 characters.");
    }
}
