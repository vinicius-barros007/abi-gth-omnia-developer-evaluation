using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Validation.Common;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator(IProductRepository repository)
    {
        RuleFor(sale => sale.Branch)
            .NotNull()
            .SetValidator(new BranchValidator());

        RuleFor(sale => sale.Customer)
            .NotNull()
            .SetValidator(new CustomerValidator());

        RuleFor(sale => sale.Items)
            .NotEmpty()
            .ForEach(item =>
            {
                item.NotEmpty()
                    .SetValidator(new CreateSaleItemValidator(repository));
            });
    }
}
public class CreateSaleItemValidator : AbstractValidator<CreateSaleItemCommand>
{
    private readonly IProductRepository _repository;

    public CreateSaleItemValidator(IProductRepository repository)
    {
        _repository = repository;

        RuleFor(item => item.ProductId)
            .NotEmpty()
            .MustAsync(IsProductIdValid)
            .WithMessage((x, id) => $"ProductId {id} doesn't exist.");

        RuleFor(item => item.Quantity)
            .GreaterThan(0)
            .LessThanOrEqualTo(20);
    }

    public async Task<bool> IsProductIdValid(Guid productId, CancellationToken cancellationToken)
    {
        var exists = await _repository.GetByIdAsync(productId, cancellationToken);
        if (exists is null)
            return false;

        return true;
    }
}