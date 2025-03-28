namespace Ambev.DeveloperEvaluation.Domain.Enums.Sales;

public enum SaleStatus
{
    None = 0,
    Pending, // The sale is created but not yet completed (e.g., waiting for payment).
    Processing, // The sale is being processed (e.g., payment verification, order fulfillment).
    Completed, // The sale has been successfully finalized and paid.
    Cancelled, // The sale was cancelled (e.g., customer changed their mind or payment failed).
    Refunded, // The sale was completed but later refunded to the customer.
    PartiallyRefunded, // Only some items in the sale were refunded.
    Failed, // The sale attempt failed (e.g., payment declined).
    OnHold  // The sale is temporarily paused (e.g., awaiting confirmation).
}
