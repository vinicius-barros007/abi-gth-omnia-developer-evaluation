using Ambev.DeveloperEvaluation.Functional.Features.Users.TestData;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Ambev.DeveloperEvaluation.Functional.Features.Users
{
    public class GetUserTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public GetUserTests(TestWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact(DisplayName = "Selection of valid user should return created status code")] 
        public async Task Valid_User_ShouldReturn_Ok()
        {
            // Arrange
            var response = await _client.PostAsJsonAsync("/api/users", CreateUserRequestTestData.GenerateValidRequest());
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateUserResponse>>();
            CreateUserResponse user = result?.Data ?? throw new Exception("Cannot create user");

            // Act
            response = await _client.GetAsync($"/api/users/{user.Id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadFromJsonAsync<ApiResponseWithData<GetUserResponse>>();

            content.Should().NotBeNull();
            content.Success.Should().BeTrue();

            content.Message.Should().Be("User retrieved successfully");   
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

        [Fact(DisplayName = "Selection of empty user id should return bad request status code")]
        public async Task Empty_UserId_ShouldReturn_BadRequest()
        {
            // Arrange
            Guid userId = Guid.Empty;

            // Act
            var response = await _client.GetAsync($"/api/users/{userId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var content = await response.Content.ReadFromJsonAsync<ApiResponse>();

            content.Should().NotBeNull();
            content.Success.Should().BeFalse();

            content.Message.Should().Be("Validation Failed");
            content.Errors.Should().HaveCountGreaterThan(0);
        }

        [Fact(DisplayName = "Selection of user id that doesn't exists should return not found status code")]
        public async Task Invalid_UserId_ShouldReturn_NotFound()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            // Act
            var response = await _client.GetAsync($"/api/users/{userId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            var content = await response.Content.ReadFromJsonAsync<ApiResponse>();

            content.Should().NotBeNull();
            content.Success.Should().BeFalse();
            content.Message.Should().Be($"User with ID {userId} not found");
        }
    }
}
