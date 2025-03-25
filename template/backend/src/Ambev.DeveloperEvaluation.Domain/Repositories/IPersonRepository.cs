using Ambev.DeveloperEvaluation.Domain.Entities.Identity;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Person entity operations
/// </summary>
public interface IPersonRepository
{
    /// <summary>
    /// Creates a new person in the repository
    /// </summary>
    /// <param name="person">The person to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created person</returns>
    Task<Person> CreateAsync(Person person, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a person by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the person</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The person if found, null otherwise</returns>
    Task<Person?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Person?> GetByUserIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a person from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the person to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the person was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
