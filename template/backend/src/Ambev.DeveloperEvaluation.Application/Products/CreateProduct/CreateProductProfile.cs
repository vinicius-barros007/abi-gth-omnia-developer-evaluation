using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities.Products;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

public class CreateProductProfile : Profile
{
    public CreateProductProfile()
    {
        CreateMap<CreateProductCommand, Product>()
            .ForMember(destination => destination.Id, option => option.Ignore())
            .ForMember(destination => destination.Category, option => option.Ignore())
            .ForMember(destination => destination.Rating, option => option.Ignore())
            .ForMember(destination => destination.CreatedAt, option => option.Ignore())
            .ForMember(destination => destination.UpdatedAt, option => option.Ignore());

        CreateMap<Product, CreateProductResult>()
            .ForMember(destination => destination.Category, option => option.MapFrom(source => source.Category.Description));
    }
}
