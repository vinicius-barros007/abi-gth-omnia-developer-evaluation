namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public sealed class CreateSaleResponse
{
    public Guid Id { get; set; }

    public long SaleNumber { get; set; }

    public DateTime CreatedAt { get; set; }
}
