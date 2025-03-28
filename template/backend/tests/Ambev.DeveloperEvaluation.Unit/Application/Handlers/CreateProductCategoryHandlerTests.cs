using Ambev.DeveloperEvaluation.Application.ProductCategories.CreateProductCategory;
using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.TestData.Products;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Handlers;

public class CreateProductCategoryHandlerTests
{
    private readonly IProductCategoryRepository _productCategoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly CreateProductCategoryHandler _handler;

    public CreateProductCategoryHandlerTests()
    {
        _productCategoryRepository = Substitute.For<IProductCategoryRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateProductCategoryHandler(_productCategoryRepository, _unitOfWork, _mapper);
    }

    [Fact(DisplayName = "Given valid product category data When creating product category Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateProductCategoryCommandTestData.GenerateValidCommand();
        var category = ProductCategoryTestData.GenerateValidCategory();
        var result = new CreateProductCategoryResult { Id = category.Id };

        _mapper.Map<ProductCategory>(command).Returns(category);
        _mapper.Map<CreateProductCategoryResult>(category).Returns(result);

        _productCategoryRepository.CreateAsync(Arg.Any<ProductCategory>(), Arg.Any<CancellationToken>())
            .Returns(category);

        // When
        var createCategoryResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createCategoryResult.Should().NotBeNull();
        createCategoryResult.Id.Should().Be(category.Id);

        await _productCategoryRepository.Received(1).CreateAsync(Arg.Any<ProductCategory>(), Arg.Any<CancellationToken>());
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}
