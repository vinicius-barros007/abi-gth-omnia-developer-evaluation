namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IUnitOfWork
{
    void UndoChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
