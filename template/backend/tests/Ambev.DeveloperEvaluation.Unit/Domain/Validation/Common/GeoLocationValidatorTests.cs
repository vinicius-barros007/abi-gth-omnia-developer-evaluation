using Ambev.DeveloperEvaluation.Domain.Validation.Common;
using Ambev.DeveloperEvaluation.TestData.User;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation.Common;

/// <summary>
/// Contains unit tests for the GeoLocationValidator class.
/// </summary>
public class GeoLocationValidatorTests
{
    private readonly GeoLocationValidator _validator;

    public GeoLocationValidatorTests()
    {
        _validator = new GeoLocationValidator();
    }

    /// <summary>
    /// Validates that a valid geo location range should pass validation.
    /// </summary>
    [Fact(DisplayName = "Valid geo location range should pass validation")]
    public void Given_ValidGeoLocation_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var geo = PersonTestData.GenerateValidGeoLocation();

        // Act
        var result = _validator.TestValidate(geo);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }


    /// <summary>
    /// Validates that an invalid latitude range shouldn't pass validation.
    /// </summary>
    [Fact(DisplayName = "Invalid latitude range shouldn't pass validation")]
    public void Given_InvalidLatitude_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var geo = PersonTestData.GenerateInvalidGeoLocation();

        // Act
        var result = _validator.TestValidate(geo);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Latitude)
            .WithErrorMessage("The latitude of the location. May range from -90.0 to 90.0.");
    }

    /// <summary>
    /// Validates that an invalid longitude range shouldn't pass validation.
    /// </summary>
    [Fact(DisplayName = "Invalid longitude range shouldn't pass validation")]
    public void Given_InvalidLongitude_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var geo = PersonTestData.GenerateInvalidGeoLocation();

        // Act
        var result = _validator.TestValidate(geo);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Longitude)
            .WithErrorMessage("The longitude of the location. May range from -180.0 to 180.0.");
    }
}
