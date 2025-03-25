using Ambev.DeveloperEvaluation.Functional.Features.Users.TestData;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using FluentAssertions;
using System.Dynamic;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Ambev.DeveloperEvaluation.Functional.Features.Users
{
    public class CreateUserTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public CreateUserTests(TestWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact(DisplayName = "Creation of valid user should return created status code")] 
        public async Task Valid_User_ShouldReturn_Created()
        {
            // Arrange
            var user = CreateUserRequestTestData.GenerateValidRequest();

            // Act
            var response = await _client.PostAsJsonAsync("/api/users", user);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var content = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateUserResponse>>();

            content.Should().NotBeNull();
            content.Success.Should().BeTrue();

            content.Message.Should().Be("User created successfully");   
            content.Data.Should().NotBeNull();

            var userResponse = content.Data;
            userResponse.Id.Should().NotBeEmpty();

            userResponse.Status.Should().Be(user.Status);   
            userResponse.Role.Should().Be(user.Role);   
            userResponse.Username.Should().Be(user.Username);   
            userResponse.Name.FirstName.Should().Be(user.Name.FirstName);
            userResponse.Name.LastName.Should().Be(user.Name.LastName);
            userResponse.Email.Should().Be(user.Email);
            userResponse.Phone.Should().Be(user.Phone);
            userResponse.Address.City.Should().Be(user.Address.City);
            userResponse.Address.Street.Should().Be(user.Address.Street);
            userResponse.Address.Number.Should().Be(user.Address.Number);
            userResponse.Address.ZipCode.Should().Be(user.Address.ZipCode);
            userResponse.Address.GeoLocation.Latitude.Should().Be(user.Address.GeoLocation.Latitude);
            userResponse.Address.GeoLocation.Longitude.Should().Be(user.Address.GeoLocation.Longitude);
        }

        [Fact(DisplayName = "Creation of invalid user should return bad request status code")]
        public async Task Invalid_User_ShouldReturn_BadRequest()
        {
            // Arrange
            var user = CreateUserRequestTestData.GenerateValidRequest();
            user.Username = string.Empty; // Invalidating the request

            // Act
            var response = await _client.PostAsJsonAsync("/api/users", user);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var content = await response.Content.ReadFromJsonAsync<ApiResponse>();

            content.Should().NotBeNull();
            content.Success.Should().BeFalse();

            content.Message.Should().Be("Validation Failed");
            content.Errors.Should().HaveCountGreaterThan(0);
        }
    }
}
