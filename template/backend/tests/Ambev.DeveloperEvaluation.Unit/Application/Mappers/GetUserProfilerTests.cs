using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.TestData.User;
using AutoMapper;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Mappers;

public class GetUserProfilerTests
{
    [Fact(DisplayName = "Assert get user profile mapper is valid")]
    public void AutoMapper_Configuration_IsValid()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<GetUserProfile>());
        config.AssertConfigurationIsValid();
    }

    [Fact(DisplayName = "Map User to CreateUserResult is working properly")]
    public void Map_GetUserCommand_To_User_ShouldSucceed()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<GetUserProfile>());
        var mapper = config.CreateMapper();

        var user = UserTestData.GenerateValidUser();

        // Act 
        var result = mapper.Map<GetUserResult>(user);

        // Assert
        user.Should().NotBeNull();
        user.Username.Should().Be(result.Username);
        user.Email.Should().Be(result.Email);
        user.Phone.Should().Be(result.Phone);
        user.Status.Should().Be(result.Status);
        user.Role.Should().Be(result.Role);

        var person = user.Person;
        person.Should().NotBeNull();
        person.FirstName.Should().Be(result.FirstName);
        person.LastName.Should().Be(result.LastName);

        var address = person.Address;
        address.Should().NotBeNull();
        address.City.Should().Be(result.Address.City);
        address.Street.Should().Be(result.Address.Street);
        address.Number.Should().Be(result.Address.Number);
        address.ZipCode.Should().Be(result.Address.ZipCode);

        var geoLocation = address.GeoLocation;
        geoLocation.Should().NotBeNull();
        geoLocation.Latitude.Should().Be(result.Address.GeoLocation.Latitude);
        geoLocation.Longitude.Should().Be(result.Address.GeoLocation.Longitude);
    }
}
