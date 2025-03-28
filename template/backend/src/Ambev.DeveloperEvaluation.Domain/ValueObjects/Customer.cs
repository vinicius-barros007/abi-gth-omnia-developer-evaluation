using Ambev.DeveloperEvaluation.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

/// <summary>
/// Represents an external reference to a customer.
/// </summary>
[Owned]
public class Customer : ValueObject
{
    /// <summary>
    /// Gets or sets the id of the customer.
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or sets the name of the customer.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Retrieves the atomic values of the customer.
    /// </summary>
    /// <returns>An enumerable of the atomic values.</returns>
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Id;
        yield return Name;
    }
}
