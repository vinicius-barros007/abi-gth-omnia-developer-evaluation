using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Bogus;

namespace Ambev.DeveloperEvaluation.TestData.User;

public static class AuthenticateUserCommandTestData
{
    private static readonly Faker<AuthenticateUserCommand> authenticateUserCommandFaker = new Faker<AuthenticateUserCommand>()
        .RuleFor(u => u.Email, f => f.Internet.UserName())
        .RuleFor(u => u.Password, f => f.Internet.Password());

    public static AuthenticateUserCommand GenerateValidCommand()
    {
        return authenticateUserCommandFaker.Generate();
    }
}
