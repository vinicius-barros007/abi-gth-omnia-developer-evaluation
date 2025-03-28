using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    public DateOnly SaleDate { get; set; }

    public Branch Branch { get; set; } = default!;

    public Customer Customer { get; set; } = default!;

    public List<CreateSaleItemCommand> Items { get; set; } = [];
}

public class CreateSaleItemCommand
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }
}
