using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequest
{
    public DateTime SaleDate { get; set; }

    public Branch Branch { get; set; } = default!;

    public Customer Customer { get; set; } = default!;

    public List<CreateSaleItem> Items { get; set; } = [];
}

public class CreateSaleItem
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }
}