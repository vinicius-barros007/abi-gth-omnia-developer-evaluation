using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities.Identity;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.TestData.Users;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Handlers;

public class AuthenticateUserHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IMapper _mapper;
    private readonly AuthenticateUserHandler _handler;

    public AuthenticateUserHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _passwordHasher = Substitute.For<IPasswordHasher>();
        _jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
        _mapper = Substitute.For<IMapper>();
        _handler = new AuthenticateUserHandler(_userRepository, _mapper, _passwordHasher, _jwtTokenGenerator);
    }

    [Fact(DisplayName = "Given valid credentials When authenticating user Then returns success response")]
    public async Task Handle_ValidAuthentication_ReturnsSuccessResponse()
    {
        // Given
        string token = Guid.NewGuid().ToString();
        var command = AuthenticateUserCommandTestData.GenerateValidCommand();
        var user = new User { Id = Guid.NewGuid(), Status = DeveloperEvaluation.Domain.Enums.Identity.UserStatus.Active };

        _userRepository.GetByEmailAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(user);

        _passwordHasher.VerifyPassword(Arg.Any<string>(), Arg.Any<string>())
            .Returns(true);

        _jwtTokenGenerator.GenerateToken(Arg.Any<IUser>())
            .Returns(token);

        _mapper.Map<AuthenticateUserResult>(Arg.Any<User>())
            .Returns(AuthenticateUserResultTestData.GenerateValidResult());

        // When
        var authenticateUserResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        authenticateUserResult.Should().NotBeNull();
        authenticateUserResult.Token.Should().Be(token);

        await _userRepository.Received(1).GetByEmailAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
        _passwordHasher.Received(1).VerifyPassword(Arg.Any<string>(), Arg.Any<string>());
        _jwtTokenGenerator.Received(1).GenerateToken(Arg.Any<IUser>());
        _mapper.Received(1).Map<AuthenticateUserResult>(Arg.Any<User>());
    }

    [Fact(DisplayName = "Given invalid user When authenticating user Then error is thrown")]
    public async Task Handle_InvalidAuthentication_UserDoesntExists_ThrowsError()
    {
        // Given
        string token = Guid.NewGuid().ToString();
        var command = AuthenticateUserCommandTestData.GenerateValidCommand();
        User user = default!;

        _userRepository.GetByEmailAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(user);

        _passwordHasher.VerifyPassword(Arg.Any<string>(), Arg.Any<string>())
            .Returns(false);

        _jwtTokenGenerator.GenerateToken(Arg.Any<IUser>())
            .Returns(token);

        // When
        var action = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await action.Should().ThrowAsync<UnauthorizedAccessException>();
        await _userRepository.Received(1).GetByEmailAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
        _passwordHasher.Received(0).VerifyPassword(Arg.Any<string>(), Arg.Any<string>());
        _jwtTokenGenerator.Received(0).GenerateToken(Arg.Any<IUser>());
        _mapper.Received(0).Map<AuthenticateUserResult>(Arg.Any<User>());
    }

    [Fact(DisplayName = "Given invalid credentials When authenticating user Then error is thrown")]
    public async Task Handle_InvalidAuthentication_PasswordDoestMatch_ThrowsError()
    {
        // Given
        string token = Guid.NewGuid().ToString();
        var command = AuthenticateUserCommandTestData.GenerateValidCommand();
        var user = new User { Id = Guid.NewGuid(), Status = DeveloperEvaluation.Domain.Enums.Identity.UserStatus.Active };

        _userRepository.GetByEmailAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(user);

        _passwordHasher.VerifyPassword(Arg.Any<string>(), Arg.Any<string>())
            .Returns(false);

        _jwtTokenGenerator.GenerateToken(Arg.Any<IUser>())
            .Returns(token);

        // When
        var action = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await action.Should().ThrowAsync<UnauthorizedAccessException>();
        await _userRepository.Received(1).GetByEmailAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
        _passwordHasher.Received(1).VerifyPassword(Arg.Any<string>(), Arg.Any<string>());
        _jwtTokenGenerator.Received(0).GenerateToken(Arg.Any<IUser>());
        _mapper.Received(0).Map<AuthenticateUserResult>(Arg.Any<User>());
    }

    [Theory(DisplayName = "Given not active user When authenticating user Then error is thrown")]
    [InlineData(DeveloperEvaluation.Domain.Enums.Identity.UserStatus.Unknown)]
    [InlineData(DeveloperEvaluation.Domain.Enums.Identity.UserStatus.Suspended)]
    [InlineData(DeveloperEvaluation.Domain.Enums.Identity.UserStatus.Inactive)]
    public async Task Handle_UserNotActive_ThrowsError(DeveloperEvaluation.Domain.Enums.Identity.UserStatus status)
    {
        // Given
        string token = Guid.NewGuid().ToString();
        var command = AuthenticateUserCommandTestData.GenerateValidCommand();
        var user = new User { Id = Guid.NewGuid(), Status = status };

        _userRepository.GetByEmailAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(user);

        _passwordHasher.VerifyPassword(Arg.Any<string>(), Arg.Any<string>())
            .Returns(false);

        _jwtTokenGenerator.GenerateToken(Arg.Any<IUser>())
            .Returns(token);

        // When
        var action = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await action.Should().ThrowAsync<UnauthorizedAccessException>();
        await _userRepository.Received(1).GetByEmailAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
        _passwordHasher.Received(1).VerifyPassword(Arg.Any<string>(), Arg.Any<string>());
        _jwtTokenGenerator.Received(0).GenerateToken(Arg.Any<IUser>());
        _mapper.Received(0).Map<AuthenticateUserResult>(Arg.Any<User>());
    }
}
