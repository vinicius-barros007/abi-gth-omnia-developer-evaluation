using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Ambev.DeveloperEvaluation.Domain.Entities.Identity;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Handlers;

public class DeleteUserHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly DeleteUserHandler _handler;

    public DeleteUserHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _personRepository = Substitute.For<IPersonRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new DeleteUserHandler(_personRepository, _userRepository, _unitOfWork);
    }

    [Fact(DisplayName = "Given valid user data When deleting user Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = new DeleteUserCommand(Guid.NewGuid());
        var person = new Person { Id = Guid.NewGuid() };

        _personRepository.GetByUserIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(person);

        _personRepository.DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(true);

        _userRepository.DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(true);

        // When
        var deleteUserResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        deleteUserResult.Should().NotBeNull();
        deleteUserResult.Success.Should().BeTrue();

        await _personRepository.Received(1).GetByUserIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        await _userRepository.Received(1).DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        await _personRepository.Received(1).DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given user id not mapped to user When deleting Then error is thrown")]
    public async Task Handle_InvalidRequest_ThrowsError()
    {
        // Given
        var command = new DeleteUserCommand(Guid.NewGuid());
        Person person = default!;

        _personRepository.GetByUserIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(person);

        // When
        var action = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await action.Should().ThrowAsync<KeyNotFoundException>();

        await _personRepository.Received(1).GetByUserIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        await _userRepository.Received(0).DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        await _personRepository.Received(0).DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        await _unitOfWork.Received(0).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}
