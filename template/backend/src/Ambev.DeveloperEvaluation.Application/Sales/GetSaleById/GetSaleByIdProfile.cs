using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

public class GetSaleByIdProfile : Profile
{
    public GetSaleByIdProfile()
    {
        CreateMap<GetSaleByIdCommand, Sale>()
            .ForMember(destination => destination.SaleDate, option => option.Ignore())
            .ForMember(destination => destination.Branch, option => option.Ignore())
            .ForMember(destination => destination.Customer, option => option.Ignore())
            .ForMember(destination => destination.SaleNumber, option => option.Ignore())
            .ForMember(destination => destination.Status, option => option.Ignore())
            .ForMember(destination => destination.Items, option => option.Ignore())
            .ForMember(destination => destination.CreatedAt, option => option.Ignore())
            .ForMember(destination => destination.UpdatedAt, option => option.Ignore());

        CreateMap<Domain.Entities.Sales.SaleItem, SaleItem>()
            .ForMember(destination => destination.TotalAmount, option => option.Ignore());

        CreateMap<Sale, GetSaleByIdResult>()
            .ForMember(destination => destination.TotalAmount, option => option.Ignore());
    }
}
