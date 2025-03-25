using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users;

public class PersonName
{
    [JsonPropertyName("firstname")]
    public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("lastname")]
    public string LastName { get; set; } = string.Empty;
}