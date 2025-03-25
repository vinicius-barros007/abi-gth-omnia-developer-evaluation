using Ambev.DeveloperEvaluation.Domain.Entities.Identity;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IPersonRepository using Entity Framework Core
/// </summary>
public class PersonRepository : IPersonRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of PersonRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public PersonRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new person in the database
    /// </summary>
    /// <param name="person">The person to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created person</returns>
    public async Task<Person> CreateAsync(Person person, CancellationToken cancellationToken = default)
    {
        await _context.Set<Person>().AddAsync(person, cancellationToken);
        return person;
    }

    /// <summary>
    /// Retrieves a person by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the person</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The person if found, null otherwise</returns>
    public async Task<Person?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Person>().FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Deletes a person from the database
    /// </summary>
    /// <param name="id">The unique identifier of the person to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the person was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var person = await GetByIdAsync(id, cancellationToken);
        if (person == null)
            return false;

        _context.Set<Person>().Remove(person);
        return true;
    }
}
