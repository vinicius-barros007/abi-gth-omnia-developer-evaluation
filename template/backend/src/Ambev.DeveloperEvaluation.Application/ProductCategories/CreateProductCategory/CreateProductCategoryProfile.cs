using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities.Catalog;

namespace Ambev.DeveloperEvaluation.Application.ProductCategories.CreateProductCategory;

public class CreateProductCategoryProfile : Profile
{
    public CreateProductCategoryProfile()
    {
        CreateMap<CreateProductCategoryCommand, ProductCategory>()
            .ForMember(destination => destination.Id, option => option.Ignore())
            .ForMember(destination => destination.Products, option => option.Ignore())
            .ForMember(destination => destination.CreatedAt, option => option.Ignore())
            .ForMember(destination => destination.UpdatedAt, option => option.Ignore());

        CreateMap<ProductCategory, CreateProductCategoryResult>();

    }
}
