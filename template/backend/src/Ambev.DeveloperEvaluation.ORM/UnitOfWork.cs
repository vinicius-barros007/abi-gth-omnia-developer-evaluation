using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.ORM;

public class UnitOfWork(DefaultContext context) : IUnitOfWork
{
    public void UndoChanges()
    {
        context.ChangeTracker.Clear();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}
