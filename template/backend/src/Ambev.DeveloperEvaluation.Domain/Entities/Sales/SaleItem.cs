using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using Ambev.DeveloperEvaluation.Domain.Validation.Sales;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Sales;

public class SaleItem : BaseEntity
{
    public Guid SaleId { get; set; }

    public Sale Sale { get; set; } = default!;

    public Guid ProductId { get; set; }

    public Product Product { get; set; } = default!;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Discount { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new SaleItemValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }


    //public decimal TotalAmount { get; set; }

    //internal static decimal CalculateTotal(decimal unitPrice, int quantity, decimal discount)
    //    => (unitPrice * quantity) - discount;

    //internal static decimal CalculateDiscount(int quantity, decimal unitPrice)
    //{
    //    decimal discountRate = 0;

    //    //It's not possible to sell above 20 identical items
    //    if (quantity > 20)
    //        throw new InvalidOperationException("Cannot sell more than 20 identical items.");

    //    //Purchases between 10 and 20 identical items have a 20% discount
    //    if (quantity >= 10 && quantity <= 20)
    //        discountRate = 0.20m;

    //    // Purchases above 4 identical items have a 10% discount
    //    else if(quantity >= 4 && quantity < 10) 
    //        discountRate = 0.10m;

    //    return unitPrice * quantity * discountRate;
    //}

    //public static SaleItem Create(Guid saleId, Guid productId, int quantity, decimal unitPrice)
    //{
    //    decimal discount = CalculateDiscount(quantity, unitPrice);  
    //    decimal total = CalculateTotal(unitPrice, quantity, discount);

    //    return new SaleItem(
    //        Guid.Empty,
    //        saleId,
    //        productId,             
    //        quantity,             
    //        unitPrice,            
    //        total,
    //        discount
    //    );
    //}
}