using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Users.DeleteUser;

/// <summary>
/// Handler for processing DeleteUserCommand requests
/// </summary>
public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of DeleteUserHandler
    /// </summary>
    /// <param name="userRepository">The user repository</param>
    /// <param name="validator">The validator for DeleteUserCommand</param>
    public DeleteUserHandler(
        IPersonRepository personRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork
    )
    {
        _personRepository = personRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;   
    }

    /// <summary>
    /// Handles the DeleteUserCommand request
    /// </summary>
    /// <param name="request">The DeleteUser command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the delete operation</returns>
    public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        string message = $"User with ID {request.Id} not found";
        var person = await _personRepository.GetByUserIdAsync(request.Id, cancellationToken) 
            ?? throw new KeyNotFoundException(message);

        var success = await _personRepository.DeleteAsync(person.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException(message);

        success = await _userRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException(message);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new DeleteUserResponse { Success = true };
    }
}
