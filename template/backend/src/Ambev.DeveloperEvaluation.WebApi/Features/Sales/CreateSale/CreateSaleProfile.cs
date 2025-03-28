using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public sealed class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleItem, CreateSaleItemCommand>();

        CreateMap<CreateSaleRequest, CreateSaleCommand>();

        CreateMap<CreateSaleResult, CreateSaleResponse>();
    }
}
