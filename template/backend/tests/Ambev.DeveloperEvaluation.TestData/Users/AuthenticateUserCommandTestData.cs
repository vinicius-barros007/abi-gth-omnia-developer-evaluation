using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Bogus;

namespace Ambev.DeveloperEvaluation.TestData.Users;

public static class AuthenticateUserCommandTestData
{
    public static AuthenticateUserCommand GenerateValidCommand()
    {
        Faker<AuthenticateUserCommand> faker = new Faker<AuthenticateUserCommand>()
                .RuleFor(u => u.Email, f => f.Internet.UserName())
                .RuleFor(u => u.Password, f => f.Internet.Password());

        return faker.Generate();
    }
}
