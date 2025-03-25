using Ambev.DeveloperEvaluation.Domain.Entities.Identity;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IUserRepository using Entity Framework Core
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of UserRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public UserRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new user in the database
    /// </summary>
    /// <param name="user">The user to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user</returns>
    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        return user;
    }

    private IQueryable<User> GetQueryable(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var users = _context.Users.AsQueryable();
        if(predicate is not null)
            users = users.Where(predicate);   

        return from u in users.AsNoTracking()
               join p in _context.Set<Person>().AsNoTracking()
                   on u.Id equals p.UserId
               select new User() 
               { 
                   Id = u.Id,
                   Email = u.Email,
                   Password = u.Password,
                   Username = u.Username,
                   Phone = u.Phone,
                   CreatedAt = u.CreatedAt,
                   UpdatedAt = u.UpdatedAt, 
                   Status = u.Status,
                   Role = u.Role,
                   Person = new Person()
                    {
                        Id = p.Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Address = p.Address,
                        UserId = p.UserId,
                        CreatedAt = p.CreatedAt,
                        UpdatedAt = p.UpdatedAt
                    }
               };
    }


    /// <summary>
    /// Retrieves a user by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetQueryable(u => u.Id == id, cancellationToken)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieves a user by their email address
    /// </summary>
    /// <param name="email">The email address to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await GetQueryable(u => u.Email == email, cancellationToken)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// Deletes a user from the database
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the user was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
            return false;

        _context.Users.Remove(user);
        return true;
    }
}
