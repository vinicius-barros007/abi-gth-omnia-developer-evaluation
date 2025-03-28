using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.TestData.Sales;

public static class BranchTestData
{
    public static Branch GenerateValidBranch()
    {
        Faker<Branch> faker = new Faker<Branch>()
            .RuleFor(c => c.Id, Guid.NewGuid())
            .RuleFor(c => c.Name, f => f.Finance.AccountName());

        return faker.Generate();
    }

    public static Branch GenerateInvalidBranch()
    {
        Branch item = GenerateValidBranch();
        item.Id = Guid.Empty;
        item.Name = new string('*', 80);
        return item;
    }
}
