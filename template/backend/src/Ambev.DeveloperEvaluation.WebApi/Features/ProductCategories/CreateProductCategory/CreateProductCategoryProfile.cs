using AutoMapper;
using Ambev.DeveloperEvaluation.Application.ProductCategories.CreateProductCategory;

namespace Ambev.DeveloperEvaluation.WebApi.Features.ProductCategories.CreateProductCategory;

public sealed class CreateProductCategoryProfile : Profile
{
    public CreateProductCategoryProfile()
    {
        CreateMap<CreateProductCategoryRequest, CreateProductCategoryCommand>();

        CreateMap<CreateProductCategoryResult, CreateProductCategoryResponse>();
    }
}
