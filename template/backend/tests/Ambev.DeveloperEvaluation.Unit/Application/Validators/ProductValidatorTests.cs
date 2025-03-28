using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.TestData.Products;
using FluentValidation.TestHelper;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Validators;

public class ProductValidatorTests
{
    private readonly IProductCategoryRepository _productCategoryRepository;
    private readonly CreateProductCommandValidator _validator;

    public ProductValidatorTests()
    {
        _productCategoryRepository = Substitute.For<IProductCategoryRepository>();
        _validator = new CreateProductCommandValidator(_productCategoryRepository);
    }

    [Fact(DisplayName = "Valid product should pass all validation rules")]
    public async Task Given_ValidProduct_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var command = CreateProductCommandTestData.GenerateValidCommand();
        ProductCategory category = ProductCategoryTestData.GenerateValidCategory();

        _productCategoryRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(category);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory(DisplayName = "Invalid product title formats should fail validation")]
    [InlineData("")] // Empty
    [InlineData("a")] // Less than 2 characters
    public async Task Given_InvalidProductTitle_When_Validated_Then_ShouldHaveError(string title)
    {
        // Arrange
        var command = CreateProductCommandTestData.GenerateValidCommand();
        command.Title = title;

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Fact(DisplayName = "Product title longer than maximum length should fail validation")]
    public async Task Given_TitleLongerThanMaximum_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var command = CreateProductCommandTestData.GenerateValidCommand();
        command.Title = new string('a', 101);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Title)
            .WithErrorCode("LengthValidator");
    }

    [Theory(DisplayName = "Invalid product description formats should fail validation")]
    [InlineData("")] // Empty
    [InlineData("ab")] // Less than 3 characters
    public async Task Given_InvalidProductDescription_When_Validated_Then_ShouldHaveError(string description)
    {
        // Arrange
        var command = CreateProductCommandTestData.GenerateValidCommand();
        command.Description = description;

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Fact(DisplayName = "Product description longer than maximum length should fail validation")]
    public async Task Given_DescriptionLongerThanMaximum_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var command = CreateProductCommandTestData.GenerateValidCommand();
        command.Description = new string('a', 501);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description)
            .WithErrorCode("LengthValidator");
    }

    [Fact(DisplayName = "Empty product image should fail validation")]
    public async Task Given_EmptyProductImage_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var command = CreateProductCommandTestData.GenerateValidCommand();
        command.Image = string.Empty;

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Image)
            .WithErrorCode("NotEmptyValidator");
    }

    [Fact(DisplayName = "Product image longer than maximum length should fail validation")]
    public async Task Given_ImageLongerThanMaximum_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var command = CreateProductCommandTestData.GenerateValidCommand();
        command.Image = new string('a', 1001);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Image)
            .WithErrorCode("MaximumLengthValidator");
    }

    [Fact(DisplayName = "Empty product category should fail validation")]
    public async Task Given_EmptyProductCategory_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var command = CreateProductCommandTestData.GenerateValidCommand();
        command.CategoryId = Guid.Empty;

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CategoryId)
            .WithErrorCode("NotEmptyValidator");
    }

    [Fact(DisplayName = "Invalid product category should fail validation")]
    public async Task Given_InvalidProductCategory_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var command = CreateProductCommandTestData.GenerateValidCommand();
        ProductCategory category = default!;   

        _productCategoryRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(category);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CategoryId)
            .WithErrorMessage($"CategoryId {command.CategoryId} doesn't exist.");
    }

    [Fact(DisplayName = "Invalid product price should fail validation")]
    public async Task Given_InvalidProductPrice_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var command = CreateProductCommandTestData.GenerateValidCommand();
        command.Price = 0;

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Price)
            .WithErrorCode("GreaterThanValidator");
    }
}
