using Ambev.DeveloperEvaluation.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

/// <summary>
/// Represents an address value object.
/// </summary>
[Owned]
public class Address : ValueObject
{
    /// <summary>
    /// Gets or sets the city of the address.
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the street of the address.
    /// </summary>
    public string Street { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the number of the address.
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// Gets or sets the zip code of the address.
    /// </summary>
    [JsonPropertyName("zipcode")]
    public string ZipCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the geolocation of the address.
    /// </summary>
    [JsonPropertyName("geolocation")]
    public GeoLocation GeoLocation { get; set; } = default!;

    /// <summary>
    /// Retrieves the atomic values of the address.
    /// </summary>
    /// <returns>An enumerable of the atomic values.</returns>
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Street;
        yield return Number;
        yield return City;
        yield return ZipCode;
        yield return GeoLocation;
    }
}
