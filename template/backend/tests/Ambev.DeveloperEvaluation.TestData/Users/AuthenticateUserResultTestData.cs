using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Bogus;
using Ambev.DeveloperEvaluation.TestData.Extensions;

namespace Ambev.DeveloperEvaluation.TestData.Users;

public static class AuthenticateUserResultTestData
{
    public static AuthenticateUserResult GenerateValidResult()
    {
        Faker<AuthenticateUserResult> faker = new Faker<AuthenticateUserResult>()
                .RuleFor(u => u.Id, f => Guid.NewGuid())
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.Phone, f => f.GenerateBrazilianPhoneNumber())
                .RuleFor(u => u.Role, f => f.GenerateRandomUserRole().ToString())
                .RuleFor(u => u.Email, f => f.Internet.UserName())
                .RuleFor(u => u.Token, f => f.Random.AlphaNumeric(36));

        return faker.Generate();
    }
}
