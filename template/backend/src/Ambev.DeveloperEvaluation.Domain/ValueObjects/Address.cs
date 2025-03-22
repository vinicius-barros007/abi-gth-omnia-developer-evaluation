using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public class Address : ValueObject
{
    public Address(
        string street,
        int number, 
        string city, 
        string state, 
        string country, 
        string zipCode
    )
    {
        Street = street;
        Number = number;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
    }

    public string City { get; private set; }

    public string Street { get; private set; }

    public int Number { get; private set; }

    public string ZipCode { get; private set; }
        
    public string State { get; private set; }

    public string Country { get; private set; }
    
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Street;
        yield return Number;
        yield return City;
        yield return State;
        yield return Country;
        yield return ZipCode;
    }
}