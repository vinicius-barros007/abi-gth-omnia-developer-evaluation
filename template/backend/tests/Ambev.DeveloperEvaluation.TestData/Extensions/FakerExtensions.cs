using Ambev.DeveloperEvaluation.Domain.Enums.Identity;
using Ambev.DeveloperEvaluation.Domain.Enums.Sales;
using Bogus;

namespace Ambev.DeveloperEvaluation.TestData.Extensions
{
    internal static class FakerExtensions
    {
        public static string GenerateBrazilianPhoneNumber(this Faker faker)
            => $"+55{faker.Random.Number(11, 99)}{faker.Random.Number(100000000, 999999999)}";

        public static string GenerateBrazilianZipCode(this Faker faker)
            => faker.Address.ZipCode("#####-###");

        public static UserStatus GenerateRandomUserStatus(this Faker faker)
            => faker.PickRandom(UserStatus.Active, UserStatus.Suspended);

        public static UserRole GenerateRandomUserRole(this Faker faker)
            => faker.PickRandom(UserRole.Customer, UserRole.Admin);

        public static SaleStatus GenerateRandomSaleStatus(this Faker faker)
            => faker.PickRandom(SaleStatus.Pending, SaleStatus.Cancelled);
    }
}
