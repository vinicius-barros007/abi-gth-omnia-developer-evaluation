using Ambev.DeveloperEvaluation.Domain.Enums.Identity;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.WebApi.Features.Users;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using Bogus;

namespace Ambev.DeveloperEvaluation.Functional.Features.Users.TestData
{
    internal static class CreateUserRequestTestData
    {
        private static readonly Faker<GeoLocation> GeoLocationFaker = new Faker<GeoLocation>()
            .RuleFor(g => g.Latitude, f => (decimal)f.Address.Latitude())
            .RuleFor(g => g.Longitude, f => (decimal)f.Address.Longitude());

        private static readonly Faker<Address> AddressFaker = new Faker<Address>()
            .RuleFor(g => g.City, f => f.Address.City())
            .RuleFor(g => g.Street, f => f.Address.StreetName())
            .RuleFor(g => g.Number, f => f.Random.Int(1, 200))
            .RuleFor(g => g.ZipCode, f => f.Address.ZipCode())
            .RuleFor(g => g.GeoLocation, GeoLocationFaker.Generate());

        private static readonly Faker<PersonName> PersonFaker = new Faker<PersonName>()
            .RuleFor(p => p.FirstName, f => f.Name.FirstName())
            .RuleFor(p => p.LastName, f => f.Name.LastName());

        private static readonly Faker<CreateUserRequest> UserFaker = new Faker<CreateUserRequest>()
            .RuleFor(u => u.Name, PersonFaker.Generate())
            .RuleFor(u => u.Address, AddressFaker.Generate())
            .RuleFor(u => u.Username, f => f.Internet.UserName())
            .RuleFor(u => u.Password, f => $"Test@{f.Random.Number(100, 999)}")
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}")
            .RuleFor(u => u.Status, f => f.PickRandom(UserStatus.Active, UserStatus.Suspended))
            .RuleFor(u => u.Role, f => f.PickRandom(UserRole.Customer, UserRole.Admin));

        public static CreateUserRequest GenerateValidRequest() => UserFaker.Generate();
    }
}
