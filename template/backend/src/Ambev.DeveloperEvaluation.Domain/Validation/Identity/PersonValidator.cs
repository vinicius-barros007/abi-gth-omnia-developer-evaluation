using Ambev.DeveloperEvaluation.Domain.Entities.Identity;
using Ambev.DeveloperEvaluation.Domain.Validation.Common;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Identity;

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(user => user.FirstName)
            .NotEmpty()
            .WithMessage("First name cannot be empty.")
            .MinimumLength(2)
            .WithMessage("First name must be at least 2 characters long.")
            .MaximumLength(50)
            .WithMessage("First name cannot be longer than 50 characters.");

        RuleFor(user => user.LastName)
            .NotEmpty()
            .WithMessage("Last name cannot be empty.")
            .MinimumLength(2)
            .WithMessage("Last name must be at least 2 characters long.")
            .MaximumLength(70)
            .WithMessage("Last name cannot be longer than 70 characters.");

        RuleFor(user => user.Address)
            .NotNull()
            .WithMessage("Address cannot be null.")
            .SetValidator(new AddressValidator());
    }
}
