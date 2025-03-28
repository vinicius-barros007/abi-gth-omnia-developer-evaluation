using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleEventHandler : INotificationHandler<SaleCreatedEvent>
{
    public Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
    {
        System.Diagnostics.Debug.WriteLine("Sale created, should populate a queue to be consumed and sync the mongodb.");
        System.Diagnostics.Debug.WriteLine("Mongodb will be used for read operation and historic purpose.");
        return Task.CompletedTask;
    }
}
