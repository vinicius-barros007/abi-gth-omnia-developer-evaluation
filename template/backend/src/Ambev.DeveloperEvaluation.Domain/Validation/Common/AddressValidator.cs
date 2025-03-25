using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Common;

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(address => address.City)
            .NotEmpty()
            .WithMessage("City cannot be empty.")
            .MaximumLength(50)
            .WithMessage("City cannot be longer than 50 characters.");

        RuleFor(address => address.Street)
            .NotEmpty()
            .WithMessage("Street cannot be empty.")
            .MaximumLength(100)
            .WithMessage("Street cannot be longer than 100 characters.");

        RuleFor(address => address.Number)
            .GreaterThan(0)
            .WithMessage("Address should be greater than zero.");

        RuleFor(address => address.ZipCode)
            .NotEmpty()
            .WithMessage("Zip code cannot be empty.")
            .MaximumLength(10)
            .WithMessage("Zip code cannot be longer than 10 characters.");

        RuleFor(address => address.GeoLocation)
            .SetValidator(new GeoLocationValidator());
    }
}
