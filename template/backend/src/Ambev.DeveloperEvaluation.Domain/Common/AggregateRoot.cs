using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Common;

public class AggregateRoot : BaseEntity
{
    private readonly List<INotification> _domainEvents = [];

    public IReadOnlyCollection<INotification> GetDomainEvents() => 
        _domainEvents.ToList();

    public void ClearDomainEvents() => _domainEvents.Clear();

    public void RaiseDomainEvent(INotification domainEvent) =>
        _domainEvents.Add(domainEvent);
}
