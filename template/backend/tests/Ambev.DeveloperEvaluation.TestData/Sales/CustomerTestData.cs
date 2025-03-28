using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.TestData.Sales;

public static class CustomerTestData
{
    public static Customer GenerateValidCustomer()
    {
        Faker<Customer> faker = new Faker<Customer>()
            .RuleFor(c => c.Id, Guid.NewGuid())
            .RuleFor(c => c.Name, f => f.Person.FullName);

        return faker.Generate();
    }

    public static Customer GenerateInvalidCustomer()
    {
        Customer item = GenerateValidCustomer();
        item.Id = Guid.Empty;
        item.Name = new string('*', 200);
        return item;
    }
}
