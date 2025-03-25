using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Entities.Identity;
using Ambev.DeveloperEvaluation.TestData.User;
using AutoMapper;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Mappers;

public class CreateUserProfilerTests
{
    [Fact(DisplayName = "Assert create user profile mapper is valid")]
    public void AutoMapper_Configuration_IsValid()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<CreateUserProfile>());
        config.AssertConfigurationIsValid();
    }

    [Fact(DisplayName = "Map CreateUserCommand to user is working properly")]
    public void Map_CreateUserCommand_To_User_ShouldSucceed()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<CreateUserProfile>());
        var mapper = config.CreateMapper();

        var command = CreateUserCommandTestData.GenerateValidCommand();

        // Act 
        var user = mapper.Map<User>(command);

        // Assert
        user.Should().NotBeNull();
        user.Username.Should().Be(command.Username);
        user.Email.Should().Be(command.Email);
        user.Phone.Should().Be(command.Phone);
        user.Status.Should().Be(command.Status);
        user.Password.Should().Be(command.Password);
        user.Role.Should().Be(command.Role);

        var person = user.Person;
        person.Should().NotBeNull();
        person.FirstName.Should().Be(command.FirstName);
        person.LastName.Should().Be(command.LastName);

        var address = person.Address;
        address.Should().NotBeNull();
        address.City.Should().Be(command.City);
        address.Street.Should().Be(command.Street);
        address.Number.Should().Be(command.Number);
        address.ZipCode.Should().Be(command.ZipCode);

        var geoLocation = address.GeoLocation;
        geoLocation.Should().NotBeNull();
        geoLocation.Latitude.Should().Be(command.Latitude);
        geoLocation.Longitude.Should().Be(command.Longitude);
    }
}
