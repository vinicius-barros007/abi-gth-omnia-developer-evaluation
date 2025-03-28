using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

public sealed class GetSaleProfile : Profile
{
    public GetSaleProfile()
    {
        CreateMap<GetSaleRequest, GetSaleByIdCommand>();

        CreateMap<Application.Sales.GetSaleById.SaleItem, SaleItem>();

        CreateMap<GetSaleByIdResult, GetSaleResponse>()
            .ForMember(destination => destination.Items, option => option.MapFrom(source => source.Items));
    }
}
