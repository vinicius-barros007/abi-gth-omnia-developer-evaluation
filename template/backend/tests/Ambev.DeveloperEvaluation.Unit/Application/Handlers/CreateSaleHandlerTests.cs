using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.TestData.Products;
using Ambev.DeveloperEvaluation.TestData.Sales;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Handlers;

public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly IProductRepository _productRepository;
    private readonly IDiscountService _discountService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly CreateSaleHandler _handler;

    public CreateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _saleItemRepository = Substitute.For<ISaleItemRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        _discountService = Substitute.For<IDiscountService>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateSaleHandler(_saleRepository, _productRepository, _saleItemRepository, _discountService, _unitOfWork, _mapper);
    }

    [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateSaleCommandTestData.GenerateValidCommand();
        var sale = SaleTestData.GenerateValidSale();

        sale.Id = Guid.NewGuid();
        var item = SaleItemTestData.GenerateValidItem();

        var result = new CreateSaleResult
        {
            Id = sale.Id
        };

        _mapper.Map<Sale>(command).Returns(sale);
        _mapper.Map<SaleItem>(Arg.Any<CreateSaleItemCommand>()).Returns(item);
        _mapper.Map<CreateSaleResult>(sale).Returns(result);

        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        _discountService.CalculateDiscount(Arg.Any<SaleItem>())
            .Returns(1);

        _saleItemRepository.CreateAsync(Arg.Any<SaleItem>(), Arg.Any<CancellationToken>())
            .Returns(item);

        var product = ProductTestData.GenerateValidProduct();
        _productRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(product);

        // When
        var createSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createSaleResult.Should().NotBeNull();
        createSaleResult.Id.Should().Be(sale.Id);

        await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        await _saleItemRepository.Received(command.Items.Count()).CreateAsync(Arg.Any<SaleItem>(), Arg.Any<CancellationToken>());

        _discountService.Received(command.Items.Count()).CalculateDiscount(Arg.Any<SaleItem>());
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}
