using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

public class GetSaleByIdHandler : IRequestHandler<GetSaleByIdCommand, GetSaleByIdResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public GetSaleByIdHandler(
        ISaleRepository saleRepository,
        IMapper mapper
    )
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<GetSaleByIdResult> Handle(GetSaleByIdCommand command, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
        return _mapper.Map<GetSaleByIdResult>(sale);
    }
}
