using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Common;

public class BranchValidator : AbstractValidator<Branch>
{
    public BranchValidator()
    {
        RuleFor(branch => branch.Id)
            .NotEmpty()
            .WithMessage("Branch id cannot be empty.");

        RuleFor(branch => branch.Name)
            .NotEmpty()
            .WithMessage("Branch name cannot be empty.")
            .MinimumLength(2)
            .WithMessage("Branch name must be at least 2 characters long.")
            .MaximumLength(70)
            .WithMessage("Branch name cannot be longer than 70 characters.");
    }
}
