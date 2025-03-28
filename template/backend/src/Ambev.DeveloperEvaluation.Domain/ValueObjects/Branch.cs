using Ambev.DeveloperEvaluation.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

/// <summary>
/// Represents an external reference to a branch.
/// </summary>
[Owned]
public class Branch : ValueObject
{
    /// <summary>
    /// Gets or sets the id of the branch.
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or sets the name of the branch.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Retrieves the atomic values of the branch.
    /// </summary>
    /// <returns>An enumerable of the atomic values.</returns>
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Id;
        yield return Name;
    }
}
