using Ambev.DeveloperEvaluation.Domain.Validation.Common;
using Ambev.DeveloperEvaluation.TestData.Users;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation.Common;

/// <summary>
/// Unit tests for the AddressValidator class.
/// </summary>
public class AddressValidatorTests
{
    private readonly AddressValidator _validator;

    public AddressValidatorTests()
    {
        _validator = new AddressValidator();
    }

    /// <summary>
    /// Valid address should pass validation.
    /// </summary>
    [Fact(DisplayName = "Valid address should pass validation")]
    public void Given_ValidAddress_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var address = PersonTestData.GenerateValidAddress();

        // Act
        var result = _validator.TestValidate(address);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Valid address without geo location should pass validation.
    /// </summary>
    [Fact(DisplayName = "Valid address without geo location should pass validation")]
    public void Given_ValidAddress_And_Missing_GeoLocation_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var address = PersonTestData.GenerateValidAddress();
        address.GeoLocation = default!;

        // Act
        var result = _validator.TestValidate(address);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Empty city should fail validation.
    /// </summary>
    [Fact(DisplayName = "Empty city should fail validation")]
    public void Given_EmptyEmail_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var address = PersonTestData.GenerateValidAddress();
        address.City = string.Empty;

        // Act
        var result = _validator.TestValidate(address);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.City)
            .WithErrorMessage("City cannot be empty.");
    }

    /// <summary>
    /// City exceed max length should fail validation.
    /// </summary>
    [Fact(DisplayName = "City exceed max length should fail validation")]
    public void Given_CityExceedMaxLength_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var address = PersonTestData.GenerateInvalidAddress();

        // Act
        var result = _validator.TestValidate(address);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.City)
            .WithErrorMessage("City cannot be longer than 50 characters.");
    }

    /// <summary>
    /// Empty street should fail validation.
    /// </summary>
    [Fact(DisplayName = "Empty street should fail validation")]
    public void Given_EmptyStreet_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var address = PersonTestData.GenerateValidAddress();
        address.Street = string.Empty;

        // Act
        var result = _validator.TestValidate(address);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Street)
            .WithErrorMessage("Street cannot be empty.");
    }

    /// <summary>
    /// Street exceed max length should fail validation.
    /// </summary>
    [Fact(DisplayName = "Street exceed max length should fail validation")]
    public void Given_StreetExceedMaxLength_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var address = PersonTestData.GenerateInvalidAddress();

        // Act
        var result = _validator.TestValidate(address);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Street)
            .WithErrorMessage("Street cannot be longer than 100 characters.");
    }

    /// <summary>
    /// Number lower or equal zero should fail validation.
    /// </summary>
    [Fact(DisplayName = "Number lower or equal zero should fail validation")]
    public void Given_NumberLowerThanZero_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var address = PersonTestData.GenerateInvalidAddress();
        address.Number = 0;

        // Act
        var result = _validator.TestValidate(address);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Number)
            .WithErrorMessage("Address should be greater than zero.");
    }

    /// <summary>
    /// Empty zip code should fail validation.
    /// </summary>
    [Fact(DisplayName = "Empty zip code should fail validation")]
    public void Given_EmptyZipCode_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var address = PersonTestData.GenerateValidAddress();
        address.ZipCode = string.Empty;

        // Act
        var result = _validator.TestValidate(address);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ZipCode)
            .WithErrorMessage("Zip code cannot be empty.");
    }

    /// <summary>
    /// Zip code exceed max length should fail validation.
    /// </summary>
    [Fact(DisplayName = "Zip code exceed max length should fail validation")]
    public void Given_ZipCodeExceedMaxLength_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var address = PersonTestData.GenerateInvalidAddress();

        // Act
        var result = _validator.TestValidate(address);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ZipCode)
            .WithErrorMessage("Zip code cannot be longer than 10 characters.");
    }
}
