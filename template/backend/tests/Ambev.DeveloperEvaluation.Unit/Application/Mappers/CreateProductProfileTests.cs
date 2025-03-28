using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using Ambev.DeveloperEvaluation.TestData.Products;
using AutoMapper;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Mappers;

public class CreateProductProfileTests
{
    [Fact(DisplayName = "Assert CreateProductProfile mapper is valid")]
    public void AutoMapper_Configuration_IsValid()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<CreateProductProfile>());
        config.AssertConfigurationIsValid();
    }

    [Fact(DisplayName = "Map CreateProductCommand to Product is working properly")]
    public void Map_CreateProductCommand_To_Product_ShouldSucceed()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<CreateProductProfile>());
        var mapper = config.CreateMapper();

        var command = CreateProductCommandTestData.GenerateValidCommand();

        // Act 
        var product = mapper.Map<Product>(command);

        // Assert
        product.Should().NotBeNull();
        product.Title.Should().Be(command.Title);
        product.Description.Should().Be(command.Description);
        product.Price.Should().Be(command.Price);
        product.CategoryId.Should().Be(command.CategoryId);
        product.Image.Should().Be(command.Image);
    }

    [Fact(DisplayName = "Map Product to CreateProductResult is working properly")]
    public void Map_Product_To_CreateProductResult_ShouldSucceed()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<CreateProductProfile>());
        var mapper = config.CreateMapper();

        var product = ProductTestData.GenerateValidProduct();

        // Act 
        var result = mapper.Map<CreateProductResult>(product);

        // Assert
        product.Should().NotBeNull();
        product.Title.Should().Be(result.Title);
        product.Description.Should().Be(result.Description);
        product.Price.Should().Be(result.Price);
        product.Category.Description.Should().Be(result.Category);
        product.Image.Should().Be(result.Image);
    }
}
