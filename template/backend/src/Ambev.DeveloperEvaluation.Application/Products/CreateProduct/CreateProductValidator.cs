using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IProductCategoryRepository _repository;

    public CreateProductCommandValidator(IProductCategoryRepository repository)
    {
        _repository = repository;

        RuleFor(product => product.Title)
            .NotEmpty()
            .Length(2, 100);

        RuleFor(product => product.Description)
            .NotEmpty()
            .Length(3, 500);

        RuleFor(product => product.Image)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(product => product.CategoryId)
        .NotEmpty()
        .MustAsync(IsCategoryIdValid)
        .WithMessage((x, id) => $"CategoryId {id} doesn't exist.");

        RuleFor(product => product.Price)
            .GreaterThan(0);
    }

    public async Task<bool> IsCategoryIdValid(Guid categoryId, CancellationToken cancellationToken)
    {
        var exists = await _repository.GetByIdAsync(categoryId, cancellationToken);
        if (exists is null)
            return false;

        return true;
    }   
}