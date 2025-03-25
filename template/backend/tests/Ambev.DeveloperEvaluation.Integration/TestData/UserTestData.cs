using Ambev.DeveloperEvaluation.Domain.Entities.Identity;
using Ambev.DeveloperEvaluation.Domain.Enums.Identity;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Integration.TestData;

public static class UserTestData
{
    private static readonly Faker<GeoLocation> GeoLocationFaker = new Faker<GeoLocation>()
        .RuleFor(g => g.Latitude, f => (decimal)f.Address.Latitude())
        .RuleFor(g => g.Longitude, f => (decimal)f.Address.Longitude());

    private static readonly Faker<Address> AddressFaker = new Faker<Address>()
        .RuleFor(g => g.City, f => f.Address.City())
        .RuleFor(g => g.Street, f => f.Address.StreetName())
        .RuleFor(g => g.Number, f => f.Random.Int(1, 200))
        .RuleFor(g => g.ZipCode, f => f.Address.ZipCode("00000-000"))
        .RuleFor(g => g.GeoLocation, GeoLocationFaker.Generate());

    private static readonly Faker<DeveloperEvaluation.Domain.Entities.Identity.Person> PersonFaker = new Faker<DeveloperEvaluation.Domain.Entities.Identity.Person>()
        .RuleFor(p => p.FirstName, f => f.Name.FirstName())
        .RuleFor(p => p.LastName, f => f.Name.LastName())
        .RuleFor(p => p.Address, AddressFaker.Generate());

    private static readonly Faker<User> UserFaker = new Faker<User>()
        .RuleFor(u => u.Person, PersonFaker.Generate())
        .RuleFor(u => u.Username, f => f.Internet.UserName())
        .RuleFor(u => u.Password, f => $"Test@{f.Random.Number(100, 999)}")
        .RuleFor(u => u.Email, f => f.Internet.Email())
        .RuleFor(u => u.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}")
        .RuleFor(u => u.Status, f => f.PickRandom(UserStatus.Active, UserStatus.Suspended))
        .RuleFor(u => u.Role, f => f.PickRandom(UserRole.Customer, UserRole.Admin));

    public static User GenerateValidUser() => UserFaker.Generate();

    public static GeoLocation GenerateValidGeoLocation() => GeoLocationFaker.Generate();

    public static GeoLocation GenerateInvalidGeoLocation()
    {
        Faker faker = new();
        decimal lat = (decimal)faker.Address.Latitude(-180, -100);
        decimal lon = (decimal)faker.Address.Longitude(-360, -200);
        return new GeoLocation(lat, lon);
    }

    public static Address GenerateValidAddress() => AddressFaker.Generate();

    public static Address GenerateInvalidAddress()
    {
        Faker faker = new();
        return new Address
        {
            City = faker.Address.City().PadLeft(51, '*'),
            Street = faker.Address.StreetName().PadLeft(101, '*'),
            Number = 0,
            ZipCode = faker.Address.ZipCode().PadLeft(11, '*'),
            GeoLocation = default!
        };
    }

    public static Domain.Entities.Identity.Person GenerateValidPerson() => PersonFaker.Generate();

    public static Domain.Entities.Identity.Person GenerateInvalidPerson()
    {
        Faker faker = new();
        return new Domain.Entities.Identity.Person
        {
            FirstName = faker.Name.FirstName().PadLeft(51, '*'),
            LastName = faker.Name.LastName().PadLeft(71, '*'),
            Address = GenerateInvalidAddress(),
            CreatedAt = faker.Date.Past(),
            UpdatedAt = DateTime.Now
        };
    }
}
