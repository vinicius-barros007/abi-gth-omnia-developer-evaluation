using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Common;

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(customer => customer.Id)
            .NotEmpty()
            .WithMessage("Customer id cannot be empty.");

        RuleFor(customer => customer.Name)
            .NotEmpty()
            .WithMessage("Customer name cannot be empty.")
            .MinimumLength(2)
            .WithMessage("Customer name must be at least 2 characters long.")
            .MaximumLength(180)
            .WithMessage("Customer name cannot be longer than 180 characters.");
    }
}
