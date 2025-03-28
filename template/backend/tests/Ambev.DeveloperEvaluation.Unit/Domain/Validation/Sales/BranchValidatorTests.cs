using Ambev.DeveloperEvaluation.Domain.Validation.Common;
using Ambev.DeveloperEvaluation.TestData.Sales;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation.Products;

public class BranchValidatorTests
{
    private readonly BranchValidator _validator;

    public BranchValidatorTests()
    {
        _validator = new BranchValidator();
    }

    [Fact(DisplayName = "Valid branch should pass all validation rules")]
    public void Given_ValidBranch_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var branch = BranchTestData.GenerateValidBranch();

        // Act
        var result = _validator.TestValidate(branch);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "Invalid branch id should fail validation")]
    public void Given_InvalidBranchId_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var branch = BranchTestData.GenerateInvalidBranch();

        // Act
        var result = _validator.TestValidate(branch);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id)
            .WithErrorMessage("Branch id cannot be empty.");
    }

    [Theory(DisplayName = "Invalid branch name formats should fail validation")]
    [InlineData("")] // Empty
    [InlineData("a")] // Less than 3 characters
    public void Given_InvalidBranchName_When_Validated_Then_ShouldHaveError(string name)
    {
        // Arrange
        var branch = BranchTestData.GenerateValidBranch();
        branch.Name = name;

        // Act
        var result = _validator.TestValidate(branch);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact(DisplayName = "Branch name longer than maximum length should fail validation")]
    public void Given_BranchNameLongerThanMaximum_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var branch = BranchTestData.GenerateInvalidBranch();

        // Act
        var result = _validator.TestValidate(branch);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("Branch name cannot be longer than 70 characters.");
    }
}
