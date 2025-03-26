using Ambev.DeveloperEvaluation.Domain.Enums.Identity;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using Bogus;

namespace Ambev.DeveloperEvaluation.Functional.Features.Users.TestData
{
    internal static class AuthenticateUserRequestTestData
    {
        private static readonly Faker<AuthenticateUserRequest> AuthenticateUserRequestFaker = new Faker<AuthenticateUserRequest>()
            .RuleFor(u => u.Password, f => $"Test@{f.Random.Number(100, 999)}")
            .RuleFor(u => u.Email, f => f.Internet.Email());

        public static AuthenticateUserRequest GenerateValidRequest() => AuthenticateUserRequestFaker.Generate();
    }
}
