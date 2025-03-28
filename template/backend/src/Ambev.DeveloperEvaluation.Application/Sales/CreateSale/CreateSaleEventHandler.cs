using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleEventHandler : INotificationHandler<SaleCreatedEvent>
{
    public Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
    {
        System.Diagnostics.Debug.WriteLine("Sale created"); 
        return Task.CompletedTask;
    }
}
