using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.TestData.Products;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Handlers;

public class CreateProductHandlerTests
{
    private readonly IProductRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly CreateProductHandler _handler;

    public CreateProductHandlerTests()
    {
        _repository = Substitute.For<IProductRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateProductHandler(_repository, _unitOfWork, _mapper);
    }

    [Fact(DisplayName = "Given valid product data When creating product Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateProductCommandTestData.GenerateValidCommand();
        var product = ProductTestData.GenerateValidProduct();

        product.CategoryId = command.CategoryId;
        var productCategory = ProductCategoryTestData.GenerateValidCategory();

        var result = new CreateProductResult
        {
            Id = product.Id
        };

        _mapper.Map<Product>(command).Returns(product);
        _mapper.Map<CreateProductResult>(product).Returns(result);

        _repository.CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>())
            .Returns(product);

        // When
        var createProductResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createProductResult.Should().NotBeNull();
        createProductResult.Id.Should().Be(product.Id);

        await _repository.Received(1).CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}
