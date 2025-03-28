using Ambev.DeveloperEvaluation.Application.Users.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.TestData.Products;
using AutoMapper;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Mappers;

public class CreateSaleProfileTests
{
    [Fact(DisplayName = "Assert CreateSaleProfile mapper is valid")]
    public void AutoMapper_Configuration_IsValid()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<CreateSaleProfile>());
        config.AssertConfigurationIsValid();
    }

    [Fact(DisplayName = "Map CreateSaleCommand to Sale is working properly")]
    public void Map_CreateProductCommand_To_Product_ShouldSucceed()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<CreateSaleProfile>());
        var mapper = config.CreateMapper();

        var command = CreateSaleCommandTestData.GenerateValidCommand();

        // Act 
        var sale = mapper.Map<Sale>(command);

        // Assert
        sale.Should().NotBeNull();
        sale.SaleDate.Should().Be(command.SaleDate);
        sale.Customer.Id.Should().Be(command.Customer.Id);
        sale.Customer.Name.Should().Be(command.Customer.Name);
        sale.Branch.Id.Should().Be(command.Branch.Id);
        sale.Branch.Name.Should().Be(command.Branch.Name);

        sale.Items.Should().NotBeNull();
        sale.Items.Count().Should().Be(command.Items.Count());

        for (int i = 0; i < sale.Items.Count(); i++)
        { 
            var mapped = sale.Items.ElementAt(i);
            var item = command.Items.ElementAt(i);

            mapped.ProductId.Should().Be(item.ProductId);
            mapped.Quantity.Should().Be(item.Quantity);
        }
    }
}
