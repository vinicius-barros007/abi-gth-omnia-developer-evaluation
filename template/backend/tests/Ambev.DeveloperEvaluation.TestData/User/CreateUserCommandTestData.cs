using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Bogus;
using Ambev.DeveloperEvaluation.TestData.Extensions;

namespace Ambev.DeveloperEvaluation.TestData.User;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateUserCommandTestData
{
    /// <summary>
    /// Configures the Faker to generate valid User entities.
    /// The generated users will have valid:
    /// - Username (using internet usernames)
    /// - Password (meeting complexity requirements)
    /// - Email (valid format)
    /// - Phone (Brazilian format)
    /// - Status (Active or Suspended)
    /// - Role (Customer or Admin)
    /// </summary>
    private static readonly Faker<CreateUserCommand> createUserCommandFaker = new Faker<CreateUserCommand>()
        .RuleFor(u => u.FirstName, f => f.Name.FirstName())
        .RuleFor(u => u.LastName, f => f.Name.LastName())
        .RuleFor(u => u.City, f => f.Address.City())
        .RuleFor(u => u.Street, f => f.Address.StreetName())
        .RuleFor(u => u.Number, f => f.Random.Int(1, 200))
        .RuleFor(u => u.ZipCode, f => f.GenerateBrazilianZipCode())
        .RuleFor(u => u.Latitude, f => (decimal)f.Address.Latitude())
        .RuleFor(u => u.Longitude, f => (decimal)f.Address.Longitude())
        .RuleFor(u => u.Username, f => f.Internet.UserName())
        .RuleFor(u => u.Password, f => $"Test@{f.Random.Number(100, 999)}")
        .RuleFor(u => u.Email, f => f.Internet.Email())
        .RuleFor(u => u.Phone, f => f.GenerateBrazilianPhoneNumber())
        .RuleFor(u => u.Status, f => f.GenerateRandomUserStatus())
        .RuleFor(u => u.Role, f => f.GenerateRandomUserRole());

    /// <summary>
    /// Generates a valid User entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid User entity with randomly generated data.</returns>
    public static CreateUserCommand GenerateValidCommand()
    {
        return createUserCommandFaker.Generate();
    }
}
