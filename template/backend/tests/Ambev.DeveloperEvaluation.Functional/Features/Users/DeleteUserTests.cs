using Ambev.DeveloperEvaluation.Functional.Features.Users.TestData;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Ambev.DeveloperEvaluation.Functional.Features.Users
{
    public class DeleteUserTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public DeleteUserTests(TestWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact(DisplayName = "Exclusion of valid user should return ok status code")] 
        public async Task Valid_UserId_ShouldReturn_Ok()
        {
            // Arrange
            var response = await _client.PostAsJsonAsync("/api/users", CreateUserRequestTestData.GenerateValidRequest());            
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateUserResponse>>();
            CreateUserResponse user = result?.Data ?? throw new Exception("Cannot create user");

            // Act
            response = await _client.DeleteAsync($"/api/users/{user.Id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadFromJsonAsync<ApiResponse>();

            content.Should().NotBeNull();
            content.Success.Should().BeTrue();
            content.Message.Should().Be("User deleted successfully");
        }

        [Fact(DisplayName = "Exclusion of empty user id should return bad request status code")]
        public async Task Empty_UserId_ShouldReturn_BadRequest()
        {
            // Arrange
            Guid userId = Guid.Empty;

            // Act
            var response = await _client.DeleteAsync($"/api/users/{userId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var content = await response.Content.ReadFromJsonAsync<ApiResponse>();

            content.Should().NotBeNull();
            content.Success.Should().BeFalse();

            content.Message.Should().Be("Validation Failed");
            content.Errors.Should().HaveCountGreaterThan(0);
        }

        [Fact(DisplayName = "Exclusion of user id that doesn't exists should return not found status code")]
        public async Task Invalid_UserId_ShouldReturn_NotFound()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            // Act
            var response = await _client.DeleteAsync($"/api/users/{userId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            var content = await response.Content.ReadFromJsonAsync<ApiResponse>();

            content.Should().NotBeNull();
            content.Success.Should().BeFalse();
            content.Message.Should().Be($"User with ID {userId} not found");
        }
    }
}
