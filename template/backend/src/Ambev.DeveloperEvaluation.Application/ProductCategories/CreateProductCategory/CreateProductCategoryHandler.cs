using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities.Products;

namespace Ambev.DeveloperEvaluation.Application.ProductCategories.CreateProductCategory;

public class CreateProductCategoryHandler : IRequestHandler<CreateProductCategoryCommand, CreateProductCategoryResult>
{
    private readonly IProductCategoryRepository _productCategoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProductCategoryHandler(
        IProductCategoryRepository productCategoryRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper
    )
    {
        _productCategoryRepository = productCategoryRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CreateProductCategoryResult> Handle(CreateProductCategoryCommand command, CancellationToken cancellationToken)
    {
        var productCategory = _mapper.Map<ProductCategory>(command);
        var createdProductCategory = await _productCategoryRepository.CreateAsync(productCategory, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        var result = _mapper.Map<CreateProductCategoryResult>(createdProductCategory);

        return result;
    }
}
