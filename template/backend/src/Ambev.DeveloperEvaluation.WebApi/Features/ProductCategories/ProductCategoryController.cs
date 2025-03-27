using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using FluentValidation;
using Ambev.DeveloperEvaluation.Application.ProductCategories.CreateProductCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.ProductCategories.CreateProductCategory;

namespace Ambev.DeveloperEvaluation.WebApi.Features.ProductCategories;

[ApiController]
[Route("api/product-category")]
public class ProductCategoryController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductCategoryController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateProductCategoryResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProductCategory([FromBody] CreateProductCategoryRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateProductCategoryRequestValidator();
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var command = _mapper.Map<CreateProductCategoryCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateProductCategoryResponse>
        {
            Success = true,
            Message = "Product category created successfully",
            Data = _mapper.Map<CreateProductCategoryResponse>(response)
        });
    }
}
