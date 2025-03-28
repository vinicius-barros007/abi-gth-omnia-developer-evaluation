using Ambev.DeveloperEvaluation.Application.ProductCategories.CreateProductCategory;
using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using Ambev.DeveloperEvaluation.TestData.Products;
using AutoMapper;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Mappers;

public class CreateProductCategoryProfileTests
{
    [Fact(DisplayName = "Assert CreateProductCategoryProfile mapper is valid")]
    public void AutoMapper_Configuration_IsValid()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<CreateProductCategoryProfile>());
        config.AssertConfigurationIsValid();
    }

    [Fact(DisplayName = "Map CreateProductCategoryCommand to ProductCategory is working properly")]
    public void Map_CreateProductCategoryCommand_To_ProductCategory_ShouldSucceed()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<CreateProductCategoryProfile>());
        var mapper = config.CreateMapper();

        var command = CreateProductCategoryCommandTestData.GenerateValidCommand();

        // Act 
        var category = mapper.Map<ProductCategory>(command);

        // Assert
        category.Should().NotBeNull();
        category.Description.Should().Be(command.Description);
    }

    [Fact(DisplayName = "Map ProductCategory to CreateProductCategoryResult is working properly")]
    public void Map_Product_To_CreateProductResult_ShouldSucceed()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<CreateProductCategoryProfile>());
        var mapper = config.CreateMapper();

        var category = ProductCategoryTestData.GenerateValidCategory();

        // Act 
        var result = mapper.Map<CreateProductCategoryResult>(category);

        // Assert
        category.Should().NotBeNull();
        category.Description.Should().Be(result.Description);
    }
}
