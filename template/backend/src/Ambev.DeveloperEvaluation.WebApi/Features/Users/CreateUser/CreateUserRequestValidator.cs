using Ambev.DeveloperEvaluation.Domain.Enums.Identity;
using Ambev.DeveloperEvaluation.Domain.Validation.Common;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// Validator for CreateUserRequest that defines validation rules for user creation.
/// </summary>
public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateUserRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Email: Must be valid format (using EmailValidator)
    /// - Username: Required, length between 3 and 50 characters
    /// - Password: Must meet security requirements (using PasswordValidator)
    /// - Phone: Must match international format (+X XXXXXXXXXX)
    /// - Status: Cannot be Unknown
    /// - Role: Cannot be None
    /// </remarks>
    public CreateUserRequestValidator()
    {
        RuleFor(user => user.Name.FirstName).NotEmpty().Length(2, 50);
        RuleFor(user => user.Name.LastName).NotEmpty().Length(2, 70);

        RuleFor(user => user.Address).NotNull();
        RuleFor(user => user.Address.City).NotEmpty().MaximumLength(50);
        RuleFor(user => user.Address.Street).NotEmpty().MaximumLength(100);
        RuleFor(user => user.Address.Number).GreaterThan(0);
        RuleFor(user => user.Address.ZipCode).NotEmpty().MaximumLength(10);

        RuleFor(user => user.Address.GeoLocation).NotNull();
        RuleFor(user => user.Address.GeoLocation.Latitude)
            .InclusiveBetween(-90.0m, 90.0m)
            .WithMessage("The latitude of the location. May range from -90.0 to 90.0.");

        RuleFor(user => user.Address.GeoLocation.Longitude)
            .InclusiveBetween(-180.0m, 180.0m)
            .WithMessage("The longitude of the location. May range from -180.0 to 180.0.");

        RuleFor(user => user.Email).SetValidator(new EmailValidator());
        RuleFor(user => user.Username).NotEmpty().Length(3, 50);
        RuleFor(user => user.Password).SetValidator(new PasswordValidator());
        RuleFor(user => user.Phone).Matches(@"^\+?[1-9]\d{1,14}$");
        RuleFor(user => user.Status).NotEqual(UserStatus.Unknown);
        RuleFor(user => user.Role).NotEqual(UserRole.None);        
    }
}