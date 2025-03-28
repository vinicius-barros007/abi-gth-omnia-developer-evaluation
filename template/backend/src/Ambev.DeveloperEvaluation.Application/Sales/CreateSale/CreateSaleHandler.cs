using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly IProductRepository _productRepository;
    private readonly IDiscountService _discountService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateSaleHandler(
        ISaleRepository saleRepository,
        IProductRepository productRepository,
        ISaleItemRepository saleItemRepository,
        IDiscountService discountService,
        IUnitOfWork unitOfWork,
        IMapper mapper
    )
    {
        _saleRepository = saleRepository;
        _saleItemRepository = saleItemRepository;
        _productRepository = productRepository;
        _discountService = discountService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var sale = _mapper.Map<Sale>(command);
        sale.Validate();

        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
        foreach(var item in command.Items)
        {
            var saleItem = _mapper.Map<SaleItem>(item);
            saleItem.SaleId = createdSale.Id;

            var product = await _productRepository.GetByIdAsync(saleItem.ProductId, cancellationToken) ?? 
                throw new KeyNotFoundException($"ProductId {saleItem.ProductId} doesn't exist.");

            saleItem.UnitPrice = product.Price;
            saleItem.Discount = _discountService.CalculateDiscount(saleItem);

            sale.Validate();
            await _saleItemRepository.CreateAsync(saleItem, cancellationToken);
        }

        sale.RaiseDomainEvent(new SaleCreatedEvent(sale.Id));
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CreateSaleResult>(createdSale);
    }
}
