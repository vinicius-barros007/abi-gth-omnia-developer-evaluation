using Ambev.DeveloperEvaluation.Domain.Validation.Identity;
using Ambev.DeveloperEvaluation.TestData.Users;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation.Identity;

/// <summary>
/// Unit tests for the <see cref="PersonValidator"/> class.
/// </summary>
public class PersonValidatorTests
{
    private readonly PersonValidator _validator;

    public PersonValidatorTests()
    {
        _validator = new PersonValidator();
    }

    /// <summary>
    /// Test case for validating a valid person.
    /// </summary>
    [Fact(DisplayName = "Valid person should pass validation")]
    public void Given_ValidPerson_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var person = PersonTestData.GenerateValidPerson();

        // Act
        var result = _validator.TestValidate(person);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Test case for validating a person with a missing address.
    /// </summary>
    [Fact(DisplayName = "Missing address should fail validation")]
    public void Given_ValidPerson_And_Missing_Address_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var person = PersonTestData.GenerateValidPerson();
        person.Address = default!;

        // Act
        var result = _validator.TestValidate(person);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Address)
            .WithErrorMessage("Address cannot be null.");
    }

    /// <summary>
    /// Test case for validating a person with an empty first name.
    /// </summary>
    [Fact(DisplayName = "Empty first name should fail validation")]
    public void Given_EmptyFirstName_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var person = PersonTestData.GenerateValidPerson();
        person.FirstName = string.Empty;

        // Act
        var result = _validator.TestValidate(person);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FirstName)
            .WithErrorMessage("First name cannot be empty.");
    }

    /// <summary>
    /// Test case for validating a person with a first name less than two characters.
    /// </summary>
    [Fact(DisplayName = "First name less than two chars should fail validation")]
    public void Given_FirstName_LessThanTwoChars_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var person = PersonTestData.GenerateValidPerson();
        person.FirstName = "a";

        // Act
        var result = _validator.TestValidate(person);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FirstName)
            .WithErrorMessage("First name must be at least 2 characters long.");
    }

    /// <summary>
    /// Test case for validating a person with a first name exceeding the maximum length.
    /// </summary>
    [Fact(DisplayName = "First name exceed maximum length should fail validation")]
    public void Given_FirstName_ExceedMaxLength_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var person = PersonTestData.GenerateInvalidPerson();

        // Act
        var result = _validator.TestValidate(person);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FirstName)
            .WithErrorMessage("First name cannot be longer than 50 characters.");
    }

    /// <summary>
    /// Test case for validating a person with an empty last name.
    /// </summary>
    [Fact(DisplayName = "Empty last name should fail validation")]
    public void Given_EmptyLastName_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var person = PersonTestData.GenerateValidPerson();
        person.LastName = string.Empty;

        // Act
        var result = _validator.TestValidate(person);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.LastName)
            .WithErrorMessage("Last name cannot be empty.");
    }

    /// <summary>
    /// Test case for validating a person with a last name less than two characters.
    /// </summary>
    [Fact(DisplayName = "Last name less than two chars should fail validation")]
    public void Given_LastName_LessThanTwoChars_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var person = PersonTestData.GenerateValidPerson();
        person.LastName = "a";

        // Act
        var result = _validator.TestValidate(person);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.LastName)
            .WithErrorMessage("Last name must be at least 2 characters long.");
    }

    /// <summary>
    /// Test case for validating a person with a last name exceeding the maximum length.
    /// </summary>
    [Fact(DisplayName = "Last name exceed maximum length should fail validation")]
    public void Given_LastName_ExceedMaxLength_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var person = PersonTestData.GenerateInvalidPerson();

        // Act
        var result = _validator.TestValidate(person);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.LastName)
            .WithErrorMessage("Last name cannot be longer than 70 characters.");
    }
}
