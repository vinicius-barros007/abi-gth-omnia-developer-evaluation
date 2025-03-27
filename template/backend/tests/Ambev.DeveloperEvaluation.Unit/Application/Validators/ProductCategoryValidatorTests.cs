using Ambev.DeveloperEvaluation.Application.ProductCategories.CreateProductCategory;
using Ambev.DeveloperEvaluation.TestData.Products;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Validators;

public class ProductCategoryValidatorTests
{
    private readonly CreateProductCategoryCommandValidator _validator;

    public ProductCategoryValidatorTests()
    {
        _validator = new CreateProductCategoryCommandValidator();
    }

    [Fact(DisplayName = "Valid product category should pass all validation rules")]
    public void Given_ValidProductCategory_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var command = CreateProductCategoryCommandTestData.GenerateValidCommand();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory(DisplayName = "Invalid product category description formats should fail validation")]
    [InlineData("")] // Empty
    [InlineData("ab")] // Less than 3 characters
    public void Given_InvalidProductTitle_When_Validated_Then_ShouldHaveError(string description)
    {
        // Arrange
        var command = CreateProductCategoryCommandTestData.GenerateValidCommand();
        command.Description = description;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Fact(DisplayName = "Product title longer than maximum length should fail validation")]
    public void Given_TitleLongerThanMaximum_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var command = CreateProductCategoryCommandTestData.GenerateValidCommand();
        command.Description = new string('a', 101);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description)
            .WithErrorCode("LengthValidator");
    }
}
