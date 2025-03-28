using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateSale;

public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleItemCommand, SaleItem>()
            .ForMember(destination => destination.Id, option => option.Ignore())
            .ForMember(destination => destination.UnitPrice, option => option.Ignore())
            .ForMember(destination => destination.Product, option => option.Ignore())
            .ForMember(destination => destination.Discount, option => option.Ignore())
            .ForMember(destination => destination.Sale, option => option.Ignore())
            .ForMember(destination => destination.SaleId, option => option.Ignore());

        CreateMap<CreateSaleCommand, Sale>()
            .ForMember(destination => destination.Id, option => option.Ignore())
            .ForMember(destination => destination.SaleNumber, option => option.Ignore())
            .ForMember(destination => destination.Status, option => option.Ignore())
            .ForMember(destination => destination.Items, option => option.MapFrom(source => source.Items))
            .ForMember(destination => destination.CreatedAt, option => option.Ignore())
            .ForMember(destination => destination.UpdatedAt, option => option.Ignore());

        CreateMap<Sale, CreateSaleResult>();
    }
}
