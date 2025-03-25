using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Entities.Identity;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Handlers;

public class GetUserHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly GetUserHandler _handler;

    public GetUserHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetUserHandler(_userRepository, _mapper);
    }

    [Fact(DisplayName = "Given existing user When querying user Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = new GetUserCommand(Guid.NewGuid());
        var user = new User { Id = Guid.NewGuid() };

        _userRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(user);

        GetUserResult mapped = new() { Id = user.Id };
        _mapper.Map<GetUserResult>(user).Returns(mapped);

        // When
        var getUserResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        getUserResult.Should().NotBeNull();
        getUserResult.Id.Should().Be(user.Id);

        await _userRepository.Received(1).GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given nonexisting user When querying user Then error is thrown")]
    public async Task Handle_InvalidRequest_ThrowsError()
    {
        // Given
        var command = new GetUserCommand(Guid.NewGuid());
        User user = default!;

        _userRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(user);

        // When
        var action = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await action.Should().ThrowAsync<KeyNotFoundException>();
        await _userRepository.Received(1).GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
    }
}
