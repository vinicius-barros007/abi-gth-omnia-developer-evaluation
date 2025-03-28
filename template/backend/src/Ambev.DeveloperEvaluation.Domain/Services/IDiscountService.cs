using Ambev.DeveloperEvaluation.Domain.Entities.Sales;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public interface IDiscountService
{
    decimal CalculateDiscount(SaleItem item);
}