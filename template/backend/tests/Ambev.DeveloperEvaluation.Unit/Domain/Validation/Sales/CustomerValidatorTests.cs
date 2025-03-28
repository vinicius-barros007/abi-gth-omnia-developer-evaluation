using Ambev.DeveloperEvaluation.Domain.Validation.Common;
using Ambev.DeveloperEvaluation.TestData.Sales;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation.Products;

public class CustomerValidatorTests
{
    private readonly CustomerValidator _validator;

    public CustomerValidatorTests()
    {
        _validator = new CustomerValidator();
    }

    [Fact(DisplayName = "Valid customer should pass all validation rules")]
    public void Given_ValidBranch_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var customer = CustomerTestData.GenerateValidCustomer();

        // Act
        var result = _validator.TestValidate(customer);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "Invalid customer id should fail validation")]
    public void Given_InvalidBranchId_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var customer = CustomerTestData.GenerateInvalidCustomer();

        // Act
        var result = _validator.TestValidate(customer);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id)
            .WithErrorMessage("Customer id cannot be empty.");
    }

    [Theory(DisplayName = "Invalid customer name formats should fail validation")]
    [InlineData("")] // Empty
    [InlineData("a")] // Less than 3 characters
    public void Given_InvalidBranchName_When_Validated_Then_ShouldHaveError(string name)
    {
        // Arrange
        var customer = CustomerTestData.GenerateValidCustomer();
        customer.Name = name;

        // Act
        var result = _validator.TestValidate(customer);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact(DisplayName = "Customer name longer than maximum length should fail validation")]
    public void Given_BranchNameLongerThanMaximum_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var customer = CustomerTestData.GenerateInvalidCustomer();

        // Act
        var result = _validator.TestValidate(customer);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("Customer name cannot be longer than 180 characters.");
    }
}
