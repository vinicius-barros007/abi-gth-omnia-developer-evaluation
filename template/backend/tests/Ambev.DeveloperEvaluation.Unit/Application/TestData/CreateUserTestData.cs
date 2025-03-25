using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Enums.Identity;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public static class CreateUserTestData
    {
        private static readonly Faker<CreateUserCommand> CreateUserCommandFaker = new Faker<CreateUserCommand>()
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.City, f => f.Address.City())
            .RuleFor(u => u.Street, f => f.Address.StreetName())
            .RuleFor(u => u.Number, f => f.Random.Int(1, 200))
            .RuleFor(u => u.ZipCode, f => f.Address.ZipCode("00000-000"))
            .RuleFor(u => u.Latitude, f => (decimal)f.Address.Latitude())
            .RuleFor(u => u.Longitude, f => (decimal)f.Address.Longitude())
            .RuleFor(u => u.Username, f => f.Internet.UserName())
            .RuleFor(u => u.Password, f => $"Test@{f.Random.Number(100, 999)}")
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}")
            .RuleFor(u => u.Status, f => f.PickRandom(UserStatus.Active, UserStatus.Suspended))
            .RuleFor(u => u.Role, f => f.PickRandom(UserRole.Customer, UserRole.Admin));

        public static CreateUserCommand GenerateValidCreateUserCommand()
        {
            return CreateUserCommandFaker.Generate();
        }
    }
}
