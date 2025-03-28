using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using Ambev.DeveloperEvaluation.Domain.Enums.Sales;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

public class GetSaleByIdResult
{
    public Guid Id { get; set; }

    public long SaleNumber { get; set; }

    public DateOnly SaleDate { get; set; }

    public IReadOnlyCollection<SaleItem> Items { get; set; } = [];

    public decimal TotalAmount => Items.Sum(o => o.TotalAmount);

    public Branch Branch { get; set; } = default!;

    public Customer Customer { get; set; } = default!;

    public SaleStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

public class SaleItem
{
    public Product Product { get; set; } = default!;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Discount { get; set; }

    public decimal TotalAmount => UnitPrice * Quantity - Discount;
}