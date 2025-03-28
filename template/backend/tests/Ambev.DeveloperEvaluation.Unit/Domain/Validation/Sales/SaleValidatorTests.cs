using Ambev.DeveloperEvaluation.Domain.Validation.Sales;
using Ambev.DeveloperEvaluation.TestData.Products;
using Ambev.DeveloperEvaluation.TestData.Sales;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation.Products;

public class SaleValidatorTests
{
    private readonly SaleValidator _validator;

    public SaleValidatorTests()
    {
        _validator = new SaleValidator();
    }

    [Fact(DisplayName = "Valid sale should pass all validation rules")]
    public void Given_ValidSale_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "Empty sale items should fail validation")]
    public void Given_EmptySaleItems_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var sale = SaleTestData.GenerateInvalidSale();
        sale.Items.Clear();

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Items)
            .WithErrorMessage("Items cannot be empty.");
    }

    [Fact(DisplayName = "Invalid sale items should fail validation")]
    public void Given_InvalidSaleItems_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        sale.Items.Clear();
        sale.Items.Add(SaleItemTestData.GenerateInvalidItem());

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveAnyValidationError();
    }

    [Fact(DisplayName = "Invalid sale status should fail validation")]
    public void Given_InvalidSaleStatus_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var sale = SaleTestData.GenerateInvalidSale();

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Status)
            .WithErrorMessage("Sale status cannot be None.");
    }

    [Fact(DisplayName = "Empty sale branch should fail validation")]
    public void Given_EmptySaleBranch_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var sale = SaleTestData.GenerateInvalidSale();
        sale.Branch = default!;

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Branch)
            .WithErrorMessage("Branch cannot be empty.");
    }

    [Fact(DisplayName = "Invalid sale branch should fail validation")]
    public void Given_InvalidSaleBranch_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        sale.Branch = BranchTestData.GenerateInvalidBranch();

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveAnyValidationError();
    }

    [Fact(DisplayName = "Empty sale customer should fail validation")]
    public void Given_EmptySaleCustomer_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var sale = SaleTestData.GenerateInvalidSale();
        sale.Customer = default!;

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Customer)
            .WithErrorMessage("Customer cannot be empty.");
    }

    [Fact(DisplayName = "Invalid sale customer should fail validation")]
    public void Given_InvalidSaleCustomer_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        sale.Customer = CustomerTestData.GenerateInvalidCustomer();

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveAnyValidationError();
    }
}
