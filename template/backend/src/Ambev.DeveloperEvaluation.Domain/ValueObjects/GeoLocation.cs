using Ambev.DeveloperEvaluation.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

[Owned]
public class GeoLocation : ValueObject
{
    public GeoLocation() { }

    public GeoLocation(decimal latitude, decimal longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    [JsonPropertyName("lat")]
    public decimal Latitude { get; set; }

    [JsonPropertyName("long")]
    public decimal Longitude { get; set; }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Latitude;
        yield return Longitude;
    }
}   