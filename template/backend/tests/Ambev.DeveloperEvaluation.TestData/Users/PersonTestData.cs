using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.TestData.Users;

public static class PersonTestData
{
    public static GeoLocation GenerateValidGeoLocation()
    {
        Faker<GeoLocation> faker = new Faker<GeoLocation>()
            .RuleFor(g => g.Latitude, f => (decimal)f.Address.Latitude())
            .RuleFor(g => g.Longitude, f => (decimal)f.Address.Longitude());

        return faker.Generate();
    }

    public static GeoLocation GenerateInvalidGeoLocation()
    {
        Faker faker = new();
        decimal lat = (decimal)faker.Address.Latitude(-180, -100);
        decimal lon = (decimal)faker.Address.Longitude(-360, -200);
        return new GeoLocation(lat, lon);
    }

    public static Address GenerateValidAddress()
    {
        Faker<Address> faker = new Faker<Address>()
            .RuleFor(g => g.City, f => f.Address.City())
            .RuleFor(g => g.Street, f => f.Address.StreetName())
            .RuleFor(g => g.Number, f => f.Random.Int(1, 200))
            .RuleFor(g => g.ZipCode, f => f.Address.ZipCode("#####-###"))
            .RuleFor(g => g.GeoLocation, GenerateValidGeoLocation());

        return faker.Generate();
    }

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

    public static Domain.Entities.Identity.Person GenerateValidPerson()
    {
        Faker<Domain.Entities.Identity.Person> faker = new Faker<Domain.Entities.Identity.Person>()
            .RuleFor(p => p.FirstName, f => f.Name.FirstName())
            .RuleFor(p => p.LastName, f => f.Name.LastName())
            .RuleFor(p => p.Address, GenerateValidAddress());

        return faker.Generate();    
    }

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
