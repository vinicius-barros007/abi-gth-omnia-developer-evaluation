using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation.Identity;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Identity;

/// <summary>
/// Represents a person in the application, a person is associated to a user.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Person : BaseEntity
{
    /// <summary>
    /// Initializes a new instance of the Person class.
    /// </summary>
    public Person()
    {
        CreatedAt = DateTime.UtcNow;
        Address = default!;
        User = default!;
    }

    /// <summary>
    /// Gets the user id.
    /// </summary>
    public Guid UserId { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets the user associated to the person.
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Gets the person's first name.
    /// Must not be null or empty.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets the person's last name.
    /// Must not be null or empty.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets the person's address.
    /// Must not be null.
    /// </summary>
    public Address Address { get; set; }

    /// <summary>
    /// Gets the date and time when the person was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets the date and time of the last update to the person's information.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Performs validation of the person entity using the PersonValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">First name is required and length</list>
    /// <list type="bullet">Last name is required and length</list>
    /// <list type="bullet">City is required and length</list>
    /// <list type="bullet">Street is required and length</list>
    /// <list type="bullet">Number is required</list>
    /// <list type="bullet">Zip code is required</list>
    /// <list type="bullet">Latitude is valid when filled</list>
    /// <list type="bullet">Longitude is valid when filled</list>
    /// </remarks>
    public ValidationResultDetail Validate()
    {
        var validator = new PersonValidator();
        var result = validator.Validate(this);

        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}