using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.TestData.User;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class PersonTestData
{
    private static readonly Faker<GeoLocation> GeoLocationFaker = new Faker<GeoLocation>()
        .RuleFor(g => g.Latitude, f => (decimal)f.Address.Latitude())
        .RuleFor(g => g.Longitude, f => (decimal)f.Address.Longitude());

    private static readonly Faker<Address> AddressFaker = new Faker<Address>()
        .RuleFor(g => g.City, f => f.Address.City())
        .RuleFor(g => g.Street, f => f.Address.StreetName())
        .RuleFor(g => g.Number, f => f.Random.Int(1, 200))
        .RuleFor(g => g.ZipCode, f => f.Address.ZipCode("#####-###"))
        .RuleFor(g => g.GeoLocation, GeoLocationFaker.Generate());

    private static readonly Faker<Domain.Entities.Identity.Person> PersonFaker = new Faker<Domain.Entities.Identity.Person>()
        .RuleFor(p => p.FirstName, f => f.Name.FirstName())
        .RuleFor(p => p.LastName, f => f.Name.LastName())
        .RuleFor(p => p.Address, AddressFaker.Generate());

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
